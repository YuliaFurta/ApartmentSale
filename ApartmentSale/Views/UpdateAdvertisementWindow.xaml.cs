namespace ApartmentSale
{
    using DAL;
    using DAL.Concrete.UnitOfWork;
    using System.Windows;

    public partial class UpdateAdvertisementWindow : Window
    {
        private UnitOfWork _unitOfWork = CustomerWindow.unitOfWork;
        private Advertisement _itemToUpdate;
        public UpdateAdvertisementWindow(Advertisement advertisement)
        {
            InitializeComponent();
            _itemToUpdate = new Advertisement();
            _itemToUpdate = _unitOfWork.AdvertisementRepository.FindByID(advertisement.AdvertisementId);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbPrice.Text != string.Empty)
            {
                _itemToUpdate.Price = int.Parse(tbPrice.Text);
            }

            if (tbSquare.Text != string.Empty)
            {
                _itemToUpdate.Square = int.Parse(tbSquare.Text);
            }

            if (tbRoomsCount.Text != string.Empty)
            {
                _itemToUpdate.RoomsCount = int.Parse(tbRoomsCount.Text);
            }

            if (tbAdress.Text != string.Empty)
            {
                _itemToUpdate.Adress = tbAdress.Text;
            }

            _unitOfWork.AdvertisementRepository.Update(_itemToUpdate);
            
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.Show();
            this.Close();
        }
    }
}
