using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebUi.Models;
using Restaurant.WebUi.Services.interfaces;
using Restaurant.WebUi.ViewModel;
using System.Security.Claims;

namespace Restaurant.WebUi.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _userManager;

        public CustomerController(IReservationService reservationService, UserManager<AppUser> userManager)
        {
            _reservationService = reservationService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var reservations = await _reservationService.GetUserReservationsAsync(userId);

            var reservationViewModels = reservations.Select(r => new ReservationViewModel
            {
                Id = r.Id,
                CustomerName = r.CustomerName,
                Email = r.Email,
                Phone = r.Phone,
                ReservationDate = r.ReservationDate,
                ReservationTime = r.ReservationTime,
                GuestCount = r.GuestCount,
                Status = r.Status,
                SpecialRequests = r.SpecialRequests,
                CreatedDate = r.CreatedDate
            }).ToList();

            var model = new CustomerViewModel
            {
                MyReservations = reservationViewModels,
                CustomerName = $"{user?.FirstName} {user?.LastName}",
                Email = user?.Email ?? "",
                Phone = "", // Bu sahə istifadəçi profilindən gələ bilər
                Date = DateTime.Now.AddDays(1),
                Time = TimeSpan.FromHours(19), // Default 19:00
                NumberOfPeople = 2,
                SpecialRequests = ""
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Əgər model valid deyilsə, rezervasiyaları yenidən yüklə
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var reservations = await _reservationService.GetUserReservationsAsync(userId);
                model.MyReservations = reservations.Select(r => new ReservationViewModel
                {
                    Id = r.Id,
                    CustomerName = r.CustomerName,
                    Email = r.Email,
                    Phone = r.Phone,
                    ReservationDate = r.ReservationDate,
                    ReservationTime = r.ReservationTime,
                    GuestCount = r.GuestCount,
                    Status = r.Status,
                    SpecialRequests = r.SpecialRequests,
                    CreatedDate = r.CreatedDate
                }).ToList();

                TempData["Error"] = "Zəhmət olmasa bütün sahələri düzgün doldurun.";
                return View("Index", model);
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.FindByIdAsync(currentUserId);

            var reservation = new Reservation
            {
                UserId = currentUserId,
                CustomerName = model.CustomerName,
                Email = model.Email,
                Phone = model.Phone,
                ReservationDate = model.Date,
                ReservationTime = model.Time,
                GuestCount = model.NumberOfPeople,
                SpecialRequests = model.SpecialRequests ?? "",
                Status = "Pending",
                CreatedDate = DateTime.Now
            };

            try
            {
                var success = await _reservationService.CreateReservationAsync(reservation);

                if (success)
                {
                    TempData["Success"] = "Rezervasiyanız uğurla yaradıldı! Tezliklə sizinlə əlaqə saxlanılacaq.";
                }
                else
                {
                    TempData["Error"] = "Rezervasiya yaradılarkən xəta baş verdi. Zəhmət olmasa yenidən cəhd edin.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sistem xətası baş verdi. Zəhmət olmasa daha sonra cəhd edin.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var success = await _reservationService.DeleteReservationAsync(id, userId);

                if (success)
                {
                    TempData["Success"] = "Rezervasiya uğurla silindi.";
                }
                else
                {
                    TempData["Error"] = "Rezervasiya silinərkən xəta baş verdi.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sistem xətası baş verdi.";
            }

            return RedirectToAction("Index");
        }
    }
}