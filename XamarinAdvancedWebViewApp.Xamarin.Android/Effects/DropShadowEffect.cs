using System;
using XamarinAdvancedWebViewApp.Xamarin.Core.Effects;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Linq;

[assembly: ResolutionGroupName("XamarinAdvancedWebViewApp.Xamarin")]
[assembly: ExportEffect(typeof(DropShadowEffect), "DropShadowEffect")]
namespace XamarinAdvancedWebViewApp.Xamarin.Droid.Effects
{
    public class DropShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control ?? Container as Android.Views.View;

                var effect = (ViewShadowEffect)Element.Effects.FirstOrDefault(e => e is ViewShadowEffect);

                if (effect != null)
                {
                    float radius = effect.Radius;

                    control.Elevation = radius;
                    control.TranslationZ = (effect.DistanceX + effect.DistanceY) / 2;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: {0}", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
