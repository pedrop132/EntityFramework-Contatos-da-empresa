using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiForEmpresa.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Utilizador")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; } = string.Empty;
    }

}
