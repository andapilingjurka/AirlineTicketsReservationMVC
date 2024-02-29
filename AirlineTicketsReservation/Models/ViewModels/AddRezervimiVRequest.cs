using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirlineTicketsReservation.Models.ViewModels
{
    public class AddRezervimiVRequest
    {
        public DateTime DataFillimit { get; set; }
        public DateTime DataKthimit { get; set; }
        public string Email { get; set; }

        public int AeroportiID { get; set; }
        public int VeturaID { get; set; }
        public decimal Cmimi { get; set; }
        public string? UserId { get; set; }

        public IEnumerable<SelectListItem> Aeroportet { get; set; }

        // Collect Tag
        public string[] SelectedAeroportet { get; set; } = Array.Empty<string>();
    }
}