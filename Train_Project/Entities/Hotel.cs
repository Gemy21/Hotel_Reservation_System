using System;
using System.Collections.Generic;

namespace Train_Project.Entities;

public partial class Hotel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
