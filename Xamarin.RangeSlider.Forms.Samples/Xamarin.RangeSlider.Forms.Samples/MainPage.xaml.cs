using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.RangeSlider.Common;

namespace Xamarin.RangeSlider.Forms.Samples
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            RangeSlider.DragStarted += RangeSliderOnDragStarted;
            RangeSlider.DragCompleted += RangeSliderOnDragCompleted;
            RangeSlider.LowerValueChanged += RangeSliderOnLowerValueChanged;
            RangeSlider.UpperValueChanged += RangeSliderOnUpperValueChanged;
            RangeSlider.FormatLabel = FormaLabel;
            RangeSliderWithEffect.Effects.Add(Effect.Resolve("EffectsSlider.RangeSliderEffect"));
        }

        private string FormaLabel(Thumb thumb, float val)
        {
            return DateTime.Today.AddDays(val).ToString("d");
        }

        private void RangeSliderOnUpperValueChanged(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("RangeSliderOnUpperValueChanged");
        }

        private void RangeSliderOnLowerValueChanged(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("RangeSliderOnLowerValueChanged");
        }

        private void RangeSliderOnDragCompleted(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("RangeSliderOnDragCompleted");
        }

        private void RangeSliderOnDragStarted(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("RangeSliderOnDragStarted");
        }

        private void SetActiveColor_Clicked(object sender, EventArgs e)
        {
            var color = Color.FromHex(ActiveColorEntry.Text);

            RangeSlider.ActiveColor = color;
            RangeSliderWithEffect.ActiveColor = color;
            ActiveColorBox.BackgroundColor = color;

            ActiveColorEntry.Text = ColorToString(color);
        }

        private void SetTextColor_Clicked(object sender, EventArgs e)
        {
            var color = Color.FromHex(TextColorEntry.Text);

            RangeSlider.TextColor = color;
            RangeSliderWithEffect.TextColor = color;
            TextColorBox.BackgroundColor = color;

            TextColorEntry.Text = ColorToString(color);
        }

        private static string ColorToString(Color color)
        {
            return color == Color.Default ?
                "Default" :
                string.Format("#{0:X}{1:X}{2:X}{3:X}", (int)(color.A * 255), (int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255));
        }
    }
}
