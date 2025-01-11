using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VisionShopAPI.Data;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

namespace VisionShopAPI.Services
{
    public class MovimentacaoEstoqueService
    {
        private VisionShopContext _context;
        private IMapper _mapper;

        public MovimentacaoEstoqueService(VisionShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadMovimentacaoEstoqueDto> CriarMovimentacao(CreateMovimentacaoEstoqueDto dto)
        {
            // Mapeia o DTO de criação para a entidade MovimentacaoEstoque
            var movimentacoes = _mapper.Map<MovimentacaoEstoque>(dto);

            // Carrega o óculos com as movimentações associadas
            var oculos = await _context.Oculos.Include(o => o.MovimentacoesEstoque)
                                              .FirstOrDefaultAsync(o => o.Id == movimentacoes.OculosId);
            if (oculos == null)
            {
                throw new ArgumentException($"Óculos com ID {movimentacoes.OculosId} não encontrado.");
            }

            // Lógica de movimentação de estoque (entrada ou saída)
            if (movimentacoes.TipoMovimentacao.ToLower() == "entrada")
            {
                oculos.Estoque += movimentacoes.Quantidade;
            }
            else if (movimentacoes.TipoMovimentacao.ToLower() == "saida")
            {
                if (oculos.Estoque < movimentacoes.Quantidade)
                    throw new InvalidOperationException("Quantidade em estoque insuficiente para a movimentação.");

                oculos.Estoque -= movimentacoes.Quantidade;
            }
            else
            {
                throw new ArgumentException("Tipo de movimentação inválido. Use 'Entrada' ou 'Saída'.");
            }

            // Cria a movimentação
            var movimentacao = new MovimentacaoEstoque
            {
                OculosId = movimentacoes.OculosId,
                Quantidade = movimentacoes.Quantidade,
                TipoMovimentacao = movimentacoes.TipoMovimentacao,
                Observacao = movimentacoes.Observacao,
                DataMovimentacao = DateTime.Now
            };

            // Salva no banco de dados
            _context.MovimentacaoEstoques.Add(movimentacao);
            _context.Oculos.Update(oculos);
            await _context.SaveChangesAsync();

            var movimentacaoCriada = await _context.MovimentacaoEstoques
                                                   .AsNoTracking()
                                                   .Include(m => m.Oculos)
                                                   .Select(m => new MovimentacaoEstoque
                                                   {
                                                       Id = m.Id,
                                                       Quantidade = m.Quantidade,
                                                       DataMovimentacao = m.DataMovimentacao,
                                                       TipoMovimentacao = m.TipoMovimentacao,
                                                       Observacao = m.Observacao,
                                                       Oculos = new Oculos
                                                       {
                                                           Id = m.Oculos.Id,
                                                           Nome = m.Oculos.Nome,
                                                           Fabricante = m.Oculos.Fabricante,
                                                           Preco = m.Oculos.Preco,
                                                           Estoque = m.Oculos.Estoque
                                                       }
                                                   })
                                                   .FirstOrDefaultAsync(m => m.Id == movimentacao.Id);

            return _mapper.Map<ReadMovimentacaoEstoqueDto>(movimentacaoCriada);
        }

        public async Task<IEnumerable<ReadMovimentacaoEstoqueDto>> ListarMovimentacoesAsync()
        {
            var movimentacoes = await _context.MovimentacaoEstoques
                .AsNoTracking()
                .Include(m => m.Oculos) // Inclui o relacionamento necessário
                .Select(m => new MovimentacaoEstoque
                {
                    Id = m.Id,
                    Quantidade = m.Quantidade,
                    DataMovimentacao = m.DataMovimentacao,
                    TipoMovimentacao = m.TipoMovimentacao,
                    Observacao = m.Observacao,
                    Oculos = new Oculos
                    {
                        Id = m.Oculos.Id,
                        Nome = m.Oculos.Nome,
                        Fabricante = m.Oculos.Fabricante,
                        Preco = m.Oculos.Preco,
                        Estoque = m.Oculos.Estoque
                    }
                })
                .ToListAsync();

            return _mapper.Map<IEnumerable<ReadMovimentacaoEstoqueDto>>(movimentacoes);
        }




        public async Task<IEnumerable<ReadMovimentacaoEstoqueDto>> ListarMovimentacaoPorId(int id)
        {
            var movimentacao = await _context.MovimentacaoEstoques
                .Where(m => m.Id == id)
                .AsNoTracking()
                .Include(m => m.Oculos) // Inclui o relacionamento necessário
                .Select(m => new MovimentacaoEstoque
                {
                    Id = m.Id,
                    Quantidade = m.Quantidade,
                    DataMovimentacao = m.DataMovimentacao,
                    TipoMovimentacao = m.TipoMovimentacao,
                    Observacao = m.Observacao,
                    Oculos = new Oculos
                    {
                        Id = m.Oculos.Id,
                        Nome = m.Oculos.Nome,
                        Fabricante = m.Oculos.Fabricante,
                        Preco = m.Oculos.Preco,
                        Estoque = m.Oculos.Estoque
                    }
                })
                .ToListAsync();

            if (movimentacao == null)
            {
                throw new ArgumentException($"Movimentação de estoque com ID {id} não encontrada.");
            }

            // Mapeia a movimentação para o DTO
            return _mapper.Map<IEnumerable<ReadMovimentacaoEstoqueDto>>(movimentacao);
        }

    }
}
