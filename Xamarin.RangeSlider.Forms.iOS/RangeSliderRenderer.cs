using System;
using System.ComponentModel;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.RangeSlider.Forms;

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
                var rangeSeekBar = new RangeSliderControl(Bounds);
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
            control.LowerValue = element.LowerValue;
            control.UpperValue = element.UpperValue;
            control.MinimumValue = element.MinimumValue;
            control.MaximumValue = element.MaximumValue;
            control.LowerHandleHidden = element.MinThumbHidden;
            control.UpperHandleHidden = element.MaxThumbHidden;
            control.StepValue = element.StepValue;
            control.StepValueContinuously = element.StepValueContinuously;
            control.ShowTextAboveThumbs = element.ShowTextAboveThumbs;
            control.TextSize = (float)element.TextSize;
            control.TextFormat = element.TextFormat;
            if (element.TextColor != Color.Default)
                control.TextColor = element.TextColor.ToUIColor();
            control.FormatLabel = element.FormatLabel;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case RangeSlider.LowerValuePropertyName:
                    Control.LowerValue = Element.LowerValue;
                    break;
                case RangeSlider.UpperValuePropertyName:
                    Control.UpperValue = Element.UpperValue;
                    break;
                case RangeSlider.MinimumValuePropertyName:
                    Control.MinimumValue = Element.MinimumValue;
                    break;
                case RangeSlider.MaximumValuePropertyName:
                    Control.MaximumValue = Element.MaximumValue;
                    break;
                case RangeSlider.MaxThumbHiddenPropertyName:
                    Control.UpperHandleHidden = Element.MaxThumbHidden;
                    break;
                case RangeSlider.MinThumbHiddenPropertyName:
                    Control.LowerHandleHidden = Element.MinThumbHidden;
                    break;
                case RangeSlider.StepValuePropertyName:
                    Control.StepValue = Element.StepValue;
                    break;
                case RangeSlider.StepValueContinuouslyPropertyName:
                    Control.StepValueContinuously = Element.StepValueContinuously;
                    break;
                case RangeSlider.ShowTextAboveThumbsPropertyName:
                    Control.ShowTextAboveThumbs = Element.ShowTextAboveThumbs;
                    ForceFormsLayout();
                    break;
                case RangeSlider.TextSizePropertyName:
                    Control.TextSize = (float)Element.TextSize;
                    ForceFormsLayout();
                    break;
                case RangeSlider.TextFormatPropertyName:
                    Control.TextFormat = Element.TextFormat;
                    break;
                case RangeSlider.TextColorPropertyName:
                    if (Element.TextColor != Color.Default)
                        Control.TextColor = Element.TextColor.ToUIColor();
                    break;
                case RangeSlider.FormatLabelPropertyName:
                    Control.FormatLabel = Element.FormatLabel;
                    break;
            }
            Control.SetNeedsLayout();
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
            Element.OnUpperValueChanged(Control.UpperValue);
        }

        private void RangeSeekBarLowerValueChanged(object sender, EventArgs e)
        {
            Element.OnLowerValueChanged(Control.LowerValue);
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