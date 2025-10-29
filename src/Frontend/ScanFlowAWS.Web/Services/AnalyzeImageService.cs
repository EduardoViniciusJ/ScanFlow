using Microsoft.AspNetCore.Components.Forms;
using ScanFlowAWS.Web.Models;
using ScanFlowAWS.Web.Models.Responses;
using ScanFlowAWS.Web.Services.Interfaces;
using System.Net.Http.Json;

namespace ScanFlowAWS.Web.Services
{
    public class AnalyzeImageService : IAnalyzeImageService
    {
        private readonly IHttpClientFactory _clientFactory;

        public AnalyzeImageService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ResponseAnalyzeFacesJson?> AnalyzeFacesAsync(IBrowserFile imageFile)
        {
            var client = _clientFactory.CreateClient("AuthorizedAPI");

            using var content = new MultipartFormDataContent();
            using var stream = imageFile.OpenReadStream(5 * 1024 * 1024);
            content.Add(new StreamContent(stream), "file", imageFile.Name); 

            var response = await client.PostAsync("api/rekognition/analyzefaces", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao analisar imagem ({response.StatusCode}).");

            return await response.Content.ReadFromJsonAsync<ResponseAnalyzeFacesJson>();
        }
    }
}
