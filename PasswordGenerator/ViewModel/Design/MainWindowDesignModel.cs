namespace PasswordGenerator
{
    public class MainWindowDesignModel : MainWindowViewModel
    {
        public MainWindowDesignModel Instance => new MainWindowDesignModel();

        public MainWindowDesignModel()
        {
            NewPasswordName = "The new name of this password";
        }
    }
}
