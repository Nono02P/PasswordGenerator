using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace PasswordGenerator
{
    /// <summary>
    /// Animation helpers for <see cref="Storyboard"/>
    /// </summary>
    public static class StoryboardHelper
    {
        private const float _DEFAULT_DECELERATION_RATIO = 0.9f;

        #region Slide

        #region From

        /// <summary>
        /// Adds a slide animation from the specified <see cref="eDirection"/> to the storyboard.
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to.</param>
        /// <param name="direction">The direction from where come from the slide.</param>
        /// <param name="seconds">The time the animation will take.</param>
        /// <param name="offset">The distance to the right to start from.</param>
        /// <param name="decelerationRatio">The rate of deceleration.</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation.</param>
        /// <param name="easingFunction">The easing function applied to the animation.</param>
        public static void AddSlideFromDirection(this Storyboard storyboard, eDirection direction, float seconds, double offset, float decelerationRatio = _DEFAULT_DECELERATION_RATIO, bool keepMargin = true, IEasingFunction easingFunction = null)
        {
            Thickness from;
            switch (direction)
            {
                case eDirection.Left:
                    from = new Thickness(-offset, 0, keepMargin ? offset : 0, 0);
                    break;
                case eDirection.Right:
                    from = new Thickness(keepMargin ? offset : 0, 0, -offset, 0);
                    break;
                case eDirection.Top:
                    from = new Thickness(0, -offset, 0, keepMargin ? offset : 0);
                    break;
                case eDirection.Bottom:
                    from = new Thickness(0, keepMargin ? offset : 0, 0, -offset);
                    break;
                default:
                case eDirection.None:
                    from = new Thickness(0);
                    break;
            }
            Thickness to = new Thickness(0);
            AddSlide(storyboard, from, to, seconds, decelerationRatio, easingFunction);
        }

        #endregion From

        #region To

        /// <summary>
        /// Adds a slide animation to the specified <see cref="eDirection"/> to the storyboard.
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to.</param>
        /// <param name="direction">The direction to be taken by the slide.</param>
        /// <param name="seconds">The time the animation will take.</param>
        /// <param name="offset">The distance to the right to start from.</param>
        /// <param name="decelerationRatio">The rate of deceleration.</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation.</param>
        /// <param name="easingFunction">The easing function applied to the animation.</param>
        public static void AddSlideToDirection(this Storyboard storyboard, eDirection direction, float seconds, double offset, float decelerationRatio = _DEFAULT_DECELERATION_RATIO, bool keepMargin = true, IEasingFunction easingFunction = null)
        {
            Thickness to;
            switch (direction)
            {
                case eDirection.Left:
                    to = new Thickness(-offset, 0, keepMargin ? offset : 0, 0);
                    break;
                case eDirection.Right:
                    to = new Thickness(keepMargin ? offset : 0, 0, -offset, 0);
                    break;
                case eDirection.Top:
                    to = new Thickness(0, -offset, 0, keepMargin ? offset : 0);
                    break;
                case eDirection.Bottom:
                    to = new Thickness(0, keepMargin ? offset : 0, 0, -offset);
                    break;
                default:
                case eDirection.None:
                    to = new Thickness(0);
                    break;
            }
            Thickness from = new Thickness(0);
            AddSlide(storyboard, from, to, seconds, decelerationRatio, easingFunction);
        }

        #endregion To

        #region From/To

        /// <summary>
        /// Adds a slide from/to animation to the storyboard.
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to.</param>
        /// <param name="from">The <see cref="Thickness"/> from.</param>
        /// <param name="to">The <see cref="Thickness"/> to.</param>
        /// <param name="seconds">The time the animation will take.</param>
        /// <param name="offset">The distance to the right to start from.</param>
        /// <param name="decelerationRatio">The rate of deceleration.</param>
        /// <param name="easingFunction">The easing function applied to the animation.</param>
        public static void AddSlide(this Storyboard storyboard, Thickness from, Thickness to, float seconds, float decelerationRatio = _DEFAULT_DECELERATION_RATIO, IEasingFunction easingFunction = null)
        {
            ThicknessAnimation animation = new ThicknessAnimation(from, to, new Duration(TimeSpan.FromSeconds(seconds)))
            {
                DecelerationRatio = decelerationRatio,
                EasingFunction = easingFunction,
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
        }

        #endregion From/To

        #endregion Slide

        #region Fade

        #region In

        /// <summary>
        /// Adds a fade in animation to the storyboard.
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to.</param>
        /// <param name="seconds">The time the animation will take.</param>
        /// <param name="easingFunction">The easing function applied to the animation.</param>
        public static void AddFadeIn(this Storyboard storyboard, float seconds, float decelerationRatio = _DEFAULT_DECELERATION_RATIO, IEasingFunction easingFunction = null)
        {
            DoubleAnimation animation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(seconds)))
            {
                DecelerationRatio = decelerationRatio,
                EasingFunction = easingFunction,
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            storyboard.Children.Add(animation);
        }

        #endregion In

        #region Out

        /// <summary>
        /// Adds a fade out animation to the storyboard.
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to.</param>
        /// <param name="seconds">The time the animation will take.</param>
        /// <param name="easingFunction">The easing function applied to the animation.</param>
        public static void AddFadeOut(this Storyboard storyboard, float seconds, float decelerationRatio = _DEFAULT_DECELERATION_RATIO, IEasingFunction easingFunction = null)
        {
            DoubleAnimation animation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(seconds)))
            {
                DecelerationRatio = decelerationRatio,
                EasingFunction = easingFunction,
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            storyboard.Children.Add(animation);
        }

        #endregion Out

        #endregion Fade
    }
}