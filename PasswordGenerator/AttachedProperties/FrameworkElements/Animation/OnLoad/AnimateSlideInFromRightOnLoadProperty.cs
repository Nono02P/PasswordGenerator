using System.Windows;

namespace PasswordGenerator
{
    /// <summary>
    /// Animates a framework element sliding it in from the right on load.
    /// </summary>
    public class AnimateSlideInFromRightOnLoadProperty : AnimatedBaseProperty<AnimateSlideInFromRightOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (firstLoad && value)
                await element.SlideAndFadeInFromDirection(eDirection.Right, 0.3f, keepMargin: KeepMarginProperty.GetValue(element));
        }
    }
}
