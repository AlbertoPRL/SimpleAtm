namespace SimpleAtm.Web.Schema.Mutation.Login;

public class LoginInput
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public LoginInput(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}
