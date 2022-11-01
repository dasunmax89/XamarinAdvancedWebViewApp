using System;
using System.Collections.Generic;
using System.Globalization;
using XamarinAdvancedWebViewApp.Xamarin.Core.Constants;
using XamarinAdvancedWebViewApp.Xamarin.Core.Enums;
using XamarinAdvancedWebViewApp.Xamarin.Core.Helpers;
using XamarinAdvancedWebViewApp.Xamarin.Core.Ioc;
using XamarinAdvancedWebViewApp.Xamarin.Core.Models;
using XamarinAdvancedWebViewApp.Xamarin.Core.Models.Base;
using XamarinAdvancedWebViewApp.Xamarin.Core.Repositories;
using XamarinAdvancedWebViewApp.Xamarin.Core.Services;
using Xamarin.Forms;

namespace XamarinAdvancedWebViewApp.Xamarin.Core
{
    public class GlobalSetting
    {
        public static GlobalSetting Instance { get; } = new GlobalSetting();

        public string AuthAccessToken { get; set; }

        public SmsVerificationResponse SmsVerificationResponse { get; set; }

        public GetConfigurationResponse AppConfiguration { get; set; }

        public PushNotificationData NewMessagesNotification { get; set; }

        public bool IsPickIntentOn { get; set; }

        private string _baseGatewayEndpoint;
        public string BaseGatewayEndpoint
        {
            get { return _baseGatewayEndpoint; }
            set
            {
                _baseGatewayEndpoint = value;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                ISettingsService settingsService = AppDependencyResolver.Resolve<ISettingsService>();

                return !string.IsNullOrEmpty(settingsService.AuthRefreshToken);
            }
        }

        public CultureInfo CurrentCulture
        {
            get
            {
                return CultureInfo.CurrentCulture;
            }
        }

        private GlobalSetting()
        {

        }

        internal void LoggedIn(SmsVerificationResponse smsVerificationResult, string userName)
        {
            GlobalSetting.Instance.SmsVerificationResponse = smsVerificationResult;

            ISettingsService settingsService = AppDependencyResolver.Resolve<ISettingsService>();

            settingsService.AuthRefreshToken = smsVerificationResult.RefreshToken;

            settingsService.UserName = userName;
        }

        internal async void LoggedOut()
        {
            ISettingsService settingsService = AppDependencyResolver.Resolve<ISettingsService>();
            IAnalyticsService analyticsService = AppDependencyResolver.Resolve<IAnalyticsService>();
            IUserService userService = AppDependencyResolver.Resolve<IUserService>();

            GlobalSetting.Instance.SmsVerificationResponse = null;
            GlobalSetting.Instance.AppConfiguration = null;

            if (!string.IsNullOrEmpty(settingsService.UserName))
            {
                var loggedData = new Dictionary<string, string>
                {
                    { "username", settingsService.UserName },
                };

                analyticsService.TrackEvent(EventNames.UserLogout, loggedData);
            }

            try
            {
                LogoutRequest logoutRequest = new LogoutRequest()
                {
                    AuthAccessToken = settingsService.AuthAccessToken,
                };

                var logoutResponse = await userService.Logout(logoutRequest);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occurred while logging out", ex);
            }

            settingsService.AuthRefreshToken = string.Empty;
            settingsService.AuthAccessToken = string.Empty;
            settingsService.SelectedMenuItemId = (int)PageType.Home;
        }

        internal void PollingStationChanged(long id)
        {
            ISettingsService settingsService = AppDependencyResolver.Resolve<ISettingsService>();

            settingsService.SelectedPollingStationId = id;
        }
    }
}
