
using System;
using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.Content;
using Plugin.Permissions;
using XamarinAdvancedWebViewApp.Xamarin.Core;
using XamarinAdvancedWebViewApp.Xamarin.Core.Constants;
using XamarinAdvancedWebViewApp.Xamarin.Core.Helpers;
using XamarinAdvancedWebViewApp.Xamarin.Core.Models;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Helpers;
using XE = Xamarin.Essentials;

namespace XamarinAdvancedWebViewApp.Xamarin.Droid.Activities
{
    [Activity(Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            XE.Platform.Init(this, savedInstanceState);

            UserDialogs.Init(this);

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            var context = Android.App.Application.Context;

            PushNotificationData pushNotification = PushHelper.GetPushNotificationData(Intent.Extras);

            if (pushNotification != null)
            {
                if (pushNotification.IsNewMessage)
                {
                    GlobalSetting.Instance.NewMessagesNotification = pushNotification;
                }
            }

            App app = new App();

            LoadApplication(app);
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            UnRegisterReceiver();
        }

        private void UnRegisterReceiver()
        {
            try
            {

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UnRegister Receiver" + ex.Message);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            XE.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
        }
        private void LogException(Exception ex)
        {
            LogHelper.LogException("Exception occured", ex);
        }
    }
}