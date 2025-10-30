using Microsoft.AspNetCore.Components.Forms;
using ScanFlowAWS.Web.Models.Responses;

namespace ScanFlowAWS.Web.Services.Interfaces
{
    public interface ICompareImageService
    {
        public Task<ResponseCompareImageJson?> CompareImage(IBrowserFile fileSource, IBrowserFile fileTarget);

    }
}
