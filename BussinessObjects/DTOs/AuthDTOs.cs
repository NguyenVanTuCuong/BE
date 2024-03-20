using BussinessObjects.Enums;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using static BussinessObjects.DTOs.SignInDTO;

namespace BussinessObjects.DTOs
{
    public class SignInDTO
    {
        public class SignInRequest
        {
            [EmailAddress]
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class SignInResponseData
        {
            public Guid UserId { get; set; }
            [EmailAddress]
            public string Email { get; set; }
            public string Password { get; set; }
            public UserRole Role { get; set; }
        }
        public class SignInResponse : AuthResponse<SignInResponseData>
        {
            
        }
    }

    public class SignUpDTO
    {
        public class SignUpRequest
        {
            public string Email { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
        }
        public class SignUpResponseData
        {
            public Guid UserId { get; set; }
        }

        public class SignUpResponse : AuthResponse<SignUpResponseData>
        {
        }
    }

    public class GetProfileDTO
    {
        public class GetProfileRequestData
        {
        }

        public class GetProfileRequest : AuthRequest<GetProfileRequestData>
        {
        }

        public class GetProfileResponseData
        {
            public Guid UserId { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Birthday { get; set; }
            public string WalletAddress { get; set; }
            public UserRole Role { get; set; }
        }

        public class GetProfileResponse : AuthResponse<GetProfileResponseData>
        {
        }
    }
}