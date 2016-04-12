namespace Interfaces
{
    public interface IAccountService
    {
        void Authenticate(string username, string password);
    }

    public interface IRequestHelper
    {
        string AuthToken { get; set; }

        string Request(string req);
        string RequestWithAuth(string req);
    }
}
