using ScanFlowAWS.Application.DTOs.Requests.Rekognition;
using ScanFlowAWS.Application.DTOs.Responses.Rekognition;
using ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces.Interface;
using ScanFlowAWS.Domain.Services;

namespace ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces
{
    public class CompareceFacesUseCase : ICompareceFaces
    {
        private readonly IRekognitionService _rekognition;

        public CompareceFacesUseCase(IRekognitionService rekognition)
        {
            _rekognition = rekognition;
        }

        public async Task<ResponseCompareceFacesJson> Execute(RequestCompareceFacesJson request)
        {
            using var memoryStreamSource = new MemoryStream();
            using var memoryStreamTarget = new MemoryStream();

            await request.fileSource.CopyToAsync(memoryStreamSource);
            await request.fileTarget.CopyToAsync(memoryStreamTarget); 

            var result = _rekognition.CompareceFaces(memoryStreamSource.ToArray(), memoryStreamTarget.ToArray());

            var response = new ResponseCompareceFacesJson()
            {
                Similarity = result.Result.Similarity,
            };

            return response;

        }



        
    }
}
