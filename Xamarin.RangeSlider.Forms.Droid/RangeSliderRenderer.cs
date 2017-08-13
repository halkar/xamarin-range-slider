using System;
using System.ComponentModel;
using Android.Runtime;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.RangeSlider.Forms;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(RangeSlider), typeof(RangeSliderRenderer))]

namespace Xamarin.RangeSlider.Forms
{
    [Preserve(AllMembers = true)]
    public class RangeSliderRenderer : ViewRenderer<RangeSlider, RangeSliderControl>
    {
        private bool _gestureEnabledPreviousState;
        protected override void OnElementChanged(ElementChangedEventArgs<RangeSlider> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;
            if (Control == null)
            {
                var rangeSeekBar = new RangeSliderControl(Context)
                {
                    NotifyWhileDragging = true,
                    TextAboveThumbsColor = Color.Black
                };
                rangeSeekBar.LowerValueChanged += RangeSeekBarLowerValueChanged;
                rangeSeekBar.UpperValueChanged += RangeSeekBarUpperValueChanged;
                rangeSeekBar.DragStarted += RangeSeekBarDragStarted;
                rangeSeekBar.DragCompleted += RangeSeekBarDragCompleted;
                SetNativeControl(rangeSeekBar);
            }
            UpdateControl(Control, Element);
        }

        private void RangeSeekBarDragCompleted(object sender, EventArgs e)
        {
            RestoreGestures();
            Element.OnDragCompleted();
        }

        private void RangeSeekBarDragStarted(object sender, EventArgs e)
        {
            Element.OnDragStarted();
            DisableGestures();
        }

        private void UpdateControl(RangeSliderControl control, RangeSlider element)
        {
            control.SetSelectedMinValue(element.LowerValue);
            control.SetSelectedMaxValue(element.UpperValue);
            control.SetRangeValues(element.MinimumValue, element.MaximumValue);
            control.MinThumbHidden = element.MinThumbHidden;
            control.MaxThumbHidden = element.MaxThumbHidden;
            control.StepValue = element.StepValue;
            control.StepValueContinuously = element.StepValueContinuously;
            if (element.BarHeight.HasValue)
                control.SetBarHeight(element.BarHeight.Value);
            control.ShowTextAboveThumbs = element.ShowTextAboveThumbs;
            control.TextSizeInSp = (int)Font.SystemFontOfSize(element.TextSize).ToScaledPixel();
            control.TextFormat = element.TextFormat;
            if (element.TextColor != Xamarin.Forms.Color.Default)
                control.TextAboveThumbsColor = element.TextColor.ToAndroid();
            control.FormatLabel = element.FormatLabel;
			control.ActivateOnDefaultValues = true;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case RangeSlider.LowerValuePropertyName:
                    Control.SetSelectedMinValue(Element.LowerValue);
                    break;
                case RangeSlider.UpperValuePropertyName:
                    Control.SetSelectedMaxValue(Element.UpperValue);
                    break;
                case RangeSlider.MinimumValuePropertyName:
                case RangeSlider.MaximumValuePropertyName:
                    Control.SetRangeValues(Element.MinimumValue, Element.MaximumValue);
                    break;
                case RangeSlider.MaxThumbHiddenPropertyName:
                    Control.MaxThumbHidden = Element.MaxThumbHidden;
                    break;
                case RangeSlider.MinThumbHiddenPropertyName:
                    Control.MinThumbHidden = Element.MinThumbHidden;
                    break;
                case RangeSlider.StepValuePropertyName:
                    Control.StepValue = Element.StepValue;
                    break;
                case RangeSlider.StepValueContinuouslyPropertyName:
                    Control.StepValueContinuously = Element.StepValueContinuously;
                    break;
                case RangeSlider.BarHeightPropertyName:
                    if (Element.BarHeight.HasValue)
                        Control.SetBarHeight(Element.BarHeight.Value);
                    break;
                case RangeSlider.ShowTextAboveThumbsPropertyName:
                    Control.ShowTextAboveThumbs = Element.ShowTextAboveThumbs;
                    ForceFormsLayout();
                    break;
                case RangeSlider.TextSizePropertyName:
                    Control.TextSizeInSp = (int)Font.SystemFontOfSize(Element.TextSize).ToScaledPixel();
                    ForceFormsLayout();
                    break;
                case RangeSlider.TextFormatPropertyName:
                    Control.TextFormat = Element.TextFormat;
                    break;
                case RangeSlider.TextColorPropertyName:
                    if (Element.TextColor != Xamarin.Forms.Color.Default)
                        Control.TextAboveThumbsColor = Element.TextColor.ToAndroid();
                    break;
                case RangeSlider.FormatLabelPropertyName:
                    Control.FormatLabel = Element.FormatLabel;
                    break;
            }
        }

        private void ForceFormsLayout()
        {
            //HACK to force Xamarin.Forms layout engine to update control size
            if (!Element.IsVisible) return;
            Element.IsVisible = false;
            Element.IsVisible = true;
        }

        private void RangeSeekBarUpperValueChanged(object sender, EventArgs e)
        {
            Element.OnUpperValueChanged(Control.GetSelectedMaxValue());
        }

        private void RangeSeekBarLowerValueChanged(object sender, EventArgs e)
        {
            Element.OnLowerValueChanged(Control.GetSelectedMinValue());
        }

        // TODO find less weird hack to make slider work on Master-Detail page
        private void DisableGestures()
        {
            var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            if (masterDetailPage != null)
            {
                _gestureEnabledPreviousState = masterDetailPage.IsGestureEnabled;
                masterDetailPage.IsGestureEnabled = false;
            }
        }

        private void RestoreGestures()
        {
            var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            if (masterDetailPage != null)
            {
                masterDetailPage.IsGestureEnabled = _gestureEnabledPreviousState;
            }
        }
    }
}