using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models.ViewModels;
using AirlineTicketsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AirlineTicketsReservation.Controllers
{
    public class RezervimiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly StripeController stripeController;


        public StripeController StripeController => stripeController;
        public RezervimiController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            var fluturimiList = _context.Fluturimet
                .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = $"{f.NrFluturimit} - {f.DeparuteAirport} to {f.ArrivalAirport}" })
                .ToList();

            var model = new AddRezervimiRequest
            {
                Fluturimi = fluturimiList
            };

            return View(model);
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddRezervimiRequest addRezervimiRequest)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                // Fetch the selected flight details
                var selectedFlight = await _context.Fluturimet.FindAsync(addRezervimiRequest.FluturimiId);

                if (selectedFlight == null)
                {
                    return NotFound();
                }

                var cmimi = selectedFlight.Cmimi;

               

                switch (addRezervimiRequest.Klasi)
                {
                    case "Economic":
                        
                        cmimi += 0; 
                        break;
                    case "VIP":
                        
                        cmimi += 200; 
                        break;
                    case "Business":
                        
                        cmimi += 100; 
                        break;
                       
                }

             
                if (addRezervimiRequest.Kthyese)
                {
                    
                    cmimi += 100; 
                }

                var rezervimi = new Rezervimi
                {
                    EmriPasagjerit = addRezervimiRequest.EmriPasagjerit,
                    MbiemriPasagjerit = addRezervimiRequest.MbiemriPasagjerit,
                    Email = addRezervimiRequest.Email,
                    Klasi = addRezervimiRequest.Klasi,
                    Cmimi = cmimi,
                    Currency = addRezervimiRequest.Currency,
                    FluturimiId = addRezervimiRequest.FluturimiId,
                    Kthyese = addRezervimiRequest.Kthyese,
                    Data_e_Rezervimit = addRezervimiRequest.Data_e_Rezervimit,
                    Data_e_Kthimit = addRezervimiRequest.Data_e_Kthimit,
                    UserId=userId
                };

                _context.Rezervimet.Add(rezervimi);
                await _context.SaveChangesAsync();
                return RedirectToAction("CreateCheckoutSession", "Stripe", new { shuma = cmimi });
            }

            // If ModelState is not valid, return to the add view with validation errors
            var fluturimiList = _context.Fluturimet
                .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = $"{f.NrFluturimit} - {f.DeparuteAirport} to {f.ArrivalAirport}" })
                .ToList();

            addRezervimiRequest.Fluturimi = new SelectList(fluturimiList, "Id", "Text", addRezervimiRequest.FluturimiId);

            return View(addRezervimiRequest);
        }
        [Authorize(Roles = "User,Admin")]
        //Metoda MyReservations GET
        [HttpGet]
        public async Task<IActionResult> MyReservations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Merrni rezervimet e klientit specifik
            var clientReservations = await _context.Rezervimet
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return View(clientReservations);
        }

        [Authorize(Roles = "Admin")]

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> ListAsync(int? page)
        {
            const int pageSize = 8; // Set the number of items per page

            // Retrieve all reservations with eager loading of the Fluturimi navigation property
            var query = _context.Rezervimet.Include(r => r.Fluturimi).AsQueryable();

            // Execute the query and convert it to a list asynchronously
            var rezervimet = await query.AsNoTracking().ToListAsync();

            // Create a paginated list
            var paginatedList = PaginatedList<Rezervimi>.Create(rezervimet, page ?? 1, pageSize);

            return View(paginatedList);
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var rezervimi = await _context.Rezervimet
                .Include(r => r.Fluturimi)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (rezervimi != null)
            {
                var fluturimiList = _context.Fluturimet
                    .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = $"{f.NrFluturimit} - {f.DeparuteAirport} to {f.ArrivalAirport}" })
                    .ToList();

                var editRezervimiRequest = new EditRezervimiRequest
                {
                    Id = rezervimi.Id,
                    EmriPasagjerit = rezervimi.EmriPasagjerit,
                    MbiemriPasagjerit = rezervimi.MbiemriPasagjerit,
                    Email = rezervimi.Email,
                    Klasi = rezervimi.Klasi,
                    Cmimi = rezervimi.Cmimi,
                    Currency = rezervimi.Currency,
                    SelectedCurrencies = rezervimi.Currency.Split(',').ToList(),
                    FluturimiId = rezervimi.FluturimiId,
                    Kthyese = rezervimi.Kthyese,
                    Data_e_Rezervimit = rezervimi.Data_e_Rezervimit,
                    Data_e_Kthimit = rezervimi.Data_e_Kthimit,
                    Fluturimi = fluturimiList.Any() ? new SelectList(fluturimiList, "Value", "Text") : null
                };

                return View(editRezervimiRequest);
            }

            return NotFound();
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditRezervimiRequest editRezervimiRequest)
        {
            if (ModelState.IsValid)
            {
                var existingRezervimi = await _context.Rezervimet
                    .Include(r => r.Fluturimi)
                    .FirstOrDefaultAsync(x => x.Id == editRezervimiRequest.Id);

                if (existingRezervimi != null)
                {
                    existingRezervimi.EmriPasagjerit = editRezervimiRequest.EmriPasagjerit;
                    existingRezervimi.MbiemriPasagjerit = editRezervimiRequest.MbiemriPasagjerit;
                    existingRezervimi.Email = editRezervimiRequest.Email;
                    existingRezervimi.Klasi = editRezervimiRequest.Klasi;
                    existingRezervimi.Cmimi = editRezervimiRequest.Cmimi;
                    existingRezervimi.Currency = string.Join(",", editRezervimiRequest.SelectedCurrencies);
                    existingRezervimi.FluturimiId = editRezervimiRequest.FluturimiId;
                    existingRezervimi.Kthyese = editRezervimiRequest.Kthyese;
                    existingRezervimi.Data_e_Rezervimit = editRezervimiRequest.Data_e_Rezervimit;
                    existingRezervimi.Data_e_Kthimit = editRezervimiRequest.Data_e_Kthimit;

                    // Assuming Fluturimi is a navigation property in Rezervimi
                    existingRezervimi.Fluturimi = await _context.Fluturimet
                        .FirstOrDefaultAsync(f => f.Id == editRezervimiRequest.FluturimiId);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Edit", new { id = editRezervimiRequest.Id });
                }
                else
                {
                    return RedirectToAction("List");
                    // Show fail notification
                }
            }

            // If ModelState is not valid, return to the edit view with validation errors
            var fluturimiList = _context.Fluturimet
                .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = $"{f.NrFluturimit} - {f.DeparuteAirport} to {f.ArrivalAirport}" })
                .ToList();

            editRezervimiRequest.Fluturimi = new SelectList(fluturimiList, "Id", "Text", editRezervimiRequest.FluturimiId);

            return View(editRezervimiRequest);
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var rezervimi = await _context.Rezervimet
                .Include(r => r.Fluturimi)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (rezervimi != null)
            {
                var fluturimiList = _context.Fluturimet
                    .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = $"{f.NrFluturimit} - {f.DeparuteAirport} to {f.ArrivalAirport}" })
                    .ToList();

                var editRezervimiRequest = new EditRezervimiRequest
                {
                    Id = rezervimi.Id,
                    EmriPasagjerit = rezervimi.EmriPasagjerit,
                    MbiemriPasagjerit = rezervimi.MbiemriPasagjerit,
                    Email = rezervimi.Email,
                    Klasi = rezervimi.Klasi,
                    Cmimi = rezervimi.Cmimi,
                    SelectedCurrencies = rezervimi.Currency.Split(',').ToList(),
                    FluturimiId = rezervimi.FluturimiId,
                    Kthyese = rezervimi.Kthyese,
                    Data_e_Rezervimit = rezervimi.Data_e_Rezervimit,
                    Data_e_Kthimit = rezervimi.Data_e_Kthimit,
                    Fluturimi = fluturimiList.Any() ? new SelectList(fluturimiList, "Value", "Text") : null
                };

                return View(editRezervimiRequest);
            }

            return NotFound();
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(EditRezervimiRequest editRezervimiRequest)
        {
            var existingRezervimi = await _context.Rezervimet
                    .Include(r => r.Fluturimi)
                    .FirstOrDefaultAsync(x => x.Id == editRezervimiRequest.Id);

            if (existingRezervimi != null)
            {
                // show success notification
                _context.Rezervimet.Remove(existingRezervimi);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }

            // show error notification or handle accordingly
            return NotFound();
        }
    }
}
