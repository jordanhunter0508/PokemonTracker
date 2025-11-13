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
using PokemonCardFinal.View.ListRecords;

namespace PokemonCardFinal.View.AddRecord
{
    /// <summary>
    /// Interaction logic for AddArtistPage.xaml
    /// </summary>
    public partial class AddArtistPage : Page
    {
        IArtistManager _artistManager;
        Artist _artist;
        AddEditContainerPage _containerPage;
        bool _isEditMode;
        public AddArtistPage()
        {
            InitializeComponent();
            _artistManager = new ArtistManager();
            _isEditMode = false;
        }

        public AddArtistPage(Artist artist, IArtistManager artistManager, AddEditContainerPage containerPage)
        {
            InitializeComponent();
            _artist = artist;
            _artistManager = artistManager;
            _containerPage = containerPage;
            _isEditMode = true;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_isEditMode)
            {
                //
            }
            else
            {
                btnClear.Content = "Go Back";
                txtGivenName.Text = _artist.GivenName;
                txtSurname.Text = _artist.Surname;

                // Disables all other tab items
                _containerPage.DisplayTabItems(false);
                _containerPage.tabArtist.IsEnabled = true;
            }
            txtGivenName.Focus();
            btnSave.IsDefault = true;

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditMode)
            {
                ClearTextAreas();
                txtGivenName.Focus();

            }
            else
            {
                DisplayListViewPage();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string givenName = txtGivenName.Text;
            string surname = txtSurname.Text;

            if (givenName == "" || givenName == null || givenName.Length > 50)
            {
                MessageBox.Show("The artist's first name entered was invalid.");
                txtGivenName.Focus();
                return;
            }
            if (surname == "" || surname == null || surname.Length > 100)
            {
                MessageBox.Show("The artist's last name entered was invalid.");
                txtSurname.Focus();
                return;
            }

            if (!_isEditMode)
            {
                CreateModeSaveButton(givenName, surname);

            }
            else
            {
                EditModeSaveButton(givenName, surname);
            }
        }

        private void CreateModeSaveButton(string givenName, string surname)
        {
            try
            {
                if (_artistManager.AddArtist(givenName, surname))
                {
                    MessageBox.Show("The artist " + givenName + ", " + surname + " was successfully created.");
                    ClearTextAreas();
                }
                else
                {
                    MessageBox.Show("The artist " + givenName + ", " + surname + " was not created.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditModeSaveButton(string givenName, string surname)
        {
            int artistID = _artist.ArtistID;

            try
            {
                if (_artistManager.EditArtist(artistID, givenName, surname))
                {
                    MessageBox.Show("The artist " + givenName + ", " + surname + " was successfully created.");
                    DisplayListViewPage();
                }
                else
                {
                    MessageBox.Show("The element " + givenName + " was not successfully updated.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextAreas()
        {
            txtGivenName.Text = "";
            txtSurname.Text = "";
        }

        private void DisplayListViewPage()
        {
            _containerPage.DisplayTabItems(true);
            _containerPage.IsListView = true;
            _containerPage.frmArtist.Navigate(new ArtistRecordsPage());
        }
    }
}
