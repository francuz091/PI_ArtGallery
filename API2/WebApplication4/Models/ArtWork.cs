using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class ArtWork
{
    public int IdartWork { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }

    public decimal? Price { get; set; }

    public DateTime? PublicationDate { get; set; }

    public int? UserId { get; set; }

    public int? ArtWorkTypeId { get; set; }

    public virtual ArtWorkType? ArtWorkType { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? User { get; set; }
}
