using Restaurant.WebUi.Models;

namespace Restaurant.WebUi.Services.interfaces
{
    public interface IReservationService
    {
        Task<bool> CreateReservationAsync(Reservation reservation);
        Task<List<Reservation>> GetUserReservationsAsync(string userId);
        Task<bool> DeleteReservationAsync(int id, string userId);
    }
}
