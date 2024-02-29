namespace AirlineTicketsReservation.Models.ViewModels
{
    public class AddAeroplaniRequest
    {
        public int Nr_Uleseve_VIP { get; set; }
        public int Nr_Uleseve_Biznes { get; set; }
        public int Nr_Uleseve_Ekonomike { get; set; }
        public string? Kompania { get; set; }
    }
}
