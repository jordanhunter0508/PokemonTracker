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

namespace PokemonCardFinal.CreateView
{
    /// <summary>
    /// Interaction logic for CreateElementPage.xaml
    /// </summary>
    public partial class CreateElementPage : Page
    {
        IElementManager _elementManager;
        ElementType _elementType;
        bool _isEditMode;
        public CreateElementPage()
        {
            _elementManager = new ElementManager();
            InitializeComponent();
        }

        public CreateElementPage(ElementType element, IElementManager elementManager)
        {
            _elementManager = elementManager;
            _elementType = element;
            InitializeComponent();
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
                txtElementTypeID.IsEnabled = false;
                txtDescription.Focus();
            }

            btnSaveElement.IsDefault = true;
        }

        private void btnClearElement_Click(object sender, RoutedEventArgs e)
        {
            ClearTextAreas();
        }

        private void btnSaveElement_Click(object sender, RoutedEventArgs e)
        {
            if (_isEditMode) 
            {
                MessageBox.Show("Edit Mode");
            }
            else
            {
                CreateModeSaveButton();
            }
        }

        private void CreateModeSaveButton() 
        {
            string elementId = txtElementTypeID.Text;
            string description = txtDescription.Text;

            try
            {
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

        private void ClearTextAreas()
        {
            txtElementTypeID.Text = "";
            txtDescription.Text = "";
        }
    }
}
