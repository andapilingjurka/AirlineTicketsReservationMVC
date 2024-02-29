using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models
{
    public class RezervimiV
    {
        [Key]

        public int RezervimiID { get; set; }

        public string Email { get; set; }
        public DateTime DataFillimit { get; set; }
        public DateTime DataKthimit { get; set; }
        public int AeroportiID { get; set; }
        public int VeturaID { get; set; }
        public decimal Cmimi { get; set; }
        public string? UserId { get; set; }
        public IdentityUser User { get; set; }

        public Aeroporti Aeroporti { get; set; }
        public Vetura Vetura { get; set; }
    }
}
