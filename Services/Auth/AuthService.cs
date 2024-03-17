using AutoMapper;
using BussinessObjects.DTOs;
using BussinessObjects.Enums;
using Repositories.User;
using Services.Common.Jwt;
using Services.Common.Sha256;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly ISha256Service _sha256Service;

        private readonly IMapper _mapper;
        public AuthService(IUserRepository userRepository, ISha256Service sha256Service, IMapper mapper)
        {
            _userRepository = userRepository;
            _sha256Service = sha256Service;
            _mapper = mapper;
        }

        public class SignInException : Exception
        {
            public enum StatusCodeEnum
            {
                UserNotFound,
                PasswordNotMatch
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public SignInException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public class SignUpException : Exception
        {
            public enum StatusCodeEnum
            {
                UserEmailExisted,
                
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public SignUpException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public async Task<SignInDTO.SignInResponseData> SignIn(SignInDTO.SignInRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new SignInException(SignInException.StatusCodeEnum.UserNotFound, "User not found");
            }

            var same = _sha256Service.Verify(request.Password, user.Password);

            if (!same)
            {
                throw new SignInException(SignInException.StatusCodeEnum.PasswordNotMatch, "Password not match");
            }

            return _mapper.Map<SignInDTO.SignInResponseData>(user);
        }

        public async Task<SignUpDTO.SignUpResponseData> SignUp(SignUpDTO.SignUpRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            
            if (user != null)
            {
                throw new SignUpException(SignUpException.StatusCodeEnum.UserEmailExisted, "User with this email has been existed.");
            }

            request.Password = _sha256Service.Hash(request.Password);
            var mapped = _mapper.Map<BussinessObjects.Models.User>(request);
            mapped.Role = UserRole.User;

            var created = await _userRepository.AddAsync(mapped);

            return new SignUpDTO.SignUpResponseData
            {
                UserId = created.UserId
            };
        }

        public async Task<GetProfileDTO.GetProfileResponseData> GetProfile(GetProfileDTO.GetProfileRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            return _mapper.Map<GetProfileDTO.GetProfileResponseData>(user);
        }
    }
}
