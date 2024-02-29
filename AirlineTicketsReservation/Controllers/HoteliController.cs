using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models.ViewModels;
using AirlineTicketsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AirlineTicketsReservation.Controllers
{
    public class HoteliController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public HoteliController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [Authorize(Roles = "Admin")]
        //Metoda GET Add
        [HttpGet]
        public IActionResult Add()
        {
            var qytetet = applicationDbContext.Qyteti.ToList();

            var model = new AddHoteliRequest
            {
                Qytetet = qytetet.Select(x => new SelectListItem { Text = x.Emri, Value = x.Id.ToString() })

            };

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        //Metoda POST Add
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddHoteliRequest addHoteliRequest)
        {
            var qyteti = await applicationDbContext.Qyteti.FindAsync(addHoteliRequest.QytetiId);

            if (qyteti == null)
            {
                return NotFound();
            }

            var hoteli = new Hoteli
            {
                Emri = addHoteliRequest.Emri,
                Adresa = addHoteliRequest.Adresa,
                Pershkrimi = addHoteliRequest.Pershkrimi,
                Image = addHoteliRequest.Image,
                QytetiId = addHoteliRequest.QytetiId,

            };
            await applicationDbContext.Hoteli.AddAsync(hoteli);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }


        [Authorize(Roles = "Admin")]
        //Metoda GET List
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(int? page, string searchString)
        {
            if (applicationDbContext == null)
            {
                return NotFound();
            }

            const int pageSize = 8;

            var hotels = await applicationDbContext.Hoteli.AsNoTracking().ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                hotels = hotels.Where(h => h.Emri.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var paginatedList = PaginatedList<Hoteli>.Create(hotels, page ?? 1, pageSize);

            ViewBag.SearchString = searchString; // Pass the search string to the view

            return View(paginatedList);
        }



        [Authorize(Roles = "Admin")]
        //Metoda GET Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var hoteli = await applicationDbContext.Hoteli.FirstOrDefaultAsync(x => x.Id == id);

            var qyteti = applicationDbContext.Qyteti.ToList();

            if (hoteli != null)
            {
                var editHoteliRequest = new EditHoteliRequest
                {
                    Id = hoteli.Id,
                    Emri = hoteli.Emri,
                    Adresa = hoteli.Adresa,
                    Pershkrimi = hoteli.Pershkrimi,
                    Image = hoteli.Image,
                    QytetiId = hoteli.QytetiId,

                    Qytetet = qyteti.Select(x => new SelectListItem
                    {
                        Text = x.Emri,
                        Value = x.Id.ToString(),
                    }),
                };
                return View(editHoteliRequest);
            }

            return View(null);
        }


        [Authorize(Roles = "Admin")]
        //Metoda POST Edit
        [HttpPost]
        public async Task<IActionResult> Edit(EditHoteliRequest editHoteliRequest)
        {
            var hoteli = new Hoteli
            {
                Id = editHoteliRequest.Id,
                Emri = editHoteliRequest.Emri,
                Adresa = editHoteliRequest.Adresa,
                Pershkrimi = editHoteliRequest.Pershkrimi,
                Image = editHoteliRequest.Image,
                QytetiId = editHoteliRequest.QytetiId,
            };

            var exisingHoteli = await applicationDbContext.Hoteli.FindAsync(hoteli.Id);

            if (exisingHoteli != null)
            {
                exisingHoteli.Emri = hoteli.Emri;
                exisingHoteli.Adresa = hoteli.Adresa;
                exisingHoteli.Pershkrimi = hoteli.Pershkrimi;
                exisingHoteli.Image = hoteli.Image;
                exisingHoteli.QytetiId = hoteli.QytetiId;

                await applicationDbContext.SaveChangesAsync();


                return RedirectToAction("List", new { id = editHoteliRequest.Id });
            }
            else
            {
                return RedirectToAction("List");

            }
        }


        [Authorize(Roles = "Admin")]
        //Metoda POST Delete 
        [HttpPost]
        public async Task<IActionResult> Delete(EditHoteliRequest editHoteliRequest)
        {
            var hoteli = await applicationDbContext.Hoteli.FindAsync(editHoteliRequest.Id);

            if (hoteli != null)
            {
                //show success notification
                applicationDbContext.Hoteli.Remove(hoteli);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("List");


            }
            //Show error notification
            return RedirectToAction("Edit", new { id = editHoteliRequest.Id });

        }

        [Authorize(Roles = "User,Admin")]
        //Metoda per index 
        public async Task<IActionResult> Index()
        {
            // Projecting only necessary fields, excluding the description
            var hotelet = await applicationDbContext.Hoteli
                .Select(q => new Hoteli
                {
                    Id = q.Id,
                    Emri = q.Emri,
                    Adresa = q.Adresa,
                    Image = q.Image,
                    QytetiId = q.QytetiId
                })
                .ToListAsync();

            return View(hotelet);
        }


        [Authorize(Roles = "User,Admin")]
        //Metoda per ViewDetails
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var hotelet = await applicationDbContext.Hoteli.FirstOrDefaultAsync(x => x.Id == id);

            if (hotelet != null)
            {
                return View(hotelet);
            }

            return View(null);
        }

    }
}

