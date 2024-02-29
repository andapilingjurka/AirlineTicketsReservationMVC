using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models.ViewModels
{
    public class EditFluturimiRequest
    {
        [Key]
        public int Id { get; set; }
        public string NrFluturimit { get; set; }
        public string DeparuteAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime KohaENisjes { get; set; }
        public DateTime KohaEArritjes { get; set; }

        public int Cmimi { get; set; }
        //kur e krijon fluturimin ja cakton qmimin tani qat qmim e mbledh me qmimin te rezervimi 
        //sipas klasit

        public int AeroplaniId { get; set; }
        public int QytetiId { get; set; }

        public IEnumerable<SelectListItem> Aeroplani { get; set; }
        // Collect Tag
        public string[] SelectedPlane { get; set; } = Array.Empty<string>();
        public IEnumerable<SelectListItem> Qyteti { get; set; }
        // Collect Tag
        public string[] SelectedCity { get; set; } = Array.Empty<string>();
    }
}
