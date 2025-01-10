using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisionShopAPI.Data;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;
using VisionShopAPI.Services;

namespace VisionShopAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OculosController : ControllerBase
    {
        private OculosService _oculosService;

        public OculosController(OculosService oculosService)
        {
            _oculosService = oculosService;
        }

        [HttpPost]
        public IActionResult CriarOculos([FromBody] CreateOculosDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var readOculos = _oculosService.CriarOculos(dto);

            return CreatedAtAction(nameof(ObterPorId), new { id = readOculos.Id }, readOculos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var oculos = _oculosService.ObterPorId(id);

            if(oculos == null) return NotFound("Óculos não encontrado.");

            return Ok(oculos);
        }
        [HttpGet]
        public IEnumerable<ReadOculosDto> ObterTodos()
        {
            var oculos = _oculosService.ObterTodos();

            return oculos;
        }

        //[HttpPut("{id}")]
        //public IActionResult AtualizarOculos(int id, [FromBody] CreateOculosDto oculosDto)
        //{
        //    var oculos = _context.Oculos.Find(id);

        //    if (oculos == null) return NotFound("Óculos não encontrado.");

        //    _mapper.Map(oculosDto, oculos);
        //    _context.SaveChanges();

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeletarOculos(int id)
        //{
        //    var oculos = _context.Oculos.Find(id);

        //    if (oculos == null) return NotFound("Óculos não encontrado.");

        //    _context.Oculos.Remove(oculos);
        //    _context.SaveChanges();

        //    return NoContent();
        //}
    }
}
