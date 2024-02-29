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
    public class APIAeroplaniController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIAeroplaniController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIAeroplani
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aeroplani>>> GetAeroplanet()
        {
            return await _context.Aeroplanet.ToListAsync();
        }

        // GET: api/APIAeroplani/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aeroplani>> GetAeroplani(int id)
        {
            var aeroplani = await _context.Aeroplanet.FindAsync(id);

            if (aeroplani == null)
            {
                return NotFound();
            }

            return aeroplani;
        }

        // PUT: api/APIAeroplani/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAeroplani(int id, Aeroplani aeroplani)
        {
            if (id != aeroplani.Id)
            {
                return BadRequest();
            }

            _context.Entry(aeroplani).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AeroplaniExists(id))
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

        // POST: api/APIAeroplani
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aeroplani>> PostAeroplani(Aeroplani aeroplani)
        {
            _context.Aeroplanet.Add(aeroplani);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAeroplani", new { id = aeroplani.Id }, aeroplani);
        }

        // DELETE: api/APIAeroplani/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAeroplani(int id)
        {
            var aeroplani = await _context.Aeroplanet.FindAsync(id);
            if (aeroplani == null)
            {
                return NotFound();
            }

            _context.Aeroplanet.Remove(aeroplani);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AeroplaniExists(int id)
        {
            return _context.Aeroplanet.Any(e => e.Id == id);
        }
    }
}
