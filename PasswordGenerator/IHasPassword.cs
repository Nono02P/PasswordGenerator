using System;
using System.Security;

namespace PasswordGenerator
{
    public interface IHasPassword
    {
        event EventHandler<SecureStringEventArgs> PasswordChanged;

        SecureString Password { get; }
    }

    public class SecureStringEventArgs : EventArgs
    {
        public readonly SecureString Password;

        public SecureStringEventArgs(SecureString password)
        {
            Password = password;
        }
    }
}