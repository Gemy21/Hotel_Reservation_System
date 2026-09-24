using Microsoft.EntityFrameworkCore;
using Train_Project.Data;
using Train_Project.DTOs.Hotel;
using Train_Project.Entities;
using Train_Project.Services.Interfaces;

namespace Train_Project.Services
{
    public class HotelService : IHotelService
    {
        private readonly AppDbContext _appDbContext;

        public HotelService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<HotelResponseDto> CreateAsync(CreateHotelDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Hotel name is required");

            var hotel = new Hotel
            {
                Name = dto.Name
            };

            _appDbContext.Hotels.Add(hotel);
            await _appDbContext.SaveChangesAsync();

            return new HotelResponseDto
            {
                Id = hotel.Id,
                Name = hotel.Name
            };
        }

        public async Task<List<HotelResponseDto>> GetAllAsync()
        {
            return await _appDbContext.Hotels
              .Select(h => new HotelResponseDto
              {
                  Id = h.Id,
                  Name = h.Name
              })
              .ToListAsync();
        }

        public async Task<HotelResponseDto?> GetByIdAsync(int id)
        {
            return await _appDbContext.Hotels
              .Where(h => h.Id == id)
              .Select(h => new HotelResponseDto
              {
                  Id = h.Id,
                  Name = h.Name
              })
              .FirstOrDefaultAsync();
        }

        public async Task<HotelResponseDto?> UpdateAsync(
          int id,
          UpdateHotelDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Hotel name is required");

            var hotel = await _appDbContext.Hotels
              .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
                return null;

            hotel.Name = dto.Name;

            await _appDbContext.SaveChangesAsync();

            return new HotelResponseDto
            {
                Id = hotel.Id,
                Name = hotel.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var hotel = await _appDbContext.Hotels
              .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
                return false;

            var hasBuildings = await _appDbContext.Buildings
              .AnyAsync(b => b.HotelId == id);

            var hasRooms = await _appDbContext.Rooms
              .AnyAsync(r => r.HotelId == id);

            var hasReservations = await _appDbContext.Reservations
              .AnyAsync(r => r.HotelId == id);

            if (hasBuildings || hasRooms || hasReservations)

                return false;

            _appDbContext.Hotels.Remove(hotel);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
