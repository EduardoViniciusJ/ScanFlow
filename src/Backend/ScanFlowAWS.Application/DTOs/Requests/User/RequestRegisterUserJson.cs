namespace ScanFlowAWS.Application.DTOs.Requests.User
{
    public class RequestRegisterUserJson
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set;} = string.Empty;
        public string Password { get; set;} = string.Empty; 
    }
}
