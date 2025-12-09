using Comandas.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Comandas.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {

        public ComandasDbContext _dbContext { get; set; }
        public PedidoCozinhaController(ComandasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IResult Get()
        {
            var pedidos = _dbContext.PedidoCozinhas
                .Select(p => new PedidoCozinhaResponse
                {
                    Id = p.Id,
                    ComandaId = p.ComandaId,
                    Itens = p.Itens.Select(pi => new PedidoCozinhaItemResponse
                    {
                        Id = pi.Id,
                        Titulo =
                                _dbContext.CardapioItens
                            .First(ci => ci.Id == _dbContext.ComandaItens
                                                    .First(ci => ci.Id == pi.ComandaItemId).CardapioItemId
                             ).Titulo
                    }),
                })
                .ToList();
            return Results.Ok(pedidos);
        }

        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var pedido = _dbContext.PedidoCozinhas
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
            var pedido = _dbContext.PedidoCozinhas
                .FirstOrDefault(p => p.Id == id);

            if (pedido is null)
                return Results.NotFound($"Pedido {id} não encontrado");

            var removidoComSucesso = _dbContext.Remove(pedido);


            return Results.StatusCode(500);
        }
    }
}