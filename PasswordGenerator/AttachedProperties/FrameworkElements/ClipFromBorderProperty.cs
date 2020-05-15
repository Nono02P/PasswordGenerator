using System;
using System.Windows;
using System.Windows.Controls;

namespace PasswordGenerator
{
    public class ClipFromBorderProperty : BaseAttachedProperty<ClipFromBorderProperty, bool>
    {
        private RoutedEventHandler _onLoaded;
        private SizeChangedEventHandler _onSizeChanged;

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is FrameworkElement child)
            {
                if (child.Parent is Border border)
                {
                    _onLoaded = (s1, e1) => UpdateChild(border, child);
                    _onSizeChanged = (s1, e1) => UpdateChild(border, child);

                    if ((bool)e.NewValue)
                    {
                        border.Loaded += _onLoaded;
                        border.SizeChanged += _onSizeChanged;
                    }
                    else
                    {
                        border.Loaded -= _onLoaded;
                        border.SizeChanged -= _onSizeChanged;
                    }
                }
                else
                    throw new Exception($"The {nameof(ClipFromBorderProperty)} isn't attached to the correct sender {sender} should be a child of a {nameof(Border)}.");
            }
            else
                throw new Exception($"The {nameof(ClipFromBorderProperty)} isn't attached to the correct sender {sender} should be a {nameof(FrameworkElement)}.");
        }

        private void UpdateChild(Border border, FrameworkElement child)
        {
            if (border.ActualWidth != 0 || border.ActualWidth != 0)
                child.Clip(border.GetRectangleGeometry());
        }
    }
}