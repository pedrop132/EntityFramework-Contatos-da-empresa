using Empresa.Data;
using Empresa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosEmpresaController : ControllerBase
    {
        private readonly AppDbContext _AppDbcontext;
        public ContatosEmpresaController(AppDbContext context)
        {
            _AppDbcontext = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddContato(ContatosEmpresa contato)
        {
            _AppDbcontext.Contatos.Add(contato);
            await _AppDbcontext.SaveChangesAsync();

            return Ok(contato);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contatos = await _AppDbcontext.Contatos.ToListAsync();
            return Ok(contatos);
        }
    }
}
