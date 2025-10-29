﻿using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        public ComandasDbContext _context;
        public ComandaController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: api/<ComandaController>
        [HttpGet]
        public IResult Get()//Esse get retorna todos os usuarios
        {
            var comandas = _context.Comandas.ToList();
            return Results.Ok(comandas);
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)//Esse get retorna um usuario pelo id
        {
            var comanda = _context.Comandas.
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
                    CardapioItemId = cardapioItemId,
                    Comanda = novaComanda,
                };
                //adiciona o item na lista de itens da comanda
                comandaItens.Add(comandaItem);

                // Criar o pedido de cozinha e o item de acordo com cadastro do cardapio possuipreparo
                var cardapioItem = _context.CardapioItems.FirstOrDefault(c => c.Id == cardapioItemId);
                
            }
            //atribui os itens a nova comanda
            novaComanda.Itens = comandaItens;
            //adiciona o usuario na lista
            _context.Comandas.Add(novaComanda);
            return Results.Created($"/api/usuario/{novaComanda.Id}", novaComanda);
            }
        

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {
            //pesquisa uma comanda na lista de comandas pelo id da comanda que veio no parametro de registro
            var comanda = _context.Comandas.FirstOrDefault(u => u.Id == id);
            if (comanda is null) // se não encontrou a comanda pesquisada
                return Results.NotFound("Comanda nao encontrada");
            // valida os dados da comanda
            if (comandaUpdate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome deve ter no minimo 3 caracteres");
            // valida os numero da mesa
            if (comandaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O numero da mesa deve ser maior que zero");
            // atualiza as infomações da comanda
            comanda.NomeCliente = comandaUpdate.NomeCliente;
            comanda.NumeroMesa = comandaUpdate.NumeroMesa;
            // retorna 204 sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var comanda = _context.Comandas.FirstOrDefault(u => u.Id == id);
            if (comanda is null)
                return Results.NotFound("Comanda nao encontrada");
            
            var removidoComSucessoComanda = _context.Comandas.Remove(comanda);
            
            _context.Comandas.Remove(comanda);
            var removido = _context.SaveChanges();
            if (removido > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}
