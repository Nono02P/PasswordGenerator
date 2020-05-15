using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace PasswordGenerator
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IHasPassword
    {
        SecureString IHasPassword.Password => EncryptionKey.SecurePassword;
        public event EventHandler<SecureStringEventArgs> PasswordChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            EncryptionKey.PasswordChanged += EncryptionPassword_PasswordChanged;
        }

        private void EncryptionPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordChanged?.Invoke(this, new SecureStringEventArgs((sender as PasswordBox).SecurePassword));
        }
    }
}