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
        Task<AddOrchidDTO.AddOrchidResponseData> AddOrchid(AddOrchidDTO.AddOrchidRequest request);
        Task<GetOrchidListResponse> GetOrchidsPagination(int skip, int top);
        Task<GetOwnedOrchidListResponseData> GetOwnedOrchidsPagination(Guid ownerId, int skip, int top);
        Task<UpdateOrchidDTO.UpdateOrchidResponseData> UpdateOrchid(UpdateOrchidDTO.UpdateOrchidRequest request);
        Task<DeleteOrchidDTO.DeleteOrchidResponseData> DeleteOrchid(DeleteOrchidDTO.DeleteOrchidRequest request);
        Task<OrchidDTO> GetOrchidById(Guid orchidId);
        Task<GetOrchidListResponse> SearchOrchids(string? name, string? decription, DepositStatus? depositStatus, int skip, int top);
    }
}
