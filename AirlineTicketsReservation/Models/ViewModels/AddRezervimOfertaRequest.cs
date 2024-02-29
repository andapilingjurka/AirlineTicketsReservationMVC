using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models.ViewModels
{
    public class AddRezervimOfertaRequest
    {
    
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Email { get; set; }
        public DateTime Data_E_Rezervimit { get; set; }
        public DateTime? Data_E_Kthimit { get; set; }
        public int Cmimi { get; set; }
        public string Currency { get; set; }

        public int OfertaId { get; set; }
        public string? UserId { get; set; }
        public IEnumerable<SelectListItem> Oferta { get; set; }
        // Collect Tag
        public string[] SelectedOffer { get; set; } = Array.Empty<string>();
        public List<string> SelectedCurrencies { get; set; } = new List<string>();
    }
}
