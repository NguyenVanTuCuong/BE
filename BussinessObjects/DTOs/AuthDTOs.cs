using BussinessObjects.Enums;

namespace BussinessObjects.DTOs
{
    public class SignInDTO
    {
        public class SignInRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class SignInResponseData
        {
            public Guid UserId { get; set; }
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
            public string Password { get; set; }
        }
        public class SignUpResponse
        {
            public Guid UserId { get; set; }
        }
    }
}