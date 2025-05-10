using System;
using System.Collections.Generic;

namespace ArtGalleryAPI.Models;

public partial class RoleType
{
    public int IdroleType { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
