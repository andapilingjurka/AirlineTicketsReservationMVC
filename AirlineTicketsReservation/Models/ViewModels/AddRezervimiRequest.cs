﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirlineTicketsReservation.Models.ViewModels
{
    public class AddRezervimiRequest
    {
        public string EmriPasagjerit { get; set; }
        public string MbiemriPasagjerit { get; set; }
        public string Email { get; set; }
        public string Klasi { get; set; }
        public long Cmimi { get; set; }
        public string Currency { get; set; }
        public int FluturimiId { get; set; }
        public Boolean Kthyese { get; set; }

        public DateTimeOffset Data_e_Rezervimit { get; set; }
        public DateTime? Data_e_Kthimit { get; set; }
        public string? UserId { get; set; }
        public IEnumerable<SelectListItem> Fluturimi { get; set; }
        // Collect Tag
        public string[] SelectedFlight { get; set; } = Array.Empty<string>();
    }
}
