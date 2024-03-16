using BussinessObjects.Enums;
using System;
using System.Collections.Generic;

namespace BussinessObjects.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public UserRole Role { get; set; }

    public virtual ICollection<Orchid> Orchids { get; set; } = new List<Orchid>();
}
