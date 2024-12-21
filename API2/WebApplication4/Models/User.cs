using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class User
{
    public int Iduser { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte[]? Picture { get; set; }

    public int? RoleTypeId { get; set; }

    public virtual ICollection<ArtWork> ArtWorks { get; set; } = new List<ArtWork>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual RoleType? RoleType { get; set; }
}
