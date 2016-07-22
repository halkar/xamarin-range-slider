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
                Control.LowerValue = Element.LowerValue;
                Control.UpperValue = Element.UpperValue;
                Control.MinimumValue = Element.MinimumValue;
                Control.MaximumValue = Element.MaximumValue;
            }
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