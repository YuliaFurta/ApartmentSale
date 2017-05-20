namespace ApartmentSale.Views
{
    using DAL;
    using DAL.Concrete.UnitOfWork;
    using System.Windows;

    public partial class ConfirmDeleteWindow : Window
    {
        private UnitOfWork _unitOfWork = CustomerWindow.unitOfWork;
        private Advertisement _itemToDelete;
        public ConfirmDeleteWindow(Advertisement deleteItem)
        {
            InitializeComponent();
            _itemToDelete = new Advertisement();
            _itemToDelete = _unitOfWork.AdvertisementRepository.FindByID(deleteItem.AdvertisementId);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            _unitOfWork.AdvertisementRepository.Delete(_itemToDelete);
            _unitOfWork.Save();

            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.Show();
            this.Close();
        }
    }
}
