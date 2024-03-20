using AutoMapper;
using BussinessObjects.DTOs;
using BussinessObjects.Enums;
using BussinessObjects.Models;
using Repositories.User;
using Services.Auth;
using Services.Common.Firebase;
using Services.Common.Jwt;
using static BussinessObjects.DTOs.GetOrchidDTO;
namespace Services.Orchid
{
    public class OrchidService : IOrchidService
    {
        private readonly IOrchidRepository _orchidRepository;
        private readonly IFirebaseService _firebaseService;
        private readonly IMapper _mapper;

        public OrchidService(IOrchidRepository orchidRepository, IFirebaseService firebaseService, IMapper mapper)
        {
            _orchidRepository = orchidRepository;
            _firebaseService = firebaseService;
            _mapper = mapper;
        }
        public class AddOrchidException : Exception
        {
            public enum StatusCodeEnum
            {

            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public AddOrchidException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public class GetOrchidException : Exception
        {
            public enum StatusCodeEnum
            {
                OrchidNotFound,
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public GetOrchidException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public class UpdateOrchidException : Exception
        {
            public enum StatusCodeEnum
            {
                UpdateOrchidFailed,
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public UpdateOrchidException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public class DeleteOrchidException : Exception
        {
            public enum StatusCodeEnum
            {
                DeleteOrchidFailed,
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public DeleteOrchidException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }
        public async Task<AddOrchidDTO.AddOrchidResponseData> AddOrchid(AddOrchidDTO.AddOrchidRequest request)
        {
     
            var imageUrl = await _firebaseService.UploadFile(request.Data.ImageFile);
            var orchid = new BussinessObjects.Models.Orchid()
            {
                ImageUrl = imageUrl,
                Name = request.Data.Json.Name,
                Description = request.Data.Json.Description,
                Color = request.Data.Json.Color,
                Origin = request.Data.Json.Origin,
                Species = request.Data.Json.Species,
                OwnerId = request.UserId,
                DepositedStatus = DepositStatus.Available,
            };

            var created = await _orchidRepository.AddAsync(orchid);

            return new AddOrchidDTO.AddOrchidResponseData()
            {
                OrchidId = created.OrchidId
            };
        }

        public async Task<UpdateOrchidDTO.UpdateOrchidResponseData> UpdateOrchid(UpdateOrchidDTO.UpdateOrchidRequest request)
        {
            var existingOrchid = await _orchidRepository.GetByIdAsync(request.Data.OrchidId);
            if (existingOrchid == null)
            {
                throw new GetOrchidException(GetOrchidException.StatusCodeEnum.OrchidNotFound, "Orchid not found");
            }
            existingOrchid.DepositedStatus = request.Data.DepositStatus ?? existingOrchid.DepositedStatus;
            existingOrchid.Name = request.Data.Json.Name ?? existingOrchid.Name;
            existingOrchid.Description = request.Data.Json.Description ?? existingOrchid.Description;
            existingOrchid.Color = request.Data.Json.Color ?? existingOrchid.Color;
            existingOrchid.Origin = request.Data.Json.Origin ?? existingOrchid.Origin;
            existingOrchid.Species = request.Data.Json.Species ?? existingOrchid.Species;
            existingOrchid.UpdatedAt = DateTime.Now;

            if (request.Data.ImageFile != null)
            {
                existingOrchid.ImageUrl = await _firebaseService.UploadFile(request.Data.ImageFile);
            }
            await _orchidRepository.UpdateAsync(existingOrchid.OrchidId, existingOrchid);

            return new UpdateOrchidDTO.UpdateOrchidResponseData()
            {
                OrchidId = existingOrchid.OrchidId
            };
        }

        public async Task<DeleteOrchidDTO.DeleteOrchidResponseData> DeleteOrchid(DeleteOrchidDTO.DeleteOrchidRequest request)
        {
            var existingOrchid = await _orchidRepository.GetByIdAsync(request.Data.OrchidId);
            if (existingOrchid == null)
            {
                throw new GetOrchidException(GetOrchidException.StatusCodeEnum.OrchidNotFound, "Orchid not found");
            }

            await _orchidRepository.DeleteAsync(existingOrchid.OrchidId);

            return new DeleteOrchidDTO.DeleteOrchidResponseData()
            {
                OrchidId = existingOrchid.OrchidId
            };
        }

        public async Task<IList<GetOrchidResponseData>> GetOrchidsPagination(int pageSize, int pageNumber)
        {
            var orchids = await _orchidRepository.GetOrchidsPagination(pageSize, pageNumber);
            return _mapper.Map<IList<GetOrchidResponseData>>(orchids);
        }

        public async Task<GetOrchidResponseData> GetOrchidById(Guid orchidId)
        {
            var orchid = await _orchidRepository.GetByIdAsync(orchidId);
            if (orchid == null)
            {
                throw new GetOrchidException(GetOrchidException.StatusCodeEnum.OrchidNotFound, "Orchid not found");
            }
            return _mapper.Map<GetOrchidResponseData>(orchid);
        }

        public async Task<IList<GetOrchidResponseData>> SearchOrchids(GetOrchidDTO.GetOrchidRequestData data)
        {
            var orchids = await _orchidRepository.SearchOrchids(data.Name, data.Description, data.DepositStatus);
            return _mapper.Map<IList<GetOrchidResponseData>>(orchids);
        }
    }
}
