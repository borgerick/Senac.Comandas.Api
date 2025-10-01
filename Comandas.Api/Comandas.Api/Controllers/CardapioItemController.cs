using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //define que essa classe é um controller de API
    public class CardapioItemController : ControllerBase // herda de ControllerBase para poder responder requisições HTTP
    {

        public List<CardapioItem> cardapios = new List<CardapioItem>() {
            new CardapioItem
            {
                Id = 1,
                Descricao = "Coxinha",
                Preco = 5.50m,
                PossuiPreparo = true
            },
            new CardapioItem
            {
                Id = 2,
                Descricao = "X-Salada",
                Preco = 25.50m,
                PossuiPreparo = true
            }
        };
        // metodo get que retorna a lista de cardapio
        // GET: api/<CardapioItemController>
        [HttpGet] // Anotação que indica que esse método responde a requisições GET
        public IResult GetCardapios()
        {
            //cria uma lista estatica de cardápio e transforma em json
            return Results.Ok(cardapios);
        }
        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            //BUSCA NA LISTA de cardapios de acordo com ID do parametro
            //Joga o valor para a variavel o primeiro elemento de acordo com o id
            var cardapio = cardapios.FirstOrDefault(c => c.Id == id);
            if (cardapio == null)
            {
                //se nao encontrar o cardapio com o id, retorna 404
                return Results.NotFound("Cardápio não encontrado!");
            }
            //retorna o valor para o endpoint da api
            return Results.Ok(cardapio);
            
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
