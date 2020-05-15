using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PasswordGenerator
{
    public static class BorderHelper
    {
        public static RectangleGeometry GetRectangleGeometry(this Border border)
        {
            RectangleGeometry rect = new RectangleGeometry();
            rect.RadiusX = rect.RadiusY = Math.Max(0, border.CornerRadius.TopLeft - (border.BorderThickness.Left * 0.5f));
            return rect;
        }

        public static void Clip(this UIElement child, RectangleGeometry rect)
        {
            RectangleGeometry copyRect = rect.Clone();
            copyRect.Rect = new Rect(child.RenderSize);
            child.Clip = copyRect;
        }
    }
}
