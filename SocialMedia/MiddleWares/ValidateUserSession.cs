using Microsoft.AspNetCore.Http;
using SocialMedia.Core.Application.Dtos.Account;
using SocialMedia.Core.Application.Helpers;

namespace WebApp.SocialMedia.MiddleWares
{
    public class ValidateUserSession
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public ValidateUserSession(IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }
        public bool HasUser()
        {
            AuthenticationResponse response = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (response==null)
            {
                return false;
            }
            return true;
        }
    }
}
