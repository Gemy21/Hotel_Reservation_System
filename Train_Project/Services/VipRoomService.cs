using Train_Project.Data;
using Train_Project.DTOs.Rooms;
using Train_Project.Entities;
using Train_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;   

namespace Train_Project.Services
{
    public class VipRoomService : IVipRoomService
    {
        private readonly AppDbContext _appDbContext;

        public VipRoomService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<VipRoomDto>> GetAllVipRoomsAsync()
        {
            return await _appDbContext.Set<Room>().Where(r => r.RoomType == RoomType.VIP)
        .Select(r => new VipRoomDto
        {
            Id = r.Id,
            Price = r.Price,
            IsAvailable = r.IsAvailable,
            RoomNumber = r.RommNumber,
            RoomType = r.RoomType.ToString(),
            NumberOfBeds = (int)Random.Shared.Next(3, 6),
            HasJacuzzi = true,
            HasPrivatePool = true
        })
        .ToListAsync();
        }
    }
}
