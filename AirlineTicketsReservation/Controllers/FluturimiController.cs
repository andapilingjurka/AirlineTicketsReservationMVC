using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models.ViewModels;
using AirlineTicketsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AirlineTicketsReservation.Controllers
{
    public class FluturimiController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public FluturimiController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fluturimet = await applicationDbContext.Fluturimet
                .Include(f => f.Aeroplani)
                .Include(f => f.Qyteti)
                .ToListAsync();

            return View(fluturimet);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            var aeroplaniList = applicationDbContext.Aeroplanet
       .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Kompania })
       .ToList();

            var qytetiList = applicationDbContext.Qyteti
                .Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Emri })
                .ToList();

            var model = new AddFluturimiRequest
            {
                Aeroplani = aeroplaniList,
                Qyteti = qytetiList
            };


            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddFluturimiRequest addFluturimiRequest)
        {

            var aeroplani = await applicationDbContext.Aeroplanet.FindAsync(addFluturimiRequest.AeroplaniId);

            if (aeroplani == null)
            {
                return NotFound();
            }

            var fluturimi = new Fluturimi
            {
                NrFluturimit = addFluturimiRequest.NrFluturimit,
                DeparuteAirport = addFluturimiRequest.DeparuteAirport,
                ArrivalAirport = addFluturimiRequest.ArrivalAirport,
                KohaENisjes = addFluturimiRequest.KohaENisjes,
                KohaEArritjes = addFluturimiRequest.KohaEArritjes,
                Cmimi = addFluturimiRequest.Cmimi,
                AeroplaniId = addFluturimiRequest.AeroplaniId,
                QytetiId = addFluturimiRequest.QytetiId

            };

            await applicationDbContext.Fluturimet.AddAsync(fluturimi);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> ListAsync(string arrivalAirportFilter, int? page)
        {
            // Ensure applicationDbContext is not null
            if (applicationDbContext == null)
            {
                return NotFound(); // Or handle the case where the context is null
            }

            const int pageSize = 8; // Set the number of items per page

            // Retrieve all flights with eager loading of the Aeroplani and Qyteti navigation properties
            var query = applicationDbContext.Fluturimet
                .Include(f => f.Aeroplani)
                .Include(f => f.Qyteti)
                .AsQueryable();

            // Apply the filter based on the arrival airport if provided
            if (!string.IsNullOrEmpty(arrivalAirportFilter))
            {
                query = query.Where(f => f.ArrivalAirport.Contains(arrivalAirportFilter));
            }

            // Execute the query and convert it to a list asynchronously
            var fluturimet = await query.AsNoTracking().ToListAsync();

            // Create a paginated list
            var paginatedList = PaginatedList<Fluturimi>.Create(fluturimet, page ?? 1, pageSize);

            // Pass the filter value to the view
            ViewBag.ArrivalAirportFilter = arrivalAirportFilter;

            return View(paginatedList);
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var fluturimi = await applicationDbContext.Fluturimet
       .Include(f => f.Aeroplani)
       .Include(f => f.Qyteti) // Include Qyteti
       .FirstOrDefaultAsync(x => x.Id == id);

            if (fluturimi != null)
            {
                var aeroplaniList = applicationDbContext.Aeroplanet
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Kompania })
                    .ToList();

                var qytetiList = applicationDbContext.Qyteti
                    .Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Emri })
                    .ToList();

                var editFluturimiRequest = new EditFluturimiRequest
                {
                    Id = fluturimi.Id,
                    NrFluturimit = fluturimi.NrFluturimit,
                    DeparuteAirport = fluturimi.DeparuteAirport,
                    ArrivalAirport = fluturimi.ArrivalAirport,
                    KohaENisjes = fluturimi.KohaENisjes,
                    KohaEArritjes = fluturimi.KohaEArritjes,
                    Cmimi = fluturimi.Cmimi,
                    AeroplaniId = fluturimi.AeroplaniId,
                    QytetiId=fluturimi.QytetiId,
                    Aeroplani = aeroplaniList.Any() ? new SelectList(aeroplaniList, "Value", "Text") : null,
                    Qyteti = qytetiList.Any() ? new SelectList(qytetiList, "Value", "Text") : null
                };

                return View(editFluturimiRequest);
            }

            return NotFound();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditFluturimiRequest editFluturimiRequest)
        {
            if (ModelState.IsValid)
            {
                var existingFluturimi = await applicationDbContext.Fluturimet
                    .Include(f => f.Aeroplani) // Include the Aeroplani navigation property
                    .FirstOrDefaultAsync(x => x.Id == editFluturimiRequest.Id);

                if (existingFluturimi != null)
                {
                    existingFluturimi.NrFluturimit = editFluturimiRequest.NrFluturimit;
                    existingFluturimi.DeparuteAirport = editFluturimiRequest.DeparuteAirport;
                    existingFluturimi.ArrivalAirport = editFluturimiRequest.ArrivalAirport;
                    existingFluturimi.KohaENisjes = editFluturimiRequest.KohaENisjes;
                    existingFluturimi.KohaEArritjes = editFluturimiRequest.KohaEArritjes;
                    existingFluturimi.Cmimi = editFluturimiRequest.Cmimi;
                    existingFluturimi.AeroplaniId = editFluturimiRequest.AeroplaniId;
                    existingFluturimi.QytetiId = editFluturimiRequest.QytetiId;

                    // Assuming Aeroplani is a navigation property in Fluturimi
                    existingFluturimi.Aeroplani = await applicationDbContext.Aeroplanet
                        .FirstOrDefaultAsync(a => a.Id == editFluturimiRequest.AeroplaniId);
                    existingFluturimi.Qyteti = await applicationDbContext.Qyteti
                    .FirstOrDefaultAsync(q => q.Id == editFluturimiRequest.QytetiId);

                    await applicationDbContext.SaveChangesAsync();

                    return RedirectToAction("Edit", new { id = editFluturimiRequest.Id });
                }
                else
                {
                    return RedirectToAction("List");
                    // Show fail notification
                }
            }

            // If ModelState is not valid, return to the edit view with validation errors
            var aeroplaniList = applicationDbContext.Aeroplanet
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Kompania })
                .ToList();
            var qytetiList = applicationDbContext.Qyteti
            .Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Emri })
            .ToList();
            editFluturimiRequest.Aeroplani = new SelectList(aeroplaniList, "Id", "Kompania", editFluturimiRequest.AeroplaniId);
            editFluturimiRequest.Qyteti = new SelectList(qytetiList, "Id", "Emri", editFluturimiRequest.QytetiId);

            return View(editFluturimiRequest);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var fluturimi = await applicationDbContext.Fluturimet
            .Include(f => f.Aeroplani)
            .Include(f => f.Qyteti)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (fluturimi != null)
            {
                var aeroplaniList = applicationDbContext.Aeroplanet
               .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Kompania })
               .ToList();

                var qytetiList = applicationDbContext.Qyteti
                    .Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Emri })
                    .ToList();

                var editFluturimiRequest = new EditFluturimiRequest
                {
                    Id = fluturimi.Id,
                    NrFluturimit = fluturimi.NrFluturimit,
                    DeparuteAirport = fluturimi.DeparuteAirport,
                    ArrivalAirport = fluturimi.ArrivalAirport,
                    KohaENisjes = fluturimi.KohaENisjes,
                    KohaEArritjes = fluturimi.KohaEArritjes,
                    Cmimi = fluturimi.Cmimi,
                    AeroplaniId = fluturimi.AeroplaniId,
                    QytetiId = fluturimi.QytetiId,
                    Aeroplani = aeroplaniList.Any() ? new SelectList(aeroplaniList, "Value", "Text") : null,
                    Qyteti = qytetiList.Any() ? new SelectList(qytetiList, "Value", "Text") : null
                };

                return View(editFluturimiRequest);
            }

            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(EditFluturimiRequest editFluturimiRequest)
        {
            var existingFluturimi = await applicationDbContext.Fluturimet
                    .Include(f => f.Aeroplani)
                    .Include(f => f.Qyteti)
                    .FirstOrDefaultAsync(x => x.Id == editFluturimiRequest.Id);

            if (existingFluturimi != null)
            {
                // show success notification
                applicationDbContext.Fluturimet.Remove(existingFluturimi);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }

            // show error notification or handle accordingly
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetCmimi(int fluturimiId)
        {
            var fluturimi = await applicationDbContext.Fluturimet.FindAsync(fluturimiId);

            if (fluturimi == null)
            {
                return NotFound();
            }

            // Assuming 'Cmimi' is the property representing the flight price
            var cmimi = fluturimi.Cmimi;

            return Json(new { cmimi });
        }
    }
}
