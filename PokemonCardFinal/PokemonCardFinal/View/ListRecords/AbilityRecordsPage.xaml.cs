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
using PokemonCardFinal.View.AddRecord;

namespace PokemonCardFinal.View.ListRecords
{
    /// <summary>
    /// Interaction logic for AbilityRecordsPage.xaml
    /// </summary>
    public partial class AbilityRecordsPage : Page
    {
        IAbilityManager _abilityManager;
        Ability _selectedAbility;

        public AbilityRecordsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _abilityManager = new AbilityManager();
            LoadList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedAbility == null)
            {
                return;
            }

            // Pop up window to confirm if the admin wants to delete the record
            MessageBoxResult conformationWindow = MessageBox.Show
            (
                "Are you sure you want to delete " + _selectedAbility.AbilityID + ".",
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
                if (_abilityManager.DeleteAbility(_selectedAbility.AbilityID))
                {
                    MessageBox.Show("The ability was successfully deleted");
                    LoadList();
                }
                else
                {
                    MessageBox.Show("The ability could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The ability failed to be deleted.");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedAbility == null)
            {
                return;
            }

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            // Navigate the main frame to the new outer page
            AddEditContainerPage containerPage = new AddEditContainerPage();
            mainWindow.frmMain.Navigate(containerPage);

            // When the outer page is loaded change the inner page
            // to AddAbilityPage
            containerPage.Loaded += (s, args) =>
            {
                containerPage.IsListView = false;
                containerPage.tabController.SelectedItem = containerPage.tabAbility;
                containerPage.frmAbility.Navigate
                (
                    new AddAbilityPage(_selectedAbility, _abilityManager, containerPage)
                );
            };
            
        }

        private void datAbility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedAbility = datAbility.SelectedItem as Ability;
        }

        private void LoadList()
        {
            try
            {
                datAbility.ItemsSource = _abilityManager.GetActiveAbilities().Items;
                _selectedAbility = datAbility.SelectedItem as Ability;

                datAbility.Columns[0].Header = "Ability Name";
                datAbility.Columns[1].Header = "Ability Type";

                datAbility.Columns[0].Width = new DataGridLength(125);
                datAbility.Columns[1].Width = new DataGridLength(125);
                datAbility.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load the list of abilites.");
            }
        }        
    }
}
