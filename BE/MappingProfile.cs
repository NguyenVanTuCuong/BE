using AutoMapper;
using BussinessObjects.DTOs;
using BussinessObjects.Models;
using static BussinessObjects.DTOs.GetOrchidDTO;

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

            CreateMap<AddDepositRequestDTO.AddDepositResponseData, Orchid>().ReverseMap();
            CreateMap<GetDepositDTO.DepositDTO, DepositRequest>().ReverseMap();

            CreateMap<AddUserDTO.AddUserResponseData, User>().ReverseMap();
            CreateMap<AddUserDTO.AddUserRequestData, User>().ReverseMap();

            CreateMap<UpdateUserDTO.UpdateUserResponseData, User>().ReverseMap();
            CreateMap<UpdateUserDTO.UpdateUserRequestData, User>().ReverseMap();

            CreateMap<DetailsUserDTO.DetailsUserResponseData, User>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
