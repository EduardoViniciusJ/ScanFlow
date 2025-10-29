using Microsoft.AspNetCore.Components.Forms;
using ScanFlowAWS.Web.Models.Responses;

namespace ScanFlowAWS.Web.Services.Interfaces
{
    public interface IAnalyzeImageService
    {
        Task<List<ResponseAnalyzeFacesJson>?> AnalyzeFacesAsync(IBrowserFile imageFile);
    }
}
