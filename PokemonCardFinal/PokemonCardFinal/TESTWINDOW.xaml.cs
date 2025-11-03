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
using System.Windows.Shapes;
using DataDomain;
using LogicLayer;

namespace PokemonCardFinal
{
    /// <summary>
    /// Interaction logic for TESTWINDOW.xaml
    /// </summary>
    public partial class TESTWINDOW : Window
    {
        public TESTWINDOW()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TestElementType();
        }

        private void TestElementType()
        {
            ElementType elmType = null;


            try
            {
                ElementManager manager = new ElementManager();
                elmType = manager.GetElementTypeByElementTypeID("fire");
                List<ElementType> elmList = manager.GetElementTypes();

                foreach (ElementType element in elmList)
                {
                    lblName.Content += "\nElementTypeID: " + element.ElementTypeID;
                }

                if (elmType != null)
                {
                    //lblName.Content = elmType.ElementTypeID + elmType.Description;
                }
                else
                {
                    MessageBox.Show("Object is nul;");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
