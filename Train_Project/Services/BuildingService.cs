using Microsoft.EntityFrameworkCore;
using Train_Project.Data;
using Train_Project.DTOs.Buildings;
using Train_Project.Entities;
using Train_Project.Services.Interfaces;

namespace Train_Project.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly AppDbContext _appDbContext;

        public BuildingService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<BuildingResponseDto>> GetAllAsync()
        {
            return await _appDbContext.Buildings.Select
                (b => new BuildingResponseDto { Id = b.Id, Location = b.Location }).ToListAsync();
        }

        public async Task<BuildingResponseDto> GetByIdAsync(int id)
        {
            var building = await _appDbContext.Buildings.FindAsync(id);
            if (building == null)
                return null;

            return new BuildingResponseDto
            {
                Id = building.Id,
                Location = building.Location
            };

        }

        public async Task<BuildingResponseDto> CreateAsync(CreateBuildingDto dto)
        {
            var building = new Building
            {
                HotelId = dto.HotelId,
                Location = dto.Location
            };
            _appDbContext.Buildings.Add(building);
            await _appDbContext.SaveChangesAsync();

            return new BuildingResponseDto
            {
                Id = building.Id,
                Location = building.Location
            };
        }
        public async Task<BuildingResponseDto> UpdateAsync(int id, UpdateBuildingDto dto)
        {
            var building = await _appDbContext.Buildings.FindAsync(id);
            if (building == null)
                return null;
            building.Location = dto.Location;
            await _appDbContext.SaveChangesAsync();

            return new BuildingResponseDto
            {
                Id = building.Id,
                Location = building.Location
            };

        }
        public async Task<bool> DeleteAsync(int id)
        {
            var building = await _appDbContext.Buildings.Include(b => b.Rooms)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (building == null)
                return false;

            if (building.Rooms.Any())
                throw new InvalidOperationException("Cannot delete building with existing rooms");

            _appDbContext.Buildings.Remove(building);
            await _appDbContext.SaveChangesAsync();
            return true;

        }
    }
}
