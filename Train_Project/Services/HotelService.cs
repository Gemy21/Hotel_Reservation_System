using Train_Project.Data;
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
    }
}
