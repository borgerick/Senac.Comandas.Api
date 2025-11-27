using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comandas.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MesaController : ControllerBase
{
    private readonly ComandasDbContext _context;

    public MesaController(ComandasDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IResult Get()
    {
        return Results.Ok(_context.Mesas.ToList());
    }


    [HttpGet("{id}")]
    public IResult Get(int id)
    {
        var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
        if (mesa is null)
        {
            return Results.NotFound("Mesa não encontrada!");
        }
        return Results.Ok(mesa);
    }

    // POST api/<MesaController>
    [HttpPost]
    public IResult Post([FromBody] MesaCreateRequest mesaCreate)
    {
        // valida se o numero da mesa é maior que zero
        if (mesaCreate.NumeroMesa <= 0)
            return Results.BadRequest("O número da mesa deve ser maior que zero.");
        // cria uma nova mesa
        var novaMesa = new Mesa
        {
            NumeroMesa = mesaCreate.NumeroMesa,
            SituacaoMesa = (int)SituacaoMesa.Livre
        };
        // adiciona a nova mesa na lista
        _context.Mesas.Add(novaMesa);
        // salva a mesa no banco de dados
        _context.SaveChanges();
        // retorna a nova mesa criada e o codigo 201 CREATED
        return Results.Created($"/api/mesa/{novaMesa.Id}", novaMesa);
    }

    // PUT api/<MesaController>/5
    [HttpPut("{id}")]
    public IResult Put(int id, [FromBody] MesaUpdateRequest mesaUpdate)
    {
        if (mesaUpdate.NumeroMesa <= 0)
            return Results.BadRequest("O número da mesa deve ser maior que zero.");
        if (mesaUpdate.SituacaoMesa < 0 || mesaUpdate.SituacaoMesa > 2)
            return Results.BadRequest("A situação da mesa deve ser 0 (Livre), " +
                "1 (Ocupada) ou 2 (Reservada).");

        var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
        if (mesa is null)
            return Results.NotFound($"Mesa {id} não encontrada!");
        mesa.NumeroMesa = mesaUpdate.NumeroMesa;
        mesa.SituacaoMesa = mesaUpdate.SituacaoMesa;
        _context.SaveChanges();
        return Results.NoContent();
    }

    // DELETE api/<MesaController>/5
    [HttpDelete("{id}")]
    public IResult Delete(int id)
    {
        var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
        if (mesa is null)
            return Results.NotFound($"Mesa {id} não encontrada!");
        _context.Mesas.Remove(mesa);
        var removidoComSucesso = _context.SaveChanges();
        if (removidoComSucesso > 0)
            return Results.NoContent();
        return Results.StatusCode(500);
    }
}