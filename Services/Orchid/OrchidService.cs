using AutoMapper;
using BussinessObjects.DTOs;
using BussinessObjects.Models;
using Repositories.User;
using Services.Auth;
using Services.Common.Firebase;
using Services.Common.Jwt;
namespace Services.Orchid
{
    public class OrchidService : IOrchidService
    {
        private readonly IOrchidRepository _orchidRepository;
        private readonly IFirebaseService _firebaseService;

        public OrchidService(IOrchidRepository orchidRepository, IFirebaseService firebaseService)
        {
            _orchidRepository = orchidRepository;
            _firebaseService = firebaseService;
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
        public async Task<AddOrchidDTO.AddOrchidResponseData> AddOrchid(AddOrchidDTO.AddOrchidRequest request)
        {
     
            var imageUrl = await _firebaseService.UploadFile(request.Data.ImageFile);
            var orchid = new BussinessObjects.Models.Orchid()
            {
                ImageUrl = imageUrl,
                Name = request.Data.Json.Name,
                OwnerId = request.UserId
            };

            var created = await _orchidRepository.AddAsync(orchid);

            return new AddOrchidDTO.AddOrchidResponseData()
            {
                OrchidId = created.OrchidId
            };
        }
    }
}
