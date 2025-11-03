using System.Windows;
using System.Windows.Controls;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;
using PokemonCardFinal.UpdateView;

namespace PokemonCardFinal
{
    /// <summary>
    /// Interaction logic for ViewRecordsPage.xaml
    /// </summary>
    public partial class ViewRecordsPage : Page
    {
        bool _isElementLoaded;
        public ViewRecordsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _isElementLoaded = false;
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
                frmElement.Navigate(new UpdateElementPage());
            }
            else { }
        }
    }
}
