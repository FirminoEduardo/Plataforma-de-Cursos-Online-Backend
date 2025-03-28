using Microsoft.AspNetCore.Identity;

namespace CursoOnline.Api.Models
{
    public class Usuario : IdentityUser
    {
        public string NomeCompleto {  get; set; }
        public string TipoUsuario { get; set; } // Aluno ou instrutor
    }
}
