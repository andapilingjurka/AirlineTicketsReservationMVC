using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirlineTicketsReservation.Models.ViewModels
{
    public class AddOfertaRequest
    {
        public int Cmimi { get; set; }
        public int HoteliId { get; set; }
        public int FluturimiId { get; set; }
        public string Description { get; set; }
        public IEnumerable<SelectListItem> Hoteli { get; set; }
      
        public string[] SelectedHotel { get; set; } = Array.Empty<string>();
        public IEnumerable<SelectListItem> Fluturimi { get; set; }
       
        public string[] SelectedFluturimi { get; set; } = Array.Empty<string>();
    }
}
