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
        bool _isEditMode;
        public AddElementPage()
        {
            InitializeComponent();
            _elementManager = new ElementManager();
        }

        public AddElementPage(ElementType element, IElementManager elementManager)
        {
            InitializeComponent();
            _elementType = element;
            _elementManager = elementManager;
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
                txtElementTypeID.Text = _elementType.ElementTypeID;
                txtDescription.Text = _elementType.Description;
                txtElementTypeID.IsEnabled = false;
                txtDescription.Focus();
            }

            btnSaveElement.IsDefault = true;
        }

        private void btnClearElement_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditMode)
            {
                ClearTextAreas();

            }
            else
            {
                txtDescription.Text = "";
                txtDescription.Focus();
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

            if (elementId == "" || elementId == null || elementId.Length > 10)
            {
                MessageBox.Show("The element name entered was invalid.");
                txtElementTypeID.Focus();
                return;
            }
            if (description == "" || description == null || description.Length > 100)
            {
                MessageBox.Show("The description entered was invalid.");
                txtDescription.Focus();
                return;
            }

            try
            {
                if (_elementManager.AddElementType(elementId, description))
                {
                    MessageBox.Show("The element " + elementId + " was successfully created.");
                    ClearTextAreas();
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

            if (description == null || description.Length > 100)
            {
                MessageBox.Show("The description entered was invalid");
                return;
            }

            try
            {
                if (_elementManager.EditElementDescritpionByElementTypeID(elementId, description))
                {
                    MessageBox.Show("The element " + elementId + " was successfully updated.");

                    // Brings the user back to the ElementRecordsPage
                    if (Application.Current.MainWindow is MainWindow mainWindow)
                    {
                        AddEditContainerPage editElementPage = new AddEditContainerPage();
                        mainWindow.frmMain.Navigate(editElementPage);
                        editElementPage.Loaded += (s, arg) =>
                        {
                            editElementPage.tabController.SelectedItem = editElementPage.tabElement;
                            editElementPage.frmElement.Navigate(new ElementRecordsPage());
                        };
                    }
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
    }
}
