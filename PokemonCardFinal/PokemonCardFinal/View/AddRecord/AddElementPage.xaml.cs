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
    /// Interaction logic for CreateElementPage.xaml
    /// </summary>
    public partial class AddElementPage : Page
    {
        IElementManager _elementManager;
        ElementType _elementType;
        AddEditContainerPage _containerPage;
        bool _isAddMode;

        public AddElementPage()
        {
            InitializeComponent();
            _elementManager = new ElementManager();
            _isAddMode = true;
        }

        public AddElementPage(ElementType element, IElementManager elementManager, AddEditContainerPage containerPage)
        {
            InitializeComponent();
            _elementType = element;
            _elementManager = elementManager;
            _containerPage = containerPage;
            _isAddMode = false;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (_isAddMode)
            {
                txtElementTypeID.Focus();
            }
            else
            {
                txtDescription.Focus();
                btnClear.Content = "Go Back";
                txtElementTypeID.Text = _elementType.ElementTypeID;
                txtDescription.Text = _elementType.Description;
                txtElementTypeID.IsEnabled = false;

                // Disables all other tab items
                _containerPage.DisplayTabItems(false);
                _containerPage.tabElement.IsEnabled = true;
            }

            btnSave.IsDefault = true;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (_isAddMode)
            {
                ClearTextAreas();
                txtElementTypeID.Focus();

            }
            else
            {
                DisplayListViewPage();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
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
            string elementID = txtElementTypeID.Text;
            string description = txtDescription.Text;
            try
            {
                if (_elementManager.AddElementType(elementID, description))
                {
                    MessageBox.Show("The element " + elementID + " was successfully created.");
                    ClearTextAreas();
                    txtElementTypeID.Focus();
                }
                else
                {
                    MessageBox.Show("The element " + elementID + " was not created.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditModeSaveButton()
        {
            string elementID = txtElementTypeID.Text;
            string description = txtDescription.Text;
            try
            {
                if (_elementManager.EditElementDescritpionByElementTypeID(elementID, description))
                {
                    MessageBox.Show("The element " + elementID + " was successfully updated.");

                    // Brings the user back to the ElementRecordsPage
                    DisplayListViewPage();
                }
                else
                {
                    MessageBox.Show("The element " + elementID + " was not successfully updated.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private bool ValidateInput() 
        {
            bool isValid = true;
            string elementID = txtElementTypeID.Text;
            string description = txtDescription.Text;

            if (elementID.Replace(" ", "") == "" || elementID == null ||
                elementID.Length > 10 || elementID.Any(char.IsDigit))
            {
                MessageBox.Show("The element name entered was invalid.");
                txtElementTypeID.SelectAll();
                txtElementTypeID.Focus();
                isValid = false;
            }

            else if (description.Replace(" ", "") == "" || description == null || description.Length > 100)
            {
                MessageBox.Show("The description entered was invalid.");
                txtDescription.SelectAll();
                txtDescription.Focus();
                isValid = false;
            }

            return isValid;
        }

        private void ClearTextAreas()
        {
            txtElementTypeID.Text = "";
            txtDescription.Text = "";
        }

        private void DisplayListViewPage()
        {
            _containerPage.DisplayTabItems(true);
            _containerPage.IsListView = true;
            _containerPage.frmElement.Navigate(new ElementRecordsPage());
        }
    }
}
