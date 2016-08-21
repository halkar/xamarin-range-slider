using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.RangeSlider.Forms;

[assembly: ExportRenderer(typeof(RangeSlider), typeof(RangeSliderRenderer))]

namespace Xamarin.RangeSlider.Forms
{
    public class RangeSliderRenderer : ViewRenderer<RangeSlider, RangeSliderControl>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<RangeSlider> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                var rangeSeekBar = new RangeSliderControl(Bounds);
                rangeSeekBar.LowerValueChanged += RangeSeekBarLowerValueChanged;
                rangeSeekBar.UpperValueChanged += RangeSeekBarUpperValueChanged;
                SetNativeControl(rangeSeekBar);
            }
            if (Control != null && Element != null)
            {
                UpdateControl(Control, Element);
            }
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
            if (element.TextSize.HasValue)
                control.TextSize = element.TextSize.Value;
            control.TextFormat = element.TextFormat;
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
                    if (Element.TextSize.HasValue)
                        Control.TextSize = Element.TextSize.Value;
                    ForceFormsLayout();
                    break;
                case RangeSlider.TextFormatPropertyName:
                    Control.TextFormat = Element.TextFormat;
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
            Element.OnUpperValueChanged(Control.UpperValue);
        }

        private void RangeSeekBarLowerValueChanged(object sender, EventArgs e)
        {
            Element.OnLowerValueChanged(Control.LowerValue);
        }
    }
}