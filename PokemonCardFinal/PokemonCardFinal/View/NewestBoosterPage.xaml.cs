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
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace PokemonCardFinal.View
{
    /// <summary>
    /// Interaction logic for NewestBoosterPage.xaml
    /// </summary>
    public partial class NewestBoosterPage : Page
    {
        ICardManager _cardManager;
        UserVM _accessToken;

        public NewestBoosterPage()
        {
            InitializeComponent();
            _cardManager = new CardManager();
            _accessToken = null;
        }

        public NewestBoosterPage(UserVM accessToken)
        {
            InitializeComponent();
            _cardManager = new CardManager();
            _accessToken = accessToken;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadList();

            if (_accessToken == null)
            {
                grdButton.Visibility = Visibility.Collapsed;
            }
        }

        private void datCard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datCard.SelectedItem == null)
            {
                return;
            }

            try
            {
                // load the detailed page
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                Card selectedCard = datCard.SelectedItem as Card;

                mainWindow.frmMain.Navigate(new DetailedCardPage(selectedCard.CardID, _accessToken));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void btnCollection_Click(object sender, RoutedEventArgs e)
        {
            if (datCard.SelectedItem == null)
            {
                return;
            }

            if (_accessToken == null)
            {
                return;
            }
            try
            {
                Card selectedCard = datCard.SelectedItem as Card;
                CardVM cardVM = _cardManager.GetCardVM(selectedCard.CardID);

                AddCollectionCardWindow collectionWindow = new AddCollectionCardWindow(_accessToken, cardVM);

                collectionWindow.Owner = Window.GetWindow(this);
                collectionWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadList()
        {
            try
            {
                //datCard.ItemsSource = _cardManager.GetCardsByReleaseDate(_releaseDate);
                MessageBox.Show("Needs Implementation");

                datCard.Columns[0].Width = new DataGridLength(65);
                datCard.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                datCard.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                datCard.Columns[3].Width = new DataGridLength(100);
                datCard.Columns[4].Width = new DataGridLength(65);
                datCard.Columns[5].Width = new DataGridLength(100);
                datCard.Columns[6].Width = new DataGridLength(25);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
