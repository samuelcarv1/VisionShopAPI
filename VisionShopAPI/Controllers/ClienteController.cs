using AutoMapper;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisionShopAPI.Data;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;
using VisionShopAPI.Services;

namespace VisionShopAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class ClienteController : ControllerBase
    {
        private ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public IActionResult CriarCliente([FromBody] CreateClienteDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cliente = _clienteService.CriarCliente(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = cliente.Id }, cliente);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var cliente = _clienteService.ObterPorId(id);

            if(cliente == null) return NotFound();

            return Ok(cliente);
        }

        [HttpGet]
        public IEnumerable<ReadClienteDto> ObterTodos()
        {
            return _clienteService.ObterTodos();
        }
    }
}
