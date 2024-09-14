using BarberBoss.Domain.Users;

namespace BarberBoss.Api.Token
{
    public class HttpContextValue : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextValue(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string TokenOnRequest()
        {
            var authorization = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

            return authorization["Bearer ".Length..].Trim();
        }
    }
}
