using BussinessObjects.Enums;
using System;
using System.Collections.Generic;

namespace BussinessObjects.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? WalletAddress { get; set; }

    public DateTime? Birthday { get; set; }

    public UserStatus Status { get; set; }

    public UserRole Role { get; set; }

    public virtual ICollection<Orchid> Orchids { get; set; } = new List<Orchid>();
}
