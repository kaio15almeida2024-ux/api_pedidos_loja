using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDBLojaJV.Models;

namespace ApiDBLojaJV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly DblojaJvContext _context;

        public PedidoController(DblojaJvContext context)
        {
            _context = context;
        }

        // GET: api/Pedido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos
                .Include(p => p.IdclienteNavigation)
                .ToListAsync();
        }

        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.IdclienteNavigation)
                .FirstOrDefaultAsync(p => p.Idpedido == id);

            if (pedido == null)
                return NotFound();

            return pedido;
        }

        // GET: api/Pedido/listarPedidos
        [HttpGet("listarPedidos")]
        public async Task<ActionResult> ListarPedidos()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.IdclienteNavigation)
                .Select(p => new
                {
                    idpedido = p.Idpedido,
                    descricao = p.Descricao,
                    valor = p.Valor,
                    idcliente = p.Idcliente,
                    nomeCliente = p.IdclienteNavigation != null ? p.IdclienteNavigation.Nome : null
                })
                .ToListAsync();

            return Ok(pedidos);
        }

        // POST: api/Pedido
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = pedido.Idpedido }, pedido);
        }

        // PUT: api/Pedido/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            var pedidoBanco = await _context.Pedidos.FindAsync(id);

            if (pedidoBanco == null)
                return NotFound();

            pedidoBanco.Descricao = pedido.Descricao;
            pedidoBanco.Valor = pedido.Valor;
            pedidoBanco.Idcliente = pedido.Idcliente;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
                return NotFound();

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
