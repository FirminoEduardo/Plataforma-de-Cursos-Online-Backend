using System.Security.Claims;
using System.Text;
using CursoOnline.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace CursoOnline.Api.Services
{
    public class AuthService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<Usuario> useManager,
            SignInManager<Usuario> signInManager,
            IConfiguration configuration)
        {
            _userManager = useManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegistarUsuarioAsync(Usuario usuario, string senha)
        {
            return await _userManager.CreateAsync(usuario, senha);
        }

        public async Task<string> LoginAsync(string email, string senha)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null) return null;

            var resultado = await _signInManager, CheckPasswordSignInAsync(usuario, senha, false);
            if (!resultado.Succeeded) return null;

            return GerarTokenJwt(usuario);
        }

        private string GerarTokenJwt(Usuario usuario)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario ?? "Aluno")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        public async Task<Usuario> ObterUsuarioPorEmailSync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
