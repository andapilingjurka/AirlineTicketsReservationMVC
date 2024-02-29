using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirlineTicketsReservation.Models.ViewModels
{
    public class EditHoteliRequest
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string Adresa { get; set; }
        public string Pershkrimi { get; set; }
        public string Image { get; set; }
        public int QytetiId { get; set; }

        public IEnumerable<SelectListItem> Qytetet { get; set; }

        public string[] SelectedQytetet { get; set; } = Array.Empty<string>();
    }
}
