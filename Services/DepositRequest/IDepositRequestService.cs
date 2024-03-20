using BussinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BussinessObjects.DTOs.GetOrchidDTO;

namespace Services.DepositRequest
{
    public interface IDepositRequestService
    {
        public Task<AddDepositRequestDTO.AddDepositResponseData> AddDepositRequest(AddDepositRequestDTO.AddDepositRequest request);
        public Task<UpdateDepositRequestDTO.UpdateDepositResponseData> UpdateDepositRequest(UpdateDepositRequestDTO.UpdateDepositRequest request);
        public Task<GetDepositDTO.GetDepositListResponse> GetAllDepositRequestPagination(int skip, int top);
        public Task<GetDepositDTO.GetDepositListResponse> GetDepositRequestByUserIdPagination(Guid? userId, int skip, int top);
    }
}
