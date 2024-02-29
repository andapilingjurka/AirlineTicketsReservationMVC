using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models.ViewModels;
using AirlineTicketsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Authorization;

namespace AirlineTicketsReservation.Controllers
{
    public class RezervimiVController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly StripeController stripeController;


        public StripeController StripeController => stripeController;
        public RezervimiVController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        [Authorize(Roles = "User,Admin")]

        //Metoda Add GET
        [HttpGet]
        public IActionResult Add(int veturaId, decimal cmimi)
        {
            // Assuming you have a DbContext to access the vehicles and airports
            var vetura = applicationDbContext.Vetura.ToList();
            var aeroportet = applicationDbContext.Aeroporti.ToList();

            var model = new AddRezervimiVRequest
            {
                Aeroportet = aeroportet.Select(x => new SelectListItem { Text = x.Emri, Value = x.AeroportiID.ToString() }),
                VeturaID = veturaId,
                Cmimi = cmimi

            };

            return View(model);
        }
        [Authorize(Roles = "User,Admin")]
        //Metoda ADD Post
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddRezervimiVRequest addRezervimiVRequest)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var vetura = await applicationDbContext.Vetura
                .FirstOrDefaultAsync(v => v.VeturaID == addRezervimiVRequest.VeturaID);

            if (vetura != null)
            {
                // Convert vetura.Cmimi to decimal
                if (decimal.TryParse(vetura.Cmimi, out decimal veturaCmimi))
                {
                    // Calculate the reservationCmimi
                    var reservationCmimi = CalculateReservationCmimi(veturaCmimi, addRezervimiVRequest.DataFillimit, addRezervimiVRequest.DataKthimit);

                    var rezervimi = new RezervimiV
                    {
                        Email = addRezervimiVRequest.Email,
                        DataFillimit = addRezervimiVRequest.DataFillimit,
                        DataKthimit = addRezervimiVRequest.DataKthimit,
                        AeroportiID = addRezervimiVRequest.AeroportiID,
                        VeturaID = addRezervimiVRequest.VeturaID,
                        Cmimi = reservationCmimi,  // Përdorimi i vlerës së llogaritur për cmimin
                        UserId = userId


                    };

                    await applicationDbContext.RezervimiV.AddAsync(rezervimi);
                    await applicationDbContext.SaveChangesAsync();
                    return RedirectToAction("CreateCheckoutSession", "Stripe", new { shuma = reservationCmimi });
                }
                else
                {
                    // Handle the case where vetura.Cmimi cannot be converted to decimal
                    return RedirectToAction("Error");
                }
            }
            else
            {
                // Handle the case where VeturaID is not valid
                return RedirectToAction("Error");
            }
        }
        private decimal CalculateReservationCmimi(decimal veturaCmimi, DateTime startDate, DateTime endDate)
        {
            // Tarifa ditore
            decimal dailyRate = veturaCmimi;

            // Numri i ditëve
            int numberOfDays = (int)(endDate - startDate).TotalDays;

            // Llogaritja e çmimit total
            decimal totalCmimi = numberOfDays * dailyRate;

            return totalCmimi;
        }

        [Authorize(Roles = "User")]
        //Metoda MyReservations GET
        [HttpGet]
        public async Task<IActionResult> MyReservations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Merrni rezervimet e klientit specifik
            var clientReservations = await applicationDbContext.RezervimiV
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return View(clientReservations);
        }

        [Authorize(Roles = "Admin")]
        //Metoda List GET
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(int? veturaId, decimal? cmimi, int? page)
        {

            IQueryable<RezervimiV> rezervimetQuery = applicationDbContext.RezervimiV;

            if (veturaId.HasValue && cmimi.HasValue)
            {
                // Filtroni veturat në varësi të ID-së së veturës nëse është dhënë
                rezervimetQuery = rezervimetQuery.Where(r => r.VeturaID == veturaId && r.Cmimi == cmimi);
            }
            const int pageSize = 8; // Set the number of items per page

            var rezervimi = await rezervimetQuery.ToListAsync();

            // Create a paginated list
            var paginatedList = PaginatedList<RezervimiV>.Create(rezervimi, page ?? 1, pageSize);

            return View(paginatedList);
        }

        [Authorize(Roles = "User,Admin")]
        //Metoda Edit GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
       
            var rezervimi = await applicationDbContext.RezervimiV.FirstOrDefaultAsync(x => x.RezervimiID == id);
            var vetura = applicationDbContext.Vetura.ToList();
            var aeroportet = applicationDbContext.Aeroporti.ToList();
            if (rezervimi != null)
            {


                var editRezervimiVRequest = new EditRezervimiVRequest
                {
                    Email = rezervimi.Email,
                    RezervimiID = rezervimi.RezervimiID,
                    DataFillimit = rezervimi.DataFillimit,
                    DataKthimit = rezervimi.DataKthimit,
                    VeturaID = rezervimi.VeturaID,
                    AeroportiID = rezervimi.AeroportiID,
                    Cmimi = rezervimi.Cmimi,

                    Aeroportet = aeroportet.Select(x => new SelectListItem
                    {
                        Text = x.Emri,
                        Value = x.AeroportiID.ToString(),
                    }),
                };

                return View(editRezervimiVRequest);
            }
            return View(null);
        }
        [Authorize(Roles = "User,Admin")]
        //Metoda Edit POST
        [HttpPost]
        public async Task<IActionResult> Edit(EditRezervimiVRequest editRezervimiVRequest)
        {
            var rezervimi = new RezervimiV
            {
                RezervimiID = editRezervimiVRequest.RezervimiID,
                Email = editRezervimiVRequest.Email,
                DataFillimit = editRezervimiVRequest.DataFillimit,
                DataKthimit = editRezervimiVRequest.DataKthimit,
                AeroportiID = editRezervimiVRequest.AeroportiID,
                VeturaID = editRezervimiVRequest.VeturaID,
                Cmimi = editRezervimiVRequest.Cmimi

            };
            var exisingRezervimi = await applicationDbContext.RezervimiV.FindAsync(rezervimi.RezervimiID);
            if (exisingRezervimi != null)
            {
                exisingRezervimi.Email = rezervimi.Email;
                exisingRezervimi.DataFillimit = rezervimi.DataFillimit;
                exisingRezervimi.DataKthimit = rezervimi.DataKthimit;
                exisingRezervimi.AeroportiID = rezervimi.AeroportiID;
                exisingRezervimi.VeturaID = rezervimi.VeturaID;
                exisingRezervimi.Cmimi = rezervimi.Cmimi;
                await applicationDbContext.SaveChangesAsync();


                return RedirectToAction("Edit", new { id = editRezervimiVRequest.RezervimiID });
            }
            else
            {
                return RedirectToAction("List");
                //Show fail notification

            }
        }


        [Authorize(Roles = "Admin")]

        //Metoda Delete POST
        [HttpPost]
        public async Task<IActionResult> Delete(EditRezervimiVRequest editRezervimiVRequest)
        {
            var rezervimi = await applicationDbContext.RezervimiV.FindAsync(editRezervimiVRequest.RezervimiID);

            if (rezervimi != null)
            {
                //show success notification
                applicationDbContext.RezervimiV.Remove(rezervimi);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("List");


            }
            //Show error notification
            return RedirectToAction("Edit", new { id = editRezervimiVRequest.RezervimiID });

        }

    }
}
