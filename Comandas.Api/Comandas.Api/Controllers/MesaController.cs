using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        List<Mesa> mesas = new List<Mesa>(){
        new Mesa
        {
                Id = 1,
                NumeroMesa = "1",
                SituacaoMesa = true,
        },
        new Mesa
        {
                Id = 1,
                NumeroMesa = "1",
                SituacaoMesa = true,
        },
    };
        // GET: api/<MesaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(mesas);
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var mesa = mesas.
                FirstOrDefault(u => u.Id == id);
            if (mesa is null)
                return Results.NotFound("Mesa nao encontrada");
            return Results.Ok(mesa);
        }

        // POST api/<MesaController>
        [HttpPost]
        public IResult Post([FromBody] MesaCreateRequest mesaCreate)
        {
            if (mesaCreate.NumeroMesa.Length < 1)
                Results.BadRequest("O numero da mesa deve ter no minimo 1 caracter");
            var mesa = new Mesa
            {
                Id = mesas.Count + 1,//gera o id automatico
                NumeroMesa = mesaCreate.NumeroMesa,
                SituacaoMesa = mesaCreate.SituacaoMesa
            };
            //adiciona o usuario na lista
            mesas.Add(mesa);
            return Results.Created($"/api/mesa/{mesa.Id}", mesa);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaUpdateRequest mesaUpdate)
        {
            if(mesaUpdate.NumeroMesa.Length < 0)
                return Results.BadRequest($"O numero da mesa{id} é invalido!");
            
            var mesa = mesas.FirstOrDefault(u => u.Id == id);
            if (mesa is null)
                return Results.NotFound($"Mesa {id} não encontrada!");
            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.SituacaoMesa = mesaUpdate.SituacaoMesa;
            return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var mesa = mesas.
                FirstOrDefault(u => u.Id == id);
            if (mesa is null)
                return Results.NotFound($"Mesa {id} não encontrada!");
            var removidoComSucessoMesa =  mesas.Remove(mesa);
            if(removidoComSucessoMesa)
            return Results.NoContent();

            return Results.StatusCode(500);
        }
    }
}
