using AutoMapper;
using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Domain.Entities;

namespace ScanFlowAWS.Application.Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RequestRegisterUserJson, User>().ReverseMap();
        }
    }
}
