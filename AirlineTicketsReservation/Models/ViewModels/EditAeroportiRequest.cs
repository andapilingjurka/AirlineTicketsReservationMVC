using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirlineTicketsReservation.Models.ViewModels
{
    public class EditAeroportiRequest
    {
        public int AeroportiID { get; set; }
        public string Emri { get; set; }
        public int QytetiID { get; set; }

    }
}
