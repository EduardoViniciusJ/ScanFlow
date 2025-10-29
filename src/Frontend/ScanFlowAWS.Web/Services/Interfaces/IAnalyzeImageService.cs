using Microsoft.AspNetCore.Components.Forms;
using ScanFlowAWS.Web.Models.Responses;

namespace ScanFlowAWS.Web.Services.Interfaces
{
    public interface IAnalyzeImageService
    {
        Task<ResponseAnalyzeFacesJson?> AnalyzeFacesAsync(IBrowserFile imageFile);
    }
}
