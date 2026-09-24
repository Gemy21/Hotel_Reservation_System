namespace Train_Project.DTOs.Reservations
{
    // اللي بيرجع للعميل
    public class ReservationDto
    {
        public int Id { get; set; }
        public int? HotelId { get; set; }
        public string? HotelName { get; set; }
        public int? RoomId { get; set; }
        public int? CustomerId { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string? Status { get; set; }
    }

    public class CreateReservationsDto
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
    }

    public class UpdateReservationsDto
    {
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
    }
}