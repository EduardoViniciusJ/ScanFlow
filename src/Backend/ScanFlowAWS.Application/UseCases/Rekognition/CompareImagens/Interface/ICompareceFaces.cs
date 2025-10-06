using ScanFlowAWS.Application.DTOs.Requests.Rekognition;
using ScanFlowAWS.Application.DTOs.Responses.Rekognition;

namespace ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces.Interface
{
    public interface ICompareceFaces
    {
        Task<ResponseCompareceFacesJson> Execute(RequestCompareceFacesJson requestCompareceFacesJson); 
    }
}
