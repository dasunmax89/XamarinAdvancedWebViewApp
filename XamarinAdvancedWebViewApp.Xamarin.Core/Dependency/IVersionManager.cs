using System;
namespace XamarinAdvancedWebViewApp.Xamarin.Core.Dependency
{
    public interface IVersionManager
    {
        String VersionNumber();
        String BuildNumber();
    }
}
