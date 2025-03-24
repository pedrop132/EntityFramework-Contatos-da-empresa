using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiForEmpresa.Models
{
    public class ContatoViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nome do contato")]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [DisplayName("Email")]
        public string Morada { get; set; } = string.Empty;
        [Required]
        [DisplayName("Telefone")]
        public string Telefone { get; set; } = string.Empty;
    }
}
