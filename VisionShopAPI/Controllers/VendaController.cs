using Microsoft.AspNetCore.Mvc;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Services;

namespace VisionShopAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class VendaController : ControllerBase
    {
        private VendaService _vendaService;

        public VendaController(VendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [HttpPost]
        public IActionResult RegistrarVenda([FromBody] CreateVendaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var sucesso = _vendaService.RegistraVenda(dto);
            if (!sucesso) return NotFound("Cliente não encontrado ou dados inválidos.");

            return Ok("Venda registrada com sucesso.");
        }

        [HttpGet("{id}")]
        public IActionResult ObterVendaPorId(int id)
        {
            var venda = _vendaService.ObterVendaPorId(id);
            if (venda == null) return NotFound("Venda não encontrada.");

            return Ok(venda);
        }
    }
}
