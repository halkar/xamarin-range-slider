using System;
using System.ComponentModel;
using Windows.UI;
using Xamarin.Forms;
using Xamarin.RangeSlider.Forms;
using Xamarin.Forms.Internals;
using Color = Windows.UI.Color;
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
            control.MinThumbTextHidden = element.MinThumbTextHidden;
            control.MaxThumbHidden = element.MaxThumbHidden;
            control.MaxThumbTextHidden = element.MaxThumbTextHidden;
            control.StepValue = element.StepValue;
            control.StepValueContinuously = element.StepValueContinuously;
            if (element.BarHeight.HasValue)
                control.SetBarHeight(element.BarHeight.Value);
            control.ShowTextAboveThumbs = element.ShowTextAboveThumbs;
            control.TextSize = (int) element.TextSize;
            control.TextFormat = element.TextFormat;
            if (element.TextColor != Xamarin.Forms.Color.Default)
                control.TextColor = Color.FromArgb((byte)(element.TextColor.A * 255), (byte)(element.TextColor.R * 255), (byte)(element.TextColor.G * 255), (byte)(element.TextColor.B * 255));
            control.FormatLabel = element.FormatLabel;
            control.IgnoreRangeChecks = false;
            control.ValidateMinValue(element.LowerValue);
            control.ValidateMaxValue(element.UpperValue);
            if (element.ActiveColor != Xamarin.Forms.Color.Default)
                control.ActiveColor = Color.FromArgb((byte)(element.ActiveColor.A * 255), (byte)(element.ActiveColor.R * 255), (byte)(element.ActiveColor.G * 255), (byte)(element.ActiveColor.B * 255));
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

            if (e.PropertyName == RangeSlider.LowerValueProperty.PropertyName)
            {
                Control.RangeMin = Element.LowerValue;
            }
            else if (e.PropertyName == RangeSlider.UpperValueProperty.PropertyName)
            {
                Control.RangeMax = Element.UpperValue;
            }
            else if (e.PropertyName == RangeSlider.MinimumValueProperty.PropertyName)
            {
                Control.Minimum = Element.MinimumValue;
            }
            else if (e.PropertyName == RangeSlider.MaximumValueProperty.PropertyName)
            {
                Control.Maximum = Element.MaximumValue;
            }
            else if (e.PropertyName == RangeSlider.MaxThumbHiddenProperty.PropertyName)
            {
                Control.MaxThumbHidden = Element.MaxThumbHidden;
            }
            else if (e.PropertyName == RangeSlider.MinThumbHiddenProperty.PropertyName)
            {
                Control.MinThumbHidden = Element.MinThumbHidden;
            }
            else if (e.PropertyName == RangeSlider.StepValueProperty.PropertyName)
            {
                Control.StepValue = Element.StepValue;
            }
            else if (e.PropertyName == RangeSlider.StepValueContinuouslyProperty.PropertyName)
            {
                Control.StepValueContinuously = Element.StepValueContinuously;
            }
            else if (e.PropertyName == RangeSlider.BarHeightProperty.PropertyName)
            {
                if (Element.BarHeight.HasValue)
                    Control.SetBarHeight(Element.BarHeight.Value);
            }
            else if (e.PropertyName == RangeSlider.ShowTextAboveThumbsProperty.PropertyName)
            {
                Control.ShowTextAboveThumbs = Element.ShowTextAboveThumbs;
            }
            else if (e.PropertyName == RangeSlider.TextSizeProperty.PropertyName)
            {
                Control.TextSize = (int)Element.TextSize;
            }
            else if (e.PropertyName == RangeSlider.TextFormatProperty.PropertyName)
            {
                Control.TextFormat = Element.TextFormat;
            }
            else if (e.PropertyName == RangeSlider.TextColorProperty.PropertyName)
            {
                if (Element.TextColor != Xamarin.Forms.Color.Default)
                    Control.TextColor = Color.FromArgb((byte)(Element.TextColor.A * 255), (byte)(Element.TextColor.R * 255), (byte)(Element.TextColor.G * 255), (byte)(Element.TextColor.B * 255));
            }
            else if (e.PropertyName == RangeSlider.FormatLabelProperty.PropertyName)
            {
                Control.FormatLabel = Element.FormatLabel;
            }
            else if (e.PropertyName == RangeSlider.ActiveColorProperty.PropertyName)
            {
                if (Element.ActiveColor != Xamarin.Forms.Color.Default)
                    Control.ActiveColor = Color.FromArgb((byte)(Element.ActiveColor.A * 255), (byte)(Element.ActiveColor.R * 255), (byte)(Element.ActiveColor.G * 255), (byte)(Element.ActiveColor.B * 255));
            }
            else if (e.PropertyName == RangeSlider.MinThumbTextHiddenProperty.PropertyName)
            {
                Control.MinThumbTextHidden = Element.MinThumbTextHidden;
            }
            else if (e.PropertyName == RangeSlider.MaxThumbTextHiddenProperty.PropertyName)
            {
                Control.MaxThumbTextHidden = Element.MaxThumbTextHidden;
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