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
using PokemonCardFinal.View.EditRecords;

namespace PokemonCardFinal.View.AddRecord
{
    /// <summary>
    /// Interaction logic for CreateRecordPage.xaml
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
            _isArtistLoaded= false;
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
                frmElement.Navigate(new AddElementPage());
            }
            else if(!_isArtistLoaded && tabController.SelectedItem == tabArtist)
            {
                _isElementLoaded = false;
                _isArtistLoaded = true;
                frmArtist.Navigate(new AddArtistPage());
            }
        }
    }
}
