namespace AirlineTicketsReservation.Models.ViewModels
{
    public class DeleteKontaktiRequest
    {
        public int KontaktiID { get; set; }

        public string Emri { get; set; }

        public string Mbiemri { get; set; }

        public string Email { get; set; }

        public string Telefoni { get; set; }

        public string Mesazhi { get; set; }
    }
}
