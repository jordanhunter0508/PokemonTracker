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
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace PokemonCardFinal.View.Profile
{
    /// <summary>
    /// Interaction logic for UserDeckPage.xaml
    /// </summary>
    public partial class UserDeckPage : Page
    {
        IUserManager _userManager;
        UserVM _accessToken;
        List<CollectionVM> _decks;
        CollectionVM _selectedCollection;
        ProfileContainerPage _containerPage;

        public UserDeckPage(IUserManager userManager, UserVM accessToken, ProfileContainerPage containerPage)
        {
            InitializeComponent();
            _userManager = userManager;
            _accessToken = accessToken;
            _containerPage = containerPage;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            try
            {
                _decks = _userManager.GetUserDecks(_accessToken.Collections);
                datDeck.ItemsSource = _decks;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void datDeck_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_selectedCollection == null)
            {
                return;
            }
            
            try
            {
                Debug.WriteLine("From UserDeckPage collectionID: " + _selectedCollection.CollectionID.ToString());
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                ProfileContainerPage containerPage = new ProfileContainerPage(_accessToken, _userManager);
                mainWindow.frmMain.Navigate(containerPage);

                containerPage.Loaded += (s, args) =>
                {
                    containerPage.tabController.SelectedItem = containerPage.tabUserDeck;
                    containerPage.frmUserDeck.Navigate
                    (
                        new UserCardsPage(new CollectionManager(), _selectedCollection)
                    );
                };
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void datDeck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedCollection = datDeck.SelectedItem as CollectionVM;
        }
    }
}
