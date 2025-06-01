namespace Restaurant.WebUi.ViewModel
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }
        public int GuestCount { get; set; }
        public string Status { get; set; } = "";
        public string SpecialRequests { get; set; } = "";
        public DateTime CreatedDate { get; set; }
    }

}
