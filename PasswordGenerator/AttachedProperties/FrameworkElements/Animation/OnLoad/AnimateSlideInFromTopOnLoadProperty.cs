using System.Windows;

namespace PasswordGenerator
{
    /// <summary>
    /// Animates a framework element sliding it in from the top on load.
    /// </summary>
    public class AnimateSlideInFromTopOnLoadProperty : AnimatedBaseProperty<AnimateSlideInFromTopOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (firstLoad && value)
                await element.SlideAndFadeInFromDirection(eDirection.Top, 0.3f, keepMargin: KeepMarginProperty.GetValue(element));
        }
    }
}
