using Train_Project.DTOs.Buildings;

namespace Train_Project.Services.Interfaces
{
    public interface IBuildingService
    {

        Task<List<BuildingResponseDto>> GetAllAsync();
        Task<BuildingResponseDto> GetByIdAsync(int id);
        Task<BuildingResponseDto> CreateAsync(CreateBuildingDto dto);
        Task<BuildingResponseDto> UpdateAsync(int id, UpdateBuildingDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
