using BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }

        public string TransactionHash { get; set; } = null!;

        public float Amount { get; set; }

        public Guid OrchidId { get; set; }
        public virtual Orchid Orchid { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

}