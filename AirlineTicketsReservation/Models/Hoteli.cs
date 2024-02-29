using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models
{
    public class Hoteli
    {
        [Key]

        public int Id { get; set; }
        public string Emri { get; set; }
        public string Adresa { get; set; }
        public string Pershkrimi { get; set; }
        public string Image { get; set; }
        public int QytetiId { get; set; }
        public Qyteti Qyteti { get; set; }
    }
}
