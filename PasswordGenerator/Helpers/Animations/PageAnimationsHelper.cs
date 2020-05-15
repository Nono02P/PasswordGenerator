using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PasswordGenerator
{
    /// <summary>
    /// Helpers to animate pages in specific ways.
    /// </summary>
    public static class PageAnimationsHelper
    {
        private const float _DEFAULT_DECELERATION_RATIO = 0.9f;
        private const float _DEFAULT_SECONDS = 0.3f;

        #region Entrance
        public static async Task SlideAndFadeInFromDirection(this Page page, eDirection direction, float seconds = _DEFAULT_SECONDS, float decelerationRatio = _DEFAULT_DECELERATION_RATIO, bool keepMargin = true, IEasingFunction easingFunction = null)
        {
            switch (direction)
            {
                case eDirection.None:
                    break;
                case eDirection.Left:
                case eDirection.Right:
                    await page.SlideAndFadeInFromDirection(direction, seconds, decelerationRatio, keepMargin, easingFunction, width: page.WindowWidth);
                    break;
                case eDirection.Top:
                case eDirection.Bottom:
                    await page.SlideAndFadeInFromDirection(direction, seconds, decelerationRatio, keepMargin, easingFunction, height: page.WindowHeight);
                    break;
                default:
                    break;
            }
        }
        #endregion Entrance

        #region Exit
        public static async Task SlideAndFadeOutToDirection(this Page page, eDirection direction, float seconds = _DEFAULT_SECONDS, float decelerationRatio = _DEFAULT_DECELERATION_RATIO, bool keepMargin = true, IEasingFunction easingFunction = null)
        {
            switch (direction)
            {
                case eDirection.None:
                    break;
                case eDirection.Left:
                case eDirection.Right:
                    await page.SlideAndFadeOutToDirection(direction, seconds, decelerationRatio, keepMargin, easingFunction, width: page.WindowWidth);
                    break;
                case eDirection.Top:
                case eDirection.Bottom:
                    await page.SlideAndFadeOutToDirection(direction, seconds, decelerationRatio, keepMargin, easingFunction, height: page.WindowHeight);
                    break;
                default:
                    break;
            }
        }
        #endregion Exit
    }
}