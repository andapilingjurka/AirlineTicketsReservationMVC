using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models.ViewModels;
using AirlineTicketsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AirlineTicketsReservation.Controllers
{
    public class KontaktiController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public KontaktiController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

     
        //Metoda GET Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        //Metoda POST Add
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddKontaktiRequest addKontaktiRequest)
        {
            var kontakti = new Kontakti
            {
                Emri = addKontaktiRequest.Emri,
                Mbiemri = addKontaktiRequest.Mbiemri,
                Email = addKontaktiRequest.Email,
                Telefoni = addKontaktiRequest.Telefoni,
                Mesazhi = addKontaktiRequest.Mesazhi,

            };
            await applicationDbContext.Kontakti.AddAsync(kontakti);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        [Authorize(Roles = "Admin")]
        //Metoda GET List
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(bool prefix049, bool prefix044, bool prefix043, bool prefix045, int? page)
        {
            // Ensure applicationDbContext is not null
            if (applicationDbContext == null)
            {
                return NotFound();
            }

            const int pageSize = 8;

            // Get all contacts
            var kontaktiQuery = applicationDbContext.Kontakti.AsQueryable();

            // Apply phone number prefix filter if selected
            if (prefix049)
            {
                kontaktiQuery = kontaktiQuery.Where(k => k.Telefoni.StartsWith("049"));
            }
            if (prefix044)
            {
                kontaktiQuery = kontaktiQuery.Where(k => k.Telefoni.StartsWith("044"));
            }
            if (prefix043)
            {
                kontaktiQuery = kontaktiQuery.Where(k => k.Telefoni.StartsWith("043"));
            }
            if (prefix045)
            {
                kontaktiQuery = kontaktiQuery.Where(k => k.Telefoni.StartsWith("045"));
            }

            var kontakti = await kontaktiQuery.AsNoTracking().ToListAsync();

            var paginatedList = PaginatedList<Kontakti>.Create(kontakti, page ?? 1, pageSize);

            return View(paginatedList);
        }


        [Authorize(Roles = "Admin")]
        //Metoda POST Delete
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteKontaktiRequest deleteKontaktiRequest)
        {
            var kontakti = await applicationDbContext.Kontakti.FindAsync(deleteKontaktiRequest.KontaktiID);

            if (kontakti != null)
            {
                //show success notification
                applicationDbContext.Kontakti.Remove(kontakti);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("List");


            }
            //Show error notification
            return RedirectToAction("Delete", new { id = deleteKontaktiRequest.KontaktiID });

        }

        [Authorize(Roles = "Admin")]
        // Metoda GET Delete
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int KontaktiID)
        {
            var kontakti = await applicationDbContext.Kontakti.FindAsync(KontaktiID);

            if (kontakti == null)
            {
                return NotFound();
            }

            var deleteKontaktiRequest = new DeleteKontaktiRequest
            {
                KontaktiID = kontakti.KontaktiID,
                Emri = kontakti.Emri,
                Mbiemri = kontakti.Mbiemri,
                Email = kontakti.Email,
                Telefoni = kontakti.Telefoni,
                Mesazhi = kontakti.Mesazhi,
   
            };

            return View(deleteKontaktiRequest);
        }




    }
}
