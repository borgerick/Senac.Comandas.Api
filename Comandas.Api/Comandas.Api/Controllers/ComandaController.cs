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
        List<Comanda> comandas = new List<Comanda>()
        {
            new Comanda
            {
                Id = 1,
                NomeCliente = "Maria Silva",
                NumeroMesa = 1,
            },
            new Comanda
            {
                Id = 2,
                NomeCliente = "Eduardo Borges",
                NumeroMesa = 2,
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
                //adiciona o usuario na lista
                comandas.Add(novaComanda);
                return Results.Created($"/api/usuario/{novaComanda.Id}", novaComanda);
            }
        

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
