[![Build status](https://ci.appveyor.com/api/projects/status/b90itu0lka0fj1sv/branch/master?svg=true)](https://ci.appveyor.com/project/halkar/xamarin-range-slider/branch/master)
# Xamarin.RangeSlider
With Xamarin.RangeSlider you can pick ranges in Xamarin and Xamarin.Forms (Android, iOS, UWP, Win8 supported).
Project is based on https://github.com/anothem/android-range-seek-bar (Android) and on https://github.com/muZZkat/NMRangeSlider (iOS).

You can find NuGet packages [here](https://www.nuget.org/packages/Xamarin.Forms.RangeSlider/). Version without Xamarin.Forms support is available [here](https://www.nuget.org/packages/Xamarin.RangeSlider/).

## Supported Properties
| Name                  | Description | Remarks |
| --------------------- | ----------- | ---------------|
| LowerValue            | Current lower value | Two way binding |
| UpperValue            | Current upper value | Two way binding |
| MinimumValue          | Maximum value ||
| MaximumValue          | Minimu value ||
| MinThumbHidden        | If *true* lower handle is hidden ||
| MaxThumbHidden        | If *true* upper handle is hidden ||
| StepValue             | Minimal difference between two consecutive values ||
| StepValueContinuously | If *false* the slider will move freely with the tounch. When the touch ends, the value will snap to the nearest step value. If *true* the slider will stay in its current position until it reaches a new step value. ||
| BarHeight             | Height of the slider bar | Not supported on iOS |
| ShowTextAboveThumbs   | Show current values above the thumbs ||
| TextSize              | Text above the thumbs size | *dp* on Android, *points* on iOS, *pixels* on UWP |
| TextFormat            | Format string for text above the thumbs ||

## Supported Events
| Name                  | Description |
| --------------------- | ----------- | 
| DragStarted           | User started moving one of the thumbs to changenge value |
| DragCompleted         | Thumb has been released |

## Supported Delegates
| Name                  | Description |
| --------------------- | ----------- | 
| FormatLabel           | Provide custom formatting for text above thumbs |

## Screenshots
| Android | iOS | UWP |
| ---| --- | --- |
| <img src="https://raw.githubusercontent.com/halkar/xamarin-range-slider/master/Screenshots/android.png" alt="Android" style="width: 300px;"/> | <img src="https://raw.githubusercontent.com/halkar/xamarin-range-slider/master/Screenshots/ios.png" alt="iOS" style="width: 300px;"/> | <img src="https://raw.githubusercontent.com/halkar/xamarin-range-slider/master/Screenshots/uwp.png" alt="UWP" style="width: 300px;"/> |

## Samples
### Sample [project](https://github.com/halkar/xamarin-range-slider/tree/master/Xamarin.RangeSlider.Forms.Samples).
### XAML initialization
```xml
<forms:RangeSlider x:Name="RangeSlider" MinimumValue="1" MaximumValue="100" LowerValue="1" UpperValue="100" StepValue="0" StepValueContinuously="False" VerticalOptions="Center" TextSize="15" />
```
### Displaying dates
```csharp
public MainPage()
{
    InitializeComponent();
    RangeSlider.FormatLabel = FormaLabel;
}

private string FormaLabel(Thumb thumb, float val)
{
    return DateTime.Today.AddDays(val).ToString("d");
}
```

## Customization
### Android
#### Change thumb image
```csharp
using Android.Graphics;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(Droid.Effects.RangeSliderEffect), nameof(Droid.Effects.RangeSliderEffect))]

namespace Droid.Effects
{
    public class RangeSliderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var themeColor = Xamarin.Forms.Color.Fuchsia.ToAndroid();
            var ctrl = (Xamarin.RangeSlider.RangeSliderControl)Control;
            ctrl.ActiveColor = themeColor;

            var thumbImage = new BitmapDrawable(ctrl.ThumbImage);
            thumbImage.SetColorFilter(new PorterDuffColorFilter(themeColor, PorterDuff.Mode.SrcIn));
            ctrl.ThumbImage = ConvertToBitmap(thumbImage, ctrl.ThumbImage.Width, ctrl.ThumbImage.Height);

            var thumbPressedImage = new BitmapDrawable(ctrl.ThumbPressedImage);
            thumbPressedImage.SetColorFilter(new PorterDuffColorFilter(themeColor, PorterDuff.Mode.SrcIn));
            ctrl.ThumbPressedImage = ConvertToBitmap(thumbPressedImage, ctrl.ThumbPressedImage.Width, ctrl.ThumbPressedImage.Height);
        }

        protected override void OnDetached()
        {
        }

        private static Bitmap ConvertToBitmap(Drawable drawable, int widthPixels, int heightPixels)
        {
            var mutableBitmap = Bitmap.CreateBitmap(widthPixels, heightPixels, Bitmap.Config.Argb8888);
            var canvas = new Canvas(mutableBitmap);
            drawable.SetBounds(0, 0, widthPixels, heightPixels);
            drawable.Draw(canvas);
            return mutableBitmap;
        }
    }
}
```
#### Change bar color
```csharp
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(Droid.Effects.RangeSliderEffect), nameof(Droid.Effects.RangeSliderEffect))]

namespace Droid.Effects
{
    public class RangeSliderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var ctrl = (Xamarin.RangeSlider.RangeSliderControl)Control;
            ctrl.DefaultColor = Color.Fuchsia.ToAndroid();
            ctrl.ActiveColor = Color.Aqua.ToAndroid();
        }

        protected override void OnDetached()
        {
        }
    }
}
```
### iOS
#### Change thumb image
Just replace handle images in the `Resources` folder.
#### Change bar color
```csharp
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(iOS.Effects.RangeSliderEffect), nameof(iOS.Effects.RangeSliderEffect))]

namespace iOS.Effects
{
    public class RangeSliderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var ctrl = (Xamarin.RangeSlider.RangeSliderControl)Control;
            ctrl.TintColor = Color.Fuchsia.ToUIColor();
        }

        protected override void OnDetached()
        {
        }
    }
}
```