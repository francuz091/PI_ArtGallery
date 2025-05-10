using System;
using System.Collections.Generic;

namespace ArtGalleryAPI.Models;

public partial class PaymentType
{
    public int IdpaymentType { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
