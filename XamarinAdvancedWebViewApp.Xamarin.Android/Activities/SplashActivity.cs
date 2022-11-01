using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using XamarinAdvancedWebViewApp.Xamarin.Core;
using XamarinAdvancedWebViewApp.Xamarin.Core.Constants;
using XamarinAdvancedWebViewApp.Xamarin.Core.Models;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Helpers;

namespace XamarinAdvancedWebViewApp.Xamarin.Droid.Activities
{
    [Activity(Icon = "@mipmap/ic_launcher",
        Theme = "@style/SplashTheme",
        MainLauncher = true,
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            PushNotificationData pushNotification = PushHelper.GetPushNotificationData(Intent.Extras);

            if (pushNotification != null)
            {
                if (pushNotification.IsNewMessage)
                {
                    GlobalSetting.Instance.NewMessagesNotification = pushNotification;
                }
            }

            CallMainActivity();
        }

        private void CallMainActivity()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}
