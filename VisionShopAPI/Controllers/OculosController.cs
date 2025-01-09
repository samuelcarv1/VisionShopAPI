using AutoMapper;
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
    public class OculosController : ControllerBase
    {
        private VisionShopContext _context;
        private IMapper _mapper;

        public OculosController(VisionShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CriarOculos([FromBody] CreateOculosDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var oculos = _mapper.Map<Oculos>(dto);
            _context.Oculos.Add(oculos);
            _context.SaveChanges();

            var readDto = _mapper.Map<ReadOculosDto>(oculos);

            return CreatedAtAction(nameof(ObterPorId), new { id = oculos.Id }, readDto);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var oculos = _context.Oculos.Find(id);

            if(oculos == null) return NotFound("Óculos não encontrado.");

            var readDto = _mapper.Map<ReadOculosDto>(oculos);

            return Ok(readDto);
        }

        [HttpGet]
        public IEnumerable<ReadOculosDto> ObterTodos()
        {
            var oculos = _context.Oculos.ToList();

            return _mapper.Map<List<ReadOculosDto>>(oculos);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarOculos(int id, [FromBody] CreateOculosDto oculosDto)
        {
            var oculos = _context.Oculos.Find(id);

            if (oculos == null) return NotFound("Óculos não encontrado.");

            _mapper.Map(oculosDto, oculos);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarOculos(int id)
        {
            var oculos = _context.Oculos.Find(id);

            if (oculos == null) return NotFound("Óculos não encontrado.");

            _context.Oculos.Remove(oculos);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
