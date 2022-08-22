using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Mvc.Models
{
    public class ContatosEdicaoModel
    {
        public Guid IdContato { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe seu nome.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de e-mail válido.")]
        [Required(ErrorMessage = "Por favor, informe seu e-mail.")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe seu nome.")]
        public string Telefone { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe seu nome.")]
        public string DataNascimento { get; set; }
    }
}
