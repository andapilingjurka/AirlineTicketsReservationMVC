using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models.ViewModels;
using AirlineTicketsReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace AirlineTicketsReservation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShtetiController : Controller
    {
      

        private readonly ApplicationDbContext applicationDbContext;

        public ShtetiController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        //Metoda GET Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        //Metoda POST Add
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddShtetiRequest addShtetiRequest)
        {
            var shteti = new Shteti
            {
                Emri = addShtetiRequest.Emri,
            };
            await applicationDbContext.Shteti.AddAsync(shteti);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }


        //Metoda GET List
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(int? page, string sortOrder)
        {
            // Ensure applicationDbContext is not null
            if (applicationDbContext == null)
            {
                return NotFound(); // Or handle the case where the context is null
            }

            const int pageSize = 8; // Set the number of items per page

            var shtetiQuery = applicationDbContext.Shteti.AsQueryable();

            switch (sortOrder)
            {
                case "name_desc":
                    shtetiQuery = shtetiQuery.OrderByDescending(s => s.Emri);
                    break;
                default:
                    shtetiQuery = shtetiQuery.OrderBy(s => s.Emri);
                    break;
            }

            var shteti = await shtetiQuery.AsNoTracking().ToListAsync();

            var paginatedList = PaginatedList<Shteti>.Create(shteti, page ?? 1, pageSize);

            ViewBag.SortOrderOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "A-Z", Value = "name_asc" },
                new SelectListItem { Text = "Z-A", Value = "name_desc" },
                new SelectListItem { Text = "Normal", Value = "" }
            };

            return View(paginatedList);
        }



        //Metoda GET Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var shteti = await applicationDbContext.Shteti.FirstOrDefaultAsync(x => x.Id == id);

            if (shteti != null)
            {
                var editShtetiRequest = new EditShtetiRequest
                {
                    Id = shteti.Id,
                    Emri = shteti.Emri,
                };
                return View(editShtetiRequest);
            }

            return View(null);
        }


        //Metoda POST Edit
        [HttpPost]
        public async Task<IActionResult> Edit(EditShtetiRequest editShtetiRequest)
        {
            var shteti = new Shteti
            {
                Id = editShtetiRequest.Id,
                Emri = editShtetiRequest.Emri,
            };

            var exisingShteti = await applicationDbContext.Shteti.FindAsync(shteti.Id);

            if (exisingShteti != null)
            {
                exisingShteti.Emri = shteti.Emri;
                await applicationDbContext.SaveChangesAsync();


                return RedirectToAction("List", new { id = editShtetiRequest.Id });
            }
            else
            {
                return RedirectToAction("List");

            }
        }


        //Metoda POST Delete 
        [HttpPost]
        public async Task<IActionResult> Delete(EditShtetiRequest editShtetiRequest)
        {
            var shteti = await applicationDbContext.Shteti.FindAsync(editShtetiRequest.Id);

            if (shteti != null)
            {
                //show success notification
                applicationDbContext.Shteti.Remove(shteti);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("List");


            }
            //Show error notification
            return RedirectToAction("Edit", new { id = editShtetiRequest.Id });

        }

    }
}


