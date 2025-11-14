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
    /// Interaction logic for UpdateElementPage.xaml
    /// </summary>
    public partial class ElementRecordsPage : Page
    {
        List<ElementType> _elementTypes;
        IElementManager _elementManager;
        ElementType _selectedElement;
        public ElementRecordsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _elementManager = new ElementManager();
            LoadList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedElement == null)
            {
                return;
            }

            // Pop up window to confirm if the admin wants to delete the record
            MessageBoxResult conformationWindow = MessageBox.Show
            (
                "Are you sure you want to delete " + _selectedElement.ElementTypeID + ".",
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
                if (_elementManager.DeleteElementTypeByElementTypeID(_selectedElement.ElementTypeID))
                {
                    MessageBox.Show("The element was successfully deleted");
                    LoadList();
                }
                else
                {
                    MessageBox.Show("The element could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The element failed to be deleted.");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedElement == null)
            {
                return;
            }

            // Navigate to CreateRecordPage
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                // Navigate the main frame to the new outer page
                AddEditContainerPage containerPage = new AddEditContainerPage();
                mainWindow.frmMain.Navigate(containerPage);

                // When the addRecordPage is loaded change the inner page
                containerPage.Loaded += (s, args) =>
                {
                    containerPage.IsListView = false;
                    containerPage.tabController.SelectedItem = containerPage.tabElement;
                    containerPage.frmElement.Navigate
                    (
                        new AddElementPage(_selectedElement, _elementManager,containerPage)
                    );  
                };
            }
        }

        private void datElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedElement = datElement.SelectedItem as ElementType;
        }

        private void LoadList()
        {
            try
            {
                _elementTypes = _elementManager.FormatElemetTypes(_elementManager.GetElementTypes()).ToList();
                _selectedElement = _elementTypes[0];
                datElement.ItemsSource = _elementTypes;

                datElement.Columns[0].Header = "Element Name";

                datElement.Columns[0].Width = new DataGridLength(100);
                datElement.Columns[1].Width = new DataGridLength(1,DataGridLengthUnitType.Star);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load the list of element types.\n" + ex.Message);
            }
        }
    }
}
