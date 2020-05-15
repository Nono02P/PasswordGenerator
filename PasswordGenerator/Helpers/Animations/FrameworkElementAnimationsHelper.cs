using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace PasswordGenerator
{
    /// <summary>
    /// Helpers to animate frameworkElements in specific ways.
    /// </summary>
    public static class FrameworkElementAnimationsHelper
    {
        private const float _DEFAULT_DECELERATION_RATIO = 0.9f;
        private const float _DEFAULT_SECONDS = 0.3f;

        #region Entrance

        #region Slide and Fade In
        public static async Task SlideAndFadeInFromDirection(this FrameworkElement frameworkElement, eDirection direction, float seconds = _DEFAULT_SECONDS, float decelerationRatio = _DEFAULT_DECELERATION_RATIO, bool keepMargin = true, IEasingFunction easingFunction = null, double width = 0, double height = 0)
        {
            // Create the animation.
            Storyboard storyboard = new Storyboard();
            switch (direction)
            {
                case eDirection.None:
                    break;
                case eDirection.Left:
                case eDirection.Right:
                    storyboard.AddSlideFromDirection(direction, seconds, width == 0 ? frameworkElement.ActualWidth : width, decelerationRatio, keepMargin, easingFunction);
                    break;
                case eDirection.Top:
                case eDirection.Bottom:
                    storyboard.AddSlideFromDirection(direction, seconds, height == 0 ? frameworkElement.ActualHeight : height, decelerationRatio, keepMargin, easingFunction);
                    break;
                default:
                    break;
            }
            storyboard.AddFadeIn(seconds, decelerationRatio, easingFunction);

            await PlayStoryboard(frameworkElement, storyboard, seconds);
        }
        #endregion

        #region Fade In
        public static async Task FadeIn(this FrameworkElement frameworkElement, float seconds = _DEFAULT_SECONDS, float decelerationRatio = _DEFAULT_DECELERATION_RATIO, IEasingFunction easingFunction = null, double height = 0)
        {
            // Create the animation.
            Storyboard storyboard = new Storyboard();
            storyboard.AddFadeIn(seconds, decelerationRatio, easingFunction);

            await PlayStoryboard(frameworkElement, storyboard, seconds);
        }
        #endregion

        #endregion Entrance

        #region Exit

        #region Slide and Fade Out
        public static async Task SlideAndFadeOutToDirection(this FrameworkElement frameworkElement, eDirection direction, float seconds = _DEFAULT_SECONDS, float decelerationRatio = _DEFAULT_DECELERATION_RATIO, bool keepMargin = true, IEasingFunction easingFunction = null, double width = 0, double height = 0, Visibility visibilityAfter = Visibility.Hidden)
        {
            // Create the animation.
            Storyboard storyboard = new Storyboard();
            switch (direction)
            {
                case eDirection.None:
                    break;
                case eDirection.Left:
                case eDirection.Right:
                    storyboard.AddSlideToDirection(direction, seconds, width == 0 ? frameworkElement.ActualWidth : width, decelerationRatio, keepMargin, easingFunction);
                    break;
                case eDirection.Top:
                case eDirection.Bottom:
                    storyboard.AddSlideToDirection(direction, seconds, height == 0 ? frameworkElement.ActualHeight : height, decelerationRatio, keepMargin, easingFunction);
                    break;
                default:
                    break;
            }
            storyboard.AddFadeOut(seconds, decelerationRatio, easingFunction);

            await PlayStoryboard(frameworkElement, storyboard, seconds);

            frameworkElement.Visibility = visibilityAfter;
        }
        #endregion

        #region Fade Out
        public static async Task FadeOut(this FrameworkElement frameworkElement, float seconds = _DEFAULT_SECONDS, float decelerationRatio = _DEFAULT_DECELERATION_RATIO, IEasingFunction easingFunction = null, double height = 0, Visibility visibilityAfter = Visibility.Hidden)
        {
            // Create the animation.
            Storyboard storyboard = new Storyboard();
            storyboard.AddFadeOut(seconds, decelerationRatio, easingFunction);

            await PlayStoryboard(frameworkElement, storyboard, seconds);

            frameworkElement.Visibility = visibilityAfter;
        }
        #endregion

        #endregion Exit

        public static async Task PlayStoryboard(this FrameworkElement frameworkElement, Storyboard storyboard, float seconds = _DEFAULT_SECONDS)
        {
            storyboard.Begin(frameworkElement);

            frameworkElement.Visibility = Visibility.Visible;

            // Wait the end
            await Task.Delay(TimeSpan.FromSeconds(seconds));
        }
    }
}