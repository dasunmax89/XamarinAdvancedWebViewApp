using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using XamarinAdvancedWebViewApp.Xamarin.Core.Constants;
using XamarinAdvancedWebViewApp.Xamarin.Core.Helpers;
using XamarinAdvancedWebViewApp.Xamarin.Core.Ioc;
using XamarinAdvancedWebViewApp.Xamarin.Core.Services;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using XF = Xamarin.Forms;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinAdvancedWebViewApp.Xamarin.Core
{
    public partial class App : XF.Application
    {
        public App()
        {
            InitializeComponent();

            InitApp();
        }

        private void InitApp()
        {
            GlobalSetting.Instance.BaseGatewayEndpoint = ApiConstants.BaseDevApiUrl;
            AppDependencyResolver.RegisterComponents();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await InitNavigation();

            SetupAppCenter();

            base.OnResume();
        }

        private void SetupAppCenter()
        {
            try
            {
                AppCenter.Start("ios=xxxx-xx-xx-xxx-xxx;" +
                                "android=xxxx-xx-xx-xxx-xxx;",
                                typeof(Analytics), typeof(Crashes));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occured while starting the app center {0}", ex);
            }
        }

        private Task InitNavigation()
        {
            var navigationService = AppDependencyResolver.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        private Task ResumeNavigation()
        {
            var navigationService = AppDependencyResolver.Resolve<INavigationService>();

            return navigationService.ResumeAsync();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected async override void OnResume()
        {
            try
            {
                await ResumeNavigation();
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while resuming the app", ex);
            }
        }
    }
}
