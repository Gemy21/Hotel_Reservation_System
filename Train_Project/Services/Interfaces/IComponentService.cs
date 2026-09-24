using Train_Project.DTOs.Components;

namespace Train_Project.Services.Interfaces
{
    public interface IComponentService
    {
        public Task<IEnumerable<ComponentsDto>> GetAllComponents();
        public Task<ComponentsDto> GetComponentById(int id);
        public Task CreateComponent(CreateComponentsDto componentDto);
        public Task DeleteComponent(int id);
    }
}
