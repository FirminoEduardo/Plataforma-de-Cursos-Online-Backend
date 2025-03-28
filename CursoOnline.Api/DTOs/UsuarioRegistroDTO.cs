using System.ComponentModel.DataAnnotations;

namespace CursoOnline.Api.DTOs
{
    public class UsuarioRegistroDTO
    {
        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        [EmailAddress]
        public string Email {  get; set; }

        [Required]
        [MinLength(6)]
        public string Senha {  get; set; }

        [Required]
        public string TipoUsuario { get; set; } // Aluno ou instrutor
        

    }
}
