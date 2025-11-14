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
        bool _isAlternateArtLoaded;
        bool _isArtistLoaded;
        bool _isBoosterLoaded;
        bool _isRuleLoaded;
        bool _isElementLoaded;
        bool _isMoveLoaded;


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
            _isArtistLoaded = option;
            _isAlternateArtLoaded = option;
            _isBoosterLoaded = option;
            _isRuleLoaded = option;
            _isElementLoaded = option;
            _isMoveLoaded = option;
        }

        private void ListPages() 
        {
            if (tabController.SelectedItem == null)
            {
                return;
            }
            else if (tabController.SelectedItem == tabAbility && !_isAbilityLoaded)
            {
                
                _isAbilityLoaded = true;
                frmAbility.Navigate(new AbilityRecordsPage());
            }
            else if (tabController.SelectedItem == tabAlternate &&
                        !_isAlternateArtLoaded)
            {
                SetAllLoaded();
                _isAlternateArtLoaded = true;
                frmAlternate.Navigate(new AlternateArtRecordsPage());
            }
            else if (tabController.SelectedItem == tabArtist && !_isArtistLoaded)
            {
                SetAllLoaded();
                _isArtistLoaded = true;
                frmArtist.Navigate(new ArtistRecordsPage());
            }
            else if (tabController.SelectedItem == tabBooster && !_isBoosterLoaded)
            {
                SetAllLoaded();
                _isBoosterLoaded = true;
                frmBooster.Navigate(new BoosterRecordsPage());
            }
            else if (tabController.SelectedItem == tabRule && !_isRuleLoaded)
            {
                SetAllLoaded();
                _isRuleLoaded = true;
                frmRule.Navigate(new RuleRecordsPage());
            }
            else if (tabController.SelectedItem == tabElement && !_isElementLoaded)
            {
                SetAllLoaded();
                _isElementLoaded = true;
                frmElement.Navigate(new ElementRecordsPage());
            }
            else if (tabController.SelectedItem == tabMove && !_isMoveLoaded)
            {
                SetAllLoaded();
                _isMoveLoaded = true;
                frmMove.Navigate(new MoveRecordsPage());
            }
        }

        private void AddPages() 
        {
            if (tabController.SelectedItem == null)
            {
                SetAllLoaded();
                return;
            }
            else if (tabController.SelectedItem == tabAbility && !_isAbilityLoaded)
            {
                SetAllLoaded();
                _isAbilityLoaded = true;
                frmAbility.Navigate(new AddAbilityPage());
            }
            else if (tabController.SelectedItem == tabAlternate &&
                        !_isAlternateArtLoaded)
            {
                SetAllLoaded();
                _isAlternateArtLoaded = true;
                frmAlternate.Navigate(new AddAlternateArtPage());
            }
            else if (!_isArtistLoaded && tabController.SelectedItem == tabArtist)
            {
                SetAllLoaded();
                _isArtistLoaded = true;
                frmArtist.Navigate(new AddArtistPage());
            }
            else if (tabController.SelectedItem == tabBooster && !_isBoosterLoaded)
            {
                SetAllLoaded();
                _isBoosterLoaded = true;
                frmBooster.Navigate(new AddBoosterPage());
            }
            else if (!_isElementLoaded && tabController.SelectedItem == tabElement)
            {
                SetAllLoaded();
                _isElementLoaded = true;
                frmElement.Navigate(new AddElementPage());
            }
            else if (tabController.SelectedItem == tabRule && !_isRuleLoaded)
            {
                SetAllLoaded();
                _isRuleLoaded = true;
                frmRule.Navigate(new AddRulePage());
            }
        }
        public void DisplayTabItems(bool option)
        {
            foreach (TabItem tabItem in tabController.Items)
            {
                tabItem.IsEnabled = option;
            }
        }
    }
}
