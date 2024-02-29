using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models
{
    public class Qyteti
    {
        [Key]
        public int Id { get; set; }

        public string Emri { get; set; }

        public string ZipCode { get; set; }

        public string Image { get; set; }

        public int ShtetiId { get; set; }

        public Shteti Shteti { get; set; }
        public Aeroporti Aeroporti { get; set; }

    }
}
