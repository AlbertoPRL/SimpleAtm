using SimpleAtm.Infrastructure.Identity;

namespace SimpleAtm.Web.Schema.Mutation.User;

public class CreateUserAccountPayload
{
    [ID]
    [GraphQLDescription("The ID of the created user")]
    public Guid? UserId { get; set; }
    public int Code { get; set; }
    public string[] Messages { get; set; }
    public bool Success { get; set; }
    public CreateUserAccountPayload(int code, string[] messages, bool success, Guid? userId)
    {
        Code = code;
        Messages = messages;
        Success = success;
        
        if(userId != null)
        {
            UserId = userId;
        }
    }
}

