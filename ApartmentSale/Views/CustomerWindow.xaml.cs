namespace ApartmentSale
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using DAL;
    using DAL.Concrete.UnitOfWork;
    using Views;

    public partial class CustomerWindow : Window
    {
        public static UnitOfWork unitOfWork = new UnitOfWork();

        private int userId = LoginWindow.UserId;
        private User currentUser;
        private GridView gridViewFirst = new GridView();
        private GridView gridViewSecond = new GridView();

        public IEnumerable<Advertisement> ListOfMyAdvertisements { get; set; }

        public IEnumerable<Advertisement> ListOfInterestingAdvertisements { get; set; }

        public IEnumerable<User> ListOfInterestedUsers { get; set; }

        public IEnumerable<Advertisement> ListOfAllAdvertisements { get; set; }

        public CustomerWindow()
        {
            InitializeComponent();

            currentUser = new User();
            currentUser = unitOfWork.UserRepository.FindByID(userId);

            ListOfAllAdvertisements = unitOfWork.AdvertisementRepository.GetAll();
            ListOfMyAdvertisements = unitOfWork.UserRepository.FindByID(userId).MyAdvertisements;
            ListOfInterestingAdvertisements = currentUser.LikeAdvertisements;

            radioButtonAllAdv.IsChecked = true;
        }

        private void radioButtonAllAdv_Checked(object sender, RoutedEventArgs e)
        {
            (tabControl.Items.GetItemAt(0) as TabItem).Header = "All advertisements";
            (tabControl.Items.GetItemAt(1) as TabItem).Header = "Favorite Advertisements";

            string headerText = (tabControl.Items.GetItemAt(0) as TabItem).Header.ToString();

            if (headerText == "All advertisements")
            {
                btnLike.Visibility = Visibility.Visible;
                btnLike.IsEnabled = true;
            }

            btnCreate.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            btnCreate.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            btnUpdate.Visibility = Visibility.Collapsed;

            gridViewFirst.Columns.Clear();
            CreateAllAdvertisementListView();
            gridViewSecond.Columns.Clear();
            CreateFavoriteAdvertisementsListView();
        }

        private void radioButtonMyAdv_Checked(object sender, RoutedEventArgs e)
        {
            (tabControl.Items.GetItemAt(0) as TabItem).Header = "My Advertisements";
            (tabControl.Items.GetItemAt(1) as TabItem).Header = "Interested Users";

            btnLike.IsEnabled = false;
            btnLike.Visibility = Visibility.Collapsed;
            string headerText = (tabControl.Items.GetItemAt(0) as TabItem).Header.ToString();

            if (headerText == "My Advertisements")
            {
                btnCreate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnUpdate.IsEnabled = true;
                btnCreate.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Visible;
            }
            gridViewFirst.Columns.Clear();
            CreateMyAdvertisementListView();
            gridViewSecond.Columns.Clear();
            CreateInterestedUsersListView();
        }

        #region AllAdvertisementsListView

        private void CreateAllAdvertisementListView()
        {
            listFirst.View = gridViewFirst;
            gridViewFirst.Columns.Add(new GridViewColumn
            {
                Header = "Price",
                Width = 100,
                DisplayMemberBinding = new Binding("Price")
            });
            gridViewFirst.Columns.Add(new GridViewColumn
            {
                Header = "Square",
                Width = 100,
                DisplayMemberBinding = new Binding("Square")
            });
            gridViewFirst.Columns.Add(new GridViewColumn
            {
                Header = "RoomsCount",
                Width = 80,
                DisplayMemberBinding = new Binding("RoomsCount")
            });
            gridViewFirst.Columns.Add(new GridViewColumn
            {
                Header = "Adress",
                Width = 100,
                DisplayMemberBinding = new Binding("Adress")
            });

            UpdateAllAdvertisementsListView(ListOfAllAdvertisements);
        }
        private void UpdateAllAdvertisementsListView(IEnumerable<Advertisement> listOfAdvertisements)
        {
            listFirst.Items.Clear();
            if (listOfAdvertisements != null)
            {
                foreach (var item in listOfAdvertisements)
                {
                    listFirst.Items.Add(new Advertisement()
                    {
                        AdvertisementId = item.AdvertisementId,
                        Adress = item.Adress,
                        Price = item.Price,
                        RoomsCount = item.RoomsCount,
                        Square = item.Square,
                        UserId = item.UserId
                    });
                }
            }
        }
        #endregion

        #region FavoriteAdvertisementsListView
        private void CreateFavoriteAdvertisementsListView()
        {
            listSecond.View = gridViewSecond;

            gridViewSecond.Columns.Add(new GridViewColumn
            {
                Header = "Owner's Name",
                Width = 64,
                DisplayMemberBinding = new Binding("Name")
            });
            gridViewSecond.Columns.Add(new GridViewColumn
            {
                Header = "Owner's SurName",
                Width = 64,
                DisplayMemberBinding = new Binding("SurName")
            });

            gridViewSecond.Columns.Add(new GridViewColumn
            {
                Header = "Email",
                Width = 64,
                DisplayMemberBinding = new Binding("Email")
            });
            gridViewSecond.Columns.Add(new GridViewColumn
            {
                Header = "TelNumber",
                Width = 64,
                DisplayMemberBinding = new Binding("TelNumber")
            });
            gridViewSecond.Columns.Add(new GridViewColumn
            {
                Header = "Price",
                Width = 64,
                DisplayMemberBinding = new Binding("Price")
            });
            gridViewSecond.Columns.Add(new GridViewColumn
            {
                Header = "Adress",
                Width = 80,
                DisplayMemberBinding = new Binding("Adress")
            });

            UpdateFavoriteAdvertisementsListView(ListOfInterestingAdvertisements);
        }
        private void UpdateFavoriteAdvertisementsListView(IEnumerable<Advertisement> listOfAdvertisements)
        {
            listSecond.Items.Clear();

            if (listOfAdvertisements != null)
            {
                foreach (var item in ListOfInterestingAdvertisements)
                {
                    var owner = unitOfWork.UserRepository.FindByID(item.UserId);
                    listSecond.Items.Add(new
                    {
                        Name = owner.Name,
                        SurName = owner.SurName,
                        Email = owner.Email,
                        TelNumber = owner.TelNumber,
                        Price = item.Price,
                        Adress = item.Adress
                    });
                }
            }
        }
        #endregion

        #region MyAdvertisementListView
        private void CreateMyAdvertisementListView()
        {
            listFirst.View = gridViewFirst;

            gridViewFirst.Columns.Add(new GridViewColumn
            {
                Header = "Price",
                Width = 100,
                DisplayMemberBinding = new Binding("Price")
            });
            gridViewFirst.Columns.Add(new GridViewColumn
            {
                Header = "Square",
                Width = 100,
                DisplayMemberBinding = new Binding("Square")
            });
            gridViewFirst.Columns.Add(new GridViewColumn
            {
                Header = "RoomsCount",
                Width = 100,
                DisplayMemberBinding = new Binding("RoomsCount")
            });
            gridViewFirst.Columns.Add(new GridViewColumn
            {
                Header = "Adress",
                Width = 100,
                DisplayMemberBinding = new Binding("Adress")
            });

            UpdateMyAdvertisementListView(ListOfMyAdvertisements);
        }
        private void UpdateMyAdvertisementListView(IEnumerable<Advertisement> listOfAdvertisements)
        {
            listFirst.Items.Clear();
            foreach (var item in listOfAdvertisements)
            {
                listFirst.Items.Add(new Advertisement()
                {
                    AdvertisementId = item.AdvertisementId,
                    Adress = item.Adress,
                    Price = item.Price,
                    RoomsCount = item.RoomsCount,
                    Square = item.Square,
                    UserId = item.UserId
                });
            }
        }
        #endregion

        #region InterestedUsersListView
        private void CreateInterestedUsersListView()
        {
            listSecond.View = gridViewSecond;

            gridViewFirst.Columns.Add(new GridViewColumn
            {
                Header = "Adress",
                Width = 100,
                DisplayMemberBinding = new Binding("Adress")
            });
            gridViewSecond.Columns.Add(new GridViewColumn
            {
                Header = "First Name",
                Width = 100,
                DisplayMemberBinding = new Binding("Name")
            });
            gridViewSecond.Columns.Add(new GridViewColumn
            {
                Header = "Last Name",
                Width = 100,
                DisplayMemberBinding = new Binding("SurName")
            });
            gridViewSecond.Columns.Add(new GridViewColumn
            {
                Header = "Email",
                Width = 100,
                DisplayMemberBinding = new Binding("Email")
            });

            UpdateInterestedUsersListView();
        }
        private void UpdateInterestedUsersListView()
        {
            listSecond.Items.Clear();

            foreach (var item in ListOfMyAdvertisements)
            {
                foreach (var inner in item.InterestedUsers)
                {
                    listSecond.Items.Add(new
                    {
                        Adress = item.Adress,
                        Name = inner.Name,
                        SurName = inner.SurName,
                        TelNumber = inner.TelNumber,
                        Email = inner.Email
                    });
                }
            }
        }

        #endregion
        private void btnLike_Click(object sender, RoutedEventArgs e)
        {
            if (listFirst.SelectedItem != null)
            {
                var advertisement = listFirst.SelectedItem as Advertisement;
                if (advertisement.UserId != userId)
                {
                    currentUser.LikeAdvertisements.Add(advertisement);
                    unitOfWork.Save();

                    ListOfInterestingAdvertisements = currentUser.LikeAdvertisements;
                    ListOfInterestedUsers = advertisement.InterestedUsers;

                  //  gridViewSecond.Columns.Clear();                  
                    MessageBox.Show("Advertisement added to favorite.");

                    UpdateFavoriteAdvertisementsListView(ListOfInterestingAdvertisements);
                    UpdateInterestedUsersListView();
                }
                else
                {
                    MessageBox.Show("You have chosen your advertisement.");
                }
            }
            else
            {
                MessageBox.Show("Please, select advertisement.");
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateAdvertisement createAdvertisementWindow = new CreateAdvertisement();
            createAdvertisementWindow.Show();
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (listFirst.SelectedItem != null)
            {
                var updateItem = listFirst.SelectedItem as Advertisement;
                UpdateAdvertisementWindow updateWindow = new UpdateAdvertisementWindow(updateItem);

                updateWindow.Show();
                this.Close();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string headerText = (tabControl.Items.GetItemAt(0) as TabItem).Header.ToString();
            if (headerText == "My Advertisements")
            {
                if (listFirst.SelectedItem != null)
                {
                    var deleteItem = listFirst.SelectedItem as Advertisement;
                    ConfirmDeleteWindow deleteWindow = new ConfirmDeleteWindow(deleteItem);
                    deleteWindow.Show();
                    this.Close();
                }
            }
        }
    }
}
