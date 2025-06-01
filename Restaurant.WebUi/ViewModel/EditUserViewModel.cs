using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebUi.ViewModel
{
        public class EditUserViewModel
        {
            public string Id { get; set; } = "";

            [Required(ErrorMessage = "Ad sahəsi tələb olunur.")]
            [Display(Name = "Ad")]
            public string FirstName { get; set; } = "";

            [Required(ErrorMessage = "Soyad sahəsi tələb olunur.")]
            [Display(Name = "Soyad")]
            public string LastName { get; set; } = "";

            [Required(ErrorMessage = "E-poçt sahəsi tələb olunur.")]
            [EmailAddress(ErrorMessage = "Düzgün e-poçt ünvanı daxil edin.")]
            [Display(Name = "E-poçt")]
            public string Email { get; set; } = "";

            // UserName sahəsini gizli edin və Email ilə eyni edin
            public string UserName { get; set; } = "";

            public List<string> UserRoles { get; set; } = new List<string>();
            public List<string> AllRoles { get; set; } = new List<string>();
            public List<string> SelectedRoles { get; set; } = new List<string>();

            // Rezervasiyalar üçün
            public List<ReservationViewModel> Reservations { get; set; } = new List<ReservationViewModel>();
        }
}
