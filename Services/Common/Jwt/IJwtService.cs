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
        public string GenerateToken(Guid userId, UserRole role);
        public Guid? GetUserIdFromContext(HttpContext context);
    }
}
