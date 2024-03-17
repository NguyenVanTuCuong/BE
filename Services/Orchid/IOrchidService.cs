using BussinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Orchid
{
    public interface IOrchidService
    {
        public Task<AddOrchidDTO.AddOrchidResponseData> AddOrchid(AddOrchidDTO.AddOrchidRequest request);
        public Task<IList<OrchidDTO>> GetOrchidsPagination(int pageSize, int pageNumber);
        public Task<UpdateOrchidDTO.UpdateOrchidResponseData> UpdateOrchid(UpdateOrchidDTO.UpdateOrchidRequest request);
        public Task<DeleteOrchidDTO.DeleteOrchidResponseData> DeleteOrchid(DeleteOrchidDTO.DeleteOrchidRequest request);
        public Task<OrchidDTO> GetOrchidById(Guid orchidId);
        public Task<OrchidDTO> GetOrchidByName(string name);
    }
}
