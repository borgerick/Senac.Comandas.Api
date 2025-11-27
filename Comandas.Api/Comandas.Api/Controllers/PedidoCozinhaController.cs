using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {
        static List<PedidoCozinha> pedidos = new List<PedidoCozinha>()
        {
            new PedidoCozinha
            {
                Id = 1,
                ComandaId = 1,
            },
            new PedidoCozinha
            {
                Id = 2,
                ComandaId = 2,
            }
        };
        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(pedidos);
        }

        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var pedido = pedidos
                .FirstOrDefault(p => p.Id == id);
            if (pedido is null)
                return Results.NotFound("Pedido não encontrado");
            return Results.Ok(pedido);
        }

        // POST api/<PedidoCozinhaController>
        [HttpPost]
        public void Post([FromBody] PedidoCozinhaCreateRequest pedido)
        {
        }

        // PUT api/<PedidoCozinhaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var pedido = pedidos
                .FirstOrDefault(p => p.Id == id);

            if (pedido is null)
                return Results.NotFound($"Pedido {id} não encontrado");

            var removidoComSucesso = pedidos.Remove(pedido);
            if (removidoComSucesso)
                return Results.NoContent();

            return Results.StatusCode(500);
        }
    }
}