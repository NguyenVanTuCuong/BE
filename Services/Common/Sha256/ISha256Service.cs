using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Sha256
{
    public interface ISha256Service
    {
            string Hash(string input);
            bool Verify(string input, string hash);
    }
}
