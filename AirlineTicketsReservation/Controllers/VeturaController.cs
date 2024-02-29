using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirlineTicketsReservation.Models;
using AirlineTicketsReservation.Data;
using AirlineTicketsReservation.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace AirlineTicketsReservation.Controllers
{


    public class VeturaController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public VeturaController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [Authorize(Roles = "User")]

        //Metoda Index GET
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, bool? bmw, bool? porsche)
        {
            ViewBag.PriceSortParam = String.IsNullOrEmpty(sortOrder) ? "normal" : sortOrder;

            var veturat = await SortVeturat(sortOrder);

            if (bmw.HasValue && bmw.Value && (!porsche.HasValue || !porsche.Value))
            {
                ViewBag.BMWChecked = "true";
                ViewBag.PorscheChecked = "false";
                // Nëse është zgjedhur vetëm checkbox për BMW, filtro veturat që janë vetëm BMW
                veturat = veturat.Where(v => v.Modeli == "BMW").ToList();
            }
            else if (porsche.HasValue && porsche.Value && (!bmw.HasValue || !bmw.Value))
            {
                ViewBag.BMWChecked = "false";
                ViewBag.PorscheChecked = "true";
                // Nëse është zgjedhur vetëm checkbox për Porsche, filtro veturat që janë vetëm Porsche
                veturat = veturat.Where(v => v.Modeli == "Porsche").ToList();
            }
            else if (bmw.HasValue && bmw.Value && porsche.HasValue && porsche.Value)
            {
                ViewBag.BMWChecked = "true";
                ViewBag.PorscheChecked = "true";
                // Nëse janë zgjedhur të dy checkbox, filtro veturat që janë edhe BMW edhe Porsche
                veturat = veturat.Where(v => v.Modeli == "BMW" || v.Modeli == "Porsche").ToList();
            }
            else
            {
                ViewBag.BMWChecked = "false";
                ViewBag.PorscheChecked = "false";
            }

            return View(veturat);
        }

        //Metoda per Sortim
        private async Task<List<Vetura>> SortVeturat(string sortOrder)
        {
            var veturatQuery = applicationDbContext.Vetura.AsQueryable();

            switch (sortOrder)
            {
                case "asc":
                    veturatQuery = veturatQuery.OrderBy(v => v.Cmimi);
                    break;
                case "desc":
                    veturatQuery = veturatQuery.OrderByDescending(v => v.Cmimi);
                    break;
                default:
                    break;
            }

            return await veturatQuery.ToListAsync();
        }


        [Authorize(Roles = "Admin")]
        // Metoda Add GET
        [HttpGet]
            public IActionResult Add()
            {
                return View();
            }



        [Authorize(Roles = "Admin")]
        //Metoda Add POST
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddVeturaRequest addVeturaRequest)
        {
            var vetura = new Vetura
            {
                Modeli = addVeturaRequest.Modeli,
                VitiProdhimit = addVeturaRequest.VitiProdhimit,
                Cmimi = addVeturaRequest.Cmimi,
                Karburanti = addVeturaRequest.Karburanti,
                PershkrimiModelit = addVeturaRequest.PershkrimiModelit,
                PhotoUrl = addVeturaRequest.PhotoUrl

            };

            await applicationDbContext.Vetura.AddAsync(vetura);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [Authorize(Roles = "User,Admin")]
        //Metoda Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var vetura = await applicationDbContext.Vetura.FirstOrDefaultAsync(x => x.VeturaID == id);

            if (vetura != null)
            {
                return View(vetura);
            }

            return View(null);
        }

        [Authorize(Roles = "Admin")]
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

            const int pageSize = 6; // Set the number of items per page

            var veturat = await applicationDbContext.Vetura.AsNoTracking().ToListAsync();

            var paginatedList = PaginatedList<Vetura>.Create(veturat, page ?? 1, pageSize);

            return View(paginatedList);
        }


        [Authorize(Roles = "Admin")]
        //Metoda Edit GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           
            var vetura = await applicationDbContext.Vetura.FirstOrDefaultAsync(x => x.VeturaID == id);

            if (vetura != null)
            {
                var editVeturaRequest = new EditVeturaRequest
                {
                    VeturaID = vetura.VeturaID,
                    Modeli = vetura.Modeli,
                    VitiProdhimit = vetura.VitiProdhimit,
                    Cmimi = vetura.Cmimi,
                    Karburanti = vetura.Karburanti,
                    PershkrimiModelit = vetura.PershkrimiModelit,
                    PhotoUrl = vetura.PhotoUrl,
                };
                return View(editVeturaRequest);
            }

            return View(null);

        }


        [Authorize(Roles = "Admin")]
        //Metoda Edit POST
        [HttpPost]
        public async Task<IActionResult> Edit(EditVeturaRequest editVeturaRequest)
        {
            var vetura = new Vetura
            {
                VeturaID = editVeturaRequest.VeturaID,
                Modeli = editVeturaRequest.Modeli,
                VitiProdhimit = editVeturaRequest.VitiProdhimit,
                Cmimi = editVeturaRequest.Cmimi,
                Karburanti = editVeturaRequest.Karburanti,
                PershkrimiModelit = editVeturaRequest.PershkrimiModelit,
                PhotoUrl = editVeturaRequest.PhotoUrl
            };
            var exisingVetura = await applicationDbContext.Vetura.FindAsync(vetura.VeturaID);
            if (exisingVetura != null)
            {
                exisingVetura.Modeli = vetura.Modeli;
                exisingVetura.VitiProdhimit = vetura.VitiProdhimit;
                exisingVetura.Cmimi = vetura.Cmimi;
                exisingVetura.Karburanti = vetura.Karburanti;
                exisingVetura.PershkrimiModelit = vetura.PershkrimiModelit;
                exisingVetura.PhotoUrl = vetura.PhotoUrl;

                await applicationDbContext.SaveChangesAsync();


                return RedirectToAction("List", new { id = editVeturaRequest.VeturaID });
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
        public async Task<IActionResult> Delete(EditVeturaRequest editVeturaRequest)
        {
            var vetura = await applicationDbContext.Vetura.FindAsync(editVeturaRequest.VeturaID);

            if (vetura != null)
            {
                //show success notification
                applicationDbContext.Vetura.Remove(vetura);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("List");


            }
            //Show error notification
            return RedirectToAction("Edit", new { id = editVeturaRequest.VeturaID });

        }

    }
}




