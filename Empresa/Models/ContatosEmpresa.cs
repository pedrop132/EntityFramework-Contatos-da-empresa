using Microsoft.EntityFrameworkCore;

namespace Empresa.Models
{
    public class ContatosEmpresa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Morada { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }
}
