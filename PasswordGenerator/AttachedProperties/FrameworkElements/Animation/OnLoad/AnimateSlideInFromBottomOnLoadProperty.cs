using System.Windows;

namespace PasswordGenerator
{
    /// <summary>
    /// Animates a framework element sliding it in from the bottom on load.
    /// </summary>
    public class AnimateSlideInFromBottomOnLoadProperty : AnimatedBaseProperty<AnimateSlideInFromBottomOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (firstLoad && value)
                await element.SlideAndFadeInFromDirection(eDirection.Bottom, 0.3f, keepMargin: KeepMarginProperty.GetValue(element));
        }
    }
}
