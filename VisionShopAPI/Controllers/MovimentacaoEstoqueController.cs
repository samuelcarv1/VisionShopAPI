using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Services;

namespace VisionShopAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class MovimentacaoEstoqueController : ControllerBase
    {
        private MovimentacaoEstoqueService _estoqueService;

        public MovimentacaoEstoqueController(MovimentacaoEstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        [HttpPost]
        public async Task<ActionResult> CriarMovimentacao([FromBody] CreateMovimentacaoEstoqueDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var movimentacao = await _estoqueService.CriarMovimentacao(dto);

                return CreatedAtAction(nameof(ObterPorId), new { id = movimentacao.Id }, movimentacao);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarMovimentacoes()
        {
            var movimentacoes = await _estoqueService.ListarMovimentacoesAsync();
            return Ok(movimentacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var estoque = await _estoqueService.ListarMovimentacaoPorId(id);

            if (estoque == null) return NotFound("Movimentação de estoque não encontrada!");

            return Ok(estoque);
        }
    }
}
