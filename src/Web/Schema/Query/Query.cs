using SimpleAtm.Infrastructure.Identity;

namespace SimpleAtm.Web.Schema.Query;

public class Query
{
   public string GetUser([Service] IdentityService identityService)
    {
        return "identityService.GetUserNameAsync();";
    }
}
