namespace SimpleAtm.Web.Schema.Mutation.User;

public class CreateUserAccountInput
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public CreateUserAccountInput(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}

