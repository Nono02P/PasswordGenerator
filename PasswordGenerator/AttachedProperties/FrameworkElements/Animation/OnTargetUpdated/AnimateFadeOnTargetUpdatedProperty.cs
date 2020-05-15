using System;
using System.Windows;

namespace PasswordGenerator
{
    public class AnimateFadeOnTargetUpdatedProperty : BaseAttachedProperty<AnimateFadeOnTargetUpdatedProperty, bool>
    {
        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            if (sender is FrameworkElement element)
            {
                if ((bool)value)
                {
                    element.TargetUpdated += Element_TargetUpdated;
                }
                else
                {
                    element.TargetUpdated -= Element_TargetUpdated;
                }
            }
            else
                throw new Exception($"The {nameof(AnimateFadeOnTargetUpdatedProperty)} isn't attached to the correct sender {sender} should be a {nameof(FrameworkElement)}.");
        }

        private async void Element_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            await element.FadeIn(0.3f);
        }
    }
}
