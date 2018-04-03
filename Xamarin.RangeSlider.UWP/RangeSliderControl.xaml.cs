using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Thumb = Xamarin.RangeSlider.Common.Thumb;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Xamarin.RangeSlider
{
    public sealed partial class RangeSliderControl
    {
        public const string MinimumPropertyName = "Minimum";
        public const string MaximumPropertyName = "Maximum";
        public const string RangeminPropertyName = "RangeMin";
        public const string RangemaxPropertyName = "RangeMax";
        public const string MinThumbHiddenPropertyName = "MinThumbHidden";
        public const string MaxThumbHiddenPropertyName = "MaxThumbHidden";
        public const string StepValuePropertyName = "StepValue";
        public const string StepValueContinuouslyPropertyName = "StepValueContinuously";
        public const string ShowTextAboveThumbsPropertyName = "ShowTextAboveThumbs";
        public const string TextSizePropertyName = "TextSize";
        public const string TextFormatPropertyName = "TextFormat";
        public const string TextColorPropertyName = "TextColor";
        public const string ActiveColorPropertyName = "ActiveColor";

        public const int ControlHeight = 32;

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(MinimumPropertyName,
            typeof(double), typeof(RangeSliderControl), new PropertyMetadata(0.0));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(MaximumPropertyName,
            typeof(double), typeof(RangeSliderControl), new PropertyMetadata(1.0));

        public static readonly DependencyProperty RangeMinProperty = DependencyProperty.Register(RangeminPropertyName,
            typeof(double), typeof(RangeSliderControl), new PropertyMetadata(0.0, OnRangeMinPropertyChanged));

        public static readonly DependencyProperty RangeMaxProperty = DependencyProperty.Register(RangemaxPropertyName,
            typeof(double), typeof(RangeSliderControl), new PropertyMetadata(1.0, OnRangeMaxPropertyChanged));

        public static readonly DependencyProperty MinThumbHiddenProperty = DependencyProperty.Register(MinThumbHiddenPropertyName,
            typeof(bool), typeof(RangeSliderControl), new PropertyMetadata(false, MinThumbHiddenPropertyChanged));

        public static readonly DependencyProperty MaxThumbHiddenProperty = DependencyProperty.Register(MaxThumbHiddenPropertyName,
            typeof(bool), typeof(RangeSliderControl), new PropertyMetadata(false, MaxThumbHiddenPropertyChanged));

        public static readonly DependencyProperty StepValueProperty = DependencyProperty.Register(StepValuePropertyName,
            typeof(double), typeof(RangeSliderControl), new PropertyMetadata(0.0));

        public static readonly DependencyProperty StepValueContinuouslyProperty = DependencyProperty.Register(StepValueContinuouslyPropertyName,
            typeof(bool), typeof(RangeSliderControl), new PropertyMetadata(false));

        public static readonly DependencyProperty ShowTextAboveThumbsProperty = DependencyProperty.Register(ShowTextAboveThumbsPropertyName,
            typeof(bool), typeof(RangeSliderControl), new PropertyMetadata(false, ShowTextAboveThumbsPropertyChanged));

        public static readonly DependencyProperty TextSizeProperty = DependencyProperty.Register(TextSizePropertyName,
            typeof(int), typeof(RangeSliderControl), new PropertyMetadata(10, TextSizePropertyChanged));

        public static readonly DependencyProperty TextFormatProperty = DependencyProperty.Register(TextFormatPropertyName,
            typeof(string), typeof(RangeSliderControl), new PropertyMetadata("F0", TextFormatPropertyChanged));

        public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register(TextColorPropertyName,
            typeof(Color?), typeof(RangeSliderControl), new PropertyMetadata(null, TextColorPropertyChanged));

        public static readonly DependencyProperty ActiveColorProperty = DependencyProperty.Register(ActiveColorPropertyName,
            typeof(Color?), typeof(RangeSliderControl), new PropertyMetadata(null, ActiveColorPropertyChanged));

        public RangeSliderControl()
        {
            InitializeComponent();
            IsEnabledChanged += RangeSliderControlIsEnabledChanged;
        }

        private void RangeSliderControlIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateActiveRectangleColor();
        }

        private void UpdateActiveRectangleColor()
        {
            ActiveRectangle.Fill = new SolidColorBrush(IsEnabled ? (ActiveColor ?? Color.FromArgb(255, 105, 160, 204)) : Color.FromArgb(255, 184, 197, 209));
        }

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public double RangeMin
        {
            get => (double)GetValue(RangeMinProperty);
            set => SetValue(RangeMinProperty, value);
        }

        public double RangeMax
        {
            get => (double)GetValue(RangeMaxProperty);
            set => SetValue(RangeMaxProperty, value);
        }

        public bool MinThumbHidden
        {
            get => (bool)GetValue(MinThumbHiddenProperty);
            set { SetValue(MinThumbHiddenProperty, value); MinThumbText.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool MaxThumbHidden
        {
            get => (bool)GetValue(MaxThumbHiddenProperty);
            set { SetValue(MaxThumbHiddenProperty, value); MaxThumbText.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        public double StepValue
        {
            get => (double)GetValue(StepValueProperty);
            set => SetValue(StepValueProperty, value);
        }

        public bool StepValueContinuously
        {
            get => (bool)GetValue(StepValueContinuouslyProperty);
            set => SetValue(StepValueContinuouslyProperty, value);
        }

        public bool ShowTextAboveThumbs
        {
            get => (bool)GetValue(ShowTextAboveThumbsProperty);
            set => SetValue(ShowTextAboveThumbsProperty, value);
        }

        public int TextSize
        {
            get => (int)GetValue(TextSizeProperty);
            set => SetValue(TextSizeProperty, value);
        }

        public Color? TextColor
        {
            get => (Color?)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public Color? ActiveColor
        {
            get => (Color?)GetValue(ActiveColorProperty);
            set => SetValue(ActiveColorProperty, value);
        }

        public string TextFormat
        {
            get => (string)GetValue(TextFormatProperty);
            set => SetValue(TextFormatProperty, value);
        }

        public Func<Thumb, float, string> FormatLabel { get; set; }
        public bool IgnoreRangeChecks { get; set; }

        public event EventHandler LowerValueChanged;
        public event EventHandler UpperValueChanged;
        public event EventHandler DragStarted;
        public event EventHandler DragCompleted;

        private double _aggregatedDrag;
        private double _initialLeft;

        private static void OnRangeMinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = (RangeSliderControl)d;
            var newValue = (double)e.NewValue;

            if (slider.IgnoreRangeChecks)
                slider.RangeMin = newValue;
            else
                slider.ValidateMinValue(newValue);

            slider.UpdateMinThumb(slider.RangeMin);

            slider.OnLowerValueChanged();
        }

        private static void OnRangeMaxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = (RangeSliderControl)d;
            var newValue = (double)e.NewValue;

            if (slider.IgnoreRangeChecks)
                slider.RangeMax = newValue;
            else
                slider.ValidateMaxValue(newValue);

            slider.UpdateMaxThumb(slider.RangeMax);

            slider.OnUpperValueChanged();
        }

        private static void MinThumbHiddenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = (RangeSliderControl)d;
            slider.MinThumb.Visibility = (bool)e.NewValue ? Visibility.Collapsed : Visibility.Visible;
        }

        private static void MaxThumbHiddenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = (RangeSliderControl)d;
            slider.MaxThumb.Visibility = (bool)e.NewValue ? Visibility.Collapsed : Visibility.Visible;
        }

        private static void TextSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = (RangeSliderControl)d;
            var newValue = (int?)e.NewValue;
            if (newValue.HasValue)
            {
                slider.MinThumbText.FontSize = newValue.Value;
                slider.MaxThumbText.FontSize = newValue.Value;
            }
        }

        private static void TextFormatPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = (RangeSliderControl)d;
            slider.UpdateMinThumb(slider.RangeMin);
            slider.UpdateMaxThumb(slider.RangeMax);
        }

        private static void TextColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = (RangeSliderControl)d;
            var newValue = (Color)e.NewValue;
            slider.MinThumbText.Foreground = new SolidColorBrush(newValue);
            slider.MaxThumbText.Foreground = new SolidColorBrush(newValue);
        }

        private static void ActiveColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = (RangeSliderControl)d;
            var newValue = (Color)e.NewValue;
            slider.ActiveRectangle.Fill = new SolidColorBrush(newValue);
            slider.MaxThumbText.Foreground = new SolidColorBrush(newValue);
        }

        private static void ShowTextAboveThumbsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = (RangeSliderControl)d;
            var newValue = (bool)e.NewValue;
            slider.TexContainerCanvas.Visibility = newValue ? Visibility.Visible : Visibility.Collapsed;
            slider.UpdateMinThumb(slider.RangeMin);
            slider.UpdateMaxThumb(slider.RangeMax);
        }

        private void UpdateTextContainerSize()
        {
            TexContainerCanvas.Height = Math.Max(MinThumbText.ActualHeight, MaxThumbText.ActualHeight);
            var height = ControlHeight + (ShowTextAboveThumbs ? TexContainerCanvas.Height : 0);
            Grid.Height = height;
            Height = height;
        }

        public void UpdateMinThumb(double min, bool update = false)
        {
            if (ContainerCanvas == null) return;
            if (!update && MinThumb.IsDragging) return;
            var relativeLeft = ValueToRelativeLeft(min);

            Canvas.SetLeft(MinThumb, relativeLeft);
            Canvas.SetLeft(ActiveRectangle, relativeLeft);

            ActiveRectangle.Width = Math.Max(RangeMax - min, 0.0) / (Maximum - Minimum) * ContainerCanvas.ActualWidth;

            MinThumbText.Text = ValueToString(min, Thumb.Lower);
            var left = relativeLeft - MinThumbText.ActualWidth / 2;
            if (left + MinThumbText.ActualWidth > Canvas.GetLeft(MaxThumbText) - 10)
                left = Canvas.GetLeft(MaxThumbText) - MinThumbText.ActualWidth - 10;
            Canvas.SetLeft(MinThumbText, left);

        }

        public void UpdateMaxThumb(double max, bool update = false)
        {
            if (ContainerCanvas == null) return;
            if (!update && MaxThumb.IsDragging) return;
            var relativeRight = ValueToRelativeLeft(max);

            Canvas.SetLeft(MaxThumb, relativeRight);

            ActiveRectangle.Width = Math.Max(max - RangeMin, 0.0) / (Maximum - Minimum) * ContainerCanvas.ActualWidth;

            MaxThumbText.Text = ValueToString(max, Thumb.Upper);
            var left = relativeRight - MaxThumbText.ActualWidth / 2;
            if (left < Canvas.GetLeft(MinThumbText) + MinThumbText.ActualWidth + 10)
                left = Canvas.GetLeft(MinThumbText) + MinThumbText.ActualWidth + 10;
            Canvas.SetLeft(MaxThumbText, left);
        }

        private double ValueToRelativeLeft(double value)
        {
            return (value - Minimum) / (Maximum - Minimum) * ContainerCanvas.ActualWidth;
        }

        private string ValueToString(double value, Thumb thumb)
        {
            var func = FormatLabel;
            return func == null
                ? value.ToString(TextFormat, CultureInfo.InvariantCulture)
                : func(thumb, (float)value);
        }

        private void ContainerCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var relativeLeft = (RangeMin - Minimum) / (Maximum - Minimum) * ContainerCanvas.ActualWidth;
            var relativeRight = (RangeMax - Minimum) / (Maximum - Minimum) * ContainerCanvas.ActualWidth;

            Canvas.SetLeft(MinThumb, relativeLeft);
            Canvas.SetLeft(ActiveRectangle, relativeLeft);
            Canvas.SetLeft(MaxThumb, relativeRight);

            ActiveRectangle.Width = (RangeMax - RangeMin) / (Maximum - Minimum) * ContainerCanvas.ActualWidth;
        }

        private void MinThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (StepValueContinuously)
            {
                _aggregatedDrag += e.HorizontalChange;
                var min = DragThumb(MinThumb, 0, Canvas.GetLeft(MaxThumb), _aggregatedDrag);
                var normalized = Math.Min(Normalize(min), RangeMax);
                UpdateMinThumb(normalized, true);
                RangeMin = normalized;
            }
            else
            {
                var min = DragThumb(MinThumb, 0, Canvas.GetLeft(MaxThumb), e.HorizontalChange);
                UpdateMinThumb(min, true);
                RangeMin = Math.Min(Normalize(min), RangeMax);
            }
        }

        private void MaxThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (StepValueContinuously)
            {
                _aggregatedDrag += e.HorizontalChange;
                var max = DragThumb(MaxThumb, Canvas.GetLeft(MinThumb), ContainerCanvas.ActualWidth, _aggregatedDrag);
                var normalized = Normalize(max);
                UpdateMaxThumb(normalized, true);
                RangeMax = Normalize(normalized);
            }
            else
            {
                var max = DragThumb(MaxThumb, Canvas.GetLeft(MinThumb), ContainerCanvas.ActualWidth, e.HorizontalChange);
                UpdateMaxThumb(max, true);
                RangeMax = Normalize(max);
            }
        }

        private double Normalize(double value)
        {
            if (Math.Abs(StepValue) < float.Epsilon)
                return value;
            return (float)Math.Round((value - Minimum) / StepValue) * StepValue + Minimum;
        }

        private double DragThumb(Windows.UI.Xaml.Controls.Primitives.Thumb thumb, double min, double max, double offset)
        {
            var currentPos = StepValueContinuously ? _initialLeft : Canvas.GetLeft(thumb);
            var nextPos = currentPos + offset;

            nextPos = Math.Max(min, nextPos);
            nextPos = Math.Min(max, nextPos);

            return Minimum + nextPos / ContainerCanvas.ActualWidth * (Maximum - Minimum);
        }

        private void MinThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            UpdateMinThumb(RangeMin);
            Canvas.SetZIndex(MinThumb, 10);
            Canvas.SetZIndex(MaxThumb, 0);
            DragCompleted?.Invoke(this, EventArgs.Empty);
        }

        private void MaxThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            UpdateMaxThumb(RangeMax);
            Canvas.SetZIndex(MinThumb, 0);
            Canvas.SetZIndex(MaxThumb, 10);
            DragCompleted?.Invoke(this, EventArgs.Empty);
        }

        private void OnLowerValueChanged()
        {
            LowerValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnUpperValueChanged()
        {
            UpperValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void MinThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            _aggregatedDrag = 0;
            _initialLeft = Canvas.GetLeft(MinThumb);
            DragStarted?.Invoke(this, EventArgs.Empty);
        }

        private void MaxThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            _aggregatedDrag = 0;
            _initialLeft = Canvas.GetLeft(MaxThumb);
            DragStarted?.Invoke(this, EventArgs.Empty);
        }

        public void SetBarHeight(int barHeight)
        {
            int margin = (ControlHeight - barHeight) / 2;
            InactiveRectangle.Margin = new Thickness(8, margin, 8, margin);
            InactiveRectangle.Height = barHeight;
            Canvas.SetTop(ActiveRectangle, margin);
            ActiveRectangle.Height = barHeight;
        }

        private void ThumbTextSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateTextContainerSize();
            Canvas.SetLeft(MinThumbText, ValueToRelativeLeft(RangeMin) - MinThumbText.ActualWidth / 2);
            Canvas.SetLeft(MaxThumbText, ValueToRelativeLeft(RangeMax) - MaxThumbText.ActualWidth / 2);
        }
        public void ValidateMinValue(double value)
        {
            if (value < Minimum)
                RangeMin = Minimum;
            else if (value > Maximum)
                RangeMin = Maximum;
            else
                RangeMin = value;

            if (RangeMin > RangeMax)
                RangeMax = RangeMin;
        }

        public void ValidateMaxValue(double value)
        {
            if (value < Minimum)
                RangeMax = Minimum;
            else if (value > Maximum)
                RangeMax = Maximum;
            else
                RangeMax = value;

            if (RangeMax < RangeMin)
                RangeMin = RangeMax;
        }
    }
}