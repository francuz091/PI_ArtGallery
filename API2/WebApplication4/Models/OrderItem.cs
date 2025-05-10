using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class OrderItem
{
    public int IdorderItem { get; set; }

    public int? OrderId { get; set; }

    public int? ArtWorkId { get; set; }

    public virtual ArtWork? ArtWork { get; set; }

    public virtual Order? Order { get; set; }
}
