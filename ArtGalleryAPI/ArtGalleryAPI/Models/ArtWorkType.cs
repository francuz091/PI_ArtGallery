using System;
using System.Collections.Generic;

namespace ArtGalleryAPI.Models;

public partial class ArtWorkType
{
    public int IdartWorkType { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<ArtWork> ArtWorks { get; set; } = new List<ArtWork>();
}
