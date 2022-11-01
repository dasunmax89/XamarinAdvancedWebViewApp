using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinAdvancedWebViewApp.Xamarin.Core.Constants;
using XamarinAdvancedWebViewApp.Xamarin.Core.Dependency;
using XamarinAdvancedWebViewApp.Xamarin.Core.Localization;
using XamarinAdvancedWebViewApp.Xamarin.Core.Models;
using XamarinAdvancedWebViewApp.Xamarin.Core.Repositories;
using XamarinAdvancedWebViewApp.Xamarin.Core.Services;
using XamarinAdvancedWebViewApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        #region Actions

        #endregion

        #region Bindables

        private string _url;
        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
                RaisePropertyChanged(() => Url);
            }
        }

        string _pageTitle;
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                _pageTitle = value;
                RaisePropertyChanged(() => PageTitle);
            }
        }

        #endregion

        #region Commands

        #endregion

        public HomeViewModel(IConnectionService connectionService, INavigationService navigationService,
            IDialogService dialogService, ISettingsService settingsService, IApplicationDataService applicationDataService,
            IUserService userService, IAnalyticsService analyticsService,
            ILocalDbContextService localDbContext, IPlatformManager platformManager)
          : base(connectionService, navigationService, dialogService, settingsService, applicationDataService,
                userService, analyticsService, localDbContext, platformManager)
        {

        }

        public override async Task Initialize(object navigationData)
        {
            WebViewArgs webViewArgs = navigationData as WebViewArgs;

            if (webViewArgs != null)
            {
                Url = webViewArgs.Url;

                PageTitle = webViewArgs.PageTitle;
            }
            else
            {
                PageTitle = AppResources.Home;
                Url = "https://kosmetik-romy.ch";
            }

            await Task.FromResult(true);
        }

        public async override Task OnAppearing()
        {
            await Task.FromResult(true);
        }

        public async override Task OnDisappearing()
        {
            await Task.FromResult(true);
        }
    }
}
