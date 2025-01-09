using AutoMapper;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisionShopAPI.Data;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

namespace VisionShopAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class ClienteController : ControllerBase
    {
        private VisionShopContext _context;
        private IMapper _mapper;

        public ClienteController(VisionShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CriarCliente([FromBody] CreateClienteDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cliente = _mapper.Map<Cliente>(dto);
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            var readDto = _mapper.Map<ReadClienteDto>(cliente);

            return CreatedAtAction(nameof(ObterPorId), new { id = cliente.Id }, readDto);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null) return NotFound("Cliente não encontrado.");

            var readDto = _mapper.Map<ReadClienteDto>(cliente);

            return Ok(readDto);
        }

        [HttpGet]
        public IEnumerable<ReadClienteDto> ObterTodos()
        {
            var clientes = _context.Clientes.ToList();

            return _mapper.Map<List<ReadClienteDto>>(clientes);
        }
    }
}
