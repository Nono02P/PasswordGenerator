using System.Windows;

namespace PasswordGenerator
{
    /// <summary>
    /// Animates a framework element fading in on show and fading out on hide.
    /// </summary>
    public class AnimateFadeOnLoadProperty : AnimatedBaseProperty<AnimateFadeOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (firstLoad && value)
                await element.FadeIn(0.3f);
        }
    }
}
