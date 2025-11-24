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
    /// Interaction logic for AlternateArtRecordsPage.xaml
    /// </summary>
    public partial class AlternateArtRecordsPage : Page
    {
        IAltArtManager _altArtManager;
        AlternateArt _selectedAltArt;

        public AlternateArtRecordsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _altArtManager = new AltArtManager();
            LoadList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedAltArt == null)
            {
                return;
            }

            // Pop up window to confirm if the admin wants to delete the record
            MessageBoxResult conformationWindow = MessageBox.Show
            (
                "Are you sure you want to delete " + _selectedAltArt.AlternateArtID + ".",
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
                if (_altArtManager.DeleteAlternateArt(_selectedAltArt.AlternateArtID))
                {
                    MessageBox.Show("The alternate art was successfully be deleted.");
                    LoadList();
                }
                else
                {
                    MessageBox.Show("The alternate art could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The alternate art failed to be deleted.");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedAltArt == null)
            {
                return;
            }

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                // Navigate the main frame to the new outer page
                AddEditContainerPage containerPage = new AddEditContainerPage();
                mainWindow.frmMain.Navigate(containerPage);

                // When the outer page is loaded change the inner page
                // to AddAlternateArtPage
                containerPage.Loaded += (s, args) =>
                {
                    containerPage.IsListView = false;
                    containerPage.tabController.SelectedItem = containerPage.tabAlternate;
                    containerPage.frmAlternate.Navigate
                    (
                        new AddAlternateArtPage(_selectedAltArt, _altArtManager, containerPage)
                    );
                };
            }
        }
        private void datAlternate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedAltArt = datAlternate.SelectedItem as AlternateArt;
        }

        private void LoadList()
        {
            try
            {
                datAlternate.ItemsSource = _altArtManager.GetAlternateArts();
                _selectedAltArt = datAlternate.SelectedItem as AlternateArt;

                datAlternate.Columns[0].Header = "Alternate Art Name";

                datAlternate.Columns[0].Width = new DataGridLength(150);
                datAlternate.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load the list of alternate arts.");
            }
        }
    }
}
