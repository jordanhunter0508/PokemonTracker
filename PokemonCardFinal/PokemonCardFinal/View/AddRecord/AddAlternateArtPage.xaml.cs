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

namespace PokemonCardFinal.View.AddRecord
{
    /// <summary>
    /// Interaction logic for AddAlternateArtPage.xaml
    /// </summary>
    public partial class AddAlternateArtPage : Page
    {
        IAltArtManager _altArtManager;
        AlternateArt _alternateArt;
        AddEditContainerPage _containerPage;
        bool _isEditMode;


        public AddAlternateArtPage()
        {
            InitializeComponent();
            _altArtManager = new AltArtManager();
            _isEditMode = false;
        }

        public AddAlternateArtPage(AlternateArt alternateArt, IAltArtManager altArtManager,
            AddEditContainerPage containerPage)
        {
            InitializeComponent();
            _alternateArt = alternateArt;
            _altArtManager = altArtManager;
            _containerPage = containerPage;
            _isEditMode = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_isEditMode)
            {
                txtAlternateID.Focus();
            }
            else 
            {
                txtDescription.Focus();
                btnClear.Content = "Go Back";
                txtAlternateID.Text = _alternateArt.AlternateArtID;
                txtDescription.Text = _alternateArt.Description;
                txtAlternateID.IsEnabled = false;

                _containerPage.DisplayTabItems(false);
                _containerPage.tabAlternate.IsEnabled = true;
            }
            btnSave.IsDefault = true;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditMode)
            {
                ClearTextAreas();
                txtAlternateID.Focus();

            }
            else
            {
                DisplayListViewPage();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditMode)
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
            string alternateID = txtAlternateID.Text;
            string description = txtDescription.Text;

            if (alternateID.Replace(" ", "") == "" || alternateID == null ||
                alternateID.Length > 50 || alternateID.Any(char.IsDigit))
            {
                MessageBox.Show("The alternate art name entered was invalid.");
                txtAlternateID.SelectAll();
                txtAlternateID.Focus();
                return;
            }
            if (description.Replace(" ", "") == "" || description == null ||
                description.Length > 250)
            {
                MessageBox.Show("The alternate art description entered was invalid.");
                txtDescription.SelectAll();
                txtDescription.Focus();
                return;
            }

            AlternateArt alternateArt = new AlternateArt()
            { 
                AlternateArtID = alternateID,
                Description = description,
            };

            try
            {
                if (_altArtManager.AddAlternateArt(alternateArt))
                {
                    MessageBox.Show("The alternate art " + alternateID + " was successfully created.");
                    ClearTextAreas();
                    txtAlternateID.Focus();
                }
                else
                {
                    MessageBox.Show("The alternate art " + alternateID + " was not created.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void EditModeSaveButton()
        {
            string alternateID = txtAlternateID.Text;
            string description = txtDescription.Text;

            if (description.Replace(" ", "") == "" || description == null ||
                description.Length > 250)
            {
                MessageBox.Show("The alternate art name entered was invalid.");
                txtAlternateID.SelectAll();
                txtAlternateID.Focus();
                return;
            }

            _alternateArt.Description = description;

            try
            {
                if (_altArtManager.EditAlternateArt(_alternateArt))
                {
                    MessageBox.Show("The alternate art " + alternateID + " was successfully updated.");
                    DisplayListViewPage();
                }
                else
                {
                    MessageBox.Show("The alternate art " + alternateID + " was not updated.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextAreas()
        {
            txtAlternateID.Text = "";
            txtDescription.Text = "";
        }

        private void DisplayListViewPage()
        { 
            _containerPage.DisplayTabItems(true);
            _containerPage.IsListView = true;
            _containerPage.frmAlternate.Navigate(new AlternateArtRecordsPage());
        }
    }
}
