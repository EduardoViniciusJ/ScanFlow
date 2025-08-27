using AutoMapper;
using ScanFlowAWS.Application.DTOs.Requests;
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
