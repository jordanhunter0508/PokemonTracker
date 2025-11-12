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
        bool _isEditMode;
        public AddElementPage()
        {
            InitializeComponent();
            _elementManager = new ElementManager();
        }

        public AddElementPage(ElementType element, IElementManager elementManager, AddEditContainerPage containerPage)
        {
            InitializeComponent();
            _elementType = element;
            _elementManager = elementManager;
            _containerPage = containerPage;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (_elementType == null)
            {
                _isEditMode = false;
                txtElementTypeID.Focus();
            }
            else
            {
                _isEditMode = true;
                btnClearElement.Content = "Go Back";
                txtElementTypeID.Text = _elementType.ElementTypeID;
                txtDescription.Text = _elementType.Description;
                txtElementTypeID.IsEnabled = false;
                txtDescription.Focus();

                // Disables all other tab items
                DisplayTabItems(false);
                _containerPage.tabElement.IsEnabled = true;
            }

            btnSaveElement.IsDefault = true;
        }

        private void btnClearElement_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditMode)
            {
                ClearTextAreas();
                txtElementTypeID.Focus();

            }
            else
            {
                LoadListViewPage();
            }
        }

        private void btnSaveElement_Click(object sender, RoutedEventArgs e)
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
            string elementId = txtElementTypeID.Text;
            string description = txtDescription.Text;

            if (elementId.Replace(" ", "") == "" || elementId == null ||
                elementId.Length > 10 || elementId.Any(char.IsDigit))
            {
                MessageBox.Show("The element name entered was invalid.");
                txtElementTypeID.SelectAll();
                txtElementTypeID.Focus();
                return;
            }
            if (description.Replace(" ","") == "" || description == null || description.Length > 100)
            {
                MessageBox.Show("The description entered was invalid.");
                txtDescription.SelectAll();
                txtDescription.Focus();
                return;
            }

            try
            {
                if (_elementManager.AddElementType(elementId, description))
                {
                    MessageBox.Show("The element " + elementId + " was successfully created.");
                    ClearTextAreas();
                    txtElementTypeID.Focus();
                }
                else
                {
                    MessageBox.Show("The element " + elementId + " was not created.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditModeSaveButton()
        {
            string elementId = txtElementTypeID.Text;
            string description = txtDescription.Text;

            if (description.Replace(" ", "") == "" || description == null || description.Length > 100)
            {
                MessageBox.Show("The description entered was invalid.");
                txtDescription.SelectAll();
                txtDescription.Focus();
                return;
            }

            try
            {
                if (_elementManager.EditElementDescritpionByElementTypeID(elementId, description))
                {
                    MessageBox.Show("The element " + elementId + " was successfully updated.");

                    // Brings the user back to the ElementRecordsPage
                    LoadListViewPage();
                }
                else
                {
                    MessageBox.Show("The element " + elementId + " was not successfully updated.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextAreas()
        {
            txtElementTypeID.Text = "";
            txtDescription.Text = "";
        }

        private void LoadListViewPage()
        {
            DisplayTabItems(true);
            _containerPage.IsListView = true;
            _containerPage.frmElement.Navigate(new ElementRecordsPage());
        }

        private void DisplayTabItems(bool option) 
        {
            foreach (TabItem tabItem in _containerPage.tabController.Items)
            { 
                tabItem.IsEnabled = option;
            }
        }
    }
}
