using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

namespace VisionShopAPI.Services
{
    public class AutenticacaoService
    {
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private IConfiguration _configuration;
        private IMapper _mapper;
        private TokenService _tokenService;

        public AutenticacaoService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IConfiguration configuration, IMapper mapper, TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task Cadastra(LoginDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);

            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            if (!resultado.Succeeded)
                throw new ApplicationException("Falha ao cadastrar usuário!");
        }

        public async Task<string> Login(LoginDto dto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if (!resultado.Succeeded) throw new ApplicationException("Usuário não autenticado!");

            var usuario = _signInManager.UserManager.Users.FirstOrDefault(u => u.UserName == dto.Username.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }

    }
}
