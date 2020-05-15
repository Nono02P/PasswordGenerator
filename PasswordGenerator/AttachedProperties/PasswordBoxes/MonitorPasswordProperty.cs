using System;
using System.Windows;
using System.Windows.Controls;

namespace PasswordGenerator
{
    /// <summary>
    /// The MonitorPassword attached property for a <see cref="PasswordBox"/>.
    /// </summary>
    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the caller.
            if (sender is PasswordBox passwordBox)
            {
                // Remove any previous events.
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

                // If the caller request to monitor the PasswordBox.
                if ((bool)e.NewValue)
                {
                    // Set default value.
                    PasswordBox_PasswordChanged(passwordBox, new RoutedEventArgs());

                    // Start listening password changes.
                    passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
                }
            }
            else
                throw new Exception($"The {nameof(MonitorPasswordProperty)} isn't attached to the correct sender {sender} should be a child of a {nameof(PasswordBox)}.");
        }

        /// <summary>
        /// Fired when the <see cref="PasswordBox.Password"/> changes.
        /// </summary>
        /// <param name="sender">The <see cref="PasswordBox"/> has attached properties.</param>
        /// <param name="e">Contains state information and event data associated with a routed event.</param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            HasTextProperty.SetValue((PasswordBox)sender);
            IsSecuredPasswordProperty.SetValue((PasswordBox)sender);
        }
    }

    /// <summary>
    /// The HasText attached property for a <see cref="PasswordBox"/>.
    /// </summary>
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool>
    {
        /// <summary>
        /// Sets the HasText property based on if the caller <see cref="PasswordBox"/> has any text.
        /// </summary>
        /// <param name="passwordBox"></param>
        public static void SetValue(PasswordBox passwordBox)
        {
            SetValue(passwordBox, passwordBox.SecurePassword.Length > 0);
        }
    }


    public class MinimumPasswordLengthProperty : BaseAttachedProperty<MinimumPasswordLengthProperty, int> { }

    /// <summary>
    /// Attached property to determine if the password length is greather than indicated in the <see cref="MinimumPasswordLengthProperty"/> for a <see cref="PasswordBox"/>.
    /// </summary>
    public class IsSecuredPasswordProperty : BaseAttachedProperty<IsSecuredPasswordProperty, bool>
    {
        /// <summary>
        /// Sets the IsSecuredPasswordProperty property based on if the length of the password is greather or equal to the <see cref="MinimumPasswordLengthProperty"/>.
        /// </summary>
        /// <param name="passwordBox"></param>
        public static void SetValue(PasswordBox passwordBox)
        {
            SetValue(passwordBox, passwordBox.SecurePassword.Length >= MinimumPasswordLengthProperty.GetValue(passwordBox));
        }
    }
}