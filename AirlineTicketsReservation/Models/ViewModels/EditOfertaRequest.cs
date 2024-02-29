using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models.ViewModels
{
    public class EditOfertaRequest
    {
        [Key]
        public int Id { get; set; }
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
