namespace ApartmentSale
{
    using DAL;
    using DAL.Concrete.UnitOfWork;
    using System.Linq;
    using System.Windows;

    public partial class RegistrationWindow : Window
    {
        private UnitOfWork _unitOfWork = CustomerWindow.unitOfWork;
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != string.Empty && tbPassword.Text != string.Empty)
            {
                User user = new User();
                user.Login = tbLogin.Text;
                user.Password = LoginWindow.GetMd5HashPassword(tbPassword.Text);
                user.Name = tbName.Text;
                user.SurName = tbName.Text;
                int celNumber;
                bool isCorrectPhoneNumber = int.TryParse(tbCellPhone.Text, out celNumber);
                if (isCorrectPhoneNumber)
                {
                    user.TelNumber = celNumber;
                }
                else
                {
                    MessageBox.Show(this, "Invalid phone number", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (IsValidEmail(tbEmail.Text))
                {
                    user.Email = tbEmail.Text;
                }
                else
                {
                    MessageBox.Show(this, "Invalid email", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                _unitOfWork.UserRepository.Insert(user);
                _unitOfWork.Save();

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private bool IsValidEmail(string email)
        {
            bool isCorrectEmail = false;
            if (email.Contains('@') && email.Contains('.'))
            {
                isCorrectEmail = true;
            }

            return isCorrectEmail;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
