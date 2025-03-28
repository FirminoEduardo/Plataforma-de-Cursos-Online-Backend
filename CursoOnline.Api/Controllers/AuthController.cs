using System.Diagnostics.CodeAnalysis;
using CursoOnline.Api.DTOs;
using CursoOnline.Api.Models;
using CursoOnline.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDTO dto)
        {
            var usuario = new Usuario
            {
                UserName = dto.Email,
                Email = dto.Email,
                NomeCompleto = dto.NomeCompleto,
                TipoUsuario = dto.TipoUsuario
            };

            var resultado = await _authService.RegistarUsuarioAsync(usuario, dto.Senha);

            if (!resultado.Succeeded)
                return BadRequest(resultado.Errors);

            return Ok("Usuário registrado com sucesso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
        {
            var token = await _authService.LoginAsync(dto.Email, dto.Senha);

            if (token == null)
                return Unauthorized("Email ou senha inválidos.");

            return Ok(new { token });
        }
    }
}
