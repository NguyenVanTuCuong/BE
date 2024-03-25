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
                OrchidNotFound,
                OrchidAlreadySentForApproval,
                DepositRequestIsPending
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
        public async Task<AddDepositRequestDTO.AddDepositRequestResponseData> AddDepositRequest(AddDepositRequestDTO.AddDepositRequestRequest request)
        {
            var orchid = await _orchidRepository.GetByIdAsync(request.Data.OrchidId);
            if (orchid == null)
            {
                throw new AddDepositRequestException(AddDepositRequestException.StatusCodeEnum.OrchidNotFound, "Orchid not found");
            }

            if (orchid.ApprovalStatus == ApprovalStatus.Sented)
            {
                throw new AddDepositRequestException(AddDepositRequestException.StatusCodeEnum.OrchidAlreadySentForApproval, "Orchid already sent for approval");
            }

            //var existingRequest = await _depositRequestRepository.GetByOrchidIdAndLatestCreatedDate(request.Data.OrchidId);
            //if (existingRequest != null && existingRequest.RequestStatus == RequestStatus.Pending)
            //{
            //    throw new AddDepositRequestException(AddDepositRequestException.StatusCodeEnum.DepositRequestIsPending, "Deposit request is already created and still pending");
            //}
            
            var depositRequest = new BussinessObjects.Models.DepositRequest()
            {
                OrchidId = request.Data.OrchidId,
                Title = request.Data.Title,
                Description = request.Data.Description,
                WalletAddress = request.Data.WalletAddress,
                RequestStatus = RequestStatus.Pending,
            };
            var created = await _depositRequestRepository.AddAsync(depositRequest);
            
            orchid.ApprovalStatus = ApprovalStatus.Sented;
            await _orchidRepository.UpdateAsync(request.Data.OrchidId, orchid);

            return new AddDepositRequestDTO.AddDepositRequestResponseData()
            {
                DepositRequestId = created.DepositRequestId
            };
        }

        public async Task<UpdateDepositRequestDTO.UpdateDepositRequestResponseData> UpdateDepositRequest(UpdateDepositRequestDTO.UpdateDepositRequestRequest request)
        {
            var existingRequest = await _depositRequestRepository.GetByIdAsync(request.Data.DepositRequestId);
            if (existingRequest == null)
            {
                throw new UpdateDepositRequestException(UpdateDepositRequestException.StatusCodeEnum.DepositRequestNotFound, "Deposit request not found");
            }
            existingRequest.Title = request.Data.Title ?? existingRequest.Title;
            existingRequest.Description = request.Data.Description ?? existingRequest.Description;
            existingRequest.WalletAddress = request.Data.WalletAddress ?? existingRequest.WalletAddress;
            existingRequest.RequestStatus = request.Data.requestStatus ?? existingRequest.RequestStatus;
            existingRequest.UpdatedAt = DateTime.Now;
            await _depositRequestRepository.UpdateAsync(existingRequest.DepositRequestId, existingRequest);

            return new UpdateDepositRequestDTO.UpdateDepositRequestResponseData()
            {
                DepositRequestId = existingRequest.DepositRequestId
            };
        }

        public async Task<GetDepositRequestDTO.GetDepositRequestListResponseData> GetAllDepositRequestPagination(int skip, int top)
        {
            var queryable = await _depositRequestRepository.GetAllIncludesAsync();
            var pagination = queryable.Skip(skip).Take(top).Reverse().AsQueryable();
            var totalCount = queryable.Count();
            var maxPage = totalCount >= top ? Math.Ceiling((double)totalCount / top) : 1;
            var response = _mapper.Map<IList<GetDepositRequestDTO.DepositRequestDTO>>(pagination);

            return new GetDepositRequestDTO.GetDepositRequestListResponseData()
            {
                Deposits = response,
                Pages = (int)maxPage
            };
        }

        public async Task<GetDepositRequestDTO.GetDepositRequestListResponseData> GetDepositRequestByUserIdPagination(Guid? userId, int skip, int top)
        {
            var queryable = await _depositRequestRepository.GetDepositRequestByUserIdPagination(userId);
            var pagination = queryable.Skip(skip).Take(top).AsQueryable();
            var totalCount = queryable.Count();
            var maxPage = totalCount >= top ? Math.Ceiling((double)totalCount / top) : 1;
            var response = _mapper.Map<IList<GetDepositRequestDTO.DepositRequestDTO>>(pagination);
            return new GetDepositRequestDTO.GetDepositRequestListResponseData()
            {
                Deposits = response,
                Pages = (int)maxPage
            };
        }

        public async Task<GetDepositRequestDTO.DepositRequestDTO> GetDepositRequestById(Guid depositRequestId)
        {
            var depositRequest = await _depositRequestRepository.GetByIdAsync(depositRequestId);
            if (depositRequest == null)
            {
                throw new GetDepositRequestException(GetDepositRequestException.StatusCodeEnum.DepositRequestNotFound, "Deposit request not found");
            }
            return _mapper.Map<GetDepositRequestDTO.DepositRequestDTO>(depositRequest);
        }
    }
}
