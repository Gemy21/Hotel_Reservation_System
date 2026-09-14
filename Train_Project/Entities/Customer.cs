using System;
using System.Collections.Generic;

namespace Train_Project.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long? Phone { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
