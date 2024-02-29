using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models;

namespace AirlineTicketsReservation.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIVeturaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIVeturaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIVetura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vetura>>> GetVetura()
        {
            return await _context.Vetura.ToListAsync();
        }

        // GET: api/APIVetura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vetura>> GetVetura(int id)
        {
            var vetura = await _context.Vetura.FindAsync(id);

            if (vetura == null)
            {
                return NotFound();
            }

            return vetura;
        }

        // PUT: api/APIVetura/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVetura(int id, Vetura vetura)
        {
            if (id != vetura.VeturaID)
            {
                return BadRequest();
            }

            _context.Entry(vetura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeturaExists(id))
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

        // POST: api/APIVetura
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vetura>> PostVetura(Vetura vetura)
        {
            _context.Vetura.Add(vetura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVetura", new { id = vetura.VeturaID }, vetura);
        }

        // DELETE: api/APIVetura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVetura(int id)
        {
            var vetura = await _context.Vetura.FindAsync(id);
            if (vetura == null)
            {
                return NotFound();
            }

            _context.Vetura.Remove(vetura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VeturaExists(int id)
        {
            return _context.Vetura.Any(e => e.VeturaID == id);
        }
    }
}
