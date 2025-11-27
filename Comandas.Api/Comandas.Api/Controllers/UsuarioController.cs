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
        // variavel que representa o banco de dados
        public ComandasDbContext _context { get; set; }
        // construtor
        public UsuarioController(ComandasDbContext context)
        {
            _context = context;
        }

        // iresult  que retorna a lista de usuarios
        // GET: api/<UsuarioController>
        [HttpGet]
        public IResult Get()
        {   // conectar no banco
            // executar a consulta SELECT * FROM usuarios
            var usuarios = _context.Usuarios.ToList();
            return Results.Ok(usuarios);
        }
        // iresult  que retorna um usuario pelo id
        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var usuario = _context.Usuarios.
                    FirstOrDefault(u => u.Id == id);
            if (usuario is null)
                return Results.NotFound("Usuário não encontrado!");

            return Results.Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IResult Post([FromBody] UsuarioCreateRequest usuarioCreate)
        {
            if (usuarioCreate.Senha.Length < 6)
                return Results.BadRequest("A senha deve ter no mínimo 6 caracteres.");
            if (usuarioCreate.Nome.Length < 3)
                return Results.BadRequest("O nome deve ter no mínimo 3 caracteres.");
            if (usuarioCreate.Email.Length < 5 || !usuarioCreate.Email.Contains("@"))
                return Results.BadRequest("O email deve ser válido.");
            var usuario = new Usuario
            {
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha
            };

            // adiciona o usuario no Contexto do banco de dados
            _context.Usuarios.Add(usuario);

            // EXECUTA o INSERT INTO Usuarios (Id, Nome, Email,SEnha) VALUES(...)
            _context.SaveChanges();

            return Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }

        // PUT api/<UsuarioController>/5
        /// <summary>
        /// ATUALIZA UM USUARIO
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="usuarioUpdate">Dados do usuario </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioUpdate)
        {
            // busca o usuario na lista pelo id
            var usuario = _context.Usuarios.
                    FirstOrDefault(u => u.Id == id);
            // se nao encontrar retorna notfound
            if (usuario is null)
                return Results.NotFound($"Usuário do id {id} nao encontrado.");
            // atualiza o usuario
            usuario.Nome = usuarioUpdate.Nome;
            usuario.Email = usuarioUpdate.Email;
            usuario.Senha = usuarioUpdate.Senha;
            // update USUarios set Nome=..., Email=..., Senha=... where Id=...
            _context.SaveChanges();
            //retorna no content
            return Results.NoContent();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
                return Results.NotFound($"Usuário do id {id} nao encontrado.");

            _context.Usuarios.Remove(usuario);
            var removido = _context.SaveChanges();
            if (removido > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}