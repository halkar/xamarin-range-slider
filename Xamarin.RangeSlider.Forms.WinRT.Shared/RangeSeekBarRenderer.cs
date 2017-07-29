using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.RangeSlider.Forms;
using Xamarin.Forms.Internals;
using RangeSlider = Xamarin.RangeSlider.Forms.RangeSlider;

#if WINDOWS_APP || WINDOWS_PHONE_APP
using Xamarin.Forms.Platform.WinRT;
#elif WINDOWS_UWP
using Xamarin.Forms.Platform.UWP;
#endif

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
            if(Element == null)
                return;
            if (Control == null)
            {
                var rangeSlider = new RangeSliderControl();
                rangeSlider.LowerValueChanged += RangeSlider_LowerValueChanged;
                rangeSlider.UpperValueChanged += RangeSlider_UpperValueChanged;
                rangeSlider.DragStarted += RangeSlider_DragStarted;
                rangeSlider.DragCompleted += RangeSlider_DragCompleted;
                rangeSlider.SizeChanged += RangeSlider_SizeChanged;
                SetNativeControl(rangeSlider);
            }
            UpdateControl(Control, Element);
        }

        private void RangeSlider_DragCompleted(object sender, EventArgs e)
        {
            RestoreGestures();
            Element.OnDragCompleted();
        }

        private void RangeSlider_DragStarted(object sender, EventArgs e)
        {
            Element.OnDragStarted();
            DisableGestures();
        }

        private void RangeSlider_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            ForceFormsLayout();
        }

        private void UpdateControl(RangeSliderControl control, RangeSlider element)
        {
            control.IgnoreRangeChecks = true;
            control.Minimum = element.MinimumValue;
            control.Maximum = element.MaximumValue;
            control.RangeMin = element.LowerValue;
            control.RangeMax = element.UpperValue;
            control.MinThumbHidden = element.MinThumbHidden;
            control.MaxThumbHidden = element.MaxThumbHidden;
            control.StepValue = element.StepValue;
            control.StepValueContinuously = element.StepValueContinuously;
            if (element.BarHeight.HasValue)
                control.SetBarHeight(element.BarHeight.Value);
            control.ShowTextAboveThumbs = element.ShowTextAboveThumbs;
            control.TextSize = (int) element.TextSize;
            control.TextFormat = element.TextFormat;
            control.FormatLabel = element.FormatLabel;
            control.IgnoreRangeChecks = false;
            control.ValidateMinValue(element.LowerValue);
            control.ValidateMaxValue(element.UpperValue);
        }

        private void RangeSlider_UpperValueChanged(object sender, EventArgs e)
        {
            Element.OnUpperValueChanged((float)Control.RangeMax);
        }

        private void RangeSlider_LowerValueChanged(object sender, EventArgs e)
        {
            Element.OnLowerValueChanged((float)Control.RangeMin);
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
                    break;
                case RangeSlider.TextSizePropertyName:
                    Control.TextSize = (int) Element.TextSize;
                    break;
                case RangeSlider.TextFormatPropertyName:
                    Control.TextFormat = Element.TextFormat;
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