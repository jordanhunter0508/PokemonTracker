using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Azure.Core;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace PokemonCardFinal.View.Profile
{
    /// <summary>
    /// Interaction logic for UserCardsPage.xaml
    /// </summary>
    public partial class UserCardsPage : Page
    {
        ICollectionManager _collectionManager;
        CollectionVM _collectionVM;
        CollectionCardVM _selectedCard;
        UserVM _accessToken;
        int _collectionID;

        bool _isDeckMode;

        public UserCardsPage(ICollectionManager collectionManager, int collectionID, UserVM accessToken)
        {
            InitializeComponent();
            _collectionManager = collectionManager;
            _collectionID = collectionID;
            _accessToken = accessToken;
            _isDeckMode = false;
        }

        public UserCardsPage(ICollectionManager collectionManager, CollectionVM collectionVM, UserVM accessToken)
        {
            InitializeComponent();
            _collectionManager = collectionManager;
            _collectionVM = collectionVM;
            _accessToken = accessToken;
            _isDeckMode = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCards();

            if (_isDeckMode)
            {
                btnGoBack.Visibility = Visibility.Visible;
                btnDeleteDeck.Visibility = Visibility.Visible;
            }
        }

        private void datCard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_selectedCard == null)
            {
                return;
            }

            ICardManager cardManager = new CardManager();

            // load the detailed page
            try
            {
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

                int cardID = _selectedCard.Card.CardID;
                CardVM selectedCard = cardManager.GetCardVMByCardID(cardID);
                DetailedCardPage detailedCardPage = new DetailedCardPage(selectedCard,_accessToken);
                detailedCardPage.IsCollectionView = true;
                mainWindow.frmMain.Navigate(detailedCardPage);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void datCard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedCard = datCard.SelectedItem as CollectionCardVM;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string name = _selectedCard.Name;

            // Pop up window to confirm if the user wants to delete the deck
            MessageBoxResult conformationWindow = MessageBox.Show
            (
                "Are you sure you want to delete " + name + " from " + _collectionVM.Name + ".",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (conformationWindow != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                if (_collectionManager.DeleteCollectionCard(_selectedCard.CollectionCardID))
                {
                    MessageBox.Show("The card " + name + " was deleted.");
                    LoadCards();
                }
                else
                {
                    MessageBox.Show("The card " + name + " could not be deleted.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void LoadCards()
        {
            try
            {
                if (!_isDeckMode)
                {
                    _collectionVM = _collectionManager.GetCollectionVMByCollectionID(_collectionID);
                }

                List<CollectionCardVM> collectionCardVM = _collectionManager.ConvertCollectionCardToVM(_collectionVM.Cards);

                if (collectionCardVM == null || collectionCardVM.Count == 0)
                {
                    datCard.Visibility = Visibility.Collapsed;
                    btnDeleteDeck.Visibility = Visibility.Collapsed;
                    btnRemove.Visibility = Visibility.Collapsed;
                    grdEmpty.Visibility = Visibility.Visible;
                }

                datCard.AutoGenerateColumns = false;
                datCard.ItemsSource = collectionCardVM;

                lblName.Content = _collectionVM.Name;
                txtDescription.Text = _collectionVM.Description;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.frmMain.GoBack();
        }

        private void btnDeleteDeck_Click(object sender, RoutedEventArgs e)
        {
            if (!_isDeckMode)
            {
                return;
            }

            string name = _collectionVM.Name;

            // Pop up window to confirm if the user wants to delete the deck
            MessageBoxResult conformationWindow = MessageBox.Show
            (
                "Are you sure you want to delete " + name + ".",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (conformationWindow != MessageBoxResult.Yes)
            {
                return;
            }


            try
            {
                if (_collectionManager.DeleteCollection(_collectionVM.CollectionID))
                {
                    RemoveCollectionFromUser();
                    MessageBox.Show("The collection " + name + " was deleted.");
                    LoadUserDeckPage();
                }
                else
                {
                    MessageBox.Show("The collection " + name + " could not be deleted.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void RemoveCollectionFromUser()
        {
            int index = -1;
            for (int i = 0; i < _accessToken.Collections.Count; i++)
            {
                if (_accessToken.Collections[i].CollectionID == _collectionVM.CollectionID)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                _accessToken.Collections.RemoveAt(index);
            }
        }

        private void LoadUserDeckPage()
        {
            MainWindow mainWindow = MainWindow.GetWindow(this) as MainWindow;
            ProfileContainerPage containerPage = new ProfileContainerPage(_accessToken, new UserManager());
            mainWindow.frmMain.Navigate(containerPage);
            containerPage.Loaded += (s, args) =>
            {
                containerPage.tabController.SelectedItem = containerPage.tabUserDeck;
                containerPage.frmUserDeck.Navigate(new UserDeckPage(new UserManager(), _accessToken));
            };
        }
    }
}
