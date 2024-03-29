﻿using AutoMapper;
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
            CreateMap<GetOwnedOrchidListResponseData, GetOrchidListResponse>().ReverseMap();
            CreateMap<OrchidDTO, Orchid>().ReverseMap();

            CreateMap<GetDepositRequestDTO.DepositRequestDTO, DepositRequest>().ForMember( 
                    (src) => src.Orchid, (dest) => dest.MapFrom(x => x.Orchid)
                ).ReverseMap();

            CreateMap<AddDepositRequestDTO.AddDepositRequestResponseData, Orchid>().ReverseMap();
            CreateMap<GetDepositRequestDTO.DepositRequestDTO, DepositRequest>().ReverseMap();

            CreateMap<AddUserDTO.AddUserResponseData, User>().ReverseMap();
            CreateMap<AddUserDTO.AddUserRequestData, User>().ReverseMap();

            CreateMap<UpdateUserDTO.UpdateUserResponseData, User>().ReverseMap();
            CreateMap<UpdateUserDTO.UpdateUserRequestData, User>().ReverseMap();

            CreateMap<DetailsUserDTO.DetailsUserResponseData, User>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();

            CreateMap<GetTransactionDTO.TransactionDTO, Transaction>().ReverseMap();
        }
    }
}
