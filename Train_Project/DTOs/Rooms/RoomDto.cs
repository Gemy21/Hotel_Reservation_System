using static System.Net.Mime.MediaTypeNames;

namespace Train_Project.DTOs.Rooms
{
public class RoomDto
    {
        public int Id { get; set; }
        public int? RoomNumber { get; set; }
        public int? Price { get; set; }
        public bool IsAvailable { get; set; }
        public string RoomType { get; set; } = null!;
    }

}
