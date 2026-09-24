using Train_Project.DTOs.Reservations;

namespace Train_Project.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<IEnumerable<ReservationDto>> GetAvailableAsync();
        Task<ReservationDto?> GetByIdAsync(int id);
        Task<ReservationDto> CreateAsync(CreateReservationsDto dto);
        Task<ReservationDto?> UpdateAsync(int id, UpdateReservationsDto dto);
        Task<bool> CancelAsync(int id);
    }
}