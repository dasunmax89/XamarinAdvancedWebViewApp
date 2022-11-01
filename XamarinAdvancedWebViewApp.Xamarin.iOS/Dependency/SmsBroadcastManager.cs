using XamarinAdvancedWebViewApp.Xamarin.Core.Dependency;
using XamarinAdvancedWebViewApp.Xamarin.iOS.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(SmsBroadcastManager))]
namespace XamarinAdvancedWebViewApp.Xamarin.iOS.Dependency
{
    public class SmsBroadcastManager : ISmsBroadcastManager
    {
        public void RegisterSmsReceiver()
        {
            //TODO implement feature
        }

        public void UnRegisterSmsReceiver()
        {
            //TODO implement feature
        }
    }
}
