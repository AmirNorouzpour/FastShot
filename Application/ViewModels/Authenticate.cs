namespace Application.ViewModels
{
    public class AuthenticateReq
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class AuthenticateResponse
    {
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Token { get; set; }
    }
}
