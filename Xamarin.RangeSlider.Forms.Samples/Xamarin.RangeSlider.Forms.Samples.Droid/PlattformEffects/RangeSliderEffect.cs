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
      var ctrl = (Xamarin.RangeSlider.RangeSliderControl)Control;

      var res = Resources.System;

      Context context = Xamarin.Forms.Forms.Context;
      Bitmap icon = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.scrubber_control_normal_holo);

      var drawable = ContextCompat.GetDrawable(context, Resource.Drawable.scrubber_control_normal_holo);
      var thumbImage = new BitmapDrawable(ctrl.ThumbImage);

      ctrl.CustomThumbImage = ConvertToBitmap(drawable, icon.Width, icon.Height);
      //ctrl.ThumbPressedImage = ConvertToBitmap(drawable, icon.Width, icon.Height);
    }

    protected override void OnDetached() {
    }

    private static Bitmap ConvertToBitmap(Drawable drawable, int widthPixels, int heightPixels) {
      var mutableBitmap = Bitmap.CreateBitmap(widthPixels, heightPixels, Bitmap.Config.Argb8888);
      var canvas = new Canvas(mutableBitmap);
      drawable.SetBounds(0, 0, widthPixels, heightPixels);
      drawable.Draw(canvas);
      return mutableBitmap;
    }
  }
}