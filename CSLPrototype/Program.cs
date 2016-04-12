using Interfaces;
using Ninject;

namespace CSLPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            // We can run one client or the other
            new Html5Client().Run();
            //new UwpClient().Run();
        }
    }

    public class Html5Client
    {
        public void Run()
        {
            var kernel = RegisterTypes();
            var timelinePlayer = kernel.Get<APJS.ITimelinePlayer>();
            var account = kernel.Get<CSL.IAccountServiceCSL>(); // HTML 5 client wants to use the new interface

            account.AuthenticateUser("some", "password");
            timelinePlayer.Pause();
        }

        private static IKernel RegisterTypes()
        {
            var kernel = new StandardKernel();
            kernel.Bind<CSL.IAccountServiceCSL>().To<CSL.AccountService>(); // HTML 5 client registers the new interface
            kernel.Bind<APJS.ITimelinePlayer>().To<APJS.TimelinePlayer>();
            kernel.Bind<IRequestHelper>().To<APJS.RequestHelper>().InSingletonScope();
            kernel.Bind<APJS.IResumesService>().To<APJS.ResumesService>();

            return kernel;
        }
    }

    public class UwpClient
    {
        public void Run()
        {
            var kernel = RegisterTypes();
            var timelinePlayer = kernel.Get<APJS.ITimelinePlayer>();
            var account = kernel.Get<IAccountService>(); // UWP Client wants to use the old interface

            account.Authenticate("some", "password");
            timelinePlayer.Pause();
        }

        private static IKernel RegisterTypes()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IAccountService>().To<APJS.AccountService>(); // UWP Client registers the old interface
            kernel.Bind<APJS.ITimelinePlayer>().To<APJS.TimelinePlayer>();
            kernel.Bind<IRequestHelper>().To<APJS.RequestHelper>().InSingletonScope();
            kernel.Bind<APJS.IResumesService>().To<APJS.ResumesService>();

            return kernel;
        }
    }
}
