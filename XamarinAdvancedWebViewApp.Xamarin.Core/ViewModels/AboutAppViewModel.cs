using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinAdvancedWebViewApp.Xamarin.Core.Constants;
using XamarinAdvancedWebViewApp.Xamarin.Core.Dependency;
using XamarinAdvancedWebViewApp.Xamarin.Core.Localization;
using XamarinAdvancedWebViewApp.Xamarin.Core.Repositories;
using XamarinAdvancedWebViewApp.Xamarin.Core.Services;
using XamarinAdvancedWebViewApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace XamarinAdvancedWebViewApp.Xamarin.Core.ViewModels
{
    public class AboutAppViewModel : ViewModelBase
    {

        #region Actions

        #endregion

        #region Bindables

        #endregion

        #region Commands

        public ICommand CloseCommand { protected set; get; }

        public ICommand SupportUrlTapCommand { protected set; get; }

        #endregion

        public AboutAppViewModel(IConnectionService connectionService, INavigationService navigationService,
            IDialogService dialogService, ISettingsService settingsService, IApplicationDataService applicationDataService,
            IUserService userService, IAnalyticsService analyticsService,
            ILocalDbContextService localDbContext, IPlatformManager platformManager)
          : base(connectionService, navigationService, dialogService, settingsService, applicationDataService,
                userService, analyticsService, localDbContext, platformManager)
        {
            CloseCommand = new Command(OnClose);
            SupportUrlTapCommand = new Command<string>(OnSupportUrlTapped);
        }

        private async void OnClose()
        {
            await _navigationService.NavigateToAsync<MainViewModel>();
        }

        private async void OnSupportUrlTapped(string url)
        {
            await Launcher.OpenAsync(url);
        }

        public override async Task Initialize(object navigationData = null)
        {
            IsBusy = true;

            await Task.FromResult(true);

            IsBusy = false;
        }
    }
}
