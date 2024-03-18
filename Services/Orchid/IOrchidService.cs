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
        public Task<IList<GetOrchidResponseData>> GetOrchidsPagination(int pageSize, int pageNumber);
        public Task<UpdateOrchidDTO.UpdateOrchidResponseData> UpdateOrchid(UpdateOrchidDTO.UpdateOrchidRequest request);
        public Task<DeleteOrchidDTO.DeleteOrchidResponseData> DeleteOrchid(DeleteOrchidDTO.DeleteOrchidRequest request);
        public Task<GetOrchidResponseData> GetOrchidById(Guid orchidId);
        public Task<IList<GetOrchidResponseData>> SearchOrchids(GetOrchidDTO.GetOrchidRequestData data);
    }
}
