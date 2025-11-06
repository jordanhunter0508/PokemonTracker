using System.Windows;
using System.Windows.Controls;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace PokemonCardFinal.View.EditRecords
{
    /// <summary>
    /// Interaction logic for ViewRecordsPage.xaml
    /// </summary>
    public partial class RecordContainerPage : Page
    {
        bool _isElementLoaded;
        bool _isArtistLoaded;
        public RecordContainerPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _isElementLoaded = false;
            _isArtistLoaded = false;
        }

        private void tabController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabController.SelectedItem == null)
            {
                return;
            }
            else if (!_isElementLoaded && tabController.SelectedItem == tabElement)
            {
                _isElementLoaded = true;
                _isArtistLoaded = false;
                frmElement.Navigate(new ElementRecordsPage());
            }
            else if (!_isArtistLoaded && tabController.SelectedItem == tabArtist) 
            {
                _isElementLoaded = false;
                _isArtistLoaded = true;
                frmArtist.Navigate(new ArtistRecordsPage());
            }
        }
    }
}
