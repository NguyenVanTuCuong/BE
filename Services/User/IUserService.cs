using BussinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.User
{
    public interface IUserService
    {
        public Task<GetUserListResponseData> GetAllPagination(int skip, int top);
        public Task<DetailsUserDTO.DetailsUserResponseData> GetByIdAsync(Guid id);
        public Task<AddUserDTO.AddUserResponseData> AddAsync(AddUserDTO.AddUserRequest request);
        public Task<UpdateUserDTO.UpdateUserResponseData> UpdateAsync(UpdateUserDTO.UpdateUserRequest request, Guid Id);
        public Task<DeleteUserDTO.DeleteUserResponseData> DeleteAsync(DeleteUserDTO.DeleteUserRequest request);
        public Task<GetUserListResponseData> SearchWithPagination(int skip, int top, string input);
    }
}
