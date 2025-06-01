namespace Restaurant.WebUi.ViewModel
{
    public class UserManagementViewModel
    {
        public string Id { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string UserName { get; set; } = "";
        public List<string> Roles { get; set; } = new List<string>();
        public int ReservationCount { get; set; } = 0;
    }
}

