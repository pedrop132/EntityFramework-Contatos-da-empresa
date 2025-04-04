﻿using Empresa.Data;
using Empresa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Empresa.Controllers
{
    [Route("api/[controller]/[action]")]
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
        public async Task<IActionResult> DeleteById(int id)
        {
            var contato = await _AppDbcontext.Contatos.FirstOrDefaultAsync(x => x.Id == id);

            if (contato == null) return NotFound();

            _AppDbcontext.Contatos.Remove(contato);
            await _AppDbcontext.SaveChangesAsync();
            return Ok(contato);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] ContatosEmpresa contato)
        {
            var existingContato = await _AppDbcontext.Contatos.FindAsync(id);
            if (existingContato == null) return NotFound();

            existingContato.Nome = contato.Nome;
            existingContato.Morada = contato.Morada;
            existingContato.Telefone = contato.Telefone;

            await _AppDbcontext.SaveChangesAsync();
            return Ok(existingContato);
        }
    }
}
