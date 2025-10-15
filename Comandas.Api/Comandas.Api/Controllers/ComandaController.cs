using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        static List<Comanda> comandas = new List<Comanda>()
        {
            new Comanda
            {
                Id = 1,
                NomeCliente = "Maria Silva",
                NumeroMesa = 1,
                Itens = new List<ComandaItem>
                {
                    new ComandaItem
                    {
                        Id = 1,
                        CardapioItemId = 1,
                        ComandaId = 1,
                    }
                }
            },
            new Comanda
            {
                Id = 2,
                NomeCliente = "Eduardo Borges",
                NumeroMesa = 2,
                Itens = new List<ComandaItem>
                {
                    new ComandaItem
                    {
                        Id = 2,
                        CardapioItemId = 2,
                        ComandaId = 2,
                    }
                }
            },
        };
        // GET: api/<ComandaController>
        [HttpGet]
        public IResult Get()//Esse get retorna todos os usuarios
        {
            return Results.Ok(comandas);
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)//Esse get retorna um usuario pelo id
        {
            var comanda = comandas.
                FirstOrDefault(u => u.Id == id);
            if (comanda is null)
                return Results.NotFound("Comanda nao encontrada");
            return Results.Ok(comanda);
        }

        // POST api/<ComandaController>
        [HttpPost]
        public IResult Post([FromBody] ComandaCreateRequest comandaCreate)
        {
                if (comandaCreate.NomeCliente.Length < 3)
                    return Results.BadRequest("A senha deve ter no minimo 6 caracteres");
                if (comandaCreate.NumeroMesa <= 0)
                    return Results.BadRequest("O nome deve ter no minimo 3 caracteres");
                if (comandaCreate.CardapioItemIds.Length == 0) ;
                return Results.BadRequest("O email deve ser valido.");

                var novaComanda = new Comanda
                {
                    Id = comandas.Count + 1,//gera o id automatico
                    NomeCliente = comandaCreate.NomeCliente,
                    NumeroMesa = comandaCreate.NumeroMesa,
                };
            //cria a lista de itens da comanda
            var comandaItens = new List<ComandaItem>();
            //percorre os ids dos itens do cardapio
            foreach (int cardapioItemId in comandaCreate.CardapioItemIds)
            {
                //cria um novo item da comanda
                var comandaItem = new ComandaItem
                {
                    Id = comandaItens.Count + 1,
                    CardapioItemId = cardapioItemId,
                    ComandaId = novaComanda.Id,
                };
                //adiciona o item na lista de itens da comanda
                comandaItens.Add(comandaItem);
            }
            //atribui os itens a nova comanda
            novaComanda.Itens = comandaItens;
            //adiciona o usuario na lista
            comandas.Add(novaComanda);
            return Results.Created($"/api/usuario/{novaComanda.Id}", novaComanda);
            }
        

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {
            var comanda = comandas.FirstOrDefault(u => u.Id == id);
            if (comanda is null)
                return Results.NotFound("Comanda nao encontrada");
            if(comandaUpdate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome deve ter no minimo 3 caracteres");
            if (comandaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O numero da mesa deve ser maior que zero");
            comanda.NomeCliente = comandaUpdate.NomeCliente;
            comanda.NumeroMesa = comandaUpdate.NumeroMesa;
            return Results.NoContent();
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
