using BussinessObjects.DTOs;
using BussinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BussinessObjects.DTOs.GetOrchidDTO;

namespace Services.Orchid
{
    public interface IOrchidService
    {
        public Task<AddOrchidDTO.AddOrchidResponseData> AddOrchid(AddOrchidDTO.AddOrchidRequest request);
        public Task<GetOrchidListResponse> GetOrchidsPagination(int skip, int top);
        public Task<GetOwnedOrchidListResponseData> GetOwnedOrchidsPagination(Guid ownerId, int skip, int top);
        public Task<UpdateOrchidDTO.UpdateOrchidResponseData> UpdateOrchid(UpdateOrchidDTO.UpdateOrchidRequest request);
        public Task<DeleteOrchidDTO.DeleteOrchidResponseData> DeleteOrchid(DeleteOrchidDTO.DeleteOrchidRequest request);
        public Task<OrchidDTO> GetOrchidById(Guid orchidId);
        public Task<GetOrchidListResponse> SearchOrchids(string? name, string? decription, DepositStatus? depositStatus, int skip, int top);
    }
}
