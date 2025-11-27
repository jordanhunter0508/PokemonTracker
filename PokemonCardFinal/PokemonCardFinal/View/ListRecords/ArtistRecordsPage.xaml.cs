using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ArtistRecordsPage.xaml
    /// </summary>
    public partial class ArtistRecordsPage : Page
    {
        IArtistManager _artistManager;
        Artist _selectedArtist;

        public ArtistRecordsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _artistManager = new ArtistManager();
            LoadList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedArtist == null)
            {
                return;
            }

            // Pop up window to confirm if the admin wants to delete the record
            MessageBoxResult conformationWindow = MessageBox.Show
            (
                "Are you sure you want to delete " + _selectedArtist.GivenName + ", " + _selectedArtist.Surname + ".",
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
                if (_artistManager.DeleteArtist(_selectedArtist.ArtistID))
                {
                    MessageBox.Show("The artist was successfully deleted");
                    LoadList();
                }
                else
                {
                    MessageBox.Show("The artist could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The artist failed to be deleted.");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedArtist == null)
            {
                return;
            }

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            // Navigate the main frame to the new outer page
            AddEditContainerPage containerPage = new AddEditContainerPage();
            mainWindow.frmMain.Navigate(containerPage);

            // When the outer page is loaded change the inner page
            // to AddArtistPage
            containerPage.Loaded += (s, args) =>
            {
                containerPage.IsListView = false;
                //
                containerPage.tabController.SelectedItem = containerPage.tabArtist;
                containerPage.frmArtist.Navigate
                (
                    new AddArtistPage(_selectedArtist, _artistManager, containerPage)
                );
            };
        }

        private void datArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedArtist = datArtist.SelectedItem as Artist;
        }

        private void LoadList()
        {
            try
            {
                datArtist.ItemsSource = _artistManager.FormatArtists(_artistManager.GetArtists());
                _selectedArtist = datArtist.SelectedItem as Artist;

                datArtist.Columns[1].Header = "Given Name";
                datArtist.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                datArtist.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                datArtist.Columns.RemoveAt(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load the list of artists.");
            }
        }
    }
}