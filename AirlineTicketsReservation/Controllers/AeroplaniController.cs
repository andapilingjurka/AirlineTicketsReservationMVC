using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models.ViewModels;
using AirlineTicketsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AirlineTicketsReservation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AeroplaniController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AeroplaniController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddAeroplaniRequest addQytetiRequest)
        {
            var aeroplani = new Aeroplani
            {
                Nr_Uleseve_VIP = addQytetiRequest.Nr_Uleseve_VIP,
                Nr_Uleseve_Biznes = addQytetiRequest.Nr_Uleseve_Biznes,
                Nr_Uleseve_Ekonomike = addQytetiRequest.Nr_Uleseve_Ekonomike,
                Kompania = addQytetiRequest.Kompania,
            };
            await applicationDbContext.Aeroplanet.AddAsync(aeroplani);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }


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

            var aeroplanes = await applicationDbContext.Aeroplanet.AsNoTracking().ToListAsync();

            var paginatedList = PaginatedList<Aeroplani>.Create(aeroplanes, page ?? 1, pageSize);

            return View(paginatedList);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var aeroplani = await applicationDbContext.Aeroplanet.FirstOrDefaultAsync(x => x.Id == id);

            if (aeroplani != null)
            {
                var editAeroplaniRequest = new EditAeroplaniRequest
                {
                    Id = aeroplani.Id,
                    Nr_Uleseve_VIP = aeroplani.Nr_Uleseve_VIP,
                    Nr_Uleseve_Biznes = aeroplani.Nr_Uleseve_Biznes,
                    Nr_Uleseve_Ekonomike = aeroplani.Nr_Uleseve_Ekonomike,
                    Kompania = aeroplani.Kompania,

                };
                return View(editAeroplaniRequest);
            }

            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAeroplaniRequest editAeroplaniRequest)
        {
            var aeroplani = new Aeroplani
            {
                Id = editAeroplaniRequest.Id,
                Nr_Uleseve_VIP = editAeroplaniRequest.Nr_Uleseve_VIP,
                Nr_Uleseve_Biznes = editAeroplaniRequest.Nr_Uleseve_Biznes,
                Nr_Uleseve_Ekonomike = editAeroplaniRequest.Nr_Uleseve_Ekonomike,
                Kompania = editAeroplaniRequest.Kompania,
            };
            var exisingQyteti = await applicationDbContext.Aeroplanet.FindAsync(aeroplani.Id);
            if (exisingQyteti != null)
            {
                exisingQyteti.Nr_Uleseve_VIP = aeroplani.Nr_Uleseve_VIP;
                exisingQyteti.Nr_Uleseve_Biznes = aeroplani.Nr_Uleseve_Biznes;
                exisingQyteti.Nr_Uleseve_Ekonomike = aeroplani.Nr_Uleseve_Ekonomike;
                exisingQyteti.Kompania = aeroplani.Kompania;
                await applicationDbContext.SaveChangesAsync();


                return RedirectToAction("Edit", new { id = editAeroplaniRequest.Id });
            }
            else
            {
                return RedirectToAction("List");
                //Show fail notification

            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditAeroplaniRequest editQytetiRequest)
        {
            var qyteti = await applicationDbContext.Aeroplanet.FindAsync(editQytetiRequest.Id);

            if (qyteti != null)
            {
                //show success notification
                applicationDbContext.Aeroplanet.Remove(qyteti);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("List");


            }
            //Show error notification
            return RedirectToAction("Edit", new { id = editQytetiRequest.Id });

        }
    }
}
