using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiClientesPedidos.DataContext;
using ApiClientesPedidos.Models;

namespace ApiClientesPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly Context _context;

        public EnderecoController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetAllEnderecosAsync()
        {
            var endereco = await _context.Enderecos.AsNoTracking().ToListAsync();
            if (endereco == null)
                {
                return NotFound("Endereco não localizado");
                }
            return await _context.Enderecos.ToListAsync();
        }

        [HttpGet("{id}", Name = "ObterEndereco")]
        public async Task<ActionResult<Endereco>> GetEnderecoAsync(int id)
        {
            var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.EnderecoId == id);
            if (endereco == null)
            {
              return NotFound("Endereco não localizado");
            }
            
            return Ok(endereco);
        }

    
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnderecoAsync(int id, Endereco endereco)
        {
            if (id != endereco.EnderecoId)
            {
                return BadRequest("Endereco não localizado");
            }

            _context.Entry(endereco).State = EntityState.Modified;
            await _context.SaveChangesAsync();
          
            return Ok(endereco);
        }

        [HttpPost]
        public async Task<ActionResult<Endereco>> PostEndereco([FromBody]Endereco endereco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (endereco == null)
            {
                return BadRequest("Dados invalidos");
            }
            await _context.AddAsync(endereco);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterEndereco", new { id = endereco.EnderecoId }, endereco);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.EnderecoId == id);
            if (endereco is null)
            {
                return BadRequest("Endereco não localizado");
            }
          
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();

            return Ok(endereco);
        }
    }
}
