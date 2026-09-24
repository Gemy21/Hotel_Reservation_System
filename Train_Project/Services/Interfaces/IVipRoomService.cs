using Train_Project.DTOs.Rooms;

namespace Train_Project.Services.Interfaces
{
    public interface IVipRoomService
    {
        Task<IEnumerable<VipRoomDto>> GetAllVipRoomsAsync();
        Task<VipRoomDto?> GetVipRoomByIdAsync(int id);
        Task<VipRoomDto> CreateVipRoomAsync(CreateVipRoomDto dto);
        Task<VipRoomDto?> UpdateVipRoomAsync(int id, UpdateVipRoomDto dto);
        Task<bool> DeleteVipRoomAsync(int id);
        Task<IEnumerable<RoomServiceDto>?> GetVipRoomServicesAsync(int id);
        Task<LateCheckOutResultDto?> RequestLateCheckOutAsync(int id, LateCheckOutRequestDto dto);
    }
}
