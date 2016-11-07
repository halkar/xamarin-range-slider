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

## Screenshots
| Android | iOS | UWP |
| ---| --- | --- |
| <img src="https://raw.githubusercontent.com/halkar/xamarin-range-slider/master/Screenshots/android.png" alt="Android" style="width: 300px;"/> | <img src="https://raw.githubusercontent.com/halkar/xamarin-range-slider/master/Screenshots/ios.png" alt="iOS" style="width: 300px;"/> | <img src="https://raw.githubusercontent.com/halkar/xamarin-range-slider/master/Screenshots/uwp.png" alt="UWP" style="width: 300px;"/> |