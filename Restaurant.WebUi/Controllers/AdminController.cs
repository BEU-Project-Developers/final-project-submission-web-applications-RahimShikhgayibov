using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebUi.Data;
using Restaurant.WebUi.Models;
using Restaurant.WebUi.Services.interfaces;
using Restaurant.WebUi.ViewModel;

namespace Restaurant.WebUi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IReservationService _reservationService;

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
                              ApplicationDbContext context, IReservationService reservationService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserManagementViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var reservationCount = await _context.Reservations.CountAsync(r => r.UserId == user.Id);

                userViewModels.Add(new UserManagementViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = roles.ToList(),
                    ReservationCount = reservationCount
                });
            }

            // Rezervasiya statistikaları
            var totalReservations = await _context.Reservations.CountAsync();
            var pendingReservations = await _context.Reservations.CountAsync(r => r.Status == "Pending");
            var confirmedReservations = await _context.Reservations.CountAsync(r => r.Status == "Confirmed");
            var todayReservations = await _context.Reservations
                .CountAsync(r => r.ReservationDate.Date == DateTime.Today);

            ViewBag.TotalReservations = totalReservations;
            ViewBag.PendingReservations = pendingReservations;
            ViewBag.ConfirmedReservations = confirmedReservations;
            ViewBag.TodayReservations = todayReservations;

            return View(userViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserReservations(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Json(new { success = false, message = "İstifadəçi tapılmadı." });
            }

            var reservations = await _context.Reservations
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedDate)
                .Select(r => new
                {
                    id = r.Id,
                    customerName = r.CustomerName,
                    email = r.Email,
                    phone = r.Phone,
                    reservationDate = r.ReservationDate.ToString("dd.MM.yyyy"),
                    reservationTime = r.ReservationTime.ToString(@"hh\:mm"),
                    guestCount = r.GuestCount,
                    status = r.Status,
                    specialRequests = r.SpecialRequests,
                    createdDate = r.CreatedDate.ToString("dd.MM.yyyy HH:mm")
                })
                .ToListAsync();

            return Json(new
            {
                success = true,
                reservations = reservations,
                userName = $"{user.FirstName} {user.LastName}"
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservationStatus(int reservationId, string status)
        {
            try
            {
                var reservation = await _context.Reservations.FindAsync(reservationId);
                if (reservation == null)
                {
                    return Json(new { success = false, message = "Rezervasiya tapılmadı." });
                }

                var validStatuses = new[] { "Pending", "Confirmed", "Cancelled" };
                if (!validStatuses.Contains(status))
                {
                    return Json(new { success = false, message = "Yanlış status." });
                }

                reservation.Status = status;
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Rezervasiya statusu yeniləndi." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Xəta baş verdi." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            try
            {
                var reservation = await _context.Reservations.FindAsync(reservationId);
                if (reservation == null)
                {
                    return Json(new { success = false, message = "Rezervasiya tapılmadı." });
                }

                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Rezervasiya silindi." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Xəta baş verdi." });
            }
        }

        // Mövcud metodlar dəyişməz qalır...
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Error"] = "İstifadəçi ID-si tapılmadı.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["Error"] = "İstifadəçi tapılmadı.";
                return RedirectToAction("Index");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.ToListAsync();

            var reservations = await _context.Reservations
                .Where(r => r.UserId == user.Id)
                .OrderByDescending(r => r.CreatedDate)
                .Select(r => new ReservationViewModel
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
                })
                .ToListAsync();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                Email = user.Email ?? "",
                UserName = user.Email ?? "",
                UserRoles = userRoles.ToList(),
                AllRoles = allRoles.Select(r => r.Name).ToList(),
                SelectedRoles = userRoles.ToList(),
                Reservations = reservations
            };

            return View(model);
        }

        // Digər mövcud metodlar (EditUser POST, DeleteUser, AssignRole, RemoveRole, CreateRoles) dəyişməz qalır...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            model.UserName = model.Email;

            if (!ModelState.IsValid)
            {
                var allRoles = await _roleManager.Roles.ToListAsync();
                model.AllRoles = allRoles.Select(r => r.Name).ToList();

                var reservations = await _context.Reservations
                    .Where(r => r.UserId == model.Id)
                    .OrderByDescending(r => r.CreatedDate)
                    .Select(r => new ReservationViewModel
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
                    })
                    .ToListAsync();
                model.Reservations = reservations;

                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                TempData["Error"] = "İstifadəçi tapılmadı.";
                return RedirectToAction("Index");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                var allRoles = await _roleManager.Roles.ToListAsync();
                model.AllRoles = allRoles.Select(r => r.Name).ToList();
                return View(model);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = model.SelectedRoles ?? new List<string>();

            var rolesToAdd = selectedRoles.Except(userRoles);
            var rolesToRemove = userRoles.Except(selectedRoles);

            if (rolesToRemove.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            if (rolesToAdd.Any())
            {
                await _userManager.AddToRolesAsync(user, rolesToAdd);
            }

            TempData["Success"] = "İstifadəçi məlumatları uğurla yeniləndi.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "İstifadəçi ID-si tapılmadı." });
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Json(new { success = false, message = "İstifadəçi tapılmadı." });
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && currentUser.Id == user.Id)
            {
                return Json(new { success = false, message = "Özünüzü silə bilməzsiniz." });
            }

            var userReservations = await _context.Reservations.Where(r => r.UserId == user.Id).ToListAsync();
            _context.Reservations.RemoveRange(userReservations);
            await _context.SaveChangesAsync();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "İstifadəçi və rezervasiyaları uğurla silindi." });
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Json(new { success = false, message = $"İstifadəçi silinərkən xəta baş verdi: {errors}" });
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleName))
            {
                return Json(new { success = false, message = "Parametrlər düzgün deyil." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Json(new { success = false, message = "İstifadəçi tapılmadı." });
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                return Json(new { success = false, message = "Rol tapılmadı." });
            }

            var isInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (isInRole)
            {
                return Json(new { success = false, message = "İstifadəçi artıq bu rola malikdir." });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Rol uğurla təyin edildi." });
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Json(new { success = false, message = $"Rol təyin edilərkən xəta baş verdi: {errors}" });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleName))
            {
                return Json(new { success = false, message = "Parametrlər düzgün deyil." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Json(new { success = false, message = "İstifadəçi tapılmadı." });
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && currentUser.Id == user.Id && roleName == "Admin")
            {
                return Json(new { success = false, message = "Öz admin rolunuzu silə bilməzsiniz." });
            }

            var isInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (!isInRole)
            {
                return Json(new { success = false, message = "İstifadəçi bu rola malik deyil." });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Rol uğurla silindi." });
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Json(new { success = false, message = $"Rol silinərkən xəta baş verdi: {errors}" });
        }

        public async Task<IActionResult> CreateRoles()
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    var adminRole = new AppRole { Name = "Admin" };
                    await _roleManager.CreateAsync(adminRole);
                }

                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    var userRole = new AppRole { Name = "User" };
                    await _roleManager.CreateAsync(userRole);
                }

                TempData["Success"] = "Rollar uğurla yaradıldı.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Rollar yaradılarkən xəta baş verdi: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}