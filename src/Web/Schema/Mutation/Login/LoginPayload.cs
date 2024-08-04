namespace SimpleAtm.Web.Schema.Mutation.Login;

public class LoginPayload
{
    public int StatusCode { get; init; }
    public string[] Errors { get; init; }
    public bool Succeeded { get; init; }
    public string? AuthenticationToken { get; init; }

    public LoginPayload(int statusCode, string[] errors, bool succeeded, string? authenticationToken)
    {
        StatusCode = statusCode;
        Errors = errors;
        Succeeded = succeeded;
        AuthenticationToken = authenticationToken;
    }
}
