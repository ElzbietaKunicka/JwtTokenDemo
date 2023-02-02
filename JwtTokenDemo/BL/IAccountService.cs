namespace JwtTokenDemo.BL
{
    public interface IAccountService
    {
        Account SignupNewAccount(string username, string password);

        bool Login(string username, string password);

    }
}
