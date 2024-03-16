using AutoMapper;
using BussinessObjects.DTOs;
using BussinessObjects.Models;

namespace BE
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SignInDTO.SignInResponseData, User>().ReverseMap();
        }
    }
}
