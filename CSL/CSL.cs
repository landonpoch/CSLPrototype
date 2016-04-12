using Interfaces;

namespace CSL
{
    public interface IAccountServiceCSL
    {
        void AuthenticateUser(string username, string password);
    }

    public class AccountService : IAccountServiceCSL
    {
        private readonly IRequestHelper _requestHelper;

        public AccountService(IRequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public void AuthenticateUser(string username, string password)
        {
            var url = string.Format("http://moveauth.com/authenticate?username={0}&password={1}", username, password);
            var response = _requestHelper.Request(url);
            _requestHelper.AuthToken = response;
        }
    }
}
