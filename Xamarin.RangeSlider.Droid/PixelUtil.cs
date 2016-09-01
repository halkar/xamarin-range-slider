using System;
using Android.Content;
using Android.Util;

namespace Xamarin.RangeSlider
{
    public class PixelUtil
    {
        public static int SpToPx(Context context, int dp)
        {
            return (int)Math.Round(dp * context.Resources.DisplayMetrics.ScaledDensity);
        }

        public static int PxToSp(Context context, int px)
        {
            return (int)Math.Round(px / context.Resources.DisplayMetrics.ScaledDensity); ;
        }

        public static int DpToPx(Context context, int dp)
        {
            int px = (int)Math.Round(dp * GetPixelScaleFactor(context));
            return px;
        }

        public static int PxToDp(Context context, int px)
        {
            int dp = (int)Math.Round(px / GetPixelScaleFactor(context));
            return dp;
        }

        private static float GetPixelScaleFactor(Context context)
        {
            DisplayMetrics displayMetrics = context.Resources.DisplayMetrics;
            return displayMetrics.Xdpi / (int)DisplayMetricsDensity.Default;
        }
    }
}