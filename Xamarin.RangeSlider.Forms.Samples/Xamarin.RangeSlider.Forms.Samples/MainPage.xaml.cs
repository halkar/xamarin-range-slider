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
