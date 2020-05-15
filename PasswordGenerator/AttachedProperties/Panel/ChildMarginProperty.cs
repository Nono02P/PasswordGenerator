using System;
using System.Windows;
using System.Windows.Controls;

namespace PasswordGenerator
{
    public class ChildMarginProperty : BaseAttachedProperty<ChildMarginProperty, string>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Panel panel)
            {
                ThicknessConverter converter = new ThicknessConverter();
                panel.Loaded += (s, ee) =>
                {
                    for (int i = 0; i < panel.Children.Count; i++)
                    {
                        UIElement child = panel.Children[i];
                        if (child is FrameworkElement frameworkElement)
                            frameworkElement.Margin = (Thickness)converter.ConvertFromString(e.NewValue as string);
                    }
                };
            }
            else
                throw new Exception($"The {nameof(ChildMarginProperty)} isn't attached to the correct sender {sender} should be a child of a {nameof(Panel)}.");
        }
    }
}