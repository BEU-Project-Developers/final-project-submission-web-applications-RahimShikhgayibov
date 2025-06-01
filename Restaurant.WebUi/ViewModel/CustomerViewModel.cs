using Restaurant.WebUi.Models;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebUi.ViewModel
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "Ad Soyad sahəsi tələb olunur.")]
        [Display(Name = "Ad Soyad")]
        public string CustomerName { get; set; } = "";

        [Required(ErrorMessage = "E-poçt sahəsi tələb olunur.")]
        [EmailAddress(ErrorMessage = "Düzgün e-poçt ünvanı daxil edin.")]
        [Display(Name = "E-poçt")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Telefon sahəsi tələb olunur.")]
        [Phone(ErrorMessage = "Düzgün telefon nömrəsi daxil edin.")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; } = "";

        [Required(ErrorMessage = "Tarix sahəsi tələb olunur.")]
        [Display(Name = "Rezervasiya Tarixi")]
        public DateTime Date { get; set; } = DateTime.Now.AddDays(1);

        [Required(ErrorMessage = "Vaxt sahəsi tələb olunur.")]
        [Display(Name = "Rezervasiya Vaxtı")]
        public TimeSpan Time { get; set; } = TimeSpan.FromHours(19);

        [Required(ErrorMessage = "Nəfər sayı sahəsi tələb olunur.")]
        [Range(1, 12, ErrorMessage = "Nəfər sayı 1-12 arasında olmalıdır.")]
        [Display(Name = "Nəfər Sayı")]
        public int NumberOfPeople { get; set; } = 2;

        [Display(Name = "Xüsusi İstəklər")]
        [StringLength(500, ErrorMessage = "Xüsusi istəklər 500 simvoldan çox ola bilməz.")]
        public string SpecialRequests { get; set; } = "";

        public List<ReservationViewModel> MyReservations { get; set; } = new List<ReservationViewModel>();
    }
}
