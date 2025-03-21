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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contato = await _AppDbcontext.Contatos.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(contato);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contato = await _AppDbcontext.Contatos.FirstOrDefaultAsync(x => x.Id == id);

            if (contato == null) return NotFound();

            _AppDbcontext.Contatos.Remove(contato);
            await _AppDbcontext.SaveChangesAsync();
            return Ok(contato);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ContatosEmpresa contato)
        {
            _AppDbcontext.Contatos.Update(contato);
            await _AppDbcontext.SaveChangesAsync();
            return Ok(contato);
        }
    }
}
