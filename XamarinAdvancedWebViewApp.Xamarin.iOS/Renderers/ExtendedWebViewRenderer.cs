using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using XamarinAdvancedWebViewApp.Xamarin.Core.Extensions;
using XamarinAdvancedWebViewApp.Xamarin.iOS.Renderers;
using UIKit;
using WebKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.WebView), typeof(ExtendedWebViewRenderer))]
namespace XamarinAdvancedWebViewApp.Xamarin.iOS.Renderers
{
    public class ExtendedWebViewRenderer : WkWebViewRenderer
    {
        WKWebView _wkWebView;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            WKWebView wKWebView = this;

            WKPreferences pref = new WKPreferences() { JavaScriptCanOpenWindowsAutomatically = true, JavaScriptEnabled = true, };

            var config = new WKWebViewConfiguration();

            config.Preferences = pref;

            if (NativeView != null)
            {
                var webView = (WKWebView)NativeView;

                webView.UIDelegate = new ExtendedUIDelegate();
            }
        }

        private class ExtendedUIDelegate : WKUIDelegate
        {
            public override WebKit.WKWebView CreateWebView(WebKit.WKWebView webView, WebKit.WKWebViewConfiguration configuration, WebKit.WKNavigationAction navigationAction, WebKit.WKWindowFeatures windowFeatures)
            {
                var url = navigationAction.Request.Url;

                Launcher.OpenAsync(url);

                return null;
            }
        }

        private class ExtendedNavigatioDelegate : WKNavigationDelegate
        {
            public override void DecidePolicy(WKWebView webView, WKNavigationAction navigationAction, Action<WKNavigationActionPolicy> decisionHandler)
            {
                var url = navigationAction.Request?.Url?.ToString();

                if (url.Search("www.google.com/maps"))
                {
                    Launcher.OpenAsync(url);

                    decisionHandler(WKNavigationActionPolicy.Cancel);
                }
                else
                {
                    decisionHandler(WKNavigationActionPolicy.Allow);
                }
            }
        }
    }
}
