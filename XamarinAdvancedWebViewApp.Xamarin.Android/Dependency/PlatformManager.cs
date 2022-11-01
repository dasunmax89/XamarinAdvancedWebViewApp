using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using XamarinAdvancedWebViewApp.Xamarin.Core.Dependency;
using XamarinAdvancedWebViewApp.Xamarin.Core.Helpers;
using XamarinAdvancedWebViewApp.Xamarin.Core.Ioc;
using XamarinAdvancedWebViewApp.Xamarin.Core.Models;
using XamarinAdvancedWebViewApp.Xamarin.Core.Services;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Dependency;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Helpers;
using Xamarin.Forms;
using XamarinAdvancedWebViewApp.Xamarin.Core.Extensions;

[assembly: Dependency(typeof(PlatformManager))]
namespace XamarinAdvancedWebViewApp.Xamarin.Droid.Dependency
{
    public class PlatformManager : IPlatformManager
    {
        public PlatformManager()
        {

        }

        public string GetIPAddress()
        {
            string ipAddress = string.Empty;

            try
            {
                IPAddress[] adresses = Dns.GetHostAddresses(Dns.GetHostName());

                if (adresses != null && adresses[0] != null)
                {
                    ipAddress = adresses[0].ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("GetIPAddress - Error", ex);
            }

            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = "127.0.0.1";
            }

            return ipAddress;

        }

        public async Task<bool> CheckPushEnabled(bool isForceSettings)
        {
            bool enabled = false;

            try
            {
                var nm = NotificationManagerCompat.From(Android.App.Application.Context);

                enabled = nm.AreNotificationsEnabled();

                if (!enabled && isForceSettings)
                {
                    OpenPushSettings();
                }
            }
            catch (Exception ex)
            {
                await ShowToast(ex.Message);

                LogHelper.LogException("CheckPushEnabled-Exception occured", ex);
            }

            return enabled;
        }

        private void OpenPushSettings()
        {
            var context = Android.App.Application.Context;
            var appInfo = context.PackageManager.GetApplicationInfo(context.PackageName, 0);

            string packageName = context.PackageName;

            var uri = Android.Net.Uri.Parse("package:" + packageName);

            Intent settingIntent = new Intent(Android.Provider.Settings.ActionAppNotificationSettings);

            //for Android 5-7
            settingIntent.PutExtra("app_package", packageName);

            if (appInfo != null)
            {
                settingIntent.PutExtra("app_uid", appInfo.Uid);
            }

            // for Android 8 and above
            settingIntent.PutExtra("android.provider.extra.APP_PACKAGE", packageName);

            settingIntent.AddFlags(ActivityFlags.NewTask);

            context.StartActivity(settingIntent);
        }

        protected async Task ShowToast(string message)
        {
            var dialogService = AppDependencyResolver.Resolve<IDialogService>();
            await dialogService.ShowToast(message);
        }

        public async Task UpdateBadgeCountAndNotificationCenter(int badgeCount)
        {
            try
            {
                var context = Android.App.Application.Context;

                bool isPushEnabled = await CheckPushEnabled(false);

                if (isPushEnabled)
                {
                    var notificationManager = NotificationManager.FromContext(context);

                    var renderService = AppDependencyResolver.Resolve<IUIRenderService>();

                    string title = renderService.Translate("AppName");
                    string message = renderService.Translate("YouHaveUnreadMessages");

                    string formattedMessage = string.Format(message, badgeCount);

                    var pendingItems = notificationManager.GetActiveNotifications();

                    notificationManager.CancelAll();

                    if (badgeCount > 0)
                    {
                        PushHelper.SendNotification(formattedMessage, title, context, badgeCount);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("UpdateBadgeCount-Exception occured", ex);

                await ShowToast(ex.Message);
            }
        }

        public bool CheckPackageInstalled(string packageName)
        {
            bool installed = false;

            try
            {
                var context = Android.App.Application.Context;
                var packageInfo = context.PackageManager.GetPackageInfo(packageName, PackageInfoFlags.Activities);

                installed = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("CheckPackageInstalled-Exception occured", ex);
            }

            return installed;
        }
    }
}
