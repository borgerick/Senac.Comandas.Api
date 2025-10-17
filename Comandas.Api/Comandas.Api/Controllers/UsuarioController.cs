using Comandas.Api.DTOs;
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
        public IResult Post([FromBody] UsuarioCreateRequest usuarioCreate)
        {
            if(usuarioCreate.Senha.Length<6)
                return Results.BadRequest("A senha deve ter no minimo 6 caracteres");
            if (usuarioCreate.Nome.Length < 3)
                return Results.BadRequest("O nome deve ter no minimo 3 caracteres");
            if (usuarioCreate.Email.Length < 5 || !usuarioCreate.Email.Contains("@"));
                return Results.BadRequest("O email deve ser valido.");
            
            var usuario = new Usuario
            {
                Id = usuarios.Count + 1,//gera o id automatico
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha
            };
            //adiciona o usuario na lista
            usuarios.Add(usuario);
            return Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }
        //ATUALIZA UM USUARIO
        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioUpdate)
        {
            // BUSCA O USUARIO NA LISTA PELO ID
            var usuario = usuarios.
                FirstOrDefault(u => u.Id == id);
            // SE O USUARIO NAO FOR ENCONTRADO, RETORNA 404 NOTFOUND
            if (usuario is null)
                return Results.NotFound($"Usuário do id {id} nao encontrado.");
            // ATUALIZA CADASTRO
            usuario.Nome = usuarioUpdate.Nome;
            usuario.Email = usuarioUpdate.Email;
            usuario.Senha = usuarioUpdate.Senha;
            // RETORNA NO CONTENT
            return Results.NotFound();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var usuario = usuarios.
                FirstOrDefault(u => u.Id == id);
            if (usuario is null)
            return Results.NotFound("Usuario nao encontrado");
            var removidoComSucessoUsuario = usuarios.Remove(usuario);
            if (removidoComSucessoUsuario)
            return Results.NoContent();

            return Results.StatusCode(500);

        }
    }
}
