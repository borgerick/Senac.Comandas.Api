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
        List<PedidoCozinha> pedidosCozinhas = new List<PedidoCozinha>()
        {
            new PedidoCozinha
            {
                Id = 1,
                ComandaItemIdComandaId = 1,
                Itens = new List<PedidoCozinhaItem>
                {
                    new PedidoCozinhaItem
                    {
                        Id = 1,
                        PedidoCozinhaId = 1,
                        ComandaItemId = 1,
                    }
                }
            },
            new PedidoCozinha
            {
                Id = 2,
                ComandaItemIdComandaId = 2,
                Itens = new List<PedidoCozinhaItem>
                {
                    new PedidoCozinhaItem
                    {
                        Id = 2,
                        PedidoCozinhaId = 2,
                        ComandaItemId = 2,
                    }
                }
            },
        };
        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(pedidosCozinhas);
        }

        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var pedidoCozinha = pedidosCozinhas.
                FirstOrDefault(u => u.Id == id);
            if (pedidoCozinha is null)
                return Results.NotFound("Pedido de cozinha nao encontrado");
            return Results.Ok(pedidoCozinha);
        }

        // POST api/<PedidoCozinhaController>
        [HttpPost]
        public IResult Post([FromBody] PedidoCozinhaCreateRequest pedidoCozinhaCreate)
        {
        }

        // PUT api/<PedidoCozinhaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] PedidoCozinhaUpdateRequest pedidoCozinhaUpdate)
        {
            if (pedidoCozinhaUpdate.Id <= 0)
                return Results.BadRequest("O id da comanda deve ser maior que zero");
            var pedidoCozinha = pedidosCozinhas.
                FirstOrDefault(u => u.Id == id);
            if (pedidoCozinha is null)
                return Results.NotFound("Pedido de cozinha nao encontrado");
            pedidoCozinha.ComandaId = pedidoCozinhaUpdate.ComandaId;
            return Results.NoContent();

        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
