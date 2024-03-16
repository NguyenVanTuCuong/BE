﻿using BussinessObjects.Enums;
using BussinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Jwt
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(Guid userId, UserRole role)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, userId.ToString()),
                new (ClaimTypes.Role, role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build().GetSection("Jwt")["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public Guid? GetUserIdFromContext(HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            if (identity == null) return null;

            var userClaims = identity.Claims;
            var userId = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId != null ?Guid.Parse(userId) : null;
        }
    }
}
