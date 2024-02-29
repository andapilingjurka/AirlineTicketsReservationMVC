using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models
{
    public class Shteti
    {
        [Key]
        public int Id { get; set; }
        public string Emri { get; set; }
    }
}
