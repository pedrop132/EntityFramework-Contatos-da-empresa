using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Empresa.Models
{
    public class ContatosEmpresa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [DisplayName("Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Morada é obrigatória.")]
        [DisplayName("Morada")]
        public string Morada { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        [DisplayName("Telefone")]
        public string Telefone { get; set; } = string.Empty;
    }
}
