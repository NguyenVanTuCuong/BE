using BussinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth
{
    public interface IAuthService
    {
        public Task<SignInDTO.SignInResponseData> SignIn(SignInDTO.SignInRequest request);
        public Task<SignUpDTO.SignUpResponse> SignUp(SignUpDTO.SignUpRequest request);
        public Task<GetProfileDTO.GetProfileResponseData> GetProfile(GetProfileDTO.GetProfileRequest request);
    }
}
