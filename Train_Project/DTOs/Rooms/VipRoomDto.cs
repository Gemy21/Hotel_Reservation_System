using System.ComponentModel.DataAnnotations;

namespace Train_Project.DTOs.Rooms
{
    public class VipRoomDto : RoomDto
    {
        public decimal LivingArea { get; set; }
        public bool LateCheckOutAllowed { get; set; }
        public TimeOnly? LateCheckOutTime { get; set; }
        public decimal LateCheckOutFee { get; set; }
    }

    public class CreateVipRoomDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int RoomNumber { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        public bool IsAvailable { get; set; } = true;
        public int? HotelId { get; set; }
        public int? BuildingId { get; set; }

        [Range(1, double.MaxValue)]
        public decimal LivingArea { get; set; }

        public bool LateCheckOutAllowed { get; set; }
        public TimeOnly? LateCheckOutTime { get; set; }

        [Range(0, double.MaxValue)]
        public decimal LateCheckOutFee { get; set; }
    }

    public class UpdateVipRoomDto
    {
        [Range(1, int.MaxValue)]
        public int? Price { get; set; }
        public bool? IsAvailable { get; set; }

        [Range(1, double.MaxValue)]
        public decimal LivingArea { get; set; }

        public bool LateCheckOutAllowed { get; set; }
        public TimeOnly? LateCheckOutTime { get; set; }

        [Range(0, double.MaxValue)]
        public decimal LateCheckOutFee { get; set; }
    }

    public class RoomServiceDto
    {
        public int ComponentId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
    }

    public class LateCheckOutRequestDto
    {
        [Required]
        public int ReservationId { get; set; }
    }

    public class LateCheckOutResultDto
    {
        public int ReservationId { get; set; }
        public bool Approved { get; set; }
        public decimal ExtraCharge { get; set; }
        public string Message { get; set; } = null!;
    }


}
