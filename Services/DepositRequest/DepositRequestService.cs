using AutoMapper;
using BussinessObjects.DTOs;
using BussinessObjects.Enums;
using BussinessObjects.Models;
using Repositories.DepositRequest;
using Repositories.User;
using Services.Common.Firebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BussinessObjects.DTOs.GetOrchidDTO;
using static Services.Orchid.OrchidService;

namespace Services.DepositRequest
{
    public class DepositRequestService : IDepositRequestService
    {
        private readonly IDepositRequestRepository _depositRequestRepository;
        private readonly IOrchidRepository _orchidRepository;
        private readonly IMapper _mapper;

        public class GetDepositRequestException : Exception
        {
            public enum StatusCodeEnum
            {
                DepositRequestNotFound,
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public GetDepositRequestException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public class AddDepositRequestException : Exception
        {
            public enum StatusCodeEnum
            {
                AddDepositFailed
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public AddDepositRequestException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public class UpdateDepositRequestException : Exception
        {
            public enum StatusCodeEnum
            {
                UpdateDepositFailed,
                DepositRequestNotFound
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public UpdateDepositRequestException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public DepositRequestService(IDepositRequestRepository depositRequestRepository, IMapper mapper, IOrchidRepository orchidRepository)
        {
            _depositRequestRepository = depositRequestRepository;
            _mapper = mapper;
            _orchidRepository = orchidRepository;
        }
        public async Task<AddDepositRequestDTO.AddDepositResponseData> AddDepositRequest(AddDepositRequestDTO.AddDepositRequest request)
        {
            var depositRequest = new BussinessObjects.Models.DepositRequest()
            {
                OrchidId = request.Data.OrchidId,
                Title = request.Data.Title,
                Description = request.Data.Description,
                WalletAddress = request.Data.WalletAddress,
                RequestStatus = RequestStatus.Pending,
            };
            var created = await _depositRequestRepository.AddAsync(depositRequest);

            var orchid = await _orchidRepository.GetByIdAsync(request.Data.OrchidId);
            orchid.ApprovalStatus = ApprovalStatus.Sented;
            await _orchidRepository.UpdateAsync(request.Data.OrchidId, orchid);

            return new AddDepositRequestDTO.AddDepositResponseData()
            {
                DepositRequestId = created.DepositRequestId
            };
        }

        public async Task<UpdateDepositRequestDTO.UpdateDepositResponseData> UpdateDepositRequest(UpdateDepositRequestDTO.UpdateDepositRequest request)
        {
            var existingRequest = await _depositRequestRepository.GetByIdAsync(request.Data.DepositRequestId);
            if (existingRequest == null)
            {
                throw new UpdateDepositRequestException(UpdateDepositRequestException.StatusCodeEnum.DepositRequestNotFound, "Deposit request not found");
            }
            existingRequest.Title = request.Data.Title ?? existingRequest.Title;
            existingRequest.Description = request.Data.Description ?? existingRequest.Description;
            existingRequest.RequestStatus = request.Data.requestStatus ?? existingRequest.RequestStatus;
            existingRequest.UpdatedAt = DateTime.Now;
            await _depositRequestRepository.UpdateAsync(existingRequest.DepositRequestId, existingRequest);

            return new UpdateDepositRequestDTO.UpdateDepositResponseData()
            {
                DepositRequestId = existingRequest.DepositRequestId
            };
        }

        public async Task<GetDepositDTO.GetDepositListResponse> GetAllDepositRequestPagination(int skip, int top)
        {
            var queryable = await _depositRequestRepository.GetAllIncludesAsync();
            var pagination = queryable.Skip(skip).Take(top).Reverse().AsQueryable();
            var totalCount = queryable.Count();
            var maxPage = totalCount >= top ? Math.Ceiling((double)totalCount / top) : 1;
            var response = _mapper.Map<IList<GetDepositDTO.DepositDTO>>(pagination);

            return new GetDepositDTO.GetDepositListResponse()
            {
                deposits = response,
                pages = maxPage
            };
        }

        public async Task<GetDepositDTO.GetDepositListResponse> GetDepositRequestByUserIdPagination(Guid? userId, int skip, int top)
        {
            var queryable = await _depositRequestRepository.GetDepositRequestByUserIdPagination(userId);
            var pagination = queryable.Skip(skip).Take(top).AsQueryable();
            var totalCount = queryable.Count();
            var maxPage = totalCount >= top ? Math.Ceiling((double)totalCount / top) : 1;
            var response = _mapper.Map<IList<GetDepositDTO.DepositDTO>>(pagination);
            return new GetDepositDTO.GetDepositListResponse()
            {
                deposits = response,
                pages = maxPage
            };
        }

        public class ReviewDepositRequestException : Exception
        {
            public enum StatusCodeEnum
            {
                AlreadyReviewd
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public ReviewDepositRequestException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public async Task<ReviewDepositRequestDTO.ReviewDepositResponseData> ReviewDepositRequest(ReviewDepositRequestDTO.ReviewDepositRequest request)
        {
            var depositRequest = await _depositRequestRepository.GetByIdAsync(request.Data.DepositRequestId);
            if (depositRequest.RequestStatus != RequestStatus.Pending) throw new ReviewDepositRequestException(ReviewDepositRequestException.StatusCodeEnum.AlreadyReviewd, "Already reviewed");

            depositRequest.RequestStatus = request.Data.RequestStatus;
            await _depositRequestRepository.UpdateAsync(depositRequest.DepositRequestId, depositRequest);

            var orchid = await _orchidRepository.GetByIdAsync(depositRequest.OrchidId);
            await _orchidRepository.UpdateAsync(orchid.OrchidId, orchid);

            orchid.ApprovalStatus = ApprovalStatus.Available;

            if (request.Data.RequestStatus == RequestStatus.Approved)
            {
                orchid.DepositedStatus = DepositStatus.Deposited;
            }
            else
            {
                orchid.DepositedStatus = DepositStatus.Available;
            }

            return new ReviewDepositRequestDTO.ReviewDepositResponseData()
            {

            };
        }

        public Task<AddDepositRequestDTO.AddDepositResponseData> DepositRequest(AddDepositRequestDTO.AddDepositRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
