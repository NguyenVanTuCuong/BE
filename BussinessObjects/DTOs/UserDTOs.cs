using BussinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static BussinessObjects.DTOs.AddUserDTO;

namespace BussinessObjects.DTOs
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserStatus Status { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
    }

    public class GetUserListResponseData
    {
        public IList<UserDTO> users { get; set; }
        public double pages { get; set; }
    }

    public class GetUserListResponse : AuthResponse<GetUserListResponseData>
    {
    }

    public class AddUserDTO
    {
        public class AddUserRequestData
        {
            [Required(ErrorMessage = "Email address is required")]
            [EmailAddress(ErrorMessage = "Invalid email address")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Username is required")]
            [MinLength(3, ErrorMessage = "Username must be at least 6 characters long")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [MinLength(3, ErrorMessage = "Password must be at least 3 characters long")]
            [MaxLength(50, ErrorMessage = "Password can not be more than 20 characters")]
            public string Password { get; set; }

            [MaxLength(20, ErrorMessage = "First Name can not be more than 20 characters")]
            public string FirstName { get; set; }

            [MaxLength(20, ErrorMessage = "Last Name can not be more than characters")]
            public string LastName { get; set; }

            [DataType(DataType.Date)]
            public DateTime Birthday { get; set; }

            [Required(ErrorMessage = "User status is required")]
            public UserStatus Status { get; set; }

            [Required(ErrorMessage = "Role is required")]
            public UserRole Role { get; set; }
        }

        public class AddUserRequest : AuthRequest<AddUserRequestData>
        {
        }

        public class AddUserResponseData
        {
            public Guid UserId { get; set; }
        }

        public class AddUserResponse : AuthResponse<AddUserResponseData>
        {
        }
    }

    public class UpdateUserDTO
    {

        public class UpdateUserRequestData
        {
            [Required(ErrorMessage = "Email address is required")]
            [EmailAddress(ErrorMessage = "Invalid email address")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [MinLength(3, ErrorMessage = "Password must be at least 3 characters long")]
            [MaxLength(50, ErrorMessage = "Password can not be more than 20 characters")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Username is required")]
            [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
            public string Username { get; set; }

            [Required(ErrorMessage = "First name is required")]
            [MaxLength(20, ErrorMessage = "First Name can not be more than 20 characters")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Last name is required")]
            [MaxLength(20, ErrorMessage = "Last Name can not be more than 20 characters")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Birthday is required")]
            [DataType(DataType.Date)]
            public DateTime Birthday { get; set; }

            [Required(ErrorMessage = "Status is required")]
            public UserStatus Status { get; set; }

            [Required(ErrorMessage = "Role is required")]
            public UserRole Role { get; set; }
        }
        public class UpdateUserRequest : AuthRequest<UpdateUserRequestData>
        {
        }

        public class UpdateUserResponseData
        {
            public Guid UserId { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime Birthday { get; set; }
            [JsonConverter(typeof(JsonStringEnumConverter))]
            public UserStatus Status { get; set; }
        }

        public class UpdateUserResponse : AuthResponse<UpdateUserResponseData>
        {
        }
    }

    public class DetailsUserDTO
    {
        public class DetailsUserResponseData
        {
            public Guid UserId { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime Birthday { get; set; }
            [JsonConverter(typeof(JsonStringEnumConverter))]
            public UserStatus Status { get; set; }
            [JsonConverter(typeof(JsonStringEnumConverter))]
            public UserRole Role { get; set; }
        }

        public class DetailsUserResponse : AuthResponse<DetailsUserResponseData>
        {
        }
    }

    public class DeleteUserDTO
    {
        public class DeleteUserRequest : AuthRequest<DeleteUserRequestData>
        {
        }
        public class DeleteUserRequestData
        {
            public Guid UserId { get; set; }
        }

        public class DeleteUserResponseData
        {
            public Guid UserId { get; set; }
        }
        public class DeleteUserResponse : AuthResponse<DeleteUserResponseData>
        {
        }
    }

}
