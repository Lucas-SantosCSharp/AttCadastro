using System.ComponentModel.DataAnnotations;

namespace AttCadastro.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string Cargo { get; set; } // Administrador, Sindico ou Morador
    }
}
