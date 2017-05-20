namespace ApartmentSale
{
    using DAL;
    using DAL.Concrete.UnitOfWork;
    using System.Windows;

    public partial class CreateAdvertisement : Window
    {
        private UnitOfWork _unitOfWork = CustomerWindow.unitOfWork;

        public CreateAdvertisement()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool isParsedPrice = false;
            bool isParsedSquare = false;
            bool isParsedCountR = false;

            int price;
            isParsedPrice = int.TryParse(tbPrice.Text, out price);
            if (!isParsedPrice || price < 0)
            {
                MessageBox.Show(this, "Enter price correctly.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int square;
                isParsedSquare = int.TryParse(tbSquare.Text, out square);
                if (!isParsedSquare || square < 0)
                {
                    MessageBox.Show(this, "Enter square of apartment correctly.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    int roomsCount;
                    isParsedCountR = int.TryParse(tbRoomsCount.Text, out roomsCount);
                    if (!isParsedCountR || roomsCount < 0)
                    {
                        MessageBox.Show(this, "Enter count of rooms correctly.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        var advertisement = new Advertisement()
                        {
                            Price = price,
                            Square = square,
                            RoomsCount = roomsCount,
                            Adress = tbAdress.Text,
                            UserId = LoginWindow.UserId
                        };
                        var currentUser = CustomerWindow.unitOfWork.UserRepository.FindByID(LoginWindow.UserId);
                        currentUser.MyAdvertisements.Add(advertisement);
                        _unitOfWork.AdvertisementRepository.Insert(advertisement);
                        _unitOfWork.Save();

                        MessageBox.Show("Your advertisement is added.");
                        CustomerWindow customerWindow = new CustomerWindow();
                        customerWindow.Show();
                        this.Close();
                    }
                }
            }
        }
    }
}

