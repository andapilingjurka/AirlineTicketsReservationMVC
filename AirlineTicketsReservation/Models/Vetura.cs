using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models
{
    public class Vetura
    {
        [Key]
        public int VeturaID { get; set; }
        public string Modeli { get; set; }
        public string VitiProdhimit { get; set; }
        public string Karburanti { get; set; }
        public string Cmimi { get; set; }

        public string PershkrimiModelit { get; set; }
        public string PhotoUrl { get; set; }

    }
}
