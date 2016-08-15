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
            control.StepValueContinuously = element.StepValue.HasValue;
            if (element.StepValue.HasValue)
                control.StepValue = element.StepValue.Value;
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
                    Control.StepValueContinuously = Element.StepValue.HasValue;
                    if (Element.StepValue.HasValue)
                        Control.StepValue = Element.StepValue.Value;
                    break;
            }
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