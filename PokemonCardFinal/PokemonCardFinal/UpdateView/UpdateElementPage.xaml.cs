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
using PokemonCardFinal.CreateView;

namespace PokemonCardFinal.UpdateView
{
    /// <summary>
    /// Interaction logic for UpdateElementPage.xaml
    /// </summary>
    public partial class UpdateElementPage : Page
    {
        ElementType[] _elementTypes;
        IElementManager _elementManager;
        ElementType selectedElement;
        public UpdateElementPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _elementManager = new ElementManager();

            try
            {
                _elementTypes = _elementManager.FormatElemetTypes(_elementManager.GetElementTypes()).ToArray();
                datElement.ItemsSource = _elementTypes;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedElement == null)
            {
                return;
            }
            // Pop up window to confirm if the admin wants to delete the record
            MessageBoxResult conformationWindow = MessageBox.Show
            (
                "Are you sure you want to delete " + selectedElement.ElementTypeID,
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (conformationWindow == MessageBoxResult.Yes)
            {
                try
                {
                    if (_elementManager.DeleteElementType(selectedElement.ElementTypeID))
                    {
                        MessageBox.Show("The element was successfully deleted");
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
            else 
            {
                return;
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (selectedElement == null)
            {
                return;
            }

            this.NavigationService?.Navigate(new CreateElementPage(selectedElement, _elementManager));
        }

        private void datElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedElement = datElement.SelectedItem as ElementType;
        }
    }
}
