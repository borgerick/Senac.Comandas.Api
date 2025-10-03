using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //Lista de usuario
        static List<Usuario> usuarios = new List<Usuario>() { 
        new Usuario
        {
            Id = 1,
            Nome = "Juan Perez",
            Email = "admin@admin.com.br",
            Senha= "admin"
        },
        new Usuario
        {
            Id = 2,
            Nome = "Usuario",
            Email = "usuario@usuario.com.br",
            Senha= "usuario"
        }
};
        // GET: api/<UsuarioController>
        [HttpGet]
        
        public IResult Get()//Esse get retorna todos os usuarios
        {
            return Results.Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        
        public IResult Get(int id)//Esse get retorna um usuario pelo id
        {
            var usuario = usuarios.
                FirstOrDefault(u => u.Id == id);
            if (usuario is null)
                return Results.NotFound("Usuario nao encontrado");
            return Results.Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IResult Post([FromBody] Usuario usuario)
        {
            //adiciona o usuario na lista
            usuarios.Add(usuario);
            return Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
