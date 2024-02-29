using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models;
using AirlineTicketsReservation.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AirlineTicketsReservation.Controllers
{
    public class RezervimOfertaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly StripeController stripeController;


        public StripeController StripeController => stripeController;
        public RezervimOfertaController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "User,Admin")]

        [HttpGet]
        public IActionResult Add()
        {
            var ofertaList = _context.Ofertat
                .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = $"{o.HoteliId} - {o.FluturimiId}" })
                .ToList();

            var model = new AddRezervimOfertaRequest
            {
                Oferta = ofertaList
            };

            return View(model);
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddRezervimOfertaRequest addRezervimOfertaRequest)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                // Fetch the selected offer details
                var selectedOffer = await _context.Ofertat.FindAsync(addRezervimOfertaRequest.OfertaId);

                if (selectedOffer == null)
                {
                    return NotFound();
                }

                var rezervimOferta = new RezervimOferta
                {
                    Emri = addRezervimOfertaRequest.Emri,
                    Mbiemri = addRezervimOfertaRequest.Mbiemri,
                    Email = addRezervimOfertaRequest.Email,
                    Data_E_Rezervimit = addRezervimOfertaRequest.Data_E_Rezervimit,
                    Data_E_Kthimit = addRezervimOfertaRequest.Data_E_Kthimit,
                    Cmimi = selectedOffer.Cmimi, // Assuming Cmimi comes from Oferta
                    Currency = addRezervimOfertaRequest.Currency,
                    OfertaId = addRezervimOfertaRequest.OfertaId,
                    UserId = userId

                };


                _context.Rezervimet_me_Oferte.Add(rezervimOferta);
                await _context.SaveChangesAsync();
                 return RedirectToAction("CreateCheckoutSession", "Stripe", new { shuma = selectedOffer.Cmimi });
            }

            // If ModelState is not valid, return to the add view with validation errors
            var ofertaList = _context.Ofertat
                .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = $"{o.HoteliId} - {o.FluturimiId}" })
                .ToList();

            addRezervimOfertaRequest.Oferta = new SelectList(ofertaList, "Id", "Text", addRezervimOfertaRequest.OfertaId);

            return View(addRezervimOfertaRequest);
        }
        [Authorize(Roles = "User,Admin")]
        //Metoda MyReservations GET
        [HttpGet]
        public async Task<IActionResult> MyReservations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Merrni rezervimet e klientit specifik
            var clientReservations = await _context.Rezervimet_me_Oferte
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return View(clientReservations);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> ListAsync(int? page)
        {
            const int pageSize = 8;

            var query = _context.Rezervimet_me_Oferte.Include(r => r.Oferta).AsQueryable();

            var rezervimetOferta = await query.AsNoTracking().ToListAsync();

            var paginatedList = PaginatedList<RezervimOferta>.Create(rezervimetOferta, page ?? 1, pageSize);

            return View(paginatedList);
        }
        [Authorize(Roles = "User,Admin")]

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var rezervimOferta = await _context.Rezervimet_me_Oferte
                .Include(r => r.Oferta)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (rezervimOferta != null)
            {
                var ofertaList = _context.Ofertat
                    .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = $"{o.HoteliId} - {o.FluturimiId}" })
                    .ToList();

                var editRezervimOfertaRequest = new EditRezervimOfertaRequest
                {
                    Id = rezervimOferta.Id,
                    Emri = rezervimOferta.Emri,
                    Mbiemri = rezervimOferta.Mbiemri,
                    Email = rezervimOferta.Email,
                    Data_E_Rezervimit = rezervimOferta.Data_E_Rezervimit,
                    Data_E_Kthimit = rezervimOferta.Data_E_Kthimit,
                    Cmimi = rezervimOferta.Cmimi,
                    SelectedCurrencies = rezervimOferta.Currency.Split(',').ToList(),
                    OfertaId = rezervimOferta.OfertaId,
                    Oferta = ofertaList.Any() ? new SelectList(ofertaList, "Value", "Text") : null
                };

                return View(editRezervimOfertaRequest);
            }

            return NotFound();
        }
        [Authorize(Roles = "User,Admin")]

        [HttpPost]
        public async Task<IActionResult> Edit(EditRezervimOfertaRequest editRezervimOfertaRequest)
        {
            if (ModelState.IsValid)
            {

                var existingRezervimOferta = await _context.Rezervimet_me_Oferte
                    .Include(r => r.Oferta)
                    .FirstOrDefaultAsync(x => x.Id == editRezervimOfertaRequest.Id);

                if (existingRezervimOferta != null)
                {
                    existingRezervimOferta.Emri = editRezervimOfertaRequest.Emri;
                    existingRezervimOferta.Mbiemri = editRezervimOfertaRequest.Mbiemri;
                    existingRezervimOferta.Email = editRezervimOfertaRequest.Email;
                    existingRezervimOferta.Data_E_Rezervimit = editRezervimOfertaRequest.Data_E_Rezervimit;
                    existingRezervimOferta.Data_E_Kthimit = editRezervimOfertaRequest.Data_E_Kthimit;
                    existingRezervimOferta.Cmimi = editRezervimOfertaRequest.Cmimi;
                    existingRezervimOferta.Currency = string.Join(",", editRezervimOfertaRequest.SelectedCurrencies);
                    existingRezervimOferta.OfertaId = editRezervimOfertaRequest.OfertaId;
                    existingRezervimOferta.Oferta = await _context.Ofertat
                        .FirstOrDefaultAsync(o => o.Id == editRezervimOfertaRequest.OfertaId);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Edit", new { id = editRezervimOfertaRequest.Id });
                }
                else
                {
                    return RedirectToAction("List");
                    // Show fail notification
                }
            }

            // If ModelState is not valid, return to the edit view with validation errors
            var ofertaList = _context.Ofertat
                .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = $"{o.HoteliId} - {o.FluturimiId}" })
                .ToList();

            editRezervimOfertaRequest.Oferta = new SelectList(ofertaList, "Id", "Text", editRezervimOfertaRequest.OfertaId);

            return View(editRezervimOfertaRequest);
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(EditRezervimOfertaRequest editRezervimOfertaRequest)
        {
            var existingRezervimOferta = await _context.Rezervimet_me_Oferte
                .Include(r => r.Oferta)
                .FirstOrDefaultAsync(x => x.Id == editRezervimOfertaRequest.Id);

            if (existingRezervimOferta != null)
            {
                // show success notification
                _context.Rezervimet_me_Oferte.Remove(existingRezervimOferta);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }

            // show error notification or handle accordingly
            return NotFound();
        }

    }
}
