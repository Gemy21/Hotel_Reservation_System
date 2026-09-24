namespace Train_Project.DTOs.Buildings
{
    public class CreateBuildingDto
    {
        public string Location { get; set; }

        public int HotelId { get; set; }

    }

    public class UpdateBuildingDto
    {
        public string Location { get; set; }
    }

    public class BuildingResponseDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
    }
}
