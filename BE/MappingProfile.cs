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
            CreateMap<SignUpDTO.SignUpRequest, User>().ReverseMap();
            CreateMap<GetProfileDTO.GetProfileResponseData, User>().ReverseMap();

            CreateMap<AddOrchidDTO.AddOrchidResponseData, Orchid>().ReverseMap();
            CreateMap<OrchidDTO, Orchid>().ReverseMap();
        }
    }
}
