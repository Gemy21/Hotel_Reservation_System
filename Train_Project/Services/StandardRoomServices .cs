using Microsoft.EntityFrameworkCore;
using Train_Project.Data;
using Train_Project.DTOs.Rooms;
using Train_Project.Entities;
using Train_Project.Services.Interfaces;

namespace Train_Project.Services
{
    public class StandardRoomServices : IStandardRoomServices
    {
        private readonly AppDbContext _appDbContext;

        public StandardRoomServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> CreateRoomAsync(CreateRoomDto dto)
        {
            // Check Building exists
            var buildingExists = await _appDbContext.Buildings
                .AnyAsync(x => x.Id == dto.BuildingId);

            if (!buildingExists)
                return false;

            // Validate Room Number
            var roomNumberExists = await _appDbContext.Rooms
                .AnyAsync(x =>
                    x.RommNumber == dto.RoomNumber &&
                    x.BuildingId == dto.BuildingId);

            if (roomNumberExists)
                return false;

            // Validate Price
            if (dto.Price <= 0)
                return false;

            // Validate Room Type
            if (!Enum.TryParse<RoomType>(
                dto.RoomType,
                true,
                out var roomType))
            {
                return false;
            }

            // Validate Components
            var componentIds = dto.ComponentIds
                .Distinct()
                .ToList();

            var componentsCount = await _appDbContext.Components
                .CountAsync(x => componentIds.Contains(x.Id));

            if (componentsCount != componentIds.Count)
                return false;

            var room = new Room
            {
                RommNumber = dto.RoomNumber,
                Price = dto.Price,
                BuildingId = dto.BuildingId,
                HotelId = dto.HotelId,
                RoomType = roomType,
                IsAvailable = true
            };

            _appDbContext.Rooms.Add(room);

            await _appDbContext.SaveChangesAsync();

            foreach (var componentId in componentIds)
            {
                var componentExists = await _appDbContext.Components
                    .AnyAsync(x => x.Id == componentId);

                if (!componentExists)
                    return false;

                _appDbContext.RoomComponents.Add(new RoomComponent
                {
                    RoomId = room.Id,
                    ComponentId = componentId
                });
            }

            await _appDbContext.SaveChangesAsync();

            return true;

            return true;
        }

        public async Task<object?> GetRoomByIdAsync(int id)
        {
            var room = await _appDbContext.Rooms
                .Include(x => x.Building)
                .Include(x => x.Hotel)
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    RoomNumber = x.RommNumber,
                    x.Price,
                    x.IsAvailable,
                    CheckIn = x.ChechIn,
                    CheckOut = x.ChekOut,
                    x.RoomType,

                    Building = x.Building == null
                        ? null
                        : new
                        {
                            x.Building.Id,
                            x.Building.Location
                        },

                    Hotel = x.Hotel == null
                        ? null
                        : new
                        {
                            x.Hotel.Id
                        },

                        Components = _appDbContext.RoomComponents
    .Where(rc => rc.RoomId == x.Id)
    .Select(rc => new
    {
        rc.Component!.Id,
        rc.Component.Name,
        rc.Component.Type
    })
    .ToList()
                })
                .FirstOrDefaultAsync();

            return room;
        }

        public async Task<bool> UpdateRoomAsync(int id, UpdateRoomDto dto)
        {
            var room = await _appDbContext.Rooms
                .FirstOrDefaultAsync(x => x.Id == id);

            if (room == null)
                return false;

            // Validate Building
            var buildingExists = await _appDbContext.Buildings
                .AnyAsync(x => x.Id == dto.BuildingId);

            if (!buildingExists)
                return false;

            // Validate Room Number
            var roomNumberExists = await _appDbContext.Rooms
                .AnyAsync(x =>
                    x.RommNumber == dto.RoomNumber &&
                    x.BuildingId == dto.BuildingId &&
                    x.Id != id);

            if (roomNumberExists)
                return false;

            // Validate Price
            if (dto.Price <= 0)
                return false;

            // Validate Room Type
            if (!Enum.TryParse<RoomType>(
                dto.RoomType,
                true,
                out var roomType))
            {
                return false;
            }
            var componentIds = dto.ComponentIds
    .Distinct()
    .ToList();

            var componentsCount = await _appDbContext.Components
                .CountAsync(x => componentIds.Contains(x.Id));

            if (componentsCount != componentIds.Count)
                return false;
            room.RommNumber = dto.RoomNumber;
            room.Price = dto.Price;
            room.BuildingId = dto.BuildingId;
            room.HotelId = dto.HotelId;
            room.RoomType = roomType;

            var oldComponents = await _appDbContext.RoomComponents
    .Where(x => x.RoomId == id)
    .ToListAsync();

            _appDbContext.RoomComponents.RemoveRange(oldComponents);

            foreach (var componentId in componentIds)
            {
                _appDbContext.RoomComponents.Add(new RoomComponent
                {
                    RoomId = id,
                    ComponentId = componentId
                });
            }

            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _appDbContext.Rooms
                .FirstOrDefaultAsync(x => x.Id == id);

            if (room == null)
                return false;

            var hasReservations = await _appDbContext.Reservations
                .AnyAsync(x => x.RoomId == id);

            if (hasReservations)
                return false;

            _appDbContext.Rooms.Remove(room);

            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CheckRoomAvailabilityAsync(
    int roomId,
    DateOnly from,
    DateOnly to)
        {
            if (from >= to)
                return false;

            var roomExists = await _appDbContext.Rooms
                .AnyAsync(x => x.Id == roomId);

            if (!roomExists)
                return false;

            var hasActiveReservation = await _appDbContext.Reservations
                .AnyAsync(x =>
                    x.RoomId == roomId &&
                    x.Status != "Cancelled" &&
                    x.FromDate < to &&
                    x.ToDate > from);

            return !hasActiveReservation;
        }

        public async Task<List<object>> GetAllRoomsAsync()
        {
            var rooms = await _appDbContext.Rooms
                .Include(x => x.Building)
                .Include(x => x.Hotel)
                .Select(x => (object)new
                {
                    x.Id,
                    RoomNumber = x.RommNumber,
                    x.Price,
                    x.IsAvailable,
                    CheckIn = x.ChechIn,
                    CheckOut = x.ChekOut,
                    x.RoomType,

                    Building = x.Building == null
                        ? null
                        : new
                        {
                            x.Building.Id,
                            x.Building.Location
                        },

                    Hotel = x.Hotel == null
                        ? null
                        : new
                        {
                            x.Hotel.Id
                        }
                })
                .ToListAsync();

            return rooms;
        }
    }
}