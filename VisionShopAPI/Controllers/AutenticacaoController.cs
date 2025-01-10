using Microsoft.AspNetCore.Mvc;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Services;

namespace VisionShopAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private AutenticacaoService _autenticacaoService;

        public AutenticacaoController(AutenticacaoService authService)
        {
            _autenticacaoService = authService;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastraUsuario(LoginDto dto)
        {

            await _autenticacaoService.Cadastra(dto);
            return Ok("Usuário Cadastrado!");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var token = await _autenticacaoService.Login(dto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

    }
}
