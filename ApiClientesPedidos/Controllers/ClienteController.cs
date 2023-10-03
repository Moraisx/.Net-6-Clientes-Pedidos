using ApiClientesPedidos.DataContext;
using Microsoft.AspNetCore.Mvc;
using ApiClientesPedidos.Models;
using Microsoft.EntityFrameworkCore;
using ApiClientesPedidos.DTO;

namespace ApiClientesPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly Context _context;

        public ClienteController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllAsync()
        {
            var cliente = await _context.Clientes.AsNoTracking().ToListAsync();
            var clienteDTO = cliente.Select(cliente => new ClienteDTO() 
            { 
                PrimeiroNome = cliente.PrimeiroNome,
                SobreNome = cliente.Sobrenome,
                Telefone = cliente.Telefone,
                Email = cliente.Email
            });

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(clienteDTO);
        }

       
        [HttpGet("{id}", Name = "Obtercliente")]
        public async Task<ActionResult<Cliente>> GetClienteAsync(int id)
        {
            var cli = await _context.Clientes.Include(endereco => endereco.Endereco).AsNoTracking().ToListAsync();
            var cliente = cli.FirstOrDefault(cliente => cliente.ClienteId == id);

            if (cliente == null)
            {
                return NotFound("Cliente não cadastrado");
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody] ClienteDTO request)
        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(request == null)
            {
                return BadRequest("Dados invalidos");
            }

            Cliente cliente = new Cliente()
            {
                PrimeiroNome = request.PrimeiroNome,
                Sobrenome = request.SobreNome,
                Email = request.Email,
                Telefone = request.Telefone
            };

            await _context.AddAsync<Cliente>(cliente);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("obtercliente", new { id = cliente.ClienteId }, cliente);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutCliente(int id, [FromBody] Cliente cliente)
        {
            if(id != cliente.ClienteId)
            {
                return BadRequest($"Cliente não localizado");
            }

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(cliente);//retorna response status is 200
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync<Cliente>(c => c.ClienteId == id);
            if(cliente is null)
            {
                return NotFound($"Cliente não localizado");
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok(cliente);

        }
    }
}
