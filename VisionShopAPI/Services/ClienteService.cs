using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VisionShopAPI.Data;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

namespace VisionShopAPI.Services
{
    public class ClienteService
    {
        private VisionShopContext _context;
        private IMapper _mapper;

        public ClienteService(VisionShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadClienteDto CriarCliente(CreateClienteDto dto)
        {
            var cliente = _mapper.Map<Cliente>(dto);
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return  _mapper.Map<ReadClienteDto>(cliente);

        }

        public ReadClienteDto? ObterPorId(int id)
        {
            var cliente = _context.Clientes.Find(id);
            return cliente != null ? _mapper.Map<ReadClienteDto>(cliente) : null;
        }

        public IEnumerable<ReadClienteDto> ObterTodos()
        {
            return _mapper.Map<List<ReadClienteDto>>(_context.Clientes.ToList());
        }
    }
}   
