using AutoMapper;
using BussinessObjects.DTOs;
using BussinessObjects.Enums;
using BussinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.User;
using Services.Common.Sha256;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ISha256Service _sha256Service;


        public UserService(IUserRepository userRepository, IMapper mapper, ISha256Service sha256Service)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _sha256Service = sha256Service;
        }

        public class UserException : Exception
        {
            public enum StatusCodeEnum
            {
                UserNotFound, EmailExisted
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public UserException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public async Task<GetUserListResponseData> GetAllPagination([FromQuery] int skip, [FromQuery] int top)
        {
            var queryable = await _userRepository.GetAllAsync();
            var data = queryable.Skip(skip).Take(top).AsQueryable();
            var totalCount = queryable.Count();
            var maxPage = totalCount >= top ? Math.Ceiling((double)totalCount / top) : 1;
            var response = _mapper.Map<IList<UserDTO>>(data);

            return new GetUserListResponseData
            {
                users = response,
                pages = maxPage
            };
        }

        public async Task<DetailsUserDTO.DetailsUserResponseData> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new UserException(UserException.StatusCodeEnum.UserNotFound, "User not found");
            }
            var response = _mapper.Map<DetailsUserDTO.DetailsUserResponseData>(user);
            return response;
        }

        public async Task<GetUserListResponseData> SearchWithPagination([FromQuery] int skip, [FromQuery] int top, [FromQuery] string input)
        {
            var queryable = await _userRepository.GetAllAsync();
            var data = queryable.Where(x => x.Username!.Contains(input, StringComparison.OrdinalIgnoreCase) || x.Email!.Contains(input, StringComparison.OrdinalIgnoreCase));
            var pagination = data.Skip(skip).Take(top).AsQueryable();
            var totalCount = data.Count();
            var maxPage = totalCount >= top ? Math.Ceiling((double)totalCount / top) : 1; 
            var response = _mapper.Map<IList<UserDTO>>(pagination);

            return new GetUserListResponseData
            {
                users = response,
                pages = maxPage
            };
        }


        public async Task<AddUserDTO.AddUserResponseData> AddAsync(AddUserDTO.AddUserRequest request)
        {
            var user = new BussinessObjects.Models.User()
            {
                Email = request.Data.Email,
                Username = request.Data.Username,
                Password = request.Data.Password,
                FirstName = request.Data.FirstName,
                LastName = request.Data.LastName,
                Birthday = request.Data.Birthday,
                Role = request.Data.Role,
            };
            user.Password = _sha256Service.Hash(user.Password);
            

            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new UserException(UserException.StatusCodeEnum.EmailExisted, "Email existed!");
            }

            var created = await _userRepository.AddAsync(user);

            return new AddUserDTO.AddUserResponseData
            {
                UserId = created.UserId
            };
        }

        public async Task<UpdateUserDTO.UpdateUserResponseData> UpdateAsync(UpdateUserDTO.UpdateUserRequest request, Guid id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new UserException(UserException.StatusCodeEnum.UserNotFound, "User not found");
            }
            var updateUser = request.Data;
            updateUser.Password = _sha256Service.Hash(updateUser.Password);

            existingUser.Email = updateUser.Email ?? existingUser.Email;
            existingUser.Username = updateUser.Username ?? existingUser.Username;
            existingUser.Password = updateUser.Password ?? existingUser.Password;
            existingUser.FirstName = updateUser.FirstName ?? existingUser.FirstName;
            existingUser.LastName = updateUser.LastName ?? existingUser.LastName;
            existingUser.Status = updateUser.Status;
            existingUser.Role = updateUser.Role;

            await _userRepository.UpdateAsync(existingUser.UserId, existingUser);

            return _mapper.Map<UpdateUserDTO.UpdateUserResponseData>(existingUser);
        }

        public async Task<DeleteUserDTO.DeleteUserResponseData> DeleteAsync(DeleteUserDTO.DeleteUserRequest request)
        {
            var existingUser = await _userRepository.GetByIdAsync(request.Data.UserId);
            if (existingUser == null)
            {
                throw new UserException(UserException.StatusCodeEnum.UserNotFound, "User not found");
            }

            await _userRepository.DeleteAsync(existingUser.UserId);

            return new DeleteUserDTO.DeleteUserResponseData()
            {
                UserId = existingUser.UserId
            };
        }

    }
}
