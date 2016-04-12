using Interfaces;
using System;

namespace APJS
{
    public class RequestHelper : IRequestHelper
    {
        public string AuthToken { get; set; }

        public string Request(string req)
        {
            Console.WriteLine("Making Request");

            if (req.Contains("authenticate"))  // simulates a response from an authenticate call
                return "some random token";

            return "some random response";
        }

        public string RequestWithAuth(string req)
        {
            if (string.IsNullOrEmpty(AuthToken))
                throw new Exception("Not authenticated!");

            Console.WriteLine("Making authenticated request");

            return "some authenticated response";
        }
    }

    public class AccountService : IAccountService
    {
        private readonly IRequestHelper _requestHelper;

        public AccountService(IRequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public void Authenticate(string username, string password)
        {
            var url = string.Format("http://moveauth.com/authenticate?username={0}&password={1}", username, password);
            var response = _requestHelper.Request(url);
            _requestHelper.AuthToken = response;
        }
    }

    public interface IResumesService
    {
        void SetResume(int someVal);
    }

    public class ResumesService : IResumesService
    {
        private readonly IRequestHelper _requestHelper;

        public ResumesService(IRequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public void SetResume(int someVal)
        {
            var response = _requestHelper.RequestWithAuth("some resumes url");
            Console.WriteLine("Resume point set");
        }
    }

    public interface ITimelinePlayer
    {
        void Pause();
    }

    public class TimelinePlayer : ITimelinePlayer
    {
        private readonly IResumesService _resumesService;

        public TimelinePlayer(IResumesService resumesService)
        {
            _resumesService = resumesService;
        }

        public void Pause()
        {
            _resumesService.SetResume(5);
        }
    }
}
