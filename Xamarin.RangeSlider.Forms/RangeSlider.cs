using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.RangeSlider.Common;

namespace Xamarin.RangeSlider.Forms
{
    [Preserve(AllMembers = true)]
    public class RangeSlider : View
    {
        public static readonly BindableProperty LowerValueProperty =
            BindableProperty.Create(nameof(LowerValue), typeof(float), typeof(RangeSlider), 0f, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty MaximumValueProperty =
            BindableProperty.Create(nameof(MaximumValue), typeof(float), typeof(RangeSlider), 0f);

        public static readonly BindableProperty MaxThumbHiddenProperty =
            BindableProperty.Create(nameof(MaxThumbHidden), typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty MaxThumbTextHiddenProperty =
           BindableProperty.Create(nameof(MaxThumbTextHidden), typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty MinimumValueProperty =
            BindableProperty.Create(nameof(MinimumValue), typeof(float), typeof(RangeSlider), 0f);

        public static readonly BindableProperty MinThumbHiddenProperty =
            BindableProperty.Create(nameof(MinThumbHidden), typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty MinThumbTextHiddenProperty =
            BindableProperty.Create(nameof(MinThumbTextHidden), typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty StepValueContinuouslyProperty =
            BindableProperty.Create(nameof(StepValueContinuously), typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty StepValueProperty =
            BindableProperty.Create(nameof(StepValue), typeof(float), typeof(RangeSlider), 0f);

        public static readonly BindableProperty UpperValueProperty =
            BindableProperty.Create(nameof(UpperValue), typeof(float), typeof(RangeSlider), 0f, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty BarHeightProperty =
            BindableProperty.Create(nameof(BarHeight), typeof(int?), typeof(RangeSlider));

        public static readonly BindableProperty ShowTextAboveThumbsProperty =
            BindableProperty.Create(nameof(ShowTextAboveThumbs), typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty TextSizeProperty =
            BindableProperty.Create(nameof(TextSize), typeof(double), typeof(RangeSlider), 10D);

        public static readonly BindableProperty TextFormatProperty =
            BindableProperty.Create(nameof(TextFormat), typeof(string), typeof(RangeSlider), "F0");

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(RangeSlider), Color.Default);

        public static readonly BindableProperty FormatLabelProperty =
            BindableProperty.Create(nameof(FormatLabel), typeof(Func<Thumb, float, string>), typeof(RangeSlider));

        public static readonly BindableProperty ActiveColorProperty =
            BindableProperty.Create(nameof(ActiveColor), typeof(Color), typeof(RangeSlider), Color.Default);

        public static readonly BindableProperty MaterialUiProperty =
            BindableProperty.Create(nameof(MaterialUI), typeof(bool), typeof(RangeSlider), false);

        public float MinimumValue
        {
            get => (float)GetValue(MinimumValueProperty);
            set => SetValue(MinimumValueProperty, value);
        }

        public float MaximumValue
        {
            get => (float)GetValue(MaximumValueProperty);
            set => SetValue(MaximumValueProperty, value);
        }

        public float LowerValue
        {
            get => (float)GetValue(LowerValueProperty);
            set => SetValue(LowerValueProperty, value);
        }

        public float UpperValue
        {
            get => (float)GetValue(UpperValueProperty);
            set => SetValue(UpperValueProperty, value);
        }

        public bool MinThumbHidden
        {
            get => (bool)GetValue(MinThumbHiddenProperty);
            set => SetValue(MinThumbHiddenProperty, value);
        }

        public bool MinThumbTextHidden
        {
            get => (bool)GetValue(MinThumbTextHiddenProperty);
            set => SetValue(MinThumbTextHiddenProperty, value);
        }

        public bool MaxThumbTextHidden
        {
            get => (bool)GetValue(MaxThumbTextHiddenProperty);
            set => SetValue(MaxThumbTextHiddenProperty, value);
        }

        public bool MaxThumbHidden
        {
            get => (bool)GetValue(MaxThumbHiddenProperty);
            set => SetValue(MaxThumbHiddenProperty, value);
        }

        public float StepValue
        {
            get => (float)GetValue(StepValueProperty);
            set => SetValue(StepValueProperty, value);
        }

        public bool StepValueContinuously
        {
            get => (bool)GetValue(StepValueContinuouslyProperty);
            set => SetValue(StepValueContinuouslyProperty, value);
        }

        public int? BarHeight
        {
            get => (int?)GetValue(BarHeightProperty);
            set => SetValue(BarHeightProperty, value);
        }

        public bool ShowTextAboveThumbs
        {
            get => (bool)GetValue(ShowTextAboveThumbsProperty);
            set => SetValue(ShowTextAboveThumbsProperty, value);
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double TextSize
        {
            get => (double)GetValue(TextSizeProperty);
            set => SetValue(TextSizeProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public string TextFormat
        {
            get => (string)GetValue(TextFormatProperty);
            set => SetValue(TextFormatProperty, value);
        }

        public Color ActiveColor
        {
            get => (Color)GetValue(ActiveColorProperty);
            set => SetValue(ActiveColorProperty, value);
        }

        public bool MaterialUI
        {
            get => (bool)GetValue(MaterialUiProperty);
            set => SetValue(MaterialUiProperty, value);
        }

        public Func<Thumb, float, string> FormatLabel
        {
            get => (Func<Thumb, float, string>)GetValue(FormatLabelProperty);
            set => SetValue(FormatLabelProperty, value);
        }

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