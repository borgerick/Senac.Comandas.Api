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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
