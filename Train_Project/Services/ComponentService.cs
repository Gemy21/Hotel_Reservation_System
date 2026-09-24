using Train_Project.Entities;
using Train_Project.Data;
using Train_Project.DTOs.Components;
using Train_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Train_Project.Services
{
    public class ComponentService : IComponentService
    {
        private readonly AppDbContext _appDbContext;

        public ComponentService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<IEnumerable<ComponentsDto>> GetAllComponents()
        {
            return await _appDbContext.Set<Component>()
        .Select(c => new ComponentsDto
        {
            Id = c.Id,
            Name = c.Name ?? "undecalred"

        })
        .ToListAsync();
        }

        public async Task<ComponentsDto> GetComponentById(int id)
        {
            var component = await _appDbContext.Set<Component>().FindAsync(id);
            if (component == null)
            {
                return null;
            }
            return new ComponentsDto
            {
                Id = component.Id,
                Name = component.Name ?? "undecalred"
            };
        }

        public async Task CreateComponent(CreateComponentsDto componentDto)
        {
            var component = new Component
            {
                Name = componentDto.Name
            };
            _appDbContext.Set<Component>().Add(component);
            await _appDbContext.SaveChangesAsync();


        }

        public async Task DeleteComponent(int id)
        {
            var component = await _appDbContext.Set<Component>().FindAsync(id);
            if (component != null)
            {
                _appDbContext.Set<Component>().Remove(component);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
