using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinAdvancedWebViewApp.Xamarin.Core.Constants;
using XamarinAdvancedWebViewApp.Xamarin.Core.Dependency;
using XamarinAdvancedWebViewApp.Xamarin.Core.Helpers;
using XamarinAdvancedWebViewApp.Xamarin.Core.Localization;
using XamarinAdvancedWebViewApp.Xamarin.Core.Models;
using XamarinAdvancedWebViewApp.Xamarin.Core.Repositories;
using XamarinAdvancedWebViewApp.Xamarin.Core.Services;
using XamarinAdvancedWebViewApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.ViewModels
{
    public class InnerWebViewModel : ViewModelBase
    {
        #region Fields

        public bool IsFirstTime { get; set; } = true;

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

        public InnerWebViewModel(IConnectionService connectionService, INavigationService navigationService,
            IDialogService dialogService, ISettingsService settingsService, IApplicationDataService applicationDataService,
            IUserService userService, IAnalyticsService analyticsService,
            ILocalDbContextService localDbContext, IPlatformManager platformManager)
          : base(connectionService, navigationService, dialogService, settingsService, applicationDataService,
                userService, analyticsService, localDbContext, platformManager)
        {
            Url = ApiConstants.HomeProdEndPoint;
        }

        public override async Task Initialize(object navigationData)
        {
            WebViewArgs webViewArgs = navigationData as WebViewArgs;

            if (webViewArgs != null)
            {
                Url = webViewArgs.Url;

                PageTitle = webViewArgs.PageTitle;
            }

            await Task.FromResult(true);
        }

        public async override Task OnAppearing()
        {
            await Task.FromResult(true);

            TrackView();

            await CheckRunTimePermission();
        }

        public async override Task OnDisappearing()
        {
            await Task.FromResult(true);
        }

        public async override Task NavigateToBack()
        {
            await _navigationService.NavigateToBack();
        }

        public virtual void WebViewNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (IsFirstTime)
            {
                IsFirstTime = false;

                IsBusy = true;
            }
        }

        public virtual void WebViewNavigated(object sender, WebNavigatedEventArgs e)
        {
            IsBusy = false;
        }

        private async Task CheckRunTimePermission()
        {
            try
            {
                var locationStatus = await PermissionHelper.RequestPermission<Permissions.LocationWhenInUse>();
                var storageReadStatus = await PermissionHelper.RequestPermission<Permissions.StorageRead>();
                var storageWritwStatus = await PermissionHelper.RequestPermission<Permissions.StorageWrite>();
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while requestiong the permissions", ex);
            }
        }
    }
}
