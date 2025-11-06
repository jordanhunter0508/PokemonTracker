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

namespace PokemonCardFinal.View.EditRecords
{
    /// <summary>
    /// Interaction logic for ArtistRecordsPage.xaml
    /// </summary>
    public partial class ArtistRecordsPage : Page
    {
        Artist[] _artists;
        IArtistManager _artistManager;
        Artist _selectedArtist;
        public ArtistRecordsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _artistManager = new ArtistManager();

            try
            {
                _artists = _artistManager.FormatArtists(_artistManager.GetArtists()).ToArray();
                _selectedArtist = _artists[0];
                datArtist.ItemsSource = _artists;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Pop up window to confirm if the admin wants to delete the record
            MessageBoxResult conformationWindow = MessageBox.Show
            (
                "Are you sure you want to delete " + _selectedArtist.GivenName + ", " + _selectedArtist.Surname + ".",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (conformationWindow == MessageBoxResult.Yes)
            {
                try
                {
                    if (_artistManager.DeleteArtistByArtistID(_selectedArtist.ArtistID))
                    {
                        MessageBox.Show("The artist was successfully deleted");
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
            else
            {
                return;
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to CreateRecordPage
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                // Navigate the main frame to the new outer page
                View.AddRecord.RecordContainerPage addRecordPage = new View.AddRecord.RecordContainerPage();
                mainWindow.frmMain.Navigate(addRecordPage);

                // When the addRecordPage is loaded change the inner page
                // to addElementPage
                addRecordPage.Loaded += (s, args) =>
                {
                    addRecordPage.tabController.SelectedItem = addRecordPage.tabArtist;
                    Debug.WriteLine(_selectedArtist.GivenName);
                    addRecordPage.frmArtist.Navigate
                    (
                        new AddArtistPage(_selectedArtist, _artistManager)
                    );
                };
            }
        }

        private void datArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedArtist = datArtist.SelectedItem as Artist;
        }
    }
}