using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models;
using AirlineTicketsReservation.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace AirlineTicketsReservation.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AeroportiController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AeroportiController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        //Metoda Add GET

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //Metoda Add POST
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddAeroportiRequest addAeroportiRequest)
        {
            //Mapping the AddTagRequest to the Tag domain model
            var aeroporti = new Aeroporti
            {
                Emri = addAeroportiRequest.Emri,
                QytetiID = addAeroportiRequest.QytetiID
            };
            await applicationDbContext.Aeroporti.AddAsync(aeroporti);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        //Metoda List GET
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> ListAsync(int? page)
        {
            // Ensure applicationDbContext is not null
            if (applicationDbContext == null)
            {
                return NotFound(); // Or handle the case where the context is null
            }

            const int pageSize = 10; // Set the number of items per page

            var aeroporti = await applicationDbContext.Aeroporti.AsNoTracking().ToListAsync();

            var paginatedList = PaginatedList<Aeroporti>.Create(aeroporti, page ?? 1, pageSize);

            return View(paginatedList);
        }

        //Metoda Edit GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           
            var aeroporti = await applicationDbContext.Aeroporti.FirstOrDefaultAsync(x => x.AeroportiID == id);

            if (aeroporti != null)
            {
                var editAeroportiRequest = new EditAeroportiRequest
                {
                    AeroportiID = aeroporti.AeroportiID,
                    Emri = aeroporti.Emri,
                    QytetiID = aeroporti.QytetiID
                };
                return View(editAeroportiRequest);
            }

            return View(null);

        }


        //Metoda Edit POST
        [HttpPost]
        public async Task<IActionResult> Edit(EditAeroportiRequest editAeroportiRequest)
        {
            var aeroporti = new Aeroporti
            {
                AeroportiID = editAeroportiRequest.AeroportiID,
                Emri = editAeroportiRequest.Emri,
                QytetiID = editAeroportiRequest.QytetiID
            };
            var exisingAeroporti = await applicationDbContext.Aeroporti.FindAsync(aeroporti.AeroportiID);
            if (exisingAeroporti != null)
            {
                exisingAeroporti.Emri = aeroporti.Emri;
                exisingAeroporti.QytetiID = aeroporti.QytetiID;
                await applicationDbContext.SaveChangesAsync();


                return RedirectToAction("Edit", new { id = editAeroportiRequest.AeroportiID });
            }
            else
            {
                return RedirectToAction("List");
                //Show fail notification

            }
        }
        //Metoda Delete POST
        [HttpPost]
        public async Task<IActionResult> Delete(EditAeroportiRequest editAeroportiRequest)
        {
            var aeroporti = await applicationDbContext.Aeroporti.FindAsync(editAeroportiRequest.AeroportiID);

            if (aeroporti != null)
            {
                //show success notification
                applicationDbContext.Aeroporti.Remove(aeroporti);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("List");


            }
            //Show error notification
            return RedirectToAction("Edit", new { id = editAeroportiRequest.AeroportiID });

        }

    }
}




