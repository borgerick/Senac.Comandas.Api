using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        public ComandasDbContext _context;
        public MesaController(ComandasDbContext context) 
        {
            _context = context;
        }
        
        // GET: api/<MesaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(_context.Mesas.ToList());
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var mesa = _context.Mesas.FirstOrDefault(u => u.Id == id);
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
                NumeroMesa = mesaCreate.NumeroMesa,
                SituacaoMesa = mesaCreate.SituacaoMesa
            };
            //adiciona o usuario na lista
            _context.Mesas.Add(mesa);
            _context.SaveChanges();
            return Results.Created($"/api/mesa/{mesa.Id}", mesa);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaUpdateRequest mesaUpdate)
        {
            if(mesaUpdate.NumeroMesa.Length <= 0)
                return Results.BadRequest("O numero da mesa deve ser maior que zero");
            if(mesaUpdate.SituacaoMesa < 0 || mesaUpdate.SituacaoMesa > 2)
                return Results.BadRequest("A situacao da mesa deve ser 0 (Livre), 1 (Ocupada) ou 2 (Reservada).");

            var mesa = _context.Mesas.FirstOrDefault(u => u.Id == id);
            if (mesa is null)
                return Results.BadRequest($"O numero da mesa{id} é invalido!");

            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.SituacaoMesa = mesaUpdate.SituacaoMesa;
            _context.SaveChanges();
            return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var mesa = _context.Mesas.FirstOrDefault(u => u.Id == id);
            if (mesa is null)
                return Results.NotFound($"Mesa {id} não encontrada!");
            _context.Mesas.Remove(mesa);
            var removidoComSucessoMesa = _context.SaveChanges();
            if(removidoComSucessoMesa > 0)
            return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
}
