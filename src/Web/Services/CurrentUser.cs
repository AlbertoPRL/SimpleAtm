using SimpleAtm.Application.Common.Interfaces;

namespace SimpleAtm.Web.Services;
public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? Id 
    {
        get
        {
            var id  = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value ?? null;
            return id != null ? Guid.Parse(id) : null;
        }
    }



}
