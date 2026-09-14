using System;
using System.Collections.Generic;

namespace Train_Project.Entities;

public partial class RoomComponent
{
    public int? RoomId { get; set; }

    public int? ComponentId { get; set; }

    public virtual Component? Component { get; set; }

    public virtual Room? Room { get; set; }
}
