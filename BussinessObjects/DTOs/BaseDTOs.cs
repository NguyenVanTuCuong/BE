using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects.DTOs
{
    public class AuthTokens
    {

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class AuthResponse<T> where T : class
    {
        public T Data { get; set; }
        public AuthTokens? AuthTokens { get; set; }
    }
}
