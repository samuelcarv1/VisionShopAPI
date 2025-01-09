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

        public VendaService(VisionShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool RegistraVenda(CreateVendaDto dto)
        {
            var cliente = _context.Clientes.Find(dto.ClienteId);
            if (cliente == null) return false;

            foreach (var item in dto.Itens)
            {
                var oculos = _context.Oculos.Find(item.OculosId);
                if (oculos == null || oculos.Estoque < item.Quantidade)
                {
                    return false;
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

            foreach (var item in venda.Itens)
            {
                var oculos = _context.Oculos.Find(item.OculosId);
                oculos.Estoque -= item.Quantidade;
            }

            _context.Vendas.Add(venda);
            _context.SaveChanges();

            return true;
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
