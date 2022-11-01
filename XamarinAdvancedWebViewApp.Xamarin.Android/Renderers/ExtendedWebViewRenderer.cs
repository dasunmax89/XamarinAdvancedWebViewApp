using System;
using Android.Annotation;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using WK = Android.Webkit;
using AW = Android.Widget;

using XamarinAdvancedWebViewApp.Xamarin.Core.Extensions;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Renderers;
using XF = Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Activities;
using System.IO;
using Xamarin.Essentials;
using System.Threading.Tasks;
using XamarinAdvancedWebViewApp.Xamarin.Core.Helpers;
using XamarinAdvancedWebViewApp.Xamarin.Core.Ioc;
using XamarinAdvancedWebViewApp.Xamarin.Core.Services;
using Android.Graphics;
using Android.Net.Http;
using Android.OS;
using Android.Webkit;

[assembly: XF.ExportRenderer(typeof(Xamarin.Forms.WebView), typeof(ExtendedWebViewRenderer))]
namespace XamarinAdvancedWebViewApp.Xamarin.Droid.Renderers
{
    public class ExtendedWebViewRenderer : WebViewRenderer
    {
        Activity mContext;
        string _photoPath;

        public ExtendedWebViewRenderer(Context context) : base(context)
        {
            this.mContext = context as Activity;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<XF.WebView> e)
        {
            base.OnElementChanged(e);
            SetWebViewParams(Control, mContext);
        }

        public void SetWebViewParams(WK.WebView control, Activity context)
        {
            control.SetWebChromeClient(new ExtendedWebChromeClient(context));
            control.SetWebViewClient(new ExtendedWebClient(context, this));

            control.Settings.JavaScriptEnabled = true;
            control.ClearCache(true);
            control.Settings.DomStorageEnabled = true;
            control.Settings.SetPluginState(WK.WebSettings.PluginState.On);
            control.Settings.AllowFileAccessFromFileURLs = true;
            control.Settings.AllowUniversalAccessFromFileURLs = true;
            control.Settings.AllowContentAccess = true;
            control.Settings.SetGeolocationEnabled(true);
            control.Settings.JavaScriptCanOpenWindowsAutomatically = true;
            control.Settings.SetSupportMultipleWindows(true);
        }

        public class ExtendedWebChromeClient : WK.WebChromeClient
        {
            readonly Activity mContext;
            string _photoPath;

            public ExtendedWebChromeClient(Activity contxt)
            {
                mContext = contxt;
            }

            public override bool OnCreateWindow(WK.WebView webView, bool isDialog, bool isUserGesture, Message resultMsg)
            {
                try
                {
                    WK.WebView newWebView = new WK.WebView(mContext);
                    newWebView.Settings.JavaScriptEnabled = true;
                    newWebView.Settings.SetSupportZoom(true);
                    newWebView.Settings.BuiltInZoomControls = true;
                    newWebView.Settings.SetPluginState(WK.WebSettings.PluginState.On);
                    newWebView.Settings.SetSupportMultipleWindows(true);
                    newWebView.Settings.SetGeolocationEnabled(true);

                    WK.WebView.WebViewTransport transport = (WK.WebView.WebViewTransport)resultMsg.Obj;
                    transport.WebView = newWebView;
                    resultMsg.SendToTarget();

                    newWebView.SetWebViewClient(new WebActivityClient(mContext));
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("Error occurred in Launcher.TryOpenAsync", ex);
                }

                return true;
            }

            /// <summary>
            /// Called when the web content is requesting permission to access some resources.
            /// </summary>
            /// <param name="request">Request.</param>
            [TargetApi(Value = 21)]
            public override void OnPermissionRequest(Android.Webkit.PermissionRequest request)
            {
                mContext.RunOnUiThread(() =>
                {
                    request.Grant(request.GetResources());
                });
            }

            /// <summary>
            /// Called when the permission request is canceled by the web content.
            /// </summary>
            /// <param name="request">Request.</param>
            public override void OnPermissionRequestCanceled(Android.Webkit.PermissionRequest request)
            {

            }

            public override void OnGeolocationPermissionsShowPrompt(string origin, GeolocationPermissions.ICallback callback)
            {
                callback.Invoke(origin, true, false);
            }

