using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace PasswordGenerator
{
    public class MainWindowViewModel : BaseNotifier
    {
        #region Private Members

        private DataContractJsonSerializer _ser = new DataContractJsonSerializer(typeof(ObservableCollection<PasswordName>));
        private readonly string _folderName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PasswordGenerator\\";
        private readonly string _fileName = "Names.json";
        private string _FullPath => _folderName + _fileName;

        #endregion

        #region Public Properties

        public string NewPasswordName { get; set; }
        public string EncryptedPassword { get; set; }
        public bool PasswordIsEncrypted => !string.IsNullOrWhiteSpace(EncryptedPassword);

        public PasswordName CurrentPasswordName { get; set; }
        public ObservableCollection<PasswordName> PasswordNames { get; set; }

        #region Commands

        public SimpleCommand AddPasswordNameCommand { get; set; }
        public RelayCommand<IHasPassword> GeneratePasswordCommand { get; set; }

        #endregion

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            DeSerializeJsonPasswordNames();

            AddPasswordNameCommand = new SimpleCommand(CanAddPasswordName, AddPasswordName);
            GeneratePasswordCommand = new RelayCommand<IHasPassword>(CanGeneratePassword, GeneratePassword);

            PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        #endregion

        #region On Property changed

        private void MainWindowViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaiseCanExecuteCommands();
            switch (e.PropertyName)
            {
                case nameof(CurrentPasswordName):
                case nameof(NewPasswordName):
                    EncryptedPassword = string.Empty;
                    break;
                default:
                    break;
            }
        }

        private void HasPassword_PasswordChanged(object sender, SecureStringEventArgs e)
        {
            RaiseCanExecuteCommands();
            EncryptedPassword = string.Empty;
        }

        private void RaiseCanExecuteCommands()
        {
            AddPasswordNameCommand.RaiseCanExecuteChanged();
            GeneratePasswordCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region Password Names Serialisation/Deserialization

        private void DeSerializeJsonPasswordNames()
        {
            if (File.Exists(_FullPath))
            {
                using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(_FullPath)))
                {
                    PasswordNames = (ObservableCollection<PasswordName>)_ser.ReadObject(stream);
                }
            }
            else
            {
                PasswordNames = new ObservableCollection<PasswordName>();
            }
        }

        private void SerializePasswordNamesToJson()
        {
            Directory.CreateDirectory(_folderName);
            using (FileStream stream = File.Create(_FullPath))
                _ser.WriteObject(stream, PasswordNames);
        }

        #endregion

        #region Add Password Name Methods

        private bool CanAddPasswordName()
        {
            if (string.IsNullOrWhiteSpace(NewPasswordName))
                return false;

            return !PasswordNames.Contains(new PasswordName(NewPasswordName));
        }

        private void AddPasswordName()
        {
            PasswordName passwordName = new PasswordName(NewPasswordName);
            PasswordNames.Add(passwordName);
            CurrentPasswordName = passwordName;
            SerializePasswordNamesToJson();
        }

        #endregion

        #region Generate Password Methods

        private bool CanGeneratePassword(IHasPassword hasPassword)
        {
            if (hasPassword != null)
            {
                hasPassword.PasswordChanged -= HasPassword_PasswordChanged;
                hasPassword.PasswordChanged += HasPassword_PasswordChanged;

                if (!string.IsNullOrWhiteSpace(CurrentPasswordName.Name))
                    return hasPassword.Password.Unsecure().Length >= 10;
            }
            return false;
        }

        private void GeneratePassword(IHasPassword hasPassword)
        {
            string prefix = "$2y$10$iaYqo/z";
            string mid = "QVTSb7WNX1QbpSC";
            string suffix = "Lba+2|DIAQUpYP+lE";
            lock (new object())
            {
                string pass = hasPassword.Password.Unsecure();
                byte[] key = Encoding.UTF32.GetBytes($"{prefix}{pass.Insert(pass.Length / 2, mid)}{suffix}");
                pass = string.Empty;
                byte[] data = Encoding.UTF32.GetBytes(CurrentPasswordName.NormalizedName);

                using (HMACSHA256 hmac = new HMACSHA256(key))
                {
                    byte[] hashValue = hmac.ComputeHash(data);
                    EncryptedPassword = Convert.ToBase64String(hashValue).Substring(0, 20);
                }
            }

            Clipboard.SetText(EncryptedPassword);
        }
        
        #endregion
    }
}