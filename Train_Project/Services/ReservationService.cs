using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Train_Project.Data;
using Train_Project.DTOs.Reservations;
using Train_Project.Entities;
using Train_Project.Services.Interfaces;

namespace Train_Project.Services
{
    public class ReservationService : IReservationService
    {
        private const string Confirmed = "Confirmed";
        private const string Cancelled = "Cancelled";

        private readonly AppDbContext _db;

        public ReservationService(AppDbContext db)
        {
            _db = db;
        }

        private static readonly Expression<Func<Reservation, ReservationDto>> ToDto = r => new ReservationDto
        {
            Id = r.Id,
            HotelId = r.HotelId,
            HotelName = r.Hotel != null ? r.Hotel.Name : null,
            RoomId = r.RoomId,
            CustomerId = r.CustomerId,
            FromDate = r.FromDate,
            ToDate = r.ToDate,
            Status = r.Status
        };

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            return await _db.Reservations
                .AsNoTracking()
                .Select(ToDto)
                .ToListAsync();
        }

        public async Task<IEnumerable<ReservationDto>> GetAvailableAsync()
        {
            return await _db.Reservations
                .AsNoTracking()
                .Where(r => r.Room != null && r.Room.IsAvailable == true)
                .Select(ToDto)
                .ToListAsync();
        }

        public async Task<ReservationDto?> GetByIdAsync(int id)
        {
            return await _db.Reservations
                .AsNoTracking()
                .Where(r => r.Id == id)
                .Select(ToDto)
                .FirstOrDefaultAsync();
        }

        public async Task<ReservationDto> CreateAsync(CreateReservationsDto dto)
        {
            ValidateDates(dto.FromDate, dto.ToDate);

            var roomExists = await _db.Rooms.AnyAsync(r => r.Id == dto.RoomId);
            if (!roomExists)
                throw new InvalidOperationException("Room not found.");

            if (await HasOverlap(dto.RoomId, dto.FromDate, dto.ToDate, null))
                throw new InvalidOperationException("Room is already reserved in this period.");

            var entity = new Reservation
            {
                HotelId = dto.HotelId,
                RoomId = dto.RoomId,
                CustomerId = dto.CustomerId,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                Status = Confirmed
            };

            _db.Reservations.Add(entity);
            await _db.SaveChangesAsync();

            return (await GetByIdAsync(entity.Id))!;
        }

        public async Task<ReservationDto?> UpdateAsync(int id, UpdateReservationsDto dto)
        {
            var entity = await _db.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (entity == null) return null;

            if (entity.Status == Cancelled)
                throw new InvalidOperationException("Cannot update a cancelled reservation.");

            ValidateDates(dto.FromDate, dto.ToDate);

            if (entity.RoomId.HasValue &&
                await HasOverlap(entity.RoomId.Value, dto.FromDate, dto.ToDate, entity.Id))
                throw new InvalidOperationException("Room is already reserved in this period.");

            entity.FromDate = dto.FromDate;
            entity.ToDate = dto.ToDate;
            await _db.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> CancelAsync(int id)
        {
            var entity = await _db.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (entity == null) return false;

            entity.Status = Cancelled;
            await _db.SaveChangesAsync();
            return true;
        }


        private static void ValidateDates(DateOnly from, DateOnly to)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (from < today)
                throw new InvalidOperationException("FromDate cannot be in the past.");
            if (to <= from)
                throw new InvalidOperationException("ToDate must be after FromDate.");
        }

        private Task<bool> HasOverlap(int roomId, DateOnly from, DateOnly to, int? excludeId)
        {
            return _db.Reservations.AnyAsync(r =>
                r.RoomId == roomId &&
                r.Status != Cancelled &&
                (excludeId == null || r.Id != excludeId) &&
                r.FromDate < to && r.ToDate > from);
        }
    }
}