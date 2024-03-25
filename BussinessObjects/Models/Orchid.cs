using BussinessObjects.Enums;
using System;
using System.Collections.Generic;

namespace BussinessObjects.Models;

public partial class Orchid
{
    public Guid OrchidId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string? Color { get; set; }
    public string? Species { get; set; }
    public string? Origin { get; set; }

    public Guid OwnerId { get; set; }

    public DepositStatus DepositedStatus { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }

    public virtual User Owner { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<DepositRequest> DepositRequests { get; set; } = new List<DepositRequest>();
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
