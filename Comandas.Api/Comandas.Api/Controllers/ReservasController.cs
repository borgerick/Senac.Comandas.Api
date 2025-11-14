using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comandas.Api;
using Comandas.Api.Models;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ComandasDbContext _context;

        public ReservasController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _context.Reservas.ToListAsync();
        }

        // GET: api/Reservas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // PUT: api/Reservas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest();
            }
            _context.Entry(reserva).State = EntityState.Modified;

            // -----------
            var novaMesa = await _context.Mesas
                    .FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            if (novaMesa is null)
                return BadRequest("Mesa não encontrada.");
            novaMesa.SituacaoMesa = (int)SituacaoMesa.Reservada;

            // consulta dados da reserva original
            var reservaOriginal = await _context.Reservas.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            // consulta numero da mesa original
            var numeroMesaOriginal = reservaOriginal!.NumeroMesa;
            // consulta a mesa original
            var mesaOriginal = await _context.Mesas
                    .FirstOrDefaultAsync(m => m.NumeroMesa == numeroMesaOriginal);
            mesaOriginal!.SituacaoMesa = (int)SituacaoMesa.Livre;
            // -----------

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reservas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            _context.Reservas.Add(reserva);

            //atualizar o status da mesa para reservada
            //Consultando a mesa pelo numero
            var mesa = await _context.Mesas
                .FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            // se mesa encontrada
            if (mesa is null)
            {
                return BadRequest("Mesa não encontrada.");
            }
            //se a mesa não encontrada
            if(mesa is not null)
            {
                if(mesa.SituacaoMesa != (int)SituacaoMesa.Livre)
                {
                    return BadRequest("Mesa não está disponível para reserva.");
                }
                mesa.SituacaoMesa = (int)SituacaoMesa.Reservada;                
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserva", new { id = reserva.Id }, reserva);
        }

        // DELETE: api/Reservas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound("Reserva não encontrada");
            }
            // ---------------------
            //consultar a mesa
            var mesa = await _context.Mesas
                .FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            if (mesa is null)
            {
                return BadRequest("Mesa não encontrada.");
            }
            //atualizar o status da mesa para livre
            mesa.SituacaoMesa = (int)SituacaoMesa.Livre; // (int) converte o enum para int
            //----------------------
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}