using Microsoft.AspNetCore.Components.Forms;
using ScanFlowAWS.Web.Models.Responses;
using ScanFlowAWS.Web.Services.Interfaces;
using System.Net.Http.Json;

namespace ScanFlowAWS.Web.Services
{
    public class CompareImageService : ICompareImageService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CompareImageService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ResponseCompareImageJson?> CompareImage(IBrowserFile fileSource, IBrowserFile fileTarget)
        {
            var client = _clientFactory.CreateClient("AuthorizedAPI");

            using var content = new MultipartFormDataContent();

            using var sourceStream = fileSource.OpenReadStream(5 * 1024 * 1024);
            using var targetStream = fileTarget.OpenReadStream(5 * 1024 * 1024);

            content.Add(new StreamContent(sourceStream), "FileSource", fileSource.Name);
            content.Add(new StreamContent(targetStream), "FileTarget", fileTarget.Name);

            var response = await client.PostAsync("api/rekognition/compareimages", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao comparar imagens: {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<ResponseCompareImageJson>();
        }
    }
}