            public override bool OnShowFileChooser(Android.Webkit.WebView webView, Android.Webkit.IValueCallback filePathCallback, FileChooserParams fileChooserParams)
            {

                AlertDialog.Builder alertDialog = new AlertDialog.Builder(mContext);

                var renderService = AppDependencyResolver.Resolve<IUIRenderService>();

                string title = renderService.Translate("SelectOption");
                string takePicture = renderService.Translate("MakePhoto");
                string choosePicture = renderService.Translate("ChooseFromPhotos");

                alertDialog.SetTitle(title);

                alertDialog.SetNeutralButton(takePicture, async (sender, alertArgs) =>
                {
                    try
                    {
                        var photo = await MediaPicker.CapturePhotoAsync();
                        var uri = await LoadPhotoAsync(photo);
                        filePathCallback.OnReceiveValue(uri);
                    }
                    catch (System.Exception ex)
                    {
                        LogHelper.LogException($"CapturePhotoAsync THREW", ex);
                    }
                });

                alertDialog.SetNegativeButton(choosePicture, async (sender, alertArgs) =>
                {
                    try
                    {
                        var photo = await MediaPicker.PickPhotoAsync();
                        var uri = await LoadPhotoAsync(photo);
                        filePathCallback.OnReceiveValue(uri);
                    }
                    catch (System.Exception ex)
                    {
                        LogHelper.LogException($"PickPhotoAsync THREW", ex);
                    }
                });

                alertDialog.SetPositiveButton("Cancel", (sender, alertArgs) =>
                {
                    filePathCallback.OnReceiveValue(null);
                });

                Dialog dialog = alertDialog.Create();

                dialog.Show();

                return true;
            }

            public async Task<Android.Net.Uri[]> LoadPhotoAsync(FileResult photo)
            {
                // cancelled
                if (photo == null)
                {
                    _photoPath = null;
                    return null;
                }

                // save the file into local storage
                var newFile = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using (var stream = await photo.OpenReadAsync())
                using (var newStream = System.IO.File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
                _photoPath = newFile;

                Android.Net.Uri uri = Android.Net.Uri.FromFile(new Java.IO.File(_photoPath));

                return new Android.Net.Uri[] { uri };
            }
        }

        private class ExtendedWebClient : WK.WebViewClient
        {
            private readonly Activity mContext;
            private readonly ExtendedWebViewRenderer renderer;

            public ExtendedWebClient(Activity mContext, ExtendedWebViewRenderer renderer)
            {
                this.mContext = mContext;
                this.renderer = renderer;
            }

            public override bool ShouldOverrideUrlLoading(WK.WebView view, WK.IWebResourceRequest request)
            {
                if (request.Url != null)
                {

                }
                return base.ShouldOverrideUrlLoading(view, request);
            }

            public override void OnPageFinished(WK.WebView view, string url)
            {
                base.OnPageFinished(view, url);

                var source = new XF.UrlWebViewSource { Url = url };
                var args = new XF.WebNavigatedEventArgs(XF.WebNavigationEvent.NewPage, source, url, XF.WebNavigationResult.Success);
                renderer.ElementController?.SendNavigated(args);
            }

            public override void OnPageStarted(WK.WebView view, string url, Bitmap favicon)
            {
                base.OnPageStarted(view, url, favicon);

                var args = new XF.WebNavigatingEventArgs(XF.WebNavigationEvent.NewPage, new XF.UrlWebViewSource { Url = url }, url);
                renderer.ElementController?.SendNavigating(args);
            }

            public override void OnReceivedSslError(WK.WebView view, WK.SslErrorHandler handler, SslError error)
            {
                //base.OnReceivedSslError(view, handler, error);
                handler.Proceed();
            }
        }

        private class WebActivityClient : WK.WebViewClient
        {
            //unused
            private Activity mContext;

            public WebActivityClient(Activity mContext)
            {
                this.mContext = mContext;
            }

            public override bool ShouldOverrideUrlLoading(WK.WebView view, WK.IWebResourceRequest request)
            {
                var url = request.Url.ToString();

                if (url.Search("www.google.com/maps"))
                {
                    Launcher.OpenAsync(url);
                    return true;
                }
                else
                {
                    return base.ShouldOverrideUrlLoading(view, request);
                }
            }
        }
    }
}
