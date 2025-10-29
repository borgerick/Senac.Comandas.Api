using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {
       public ComandasDbContext _context;
        public PedidoCozinhaController(ComandasDbContext context)
        {
            _context = context;
        }
        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IResult Get()
        {
            var pedidosCozinhas = _context.PedidoCozinhas.ToList();
            return Results.Ok(pedidosCozinhas);
        }

        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var pedidoCozinha = _context.PedidoCozinhas.
                FirstOrDefault(u => u.Id == id);
            if (pedidoCozinha is null)
                return Results.NotFound("Pedido de cozinha nao encontrado");
            return Results.Ok(pedidoCozinha);
        }

        // POST api/<PedidoCozinhaController>
        [HttpPost]
        //public IResult Post([FromBody] PedidoCozinhaCreateRequest pedidoCozinhaCreate)
        //{
        //}

        // PUT api/<PedidoCozinhaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] PedidoCozinhaUpdateRequest pedidoCozinhaUpdate)
        {
            if (id <= 0)
                return Results.BadRequest("O id da comanda deve ser maior que zero");
            var pedidoCozinha = _context.PedidoCozinhas.
                FirstOrDefault(u => u.Id == id);
            if (pedidoCozinha is null)
                return Results.NotFound("Pedido de cozinha nao encontrado");
            
            return Results.NoContent();

        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var pedidoCozinha = _context.PedidoCozinhas.FirstOrDefault(u => u.Id == id);
            if (pedidoCozinha is null)
                return Results.NotFound("Pedido de cozinha nao encontrado");
            
            var removidoComSucessoPedido = _context.PedidoCozinhas.Remove(pedidoCozinha);
            
            _context.PedidoCozinhas.Remove(pedidoCozinha);
            var removido = _context.SaveChanges();
            if (removido > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}
