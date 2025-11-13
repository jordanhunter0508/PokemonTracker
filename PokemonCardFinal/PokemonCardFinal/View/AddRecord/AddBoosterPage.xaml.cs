using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for AddBoosterPage.xaml
    /// </summary>
    public partial class AddBoosterPage : Page
    {
        IBoosterManager _boosterManager;
        Booster _booster;
        AddEditContainerPage _containerPage;
        bool _isAddMode;

        public AddBoosterPage()
        {
            InitializeComponent();
            _boosterManager = new BoosterManager();
            _isAddMode = true;
        }

        public AddBoosterPage(Booster booster, IBoosterManager boosterManager, 
            AddEditContainerPage containerPage)
        {
            InitializeComponent();
            _booster = booster;
            _boosterManager = boosterManager;
            _containerPage = containerPage;
            _isAddMode = false;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isAddMode)
            {
                txtBoosterID.Focus();
            }
            else 
            {
                // edit mode
                txtBoosterID.Text = _booster.BoosterID;
                txtSeries.Text = _booster.Series;
                txtReleaseDate.Text = _booster.ReleaseDate.ToString("yyyy/MM/dd");
                txtAbbreviation.Text = _booster.Abbreviation;
                btnClearElement.Content = "Go Back";

                txtBoosterID.IsEnabled = false;

                _containerPage.DisplayTabItems(false);
                _containerPage.tabBooster.IsEnabled = true;
            }
            btnSaveElement.IsDefault = true;
        }

        private void btnClearElement_Click(object sender, RoutedEventArgs e)
        {
            if (_isAddMode)
            {
                ClearTextAreas();
                txtBoosterID.Focus();

            }
            else
            {
                DisplayListViewPage();
            }
        }

        private void btnSaveElement_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            if (_isAddMode)
            {
                CreateModeSaveButton();

            }
            else
            {
                EditModeSaveButton();
            }
        }

        private void CreateModeSaveButton()
        {
            try
            {
                DateTime releaseDate = new DateTime();

                if (!DateTime.TryParse(txtReleaseDate.Text, out releaseDate))
                {
                    MessageBox.Show("Failed to parse date.\nPlease try again.");
                }

                Booster booster = new Booster()
                {
                    BoosterID = txtBoosterID.Text,
                    Series = txtSeries.Text,
                    ReleaseDate = releaseDate,
                    Abbreviation = txtAbbreviation.Text,
                };

                if (_boosterManager.AddBooster(booster))
                {
                    MessageBox.Show("The element " + booster.BoosterID + " was successfully created.");
                    ClearTextAreas();
                    txtBoosterID.Focus();
                }
                else
                {
                    MessageBox.Show("The element " + booster.BoosterID + " was not created.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditModeSaveButton()
        {
            try
            {
                DateTime releaseDate = new DateTime();

                if (!DateTime.TryParse(txtReleaseDate.Text, out releaseDate))
                {
                    MessageBox.Show("Failed to parse the date.\nPlease try again.");
                }

                _booster.Series = txtSeries.Text;
                _booster.ReleaseDate = releaseDate;
                _booster.Abbreviation = txtAbbreviation.Text;

                if (_boosterManager.EditBooster(_booster))
                {
                    MessageBox.Show("The element " + _booster.BoosterID + " was successfully updated.");

                    // Brings the user back to the ElementRecordsPage
                    DisplayListViewPage();
                }
                else
                {
                    MessageBox.Show("The element " + _booster.BoosterID + " was not successfully updated.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidateInput()
        {
            bool result = true;

            string boosterID = txtBoosterID.Text;
            string series = txtSeries.Text;
            DateTime releaseDate = new DateTime();
            string abbreviation = txtAbbreviation.Text;

            if (boosterID.Replace(" ", "") == "" || boosterID == null ||
                boosterID.Length > 50)
            {
                MessageBox.Show("The booster name entered was invalid.");
                txtBoosterID.SelectAll();
                txtBoosterID.Focus();
                result = false;
            }

            else if (_booster != null && _booster.BoosterID != boosterID)
            {
                MessageBox.Show("The booster id was changed.\nPlease change it back to it's original value.");
                result = false;
            }

            else if (series.Replace(" ", "") == "" || series == null ||
                series.Length > 50)
            {
                MessageBox.Show("The series name entered was invalid.");
                txtSeries.SelectAll();
                txtSeries.Focus();
                result = false;
            }

            else if (!DateTime.TryParse(txtReleaseDate.Text, out releaseDate))
            {
                MessageBox.Show("The release year entered was invalid.");
                txtReleaseDate.SelectAll();
                txtReleaseDate.Focus();
                result = false;
            }

            else if (abbreviation.Replace(" ", "") == "" || abbreviation == null ||
                abbreviation.Length > 5)
            {
                MessageBox.Show("The abbreviation entered was invalid.");
                txtAbbreviation.SelectAll();
                txtAbbreviation.Focus();
                result = false;
            }

            return result;
        }

        private void ClearTextAreas()
        {
            txtBoosterID.Text = "";
            txtSeries.Text = "";
            txtAbbreviation.Text = "";
            txtReleaseDate.Text = "";
        }

        private void DisplayListViewPage()
        {
            _containerPage.DisplayTabItems(true);
            _containerPage.IsListView = true;
            _containerPage.frmBooster.Navigate(new BoosterRecordsPage());
        }
    }
}
