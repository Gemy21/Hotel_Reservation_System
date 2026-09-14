using Train_Project.Data;
using Train_Project.Services.Interfaces;

namespace Train_Project.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _appDbContext;

        public ReservationService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
