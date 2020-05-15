using System.Windows;

namespace PasswordGenerator
{
    /// <summary>
    /// Animates a framework element sliding it in from the left on load.
    /// </summary>
    public class AnimateSlideInFromLeftOnLoadProperty : AnimatedBaseProperty<AnimateSlideInFromLeftOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (firstLoad && value)
                await element.SlideAndFadeInFromDirection(eDirection.Left, 0.3f, keepMargin: KeepMarginProperty.GetValue(element));
        }
    }
}
