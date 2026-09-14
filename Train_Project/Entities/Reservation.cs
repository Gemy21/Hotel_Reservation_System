using System;
using System.Collections.Generic;

namespace Train_Project.Entities;

public partial class Reservation
{
    public int Id { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public string? Status { get; set; }

    public int? HotelId { get; set; }

    public int? RoomId { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual Room? Room { get; set; }
}
