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
            var vipRooms = await _appDbContext.VipRooms
                .Include(v => v.Room)
                .ToListAsync();

            return vipRooms.Select(ToDto).ToList();
        }

        public async Task<VipRoomDto?> GetVipRoomByIdAsync(int id)
        {
            var vipRoom = await _appDbContext.VipRooms
                .Include(v => v.Room)
                .FirstOrDefaultAsync(v => v.Id == id);

            return vipRoom is null ? null : ToDto(vipRoom);
        }

        public async Task<VipRoomDto> CreateVipRoomAsync(CreateVipRoomDto dto)
        {
            if (dto.RoomNumber <= 0)
                throw new ArgumentException("Room number is required and must be greater than zero.");

            if (dto.Price <= 0)
                throw new ArgumentException("Price is required and must be greater than zero.");

            var roomNumberTaken = await _appDbContext.Rooms.AnyAsync(r => r.RommNumber == dto.RoomNumber);
            if (roomNumberTaken)
                throw new InvalidOperationException($"A room with number {dto.RoomNumber} already exists.");

            if (dto.HotelId.HasValue && !await _appDbContext.Hotels.AnyAsync(h => h.Id == dto.HotelId))
                throw new ArgumentException($"Hotel with id {dto.HotelId} was not found.");

            if (dto.BuildingId.HasValue && !await _appDbContext.Buildings.AnyAsync(b => b.Id == dto.BuildingId))
                throw new ArgumentException($"Building with id {dto.BuildingId} was not found.");

            if (dto.LivingArea <= 0)
                throw new ArgumentException("Living area must be greater than zero for a VIP room.");

            await using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            var nextId = (await _appDbContext.Rooms.MaxAsync(r => (int?)r.Id) ?? 0) + 1;

            var room = new Room
            {
                Id = nextId,
                RommNumber = dto.RoomNumber,
                Price = dto.Price,
                IsAvailable = dto.IsAvailable,
                HotelId = dto.HotelId,
                BuildingId = dto.BuildingId,
                RoomType = RoomType.VIP
            };
            _appDbContext.Rooms.Add(room);
            await _appDbContext.SaveChangesAsync();

            var vipRoom = new VipRoom
            {
                Id = room.Id,
                LivingArea = dto.LivingArea,
                LateCheckOutAllowed = dto.LateCheckOutAllowed,
                LateCheckOutTime = dto.LateCheckOutTime,
                LateCheckOutFee = dto.LateCheckOutFee
            };
            _appDbContext.VipRooms.Add(vipRoom);
            await _appDbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            vipRoom.Room = room;
            return ToDto(vipRoom);
        }

        public async Task<VipRoomDto?> UpdateVipRoomAsync(int id, UpdateVipRoomDto dto)
        {
            var vipRoom = await _appDbContext.VipRooms
                .Include(v => v.Room)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vipRoom is null)
                return null;

            if (dto.LivingArea <= 0)
                throw new ArgumentException("Living area must be greater than zero.");

            vipRoom.LivingArea = dto.LivingArea;
            vipRoom.LateCheckOutAllowed = dto.LateCheckOutAllowed;
            vipRoom.LateCheckOutTime = dto.LateCheckOutTime;
            vipRoom.LateCheckOutFee = dto.LateCheckOutFee;

            if (dto.Price.HasValue)
                vipRoom.Room.Price = dto.Price;

            if (dto.IsAvailable.HasValue)
                vipRoom.Room.IsAvailable = dto.IsAvailable.Value;

            await _appDbContext.SaveChangesAsync();

            return ToDto(vipRoom);
        }

        public async Task<bool> DeleteVipRoomAsync(int id)
        {
            var vipRoom = await _appDbContext.VipRooms.FirstOrDefaultAsync(v => v.Id == id);
            if (vipRoom is null)
                return false;

            var room = await _appDbContext.Rooms.FindAsync(id);

            _appDbContext.VipRooms.Remove(vipRoom);
            if (room is not null)
                _appDbContext.Rooms.Remove(room);

            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RoomServiceDto>?> GetVipRoomServicesAsync(int id)
        {
            var vipRoomExists = await _appDbContext.VipRooms.AnyAsync(v => v.Id == id);
            if (!vipRoomExists)
                return null;

            return await _appDbContext.RoomComponents
    .Where(rc => rc.RoomId == id)
    .Select(rc => new RoomServiceDto
    {
        ComponentId = rc.ComponentId,
        Name = rc.Component!.Name ?? string.Empty,
        Type = rc.Component!.Type ?? string.Empty
    })
    .ToListAsync();
        }

        public async Task<LateCheckOutResultDto?> RequestLateCheckOutAsync(int id, LateCheckOutRequestDto dto)
        {
            var vipRoom = await _appDbContext.VipRooms.FirstOrDefaultAsync(v => v.Id == id);
            if (vipRoom is null)
                return null;

            var reservation = await _appDbContext.Reservations
                .FirstOrDefaultAsync(r => r.Id == dto.ReservationId && r.RoomId == id);

            if (reservation is null)
                throw new ArgumentException("Reservation was not found for this VIP room.");

            if (!string.Equals(reservation.Status, "Confirmed", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Late check-out can only be requested for a confirmed reservation.");

            var approved = vipRoom.LateCheckOutAllowed;
            var extraCharge = approved ? vipRoom.LateCheckOutFee : 0m;

            var result = new LateCheckOutResultDto
            {
                ReservationId = reservation.Id,
                Approved = approved,
                ExtraCharge = extraCharge,
                Message = approved
                    ? $"Late check-out approved. An extra charge of {extraCharge:0.00} applies."
                    : "Late check-out is not available for this room."
            };

            _appDbContext.LateCheckOutRequests.Add(new LateCheckOutRequest
            {
                ReservationId = reservation.Id,
                VipRoomId = vipRoom.Id,
                Approved = result.Approved,
                ExtraCharge = result.ExtraCharge,
                RequestedAt = DateTime.UtcNow
            });
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        private static VipRoomDto ToDto(VipRoom vipRoom) => new()
        {
            Id = vipRoom.Id,
            RoomNumber = vipRoom.Room.RommNumber,
            Price = vipRoom.Room.Price,
            IsAvailable = vipRoom.Room.IsAvailable,
            RoomType = vipRoom.Room.RoomType.ToString(),
            LivingArea = vipRoom.LivingArea,
            LateCheckOutAllowed = vipRoom.LateCheckOutAllowed,
            LateCheckOutTime = vipRoom.LateCheckOutTime,
            LateCheckOutFee = vipRoom.LateCheckOutFee
        };
    }
}
