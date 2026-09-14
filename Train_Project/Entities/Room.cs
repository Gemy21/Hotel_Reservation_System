using System;
using System.Collections.Generic;

namespace Train_Project.Entities;

public partial class Room
{
    public int Id { get; set; }

    public int? RommNumber { get; set; }

    public int? Price { get; set; }

    public bool IsAvailable { get; set; }

    public DateOnly? ChechIn { get; set; }

    public DateOnly? ChekOut { get; set; }

    public int? HotelId { get; set; }

    public int? BuildingId { get; set; }

    public virtual Building? Building { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public RoomType RoomType { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
