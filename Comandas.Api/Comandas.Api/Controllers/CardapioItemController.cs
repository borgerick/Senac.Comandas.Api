using Comandas.Api.DTOs;
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
        public IResult Post([FromBody] CardapioItemCreateRequest cardapioCreate)
        {
            if (cardapioCreate.Descricao.Length < 3)
                Results.BadRequest("A descrição deve ter no minimo 3 caracteres");
            if (cardapioCreate.Preco <= 0)
                Results.BadRequest("O preço deve ser maior que zero");
            var cardapioItem = new CardapioItem
            {
                Id = cardapios.Count + 1,
                Descricao = cardapioCreate.Descricao,
                Preco = cardapioCreate.Preco,
                PossuiPreparo = cardapioCreate.PossuiPreparo
            };
            cardapios.Add(cardapioItem);
            return Results.Ok(cardapioItem);
        }

        // PUT api/<CardapioItemController>/5
        /// <summary>
        /// Atualiza um item do cardápio
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cardapioUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapioUpdate)
        {
            var cardapio = cardapios.
                FirstOrDefault(c => c.Id == id);
            if (cardapio is null)
                return Results.NotFound("Cardápio não encontrado");
            //cardapio.Titulo = cardapioUpdate.Titulo;
            cardapio.Descricao = cardapioUpdate.Descricao;
            cardapio.Preco = cardapioUpdate.Preco;
            cardapio.PossuiPreparo = cardapioUpdate.PossuiPreparo;
            return Results.NotFound();
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
