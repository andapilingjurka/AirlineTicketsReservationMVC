using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models.ViewModels;
using AirlineTicketsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AirlineTicketsReservation.Controllers
{
    public class OfertaController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public OfertaController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Index()
        {
            // Projecting only necessary fields, excluding the description
            var ofertat = await applicationDbContext.Ofertat
                .Select(o => new Oferta
                {
                    Id = o.Id,
                    Hoteli = o.Hoteli,
                    Fluturimi = o.Fluturimi,
                    Cmimi = o.Cmimi,
                    // Add other fields as needed
                })
                .ToListAsync();

            return View(ofertat);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            var hoteliList = applicationDbContext.Hoteli
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Emri })
                .ToList();

            var fluturimiList = applicationDbContext.Fluturimet
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.NrFluturimit })
                .ToList();
            var model = new AddOfertaRequest
            {
                Hoteli = hoteliList,
                Fluturimi = fluturimiList
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddOfertaRequest addOfertaRequest)
        {

            var hoteli = await applicationDbContext.Hoteli.FindAsync(addOfertaRequest.HoteliId);
            var fluturimi = await applicationDbContext.Fluturimet.FindAsync(addOfertaRequest.FluturimiId);

            if (hoteli == null)
            {
                return NotFound();
            }
            if (fluturimi == null)
            {
                return NotFound();
            }

            var oferta = new Oferta
            {


                Cmimi = addOfertaRequest.Cmimi,
                Description = addOfertaRequest.Description,
                HoteliId = addOfertaRequest.HoteliId,
                FluturimiId = addOfertaRequest.FluturimiId,

            };

            await applicationDbContext.Ofertat.AddAsync(oferta);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> ListAsync(int? page)
        {
            // Ensure applicationDbContext is not null
            if (applicationDbContext == null)
            {
                return NotFound(); // Or handle the case where the context is null
            }

            const int pageSize = 8; // Set the number of items per page

            var ofertat = await applicationDbContext.Ofertat
             .Include(o => o.Hoteli)       // Include Hoteli navigation property
             .Include(o => o.Fluturimi)    // Include Fluturimi navigation property
             .AsNoTracking()
             .ToListAsync();

            var paginatedList = PaginatedList<Oferta>.Create(ofertat, page ?? 1, pageSize);

            return View(paginatedList);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var oferta = await applicationDbContext.Ofertat
                .Include(f => f.Hoteli)
                .Include(f => f.Fluturimi)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (oferta != null)
            {
                var hoteliList = applicationDbContext.Hoteli
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Emri })
                    .ToList();

                var fluturimiList = applicationDbContext.Fluturimet
                   .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.NrFluturimit })
                   .ToList();

                var editOfertaRequest = new EditOfertaRequest
                {
                    Id = oferta.Id,

                    Cmimi = oferta.Cmimi,
                    Description = oferta.Description,
                    HoteliId = oferta.HoteliId,
                    Hoteli = hoteliList.Any() ? new SelectList(hoteliList, "Value", "Text") : null,
                    FluturimiId = oferta.FluturimiId,
                    Fluturimi = fluturimiList.Any() ? new SelectList(fluturimiList, "Value", "Text") : null
                };

                return View(editOfertaRequest);
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Edit(EditOfertaRequest editOfertaRequest)
        {
            if (ModelState.IsValid)
            {
                var existingOferta = await applicationDbContext.Ofertat
                    .Include(f => f.Hoteli)
                    .Include(f => f.Fluturimi)// Include the Aeroplani navigation property
                    .FirstOrDefaultAsync(x => x.Id == editOfertaRequest.Id);

                if (existingOferta != null)
                {

                    existingOferta.Cmimi = editOfertaRequest.Cmimi;
                    existingOferta.Description = editOfertaRequest.Description;
                    existingOferta.HoteliId = editOfertaRequest.HoteliId;

                    // Assuming Aeroplani is a navigation property in Fluturimi
                    existingOferta.Hoteli = await applicationDbContext.Hoteli
                        .FirstOrDefaultAsync(a => a.Id == editOfertaRequest.HoteliId);

                    existingOferta.FluturimiId = editOfertaRequest.FluturimiId;
                    existingOferta.Fluturimi = await applicationDbContext.Fluturimet
                       .FirstOrDefaultAsync(a => a.Id == editOfertaRequest.FluturimiId);

                    await applicationDbContext.SaveChangesAsync();

                    return RedirectToAction("Edit", new { id = editOfertaRequest.Id });
                }
                else
                {
                    return RedirectToAction("List");
                    // Show fail notification
                }
            }

            // If ModelState is not valid, return to the edit view with validation errors
            var hoteliList = applicationDbContext.Hoteli
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Emri })
                .ToList();
            var fluturimiList = applicationDbContext.Fluturimet
            .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.NrFluturimit })
            .ToList();

            editOfertaRequest.Hoteli = new SelectList(hoteliList, "Id", "Emri", editOfertaRequest.HoteliId);
            editOfertaRequest.Fluturimi = new SelectList(fluturimiList, "Id", "NrFluturimit", editOfertaRequest.FluturimiId);

            return View(editOfertaRequest);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(EditOfertaRequest editOfertaRequest)
        {
            var existingOferta = await applicationDbContext.Ofertat
                    .Include(f => f.Hoteli)
                     .Include(f => f.Fluturimi) // Include the Aeroplani navigation property
                    .FirstOrDefaultAsync(x => x.Id == editOfertaRequest.Id);


            if (existingOferta != null)
            {
                // show success notification
                applicationDbContext.Ofertat.Remove(existingOferta);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }

            // show error notification or handle accordingly
            return NotFound();
        }
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var oferta = await applicationDbContext.Ofertat
                .Include(o => o.Hoteli)
                .Include(o => o.Fluturimi)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (oferta == null)
            {
                return NotFound();
            }

            return View(oferta);
        }
        [HttpGet]
        public IActionResult GetCmimi(int ofertaId)
        {
            // Fetch the Cmimi for the given ofertaId from your data source
            var cmimi = applicationDbContext.Ofertat.Where(o => o.Id == ofertaId).Select(o => o.Cmimi).FirstOrDefault();

            return Json(cmimi);
        }
    }
}
