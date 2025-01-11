using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VisionShopAPI.Data;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

namespace VisionShopAPI.Services
{
    public class VendaService
    {
        private VisionShopContext _context;
        private IMapper _mapper;
        private MovimentacaoEstoqueService _movimentacaoEstoqueService;

        public VendaService(VisionShopContext context, IMapper mapper, MovimentacaoEstoqueService movimentacaoEstoqueService)
        {
            _context = context;
            _mapper = mapper;
            _movimentacaoEstoqueService = movimentacaoEstoqueService;
        }

        public async Task<Dictionary<bool, string>> RegistraVendaAsync(CreateVendaDto dto)
        {
            var mensagem = new Dictionary<bool, string>();

            var cliente = _context.Clientes.Find(dto.ClienteId);
            if (cliente == null)
            {
                mensagem.Add(false, "Cliente não encontrado!");
                return mensagem;
            }

            foreach (var item in dto.Itens)
            {
                var oculos = _context.Oculos.Find(item.OculosId);
                if (oculos == null || oculos.Estoque < item.Quantidade)
                {
                    mensagem.Add(false, $"Óculos (ID: {item.OculosId}) não encontrado ou estoque insuficiente!");
                    return mensagem;
                }
            }

            var venda = new Venda
            {
                ClienteId = dto.ClienteId,
                DataVenda = dto.DataVenda,
                Itens = dto.Itens.Select(i => new ItemVenda
                {
                    OculosId = i.OculosId,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario
                }).ToList()
            };

            // Registra saída de estoque para cada item vendido
            foreach (var item in venda.Itens)
            {
                try
                {
                    await _movimentacaoEstoqueService.CriarMovimentacao(new CreateMovimentacaoEstoqueDto
                    {
                        OculosId = item.OculosId,
                        Quantidade = item.Quantidade,
                        TipoMovimentacao = "Saída",
                        Observacao = $"Venda ID: {venda.Id}"
                    });
                }
                catch (Exception ex)
                {
                    mensagem.Add(false, $"Erro ao registrar movimentação para Óculos (ID: {item.OculosId}): {ex.Message}");
                    return mensagem;
                }
            }

            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();

            mensagem.Add(true, "Venda registrada com sucesso.");
            return mensagem;
        }

        public ReadVendaDto ObterVendaPorId(int id)
        {
            var venda = _context.Vendas.Include(v => v.Itens)
                                       .ThenInclude(i => i.Oculos)
                                       .Include(v => v.Cliente)
                                       .FirstOrDefault(v => v.Id == id);

            if (venda == null) return null;

            return _mapper.Map<ReadVendaDto>(venda);
        }
    }
}
