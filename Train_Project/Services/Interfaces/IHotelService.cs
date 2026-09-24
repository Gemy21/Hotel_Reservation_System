using Train_Project.DTOs.Hotel;

namespace Train_Project.Services.Interfaces
{
    public interface IHotelService
    {
        Task<HotelResponseDto> CreateAsync(CreateHotelDto dto);

        Task<List<HotelResponseDto>> GetAllAsync();

        Task<HotelResponseDto?> GetByIdAsync(int id);

        Task<HotelResponseDto?> UpdateAsync(int id, UpdateHotelDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
