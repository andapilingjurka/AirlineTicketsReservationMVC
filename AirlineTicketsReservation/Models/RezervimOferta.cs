using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AirlineTicketsReservation.Models
{
    public class RezervimOferta
    {
        [Key]

        public int Id { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Email { get; set; }
        public DateTime Data_E_Rezervimit { get; set; }
        public DateTime? Data_E_Kthimit { get; set; }
        public int Cmimi { get; set; }
        public string Currency { get; set; }

        public int OfertaId { get; set; }
        public Oferta Oferta { get; set; }
        public string? UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
