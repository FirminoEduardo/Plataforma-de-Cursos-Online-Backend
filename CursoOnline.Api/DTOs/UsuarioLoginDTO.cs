using System.ComponentModel.DataAnnotations;

namespace CursoOnline.Api.DTOs
{
    public class UsuarioLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }



    }
}
