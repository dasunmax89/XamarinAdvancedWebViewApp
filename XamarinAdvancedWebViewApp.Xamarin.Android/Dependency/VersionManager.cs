using System;
using Android.Content.PM;
using XamarinAdvancedWebViewApp.Xamarin.Core.Dependency;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(VersionManager))]
namespace XamarinAdvancedWebViewApp.Xamarin.Droid.Dependency
{
    public class VersionManager : IVersionManager
    {
        PackageInfo _appInfo;

        public VersionManager()
        {
            var context = Android.App.Application.Context;
            _appInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
        }

        public string BuildNumber()
        {
            return _appInfo.VersionCode.ToString();
        }

        public string VersionNumber()
        {
            return _appInfo.VersionName;
        }
    }
}
