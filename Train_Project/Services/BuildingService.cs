using Train_Project.Data;
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
    }
}
