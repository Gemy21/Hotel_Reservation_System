using System;
using System.Collections.Generic;

namespace Train_Project.Entities;

public partial class Building
{
    public int Id { get; set; }

    public string Location { get; set; } = null!;

    public int? HotelId { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
