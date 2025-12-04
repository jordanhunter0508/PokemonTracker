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
        int _collectionID;

        bool _isDeckMode;

        public UserCardsPage(ICollectionManager collectionManager, int collectionID)
        {
            InitializeComponent();
            _collectionManager = collectionManager;
            _collectionID = collectionID;
            _isDeckMode = false;
        }

        public UserCardsPage(ICollectionManager collectionManager, CollectionVM collectionVM)
        {
            InitializeComponent();
            _collectionManager = collectionManager;
            _collectionVM = collectionVM;
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

                DetailedCardPage detailedCardPage = new DetailedCardPage(selectedCard);
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
            // remove the card from the collection
        }

        private void LoadCards()
        {
            try
            {
                // the starting value in the database is 1
                // the only way for 0 is if it wasn't set.
                if (_collectionID == 0)
                {
                    _collectionVM = _collectionManager.GetCollectionVMByCollectionID(_collectionVM.CollectionID);
                }
                else
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

            //_containerPage.Loaded += (s, args) =>
            //{ 
            //    _containerPage.frmUserDeck.Navigate(_previousDeck);
            //};
        }

        private void btnDeleteDeck_Click(object sender, RoutedEventArgs e)
        {
            // remove deck from user then return them to the UserDeckPage
        }
    }
}
