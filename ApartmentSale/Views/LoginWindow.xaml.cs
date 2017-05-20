namespace ApartmentSale
{
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using DAL;
    using System.Security.Cryptography;
    using DAL.Concrete.UnitOfWork;

    public partial class LoginWindow : Window
    {
        private UnitOfWork _unitOfWork = CustomerWindow.unitOfWork;
        public static int UserId { get; set; }

        public static IEnumerable<User> ListOfUsers { get; private set; }
        public LoginWindow()
        {
            InitializeComponent();
            ListOfUsers = _unitOfWork.UserRepository.GetAll();
        }

        public static string GetMd5HashPassword(string password)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(password));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var login = loginText.Text;
            var password = GetMd5HashPassword(passwordBox.Password);

            bool isExistingLogin = false;
            bool isRightPassword = false;
            foreach (var item in ListOfUsers)
            {
                if (item.Login == login)
                {
                    isExistingLogin = true;
                    UserId = item.UserId;
                    break;
                }
            }

            if (!isExistingLogin)
            {
                MessageBox.Show(this, "Invalid user name", "Authentication Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (isExistingLogin)
            {
                foreach (var item in ListOfUsers)
                {
                    if (item.Password == password)
                    {
                        isRightPassword = true;
                        break;
                    }
                }

                if (!isRightPassword)
                {
                    MessageBox.Show(this, "Invalid password", "Authentication Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            if (isExistingLogin && isRightPassword)
            {
                CustomerWindow customerWindow = new CustomerWindow();
                customerWindow.Show();
                this.Close();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registerWindow = new RegistrationWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
