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
        public Task<AddDepositRequestDTO.AddDepositRequestResponseData> AddDepositRequest(AddDepositRequestDTO.AddDepositRequestRequest request);
        public Task<UpdateDepositRequestDTO.UpdateDepositRequestResponseData> UpdateDepositRequest(UpdateDepositRequestDTO.UpdateDepositRequestRequest request);
        public Task<GetDepositRequestDTO.GetDepositRequestListResponseData> GetAllDepositRequestPagination(int skip, int top);
        public Task<GetDepositRequestDTO.DepositRequestDTO> GetDepositRequestById(Guid depositRequestId);
        public Task<GetDepositRequestDTO.GetDepositRequestListResponseData> GetDepositRequestByUserIdPagination(Guid? userId, int skip, int top);
    }
}
