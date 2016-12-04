using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.RangeSlider.Common;

namespace Xamarin.RangeSlider.Forms
{
    [Preserve(AllMembers = true)]
    public class RangeSlider : View
    {
        public const string LowerValuePropertyName = "LowerValue";
        public const string MaximumValuePropertyName = "MaximumValue";
        public const string MinimumValuePropertyName = "MinimumValue";
        public const string UpperValuePropertyName = "UpperValue";
        public const string MinThumbHiddenPropertyName = "MinThumbHidden";
        public const string MaxThumbHiddenPropertyName = "MaxThumbHidden";
        public const string StepValuePropertyName = "StepValue";
        public const string StepValueContinuouslyPropertyName = "StepValueContinuously";
        public const string BarHeightPropertyName = "BarHeight";
        public const string ShowTextAboveThumbsPropertyName = "ShowTextAboveThumbs";
        public const string TextSizePropertyName = "TextSize";
        public const string TextFormatPropertyName = "TextFormat";

        public static readonly BindableProperty LowerValueProperty =
            BindableProperty.Create(LowerValuePropertyName, typeof(float), typeof(RangeSlider), 0f);

        public static readonly BindableProperty MaximumValueProperty =
            BindableProperty.Create(MaximumValuePropertyName, typeof(float), typeof(RangeSlider), 0f);

        public static readonly BindableProperty MaxThumbHiddenProperty =
            BindableProperty.Create(MaxThumbHiddenPropertyName, typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty MinimumValueProperty =
            BindableProperty.Create(MinimumValuePropertyName, typeof(float), typeof(RangeSlider), 0f);

        public static readonly BindableProperty MinThumbHiddenProperty =
            BindableProperty.Create(MinThumbHiddenPropertyName, typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty StepValueContinuouslyProperty =
            BindableProperty.Create(StepValueContinuouslyPropertyName, typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty StepValueProperty =
            BindableProperty.Create(StepValuePropertyName, typeof(float), typeof(RangeSlider), 0f);

        public static readonly BindableProperty UpperValueProperty =
            BindableProperty.Create(UpperValuePropertyName, typeof(float), typeof(RangeSlider), 0f);

        public static readonly BindableProperty BarHeightProperty =
            BindableProperty.Create(BarHeightPropertyName, typeof(int?), typeof(RangeSlider));

        public static readonly BindableProperty ShowTextAboveThumbsProperty =
            BindableProperty.Create(ShowTextAboveThumbsPropertyName, typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty TextSizeProperty =
            BindableProperty.Create(TextSizePropertyName, typeof(double), typeof(RangeSlider), 10D);

        public static readonly BindableProperty TextFormatProperty =
            BindableProperty.Create(TextFormatPropertyName, typeof(string), typeof(RangeSlider), "F0");

        public float MinimumValue
        {
            get { return (float)GetValue(MinimumValueProperty); }
            set { SetValue(MinimumValueProperty, value); }
        }

        public float MaximumValue
        {
            get { return (float)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }

        public float LowerValue
        {
            get { return (float)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, value); }
        }

        public float UpperValue
        {
            get { return (float)GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, value); }
        }

        public bool MinThumbHidden
        {
            get { return (bool)GetValue(MinThumbHiddenProperty); }
            set { SetValue(MinThumbHiddenProperty, value); }
        }

        public bool MaxThumbHidden
        {
            get { return (bool)GetValue(MaxThumbHiddenProperty); }
            set { SetValue(MaxThumbHiddenProperty, value); }
        }

        public float StepValue
        {
            get { return (float)GetValue(StepValueProperty); }
            set { SetValue(StepValueProperty, value); }
        }

        public bool StepValueContinuously
        {
            get { return (bool)GetValue(StepValueContinuouslyProperty); }
            set { SetValue(StepValueContinuouslyProperty, value); }
        }

        public int? BarHeight
        {
            get { return (int?)GetValue(BarHeightProperty); }
            set { SetValue(BarHeightProperty, value); }
        }

        public bool ShowTextAboveThumbs
        {
            get { return (bool)GetValue(ShowTextAboveThumbsProperty); }
            set { SetValue(ShowTextAboveThumbsProperty, value); }
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double TextSize
        {
            get { return (double)GetValue(TextSizeProperty); }
            set { SetValue(TextSizeProperty, value); }
        }

        public string TextFormat
        {
            get { return (string)GetValue(TextFormatProperty); }
            set { SetValue(TextFormatProperty, value); }
        }

        public Func<Thumb, float, string> FormatLabel { get; set; }

        public event EventHandler LowerValueChanged;
        public event EventHandler UpperValueChanged;
        public event EventHandler DragStarted;
        public event EventHandler DragCompleted;

        public void OnLowerValueChanged(float value)
        {
            LowerValue = value;
            LowerValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public void OnUpperValueChanged(float value)
        {
            UpperValue = value;
            UpperValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public void OnDragStarted()
        {
            DragStarted?.Invoke(this, EventArgs.Empty);
        }

        public void OnDragCompleted()
        {
            DragCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}