using Train_Project.DTOs.Rooms;

namespace Train_Project.Services.Interfaces
{
    public interface IStandardRoomServices
    {
        Task<bool> CreateRoomAsync(CreateRoomDto dto);

        Task<object?> GetRoomByIdAsync(int id);
        Task<List<object>> GetAllRoomsAsync();

        Task<bool> UpdateRoomAsync(int id, UpdateRoomDto dto);

        Task<bool> DeleteRoomAsync(int id);
        Task<bool> CheckRoomAvailabilityAsync(
    int roomId,
    DateOnly from,
    DateOnly to);


    }
}