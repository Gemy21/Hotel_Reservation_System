namespace Train_Project.DTOs.Hotel
{

    public class HotelResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
    public class CreateHotelDto
    {
        public string Name { get; set; } = null!;
    }
    public class UpdateHotelDto
    {
        public string Name { get; set; } = null!;
    }
}
