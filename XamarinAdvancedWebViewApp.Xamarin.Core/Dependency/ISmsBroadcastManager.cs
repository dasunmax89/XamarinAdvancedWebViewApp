using System;
namespace XamarinAdvancedWebViewApp.Xamarin.Core.Dependency
{
    public interface ISmsBroadcastManager
    {
        void RegisterSmsReceiver();
        void UnRegisterSmsReceiver();
    }
}
