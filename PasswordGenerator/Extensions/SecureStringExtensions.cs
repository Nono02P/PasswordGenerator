using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PasswordGenerator
{
    public static class SecureStringExtensions
    {
        /// <summary>
        /// Unsecures a <see cref="SecureString"/> to plain text.
        /// </summary>
        /// <param name="secureString">The secure string to unsecure.</param>
        /// <returns></returns>
        public static string Unsecure(this SecureString secureString)
        {
            // In case of empty string.
            if (secureString == null)
                return string.Empty;

            // Get a pointer for an unsecure string in memory.
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                // Unsecures the password
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                // Clean up any memory allocation.
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
