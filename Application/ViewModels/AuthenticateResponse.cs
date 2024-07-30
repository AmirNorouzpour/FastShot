using Domain.Models;

namespace Application.ViewModels
{
    public class AuthenticateResponse
    {
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Token { get; set; }
    }
}
