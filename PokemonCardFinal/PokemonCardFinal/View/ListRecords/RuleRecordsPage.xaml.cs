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
    /// Interaction logic for RuleRecordsPage.xaml
    /// </summary>
    public partial class RuleRecordsPage : Page
    {
        IRuleManager _ruleManager;
        PokemonRule _selectedRule;

        public RuleRecordsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _ruleManager = new RuleManager();
            LoadList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRule == null)
            {
                return;
            }

            // Pop up window to confirm if the admin wants to delete the record
            MessageBoxResult conformationWindow = MessageBox.Show
            (
                "Are you sure you want to delete " + _selectedRule.RuleID + ".",
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
                if (_ruleManager.DeleteRule(_selectedRule.RuleID))
                {
                    MessageBox.Show("The card rule was successfully deleted");
                    LoadList();
                }
                else
                {
                    MessageBox.Show("The card rule could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The card rule failed to be deleted.");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRule == null)
            {
                return;
            }

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            // Navigate the main frame to the new outer page
            AddEditContainerPage containerPage = new AddEditContainerPage();
            mainWindow.frmMain.Navigate(containerPage);

            // When the outer page is loaded change the inner page
            // to AddRulePage
            containerPage.Loaded += (s, args) =>
            {
                containerPage.IsListView = false;
                containerPage.tabController.SelectedItem = containerPage.tabRule;
                containerPage.frmRule.Navigate
                (
                    new AddRulePage(_selectedRule, _ruleManager, containerPage)
                );
            };
            
        }

        private void datRule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRule = datRule.SelectedItem as PokemonRule;
        }

        private void LoadList()
        {
            try
            {
                datRule.ItemsSource = _ruleManager.GetRules();
                _selectedRule = datRule.SelectedItem as PokemonRule;

                datRule.Columns[0].Header = "Rule Name";

                datRule.Columns[0].Width = new DataGridLength(175);
                datRule.Columns[1].Width = new DataGridLength(1,DataGridLengthUnitType.Star);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load the list of rules.\n" + ex.Message);
            }
        }
    }
}
