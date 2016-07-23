using System.ComponentModel;
using Xamarin.RangeSlider.Forms;
using RangeSlider = Xamarin.RangeSlider.Forms.RangeSlider;

#if WINDOWS_APP || WINDOWS_PHONE_APP
using Xamarin.Forms.Platform.WinRT;
#elif WINDOWS_UWP
using Xamarin.Forms.Platform.UWP;
#endif

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
                var rangeSlider = new RangeSliderControl();
                if (Element != null)
                    UpdateControl(rangeSlider, Element);
                rangeSlider.LowerValueChanged += RangeSlider_LowerValueChanged;
                rangeSlider.UpperValueChanged += RangeSlider_UpperValueChanged;
                SetNativeControl(rangeSlider);
            }

            if (Control != null && Element != null)
            {
                UpdateControl(Control, Element);
            }
        }

        private void UpdateControl(RangeSliderControl control, RangeSlider element)
        {
            control.Minimum = element.MinimumValue;
            control.Maximum = element.MaximumValue;
            control.RangeMin = element.LowerValue;
            control.RangeMax = element.UpperValue;
        }

        private void RangeSlider_UpperValueChanged(object sender, System.EventArgs e)
        {
            Element.UpperValue = (float)Control.RangeMax;
        }

        private void RangeSlider_LowerValueChanged(object sender, System.EventArgs e)
        {
            Element.LowerValue = (float)Control.RangeMin;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case RangeSlider.LowerValuePropertyName:
                    Control.RangeMin = Element.LowerValue;
                    break;
                case RangeSlider.UpperValuePropertyName:
                    Control.RangeMax = Element.UpperValue;
                    break;
                case RangeSlider.MinimumValuePropertyName:
                    Control.Minimum = Element.MinimumValue;
                    break;
                case RangeSlider.MaximumValuePropertyName:
                    Control.Maximum = Element.MaximumValue;
                    break;
            }
        }
    }
}