using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models.ViewModels;
using AirlineTicketsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AirlineTicketsReservation.Controllers
{
    public class QytetiController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public QytetiController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        [Authorize(Roles = "Admin")]
        //Metoda GET Add
        [HttpGet]
        public IActionResult Add()
        {
            var shtetet = applicationDbContext.Shteti.ToList();

            var model = new AddQytetiRequest
            {
                Shtetet = shtetet.Select(x => new SelectListItem { Text = x.Emri, Value = x.Id.ToString() })

            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        //Metoda POST Add
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddQytetiRequest addQytetiRequest)
        {
            var shteti = await applicationDbContext.Shteti.FindAsync(addQytetiRequest.ShtetiId);

            if (shteti == null)
            {
                return NotFound();
            }

            var qyteti = new Qyteti
            {
                Emri = addQytetiRequest.Emri,
                ZipCode = addQytetiRequest.ZipCode,
                Image = addQytetiRequest.Image,
                ShtetiId = addQytetiRequest.ShtetiId,
            };
            await applicationDbContext.Qyteti.AddAsync(qyteti);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        //Metoda GET List
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(int? page, string filterLetter)
        {
            if (applicationDbContext == null)
            {
                return NotFound();
            }

            const int pageSize = 8;

            // Filter the cities based on the starting letter
            var qytetiQuery = applicationDbContext.Qyteti.AsQueryable();
            if (!string.IsNullOrEmpty(filterLetter))
            {
                qytetiQuery = qytetiQuery.Where(q => q.Emri.StartsWith(filterLetter));
            }

            var qyteti = await qytetiQuery.AsNoTracking().ToListAsync();

            var paginatedList = PaginatedList<Qyteti>.Create(qyteti, page ?? 1, pageSize);

            // Pass the filterLetter to the view to maintain the filter state
            ViewBag.FilterLetter = filterLetter;

            return View(paginatedList);
        }


        [Authorize(Roles = "Admin")]
        //Metoda GET Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var qyteti = await applicationDbContext.Qyteti.FirstOrDefaultAsync(x => x.Id == id);

            var shteti = applicationDbContext.Shteti.ToList();

            if (qyteti != null)
            {
                var editQytetiRequest = new EditQytetiRequest
                {
                    Id = qyteti.Id,
                    Emri = qyteti.Emri,
                    ZipCode = qyteti.ZipCode,
                    Image = qyteti.Image,
                    ShtetiId = qyteti.ShtetiId,

                    Shtetet = shteti.Select(x => new SelectListItem
                    {
                        Text = x.Emri,
                        Value = x.Id.ToString(),
                    }),
                };
                return View(editQytetiRequest);
            }

            return View(null);
        }

        [Authorize(Roles = "Admin")]
        //Metoda POST Edit
        [HttpPost]
        public async Task<IActionResult> Edit(EditQytetiRequest editQytetiRequest)
        {
            var qyteti = new Qyteti
            {
                Id = editQytetiRequest.Id,
                Emri = editQytetiRequest.Emri,
                ZipCode = editQytetiRequest.ZipCode,
                Image = editQytetiRequest.Image,
                ShtetiId = editQytetiRequest.ShtetiId,
            };

            var exisingQyteti = await applicationDbContext.Qyteti.FindAsync(qyteti.Id);

            if (exisingQyteti != null)
            {
                exisingQyteti.Emri = qyteti.Emri;
                exisingQyteti.ZipCode = qyteti.ZipCode;
                exisingQyteti.Image = qyteti.Image;
                exisingQyteti.ShtetiId = qyteti.ShtetiId;
                await applicationDbContext.SaveChangesAsync();


                return RedirectToAction("List", new { id = editQytetiRequest.Id });
            }
            else
            {
                return RedirectToAction("List");

            }
        }

        [Authorize(Roles = "Admin")]
        //Metoda POST Delete 
        [HttpPost]
        public async Task<IActionResult> Delete(EditQytetiRequest editQytetiRequest)
        {
            var qyteti = await applicationDbContext.Qyteti.FindAsync(editQytetiRequest.Id);

            if (qyteti != null)
            {
                //show success notification
                applicationDbContext.Qyteti.Remove(qyteti);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("List");


            }
            //Show error notification
            return RedirectToAction("Edit", new { id = editQytetiRequest.Id });

        }
        [Authorize(Roles = "User,Admin")]
        //Metoda per Index 
        public async Task<IActionResult> Index()
        {
            // Projecting only necessary fields, excluding the description
            var qytetet = await applicationDbContext.Qyteti
                .Select(q => new Qyteti
                {
                    Id = q.Id,
                    Emri = q.Emri,
                    ZipCode = q.ZipCode,
                    Image = q.Image,
                    ShtetiId = q.ShtetiId
                })
                .ToListAsync();

            return View(qytetet);
        }

    }
}