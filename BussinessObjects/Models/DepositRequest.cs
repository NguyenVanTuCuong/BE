using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObjects.Enums;

namespace BussinessObjects.Models
{
    public class DepositRequest
    {
        public Guid DepositRequestId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid OrchidId { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public virtual Orchid Orchid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
