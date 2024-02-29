using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models
{
    public class Oferta
    {
        [Key]
        public int Id { get; set; }
        public int Cmimi { get; set; }
        public int HoteliId { get; set; }
        public int FluturimiId { get; set; }
        public string Description { get; set; }

        public Hoteli Hoteli { get; set; }
        public Fluturimi Fluturimi { get; set; }
    }
}
