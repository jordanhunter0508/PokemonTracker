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

namespace PokemonCardFinal.View.AddRecord
{
    /// <summary>
    /// Interaction logic for AddMovePage.xaml
    /// </summary>
    public partial class AddMovePage : Page
    {
        public AddMovePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadElementGrid();
        }

        private void LoadElementGrid()
        {
            IElementManager elementManager = null;
            List<ElementType> elements = null;
            try
            {
                elementManager = new ElementManager();
                elements = elementManager.GetElementTypes();
                List<string> elementTypes = new List<string>();
                foreach (ElementType element in elements)
                {
                    elementTypes.Add(element.ElementTypeID);
                }

                lstElement.ItemsSource = elementTypes;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException);
            }
        }
    }
}
