using System;
using System.Diagnostics;
using Xamarin.Forms;

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
    }
}
