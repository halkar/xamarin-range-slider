using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
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
                var rangeSeekBar = new RangeSliderControl(Context)
                {
                    NotifyWhileDragging = true
                };
                rangeSeekBar.LowerValueChanged += RangeSeekBarLowerValueChanged;
                rangeSeekBar.UpperValueChanged += RangeSeekBarUpperValueChanged;
                SetNativeControl(rangeSeekBar);
            }

            if (Control != null && Element != null)
            {
                Control.SetSelectedMinValue(Element.LowerValue);
                Control.SetSelectedMaxValue(Element.UpperValue);
                Control.SetRangeValues(Element.MinimumValue, Element.MaximumValue);
            }
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
            }
        }

        private void RangeSeekBarUpperValueChanged(object sender, EventArgs e)
        {
            Element.OnUpperValueChanged(Control.GetSelectedMaxValue());
        }

        private void RangeSeekBarLowerValueChanged(object sender, EventArgs e)
        {
            Element.OnLowerValueChanged(Control.GetSelectedMinValue());
        }
    }
}