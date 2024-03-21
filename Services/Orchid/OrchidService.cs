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
                OrchidNotFound
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
                ApprovalStatus = ApprovalStatus.Available,
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
                throw new UpdateOrchidException(UpdateOrchidException.StatusCodeEnum.OrchidNotFound, "Orchid not found");
            }
            existingOrchid.DepositedStatus = request.Data.DepositStatus ?? existingOrchid.DepositedStatus;
            existingOrchid.ApprovalStatus = request.Data.ApprovalStatus ?? existingOrchid.ApprovalStatus;
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

        public async Task<GetOrchidListResponse> GetOrchidsPagination(int skip, int top)
        {
            var queryable = await _orchidRepository.GetAllAsync();
            var pagination = queryable.Skip(skip).Take(top).AsQueryable();
            var totalCount = queryable.Count();
            var maxPage = totalCount >= top ? Math.Ceiling((double)totalCount / top) : 1;
            var response = _mapper.Map<IList<OrchidDTO>>(pagination);

            return new GetOrchidListResponse
            {
                Orchids = response,
                Pages = (int)maxPage
            };
        }

        public async Task<OrchidDTO> GetOrchidById(Guid orchidId)
        {
            var orchid = await _orchidRepository.GetByIdAsync(orchidId);
            if (orchid == null)
            {
                throw new GetOrchidException(GetOrchidException.StatusCodeEnum.OrchidNotFound, "Orchid not found");
            }
            return _mapper.Map<OrchidDTO>(orchid);
        }

        public async Task<GetOrchidListResponse> SearchOrchids(string? name, string? description, DepositStatus? depositStatus, int skip, int top)
        {
            var queryable = await _orchidRepository.GetAllAsync();
            var data = queryable
            .Where(x => name == null || x.Name.Contains(name))
            .Where(x => description == null || x.Description.Contains(description))
            .Where(x => depositStatus == null || x.DepositedStatus == depositStatus);
            var pagination = data.Skip(skip).Take(top).AsQueryable();
            var totalCount = data.Count();
            var maxPage = totalCount >= top ? Math.Ceiling((double)totalCount / top) : 1;
            var response = _mapper.Map<IList<OrchidDTO>>(pagination);

            return new GetOrchidListResponse
            {
                Orchids = response,
                Pages = (int)maxPage
            };
        }

        public async Task<GetOwnedOrchidListResponseData> GetOwnedOrchidsPagination(Guid ownerId, int skip, int top)
        {
            var queryable = await _orchidRepository.GetAllAsync();
            var pagination = queryable.Where(orchid => orchid.OwnerId == ownerId).Skip(skip).Take(top).AsQueryable();
            var totalCount = queryable.Count();
            var maxPage = totalCount >= top ? Math.Ceiling((double)totalCount / top) : 1;
            var response = _mapper.Map<IList<OrchidDTO>>(pagination);

            return new GetOwnedOrchidListResponseData()
            {
                Orchids = response,
                Pages = (int)maxPage
            };
        }
    }
}
