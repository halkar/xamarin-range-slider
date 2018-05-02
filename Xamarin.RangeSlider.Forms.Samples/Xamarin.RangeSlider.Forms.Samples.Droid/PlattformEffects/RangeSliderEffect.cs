using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.RangeSlider.Forms.Samples.Droid.PlattformEffects;

[assembly: ResolutionGroupName("EffectsSlider")]
[assembly: ExportEffect(typeof(RangeSliderEffect), "RangeSliderEffect")]
namespace Xamarin.RangeSlider.Forms.Samples.Droid.PlattformEffects {
  public class RangeSliderEffect : PlatformEffect {
    protected override void OnAttached() {
      var ctrl = (RangeSliderControl)Control;

      Context context = Xamarin.Forms.Forms.Context;
      Bitmap icon = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.scrubber_control_normal_holo);

      ctrl.SetCustomThumbImage(icon);
    }

    protected override void OnDetached() {
    }
  }
}