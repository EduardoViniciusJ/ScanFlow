namespace ScanFlowAWS.Web.Validators
{
    public class LoginFormModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; } 
    }
}
