using System;
using System.Threading.Tasks;
using XamarinAdvancedWebViewApp.Xamarin.Core.Dependency;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Services.Mocks
{
    public class PlatformManagerMock : IPlatformManager
    {
        public async Task<bool> CheckPushEnabled(bool isForceSettings)
        {
            return await Task.FromResult(true);
        }

        public string GetIPAddress()
        {
            return "127.0.0.1";
        }

        public async Task UpdateBadgeCountAndNotificationCenter(int badgeCount)
        {
            await Task.FromResult(true);
        }
    }
}
