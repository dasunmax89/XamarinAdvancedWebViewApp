using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinAdvancedWebViewApp.Xamarin.Core.Models;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Dependency
{
    public interface IPlatformManager
    {
        string GetIPAddress();

        Task<bool> CheckPushEnabled(bool isForceSettings);

        Task UpdateBadgeCountAndNotificationCenter(int badgeCount);
    }
}
