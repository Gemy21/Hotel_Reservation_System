using Train_Project.DTOs.Rooms;

namespace Train_Project.Services.Interfaces
{
    public interface IVipRoomService
    {
        Task<IEnumerable<VipRoomDto>> GetAllVipRoomsAsync();
    }
}
