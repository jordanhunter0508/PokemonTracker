using System;
using System.Collections.Generic;
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

        public UserCardsPage(ICollectionManager collectionManager, int collectionID)
        {
            InitializeComponent();
            _collectionManager = collectionManager;
            _collectionID = collectionID;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _collectionVM = _collectionManager.GetCollectionVMByCollectionID(_collectionID);

                List<CollectionCardVM> collectionCardVM = _collectionManager.ConvertCollectionCardToVM(_collectionVM.Cards);

                if (collectionCardVM == null || collectionCardVM.Count == 0)
                {
                    datCard.Visibility = Visibility.Collapsed;
                    grdEmpty.Visibility = Visibility.Visible;
                }

                datCard.AutoGenerateColumns = false;
                datCard.ItemsSource = collectionCardVM;

                lblName.Content = _collectionVM.Name;
                txtDescription.Text = _collectionVM.Description;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void datCard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ICardManager cardManager = new CardManager();

            // load the detailed page
            try
            {
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

                int cardID = _selectedCard.Card.CardID;
                CardVM selectedCard = cardManager.GetCardVMByCardID(cardID);

                mainWindow.frmMain.Navigate(new DetailedCardPage(selectedCard, this));
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
    }
}
