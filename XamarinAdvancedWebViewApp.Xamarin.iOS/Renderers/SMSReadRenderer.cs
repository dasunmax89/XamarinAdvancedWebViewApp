﻿using System;
using System.Drawing;
using CoreGraphics;
using XamarinAdvancedWebViewApp.Xamarin.Core.Controls;
using XamarinAdvancedWebViewApp.Xamarin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SMSAutoReadEntry), typeof(SMSReadRenderer))]
namespace XamarinAdvancedWebViewApp.Xamarin.iOS.Renderers
{
    public class SMSReadRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control != null)
                {
                    if (UIDevice.CurrentDevice.CheckSystemVersion(12, 0))
                    {
                        Control.TextContentType = UITextContentType.OneTimeCode;
                    }

                    if (this.Element.Keyboard == Keyboard.Numeric)
                    {
                        RenderUtil.AddDoneButton(Control);
                    }
                }
            }
        }
    }
}
