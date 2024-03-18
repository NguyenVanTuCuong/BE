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

    public enum DepositStatus
    {
        [Description("Available")]
        Available,
        [Description("Deposited")]
        Deposited,
    }
}
