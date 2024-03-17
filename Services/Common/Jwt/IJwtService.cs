using BussinessObjects.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Jwt
{
    public interface IJwtService
    {
        public Task<string> GenerateToken(Guid userId);
        public Guid? GetUserIdFromContext(HttpContext context);
    }
}
