using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardapioItemController : ControllerBase
    {
        private readonly ComandasDbContext _context;
        public CardapioItemController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: api/cardapioitens
        [HttpGet]
        public IResult Get()
        {
            var cardapios = _context.CardapioItens.Include(c => c.CategoriaCardapio).ToList();
            return Results.Ok(cardapios);
        }

        // GET api/<CardapioItemController>/1
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            // BUSCAR NA LISTA de cardapios de acordo com o Id do parametro
            // joga o valor para a variavel o primeiro elemento de acordo com o id
            var cardapio = _context
                        .CardapioItens
                        .Include(ci => ci.CategoriaCardapio)
                        .FirstOrDefault(c => c.Id == id);
            if (cardapio is null)
            {
                return Results.NotFound("Cardápio não encontrado!");
            }
            // retorna o valor para o endpoint da api
            return Results.Ok(cardapio);
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public IResult Post([FromBody] CardapioItemCreateRequest cardapio)
        {
            if (cardapio.Titulo.Length < 3)
                return Results.BadRequest("O título do item do cardápio deve ter no mínimo 3 caracteres.");
            if (cardapio.Descricao.Length < 3)
                return Results.BadRequest("A descrição do item do cardápio deve ter no mínimo 3 caracteres.");
            if (cardapio.Preco <= 0)
                return Results.BadRequest("O preço do item do cardápio deve ser maior que zero.");

            // validação da categoria se for preenchida
            if (cardapio.CategoriaCardapioId.HasValue)
            {
                var categoria = _context.CategoriaCardapios
                    .FirstOrDefault(c => c.Id == cardapio.CategoriaCardapioId);
                if (categoria is null)
                    return Results.BadRequest("Categoria de cardápio inválida.");
            }
            var cardapioItem = new CardapioItem
            {
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo,
                CategoriaCardapioId = cardapio.CategoriaCardapioId
            };
            // adiciona o cardapio na lista
            _context.CardapioItens.Add(cardapioItem);
            _context.SaveChanges();
            return Results.Created($"/api/cardapioitem/{cardapioItem.Id}", cardapioItem);
        }

        // PUT api/<CardapioItemController>/5
        /// <summary>
        /// Atualiza um cardapio item.
        /// </summary>
        /// <remarks>The specified ID must correspond to an existing menu item. If the ID does not exist,
        /// the update operation will fail. Ensure that the update request contains valid data for the menu item
        /// fields.</remarks>
        /// <param name="id">The unique identifier of the menu item to update.</param>
        /// <param name="cardapio">The update request containing the new values for the menu item. This parameter must not be null.</param>
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapio)
        {
            var cardapioItem = _context.CardapioItens.
                    FirstOrDefault(c => c.Id == id);

            if (cardapioItem is null)
                return Results.NotFound($"Cardápio {id} não encontrado!");
            // se categoria informada
            if (cardapio.CategoriaCardapioId.HasValue)
            {
                // consulta no banco pelo id da categoria
                var categoria = _context.CategoriaCardapios
                     .FirstOrDefault(c => c.Id == cardapio.CategoriaCardapioId);
                // se o retorno da consulta retornou nulo
                if (categoria is null)
                    return Results.BadRequest("Categoria de cardápio inválida.");
            }
            cardapioItem.Titulo = cardapio.Titulo;
            cardapioItem.Descricao = cardapio.Descricao;
            cardapioItem.Preco = cardapio.Preco;
            cardapioItem.PossuiPreparo = cardapio.PossuiPreparo;
            cardapioItem.CategoriaCardapioId = cardapio.CategoriaCardapioId;

            _context.SaveChanges();
            return Results.NoContent();
        }

        // DELETE http:5100/api/cardapioitem/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // buscar o cardapio na lista pelo id
            var cardapioItem = _context.CardapioItens
                .FirstOrDefault(c => c.Id == id);
            // se estiver nulo, retorna 404
            if (cardapioItem is null)
                return Results.NotFound($"Cardápio {id} não encontrado!");
            // remove o objeto cardapio da lista
            _context.CardapioItens.Remove(cardapioItem);
            _context.SaveChanges();
            return Results.NoContent();


        }
    }
}