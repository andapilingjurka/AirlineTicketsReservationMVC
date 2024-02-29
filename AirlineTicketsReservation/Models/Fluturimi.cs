using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models
{
    public class Fluturimi
    {
        [Key]
        public int Id { get; set; }
        public string NrFluturimit { get; set; }
        public string DeparuteAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime KohaENisjes { get; set; }
        public DateTime KohaEArritjes { get; set; }

        public int Cmimi { get; set; }
  

        public int AeroplaniId { get; set; }
        public Aeroplani Aeroplani { get; set; }
        public int QytetiId { get; set; }
        public Qyteti Qyteti { get; set; }
    }
}
