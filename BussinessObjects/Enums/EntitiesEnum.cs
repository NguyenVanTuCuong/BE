using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects.Enums
{
    public enum UserRole
    {
        User,
        Administrator
    }

    public enum UserStatus
    {
        Active,
        Inactive,
    }

    public enum DepositStatus
    {
        [Description("Available")]
        Available,
        [Description("Pending")]
        Pending,
        [Description("Deposited")]
        Deposited,
    }

    public enum RequestStatus
    {
        [Description("Pending")]
        Pending,
        [Description("Approved")]
        Approved,
        [Description("Rejected")]
        Rejected,
    }
}
