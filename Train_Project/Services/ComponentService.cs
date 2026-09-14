using Train_Project.Data;
using Train_Project.Services.Interfaces;

namespace Train_Project.Services
{
    public class ComponentService : IComponentService
    {
        private readonly AppDbContext _appDbContext;

        public ComponentService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
