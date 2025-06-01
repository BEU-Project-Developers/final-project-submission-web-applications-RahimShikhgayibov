using Microsoft.EntityFrameworkCore;
using Restaurant.WebUi.Data;
using Restaurant.WebUi.Models;
using Restaurant.WebUi.Services.interfaces;


namespace Restaurant.WebUi.Services.Concrete
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateReservationAsync(Reservation reservation)
        {
            try
            {
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Reservation>> GetUserReservationsAsync(string userId)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> DeleteReservationAsync(int id, string userId)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
