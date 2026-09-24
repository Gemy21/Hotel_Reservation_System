namespace Train_Project.Entities;

public class LateCheckOutRequest
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public int VipRoomId { get; set; }
    public bool Approved { get; set; }
    public decimal ExtraCharge { get; set; }
    public DateTime RequestedAt { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;
    public virtual VipRoom VipRoom { get; set; } = null!;
}