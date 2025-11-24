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
    /// Interaction logic for BoosterRecordsPage.xaml
    /// </summary>
    public partial class BoosterRecordsPage : Page
    {
        IBoosterManager _boosterManger;
        Booster _selectedBooster;
        public BoosterRecordsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _boosterManger = new BoosterManager();
            LoadList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBooster == null)
            {
                return;
            }

            // Pop up window to confirm if the admin wants to delete the record
            MessageBoxResult conformationWindow = MessageBox.Show
            (
                "Are you sure you want to delete " + _selectedBooster.BoosterID + ".",
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
                if (_boosterManger.DeleteBooster(_selectedBooster.BoosterID))
                {
                    MessageBox.Show("The booster was successfully deleted");
                    LoadList();
                }
                else
                {
                    MessageBox.Show("The booster could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The booster failed to be deleted.\n" + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBooster == null)
            {
                return;
            }

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                // Navigate the main frame to the new outer page
                AddEditContainerPage containerPage = new AddEditContainerPage();
                mainWindow.frmMain.Navigate(containerPage);

                // When the outer page is loaded change the inner page
                // to AddBoosterPage
                containerPage.Loaded += (s, args) =>
                {
                    containerPage.IsListView = false;
                    containerPage.tabController.SelectedItem = containerPage.tabBooster;
                    containerPage.frmBooster.Navigate
                    (
                        new AddBoosterPage(_selectedBooster, _boosterManger, containerPage)
                    );
                };
            }
        }

        private void datBooster_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBooster = datBooster.SelectedItem as Booster;
        }

        private void LoadList()
        {
            try
            {

                datBooster.ItemsSource = _boosterManger.GetBoosters();
                _selectedBooster = datBooster.SelectedItem as Booster;

                datBooster.Columns[0].Header = "Booster Name";
                datBooster.Columns[2].Header = "Release Date";

                datBooster.Columns[0].Width = new DataGridLength(200);
                datBooster.Columns[1].Width = new DataGridLength(200);
                datBooster.Columns[2].Width = new DataGridLength(200);
                datBooster.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load the list of boosters.\n" + ex.Message);
            }
        }
    }
}
