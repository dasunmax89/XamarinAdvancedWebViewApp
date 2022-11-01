using System;
using System.Diagnostics;
using Android.Content;
using Com.Google.Android.Gms.Auth.Api.Phone;
using Plugin.CurrentActivity;
using XamarinAdvancedWebViewApp.Xamarin.Core.Dependency;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Activities;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(SmsBroadcastManager))]
namespace XamarinAdvancedWebViewApp.Xamarin.Droid.Dependency
{
    public class SmsBroadcastManager: ISmsBroadcastManager
    {
        public SmsBroadcastManager()
        {
        }

        public void RegisterSmsReceiver()
        {
            try
            {
                CrossCurrentActivity.Current.Activity.RegisterReceiver(MainActivity.AppReceiver, new IntentFilter( SmsRetriever.SmsRetrievedAction ));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RegisterSmsReceiver - issue " + ex.Message);
            }
        }

        public void UnRegisterSmsReceiver()
        {
            try
            {
                CrossCurrentActivity.Current.Activity.UnregisterReceiver(MainActivity.AppReceiver);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UnRegisterSmsReceiver - issue " + ex.Message);
            }
        }
    }
}
