using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models
{
    public class Aeroplani
    {
        [Key]

        public int Id { get; set; }

        public int Nr_Uleseve_VIP { get; set; }
        public int Nr_Uleseve_Biznes { get; set; }
        public int Nr_Uleseve_Ekonomike { get; set; }
        public string Kompania { get; set; }
    }
}
