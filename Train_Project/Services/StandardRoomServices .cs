using Train_Project.Data;
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
    }
}
