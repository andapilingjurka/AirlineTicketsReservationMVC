using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirlineTicketsReservation.Models.ViewModels
{
    public class AddQytetiRequest
    {
        public string Emri { get; set; }

        public string ZipCode { get; set; }

        public string Image { get; set; }

        public int ShtetiId { get; set; }

        public IEnumerable<SelectListItem> Shtetet { get; set; }

        public string[] SelectedShtetet { get; set; } = Array.Empty<string>();
    }
}
