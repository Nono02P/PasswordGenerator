using System;
using System.Collections.Generic;
using System.Windows;

namespace PasswordGenerator
{
    /// <summary>
    /// A base class to run any animation method when a boolean is set 
    /// to true and a reverse animation when set to false.
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class AnimatedBaseProperty<Parent> : BaseAttachedProperty<Parent, bool> where Parent : BaseAttachedProperty<Parent, bool>, new()
    {
        #region Private Members
        /// <summary>
        /// For each attached properties on the element, True if this is the first time the value has been updated
        /// Used to make sure we run the logic at least once during first load.
        /// </summary>
        protected Dictionary<DependencyObject, bool> _alreadyLoaded = new Dictionary<DependencyObject, bool>();

        /// <summary>
        /// For each attached properties on the element, the most recent value used if we get a value changed before we do the first load.
        /// </summary>
        protected Dictionary<DependencyObject, bool> _firstLoadValue = new Dictionary<DependencyObject, bool>();
        #endregion

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            if (!(sender is FrameworkElement element))
                throw new Exception($"The {nameof(AnimateFadeOnTargetUpdatedProperty)} isn't attached to the correct sender {sender} should be a {nameof(FrameworkElement)}.");

            // Don't fire if the value doesn't change
            if ((bool)sender.GetValue(ValueProperty) == (bool)value && _alreadyLoaded.ContainsKey(sender))
                return;

            // On first load
            if (!_alreadyLoaded.ContainsKey(sender))
            {
                _alreadyLoaded[sender] = false;

                //if (!(bool)value)
                //    element.Visibility = Visibility.Hidden;

                // Create a single self-unhookable event for the elements loaded event
                RoutedEventHandler onLoaded = null;
                onLoaded = (s, e) =>
                {
                    // Unhook ourselves
                    element.Loaded -= onLoaded;

                    //await Task.Delay(5);

                    // Do desired animation
                    DoAnimation(element, _firstLoadValue.ContainsKey(sender) ? _firstLoadValue[sender] : (bool)value, true);

                    _alreadyLoaded[sender] = true;
                };

                // Hoob into the loaded event of the element.
                element.Loaded += onLoaded;
            }
            else if(!_alreadyLoaded[sender])
            {
                _firstLoadValue[sender] = (bool)value;
            }
            else
            {
                // Do desired animation
                DoAnimation(element, (bool)value, false);
            }

            base.OnValueUpdated(sender, value);
        }

        /// <summary>
        /// The animation method that is fired when the value changes.
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="value">The new value.</param>
        protected abstract void DoAnimation(FrameworkElement element, bool value, bool firstLoad);
    }
}
