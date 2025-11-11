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

namespace PokemonCardFinal.View.ListRecords
{
    /// <summary>
    /// Interaction logic for AbilityRecordsPage.xaml
    /// </summary>
    public partial class AbilityRecordsPage : Page
    {
        IAbilityManager _abilityManager;
        Ability[] _abilities;
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

            if (conformationWindow == MessageBoxResult.Yes)
            {
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
            else
            {
                return;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void datAbility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedAbility = datAbility.SelectedItem as Ability;
        }

        private void LoadList()
        {
            try
            {
                _abilities = _abilityManager.FormatAbility(_abilityManager.GetAbilities()).ToArray();
                _selectedAbility = _abilities[0];
                datAbility.ItemsSource = _abilities;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
