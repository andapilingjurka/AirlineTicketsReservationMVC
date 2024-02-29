using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models
{
    public class Rezervimi
    {
        [Key]

        public int Id { get; set; }
        public string EmriPasagjerit { get; set; }
        public string MbiemriPasagjerit { get; set; }
        public string Email { get; set; }
        public string Klasi { get; set; }
        public long Cmimi { get; set; }
        public string Currency { get; set; }
        public int FluturimiId { get; set; }
        public Boolean Kthyese { get; set; }

        public DateTimeOffset Data_e_Rezervimit { get; set; }
        public DateTime? Data_e_Kthimit { get; set; }

        public Fluturimi Fluturimi { get; set; }
        public string? UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
