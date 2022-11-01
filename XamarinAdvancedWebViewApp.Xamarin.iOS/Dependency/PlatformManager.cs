using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using XamarinAdvancedWebViewApp.Xamarin.Core.Dependency;
using XamarinAdvancedWebViewApp.Xamarin.Core.Helpers;
using XamarinAdvancedWebViewApp.Xamarin.iOS.Dependency;
using Xamarin.Forms;
using System.Linq;
using System.Diagnostics;
using UserNotifications;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using XamarinAdvancedWebViewApp.Xamarin.Core.Ioc;
using XamarinAdvancedWebViewApp.Xamarin.Core.Services;
using XamarinAdvancedWebViewApp.Xamarin.iOS.Helpers;
using System.Collections.Generic;
using XamarinAdvancedWebViewApp.Xamarin.Core.Models;
using XamarinAdvancedWebViewApp.Xamarin.Core.Extensions;
using XamarinAdvancedWebViewApp.Xamarin.Core.Constants;

[assembly: Dependency(typeof(PlatformManager))]
namespace XamarinAdvancedWebViewApp.Xamarin.iOS.Dependency
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
                var netInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                foreach (var netInterface in netInterfaces)
                {
                    if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                        netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                        {
                            if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                ipAddress = addrInfo.Address.ToString();

                                if (!string.IsNullOrEmpty(ipAddress))
                                    break;
                            }
                        }
                    }
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
            UNNotificationSettings settings = null;

            bool enabled = false;

            try
            {
                settings = await UNUserNotificationCenter.Current.GetNotificationSettingsAsync();

                enabled = (settings?.AlertSetting == UNNotificationSetting.Enabled);

                if (!enabled & isForceSettings)
                {
                    UIApplication.SharedApplication.OpenUrl(new NSUrl("app-settings:"));
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("CheckPushEnabled - Error", ex);

                await ShowToast(ex.Message);
            }

            return enabled;
        }

        protected async Task ShowToast(string message)
        {
            var dialogService = AppDependencyResolver.Resolve<IDialogService>();
            await dialogService.ShowToast(message);
        }

        public async Task UpdateBadgeCountAndNotificationCenter(int badgeCount)
        {
            var renderService = AppDependencyResolver.Resolve<IUIRenderService>();

            await DeletePendingNotifications();

            bool isPushEnabled = await CheckPushEnabled(false);

            if (UIApplication.SharedApplication != null)
            {
                UIApplication.SharedApplication.ApplicationIconBadgeNumber = badgeCount;
            }

            if (isPushEnabled)
            {
                if (badgeCount > 0)
                {
                    string titleString = renderService.Translate("AppName");
                    string messageString = renderService.Translate("YouHaveUnreadMessages");

                    string body = string.Format(messageString, badgeCount);

                    NSMutableDictionary userDic = new NSMutableDictionary();
                    userDic.SetValueForKey(NSObject.FromObject(PushNotificationKeys.CHAT_NOTIFICATION), new NSString("messageId"));
                    userDic.SetValueForKey(NSObject.FromObject(PushNotificationStatuses.MESSAGE_NEW), new NSString("status"));
                    userDic.SetValueForKey(NSObject.FromObject(titleString), new NSString("title"));
                    userDic.SetValueForKey(NSObject.FromObject(body), new NSString("body"));
                    userDic.SetValueForKey(NSObject.FromObject(true), new NSString("isLocal"));

                    PushHelper.SendNotification(body, titleString, badgeCount, userDic);
                }
            }
        }

        private async Task DeletePendingNotifications()
        {
            try
            {
                var pendingItems = await UNUserNotificationCenter.Current.GetDeliveredNotificationsAsync();

                if (pendingItems != null)
                {
                    foreach (var pendingItem in pendingItems)
                    {
                        string[] ids = new string[] { pendingItem.Request.Identifier };

                        UNUserNotificationCenter.Current.RemoveDeliveredNotifications(ids);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("DeletePendingNotifications-Exception occured", ex);
            }
        }

        public bool CheckPackageInstalled(string packageName)
        {
            bool installed = false;

            try
            {
                NSUrl url = NSUrl.FromString(packageName);
                installed = UIApplication.SharedApplication.CanOpenUrl(url);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("CheckPackageInstalled-Exception occured", ex);
            }

            return installed;
        }
    }
}
