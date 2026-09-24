namespace Train_Project.DTOs.Rooms
{
    public class CreateRoomDto
    {
        public int RoomNumber { get; set; }
        public int Price { get; set; }
        public int BuildingId { get; set; }
        public int HotelId { get; set; }
        public string RoomType { get; set; } = null!;
        public List<int> ComponentIds { get; set; } = new();
    }
}