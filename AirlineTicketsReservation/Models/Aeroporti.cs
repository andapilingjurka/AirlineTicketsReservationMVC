using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models
{
    public class Aeroporti
    {
        [Key]
        public int AeroportiID { get; set; }
        public string Emri { get; set; }
        public int QytetiID { get; set; }

        public Qyteti Qyteti { get; set; }
    }
}
