using System;
using System.Collections.Generic;

namespace ArtGalleryAPI.Models;

public partial class Order
{
    public int Idorder { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? UserId { get; set; }

    public int? PaymentTypeId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual PaymentType? PaymentType { get; set; }

    public virtual User? User { get; set; }
}
