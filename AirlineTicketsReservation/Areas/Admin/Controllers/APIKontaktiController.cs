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
    public class APIKontaktiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIKontaktiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIKontakti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kontakti>>> GetKontakti()
        {
            return await _context.Kontakti.ToListAsync();
        }

        // GET: api/APIKontakti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kontakti>> GetKontakti(int id)
        {
            var kontakti = await _context.Kontakti.FindAsync(id);

            if (kontakti == null)
            {
                return NotFound();
            }

            return kontakti;
        }

        // PUT: api/APIKontakti/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKontakti(int id, Kontakti kontakti)
        {
            if (id != kontakti.KontaktiID)
            {
                return BadRequest();
            }

            _context.Entry(kontakti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KontaktiExists(id))
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

        // POST: api/APIKontakti
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kontakti>> PostKontakti(Kontakti kontakti)
        {
            _context.Kontakti.Add(kontakti);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKontakti", new { id = kontakti.KontaktiID }, kontakti);
        }

        // DELETE: api/APIKontakti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKontakti(int id)
        {
            var kontakti = await _context.Kontakti.FindAsync(id);
            if (kontakti == null)
            {
                return NotFound();
            }

            _context.Kontakti.Remove(kontakti);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KontaktiExists(int id)
        {
            return _context.Kontakti.Any(e => e.KontaktiID == id);
        }
    }
}
