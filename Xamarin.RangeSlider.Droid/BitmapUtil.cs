using Android.Graphics;
using Android.Graphics.Drawables;

namespace Xamarin.RangeSlider
{
    public class BitmapUtil
    {
        public static Bitmap DrawableToBitmap(Drawable drawable)
        {
            if (drawable is BitmapDrawable)
            {
                return ((BitmapDrawable) drawable).Bitmap;
            }

            // We ask for the bounds if they have been set as they would be most
            // correct, then we check we are  > 0
            var width = !drawable.Bounds.IsEmpty
                ? drawable.Bounds.Width()
                : drawable.IntrinsicWidth;

            var height = !drawable.Bounds.IsEmpty
                ? drawable.Bounds.Height()
                : drawable.IntrinsicHeight;

            // Now we check we are > 0
            var bitmap = Bitmap.CreateBitmap(width <= 0 ? 1 : width, height <= 0 ? 1 : height,
                Bitmap.Config.Argb8888);
            var canvas = new Canvas(bitmap);
            drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
            drawable.Draw(canvas);

            return bitmap;
        }
    }
}