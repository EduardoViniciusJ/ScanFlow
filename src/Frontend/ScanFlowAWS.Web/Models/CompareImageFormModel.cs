using Microsoft.AspNetCore.Components.Forms;

namespace ScanFlowAWS.Web.Models
{
    public class CompareImageFormModel
    {
        public IBrowserFile? FileSource { get; set; }
        public IBrowserFile? FileTarget { get; set; }

    }
}
