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
        public const string TextColorPropertyName = "TextColor";
        public const string FormatLabelPropertyName = "FormatLabel";
        public const string ActiveColorPropertyName = "ActiveColor";
        public const string MaterialUiPropertyName = "MaterialUI";

        public static readonly BindableProperty LowerValueProperty =
            BindableProperty.Create(LowerValuePropertyName, typeof(float), typeof(RangeSlider), 0f);

        public static readonly BindableProperty MaximumValueProperty =
            BindableProperty.Create(MaximumValuePropertyName, typeof(float), typeof(RangeSlider), 0f);

        public static readonly BindableProperty MaxThumbHiddenProperty =
            BindableProperty.Create(MaxThumbHiddenPropertyName, typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty MaxThumbTextHiddenProperty =
           BindableProperty.Create(nameof(MaxThumbTextHidden), typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty MinimumValueProperty =
            BindableProperty.Create(MinimumValuePropertyName, typeof(float), typeof(RangeSlider), 0f);

        public static readonly BindableProperty MinThumbHiddenProperty =
            BindableProperty.Create(MinThumbHiddenPropertyName, typeof(bool), typeof(RangeSlider), false);

        public static readonly BindableProperty MinThumbTextHiddenProperty =
            BindableProperty.Create(nameof(MinThumbTextHidden), typeof(bool), typeof(RangeSlider), false);

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

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(TextColorPropertyName, typeof(Color), typeof(RangeSlider), Color.Default);

        public static readonly BindableProperty FormatLabelProperty =
            BindableProperty.Create(FormatLabelPropertyName, typeof(Func<Thumb, float, string>), typeof(RangeSlider));

        public static readonly BindableProperty ActiveColorProperty =
            BindableProperty.Create(ActiveColorPropertyName, typeof(Color), typeof(RangeSlider), Color.Default);

        public static readonly BindableProperty MaterialUiProperty =
            BindableProperty.Create(MaterialUiPropertyName, typeof(bool), typeof(RangeSlider), false);

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