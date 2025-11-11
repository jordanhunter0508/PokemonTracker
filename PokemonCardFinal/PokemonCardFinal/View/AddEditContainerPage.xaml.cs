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
using PokemonCardFinal.View.ListRecords;
using PokemonCardFinal.View.AddRecord;

namespace PokemonCardFinal.View
{
    /// <summary>
    /// Interaction logic for CreateRecordPage.xaml
    /// </summary>
    public partial class AddEditContainerPage : Page
    {
        public bool IsListView;

        // Used to specify which page is loaded
        bool _isAbilityLoaded;
        bool _isElementLoaded;
        bool _isArtistLoaded;
        
        public AddEditContainerPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetAllLoaded();
        }

        private void tabController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsListView)
            {
                // load the corresponind list page
                ListPages();
            }
            else
            {
                // load a add record page
                AddPages();
            }            
        }

        /// <summary>
        /// Sets all isLoaded variables to false by default
        /// </summary>
        /// <param name="option">Used to set the isLoaded variables</param>
        private void SetAllLoaded(bool option = false)
        {
            _isAbilityLoaded = option;
            _isElementLoaded = option;
            _isArtistLoaded = option;
        }

        private void ListPages() 
        {
            if (tabController.SelectedItem == null)
            {
                return;
            }
            else if (tabController.SelectedItem == tabAbility && !_isAbilityLoaded)
            {
                SetAllLoaded();
                _isAbilityLoaded = true;
                frmAbility.Navigate(new AbilityRecordsPage());
            }
            else if (tabController.SelectedItem == tabElement && !_isElementLoaded)
            {
                SetAllLoaded();
                _isElementLoaded = true;
                frmElement.Navigate(new ElementRecordsPage());
            }
            else if (tabController.SelectedItem == tabArtist && !_isArtistLoaded)
            {
                SetAllLoaded();
                _isArtistLoaded = true;
                frmArtist.Navigate(new ArtistRecordsPage());
            }
        }

        public void AddPages() 
        {
            if (tabController.SelectedItem == null)
            {
                return;
            }
            else if (tabController.SelectedItem == tabAbility && !_isAbilityLoaded)
            {
                SetAllLoaded();
                _isAbilityLoaded = true;
                frmAbility.Navigate(new AddAbilityPage());
            }
            else if (!_isElementLoaded && tabController.SelectedItem == tabElement)
            {
                SetAllLoaded();
                _isElementLoaded = true;
                frmElement.Navigate(new AddElementPage());
            }
            else if (!_isArtistLoaded && tabController.SelectedItem == tabArtist)
            {
                SetAllLoaded();
                _isArtistLoaded = true;
                frmArtist.Navigate(new AddArtistPage());
            }
        }
    }
}
