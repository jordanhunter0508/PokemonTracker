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
using PokemonCardFinal.UpdateView;

namespace PokemonCardFinal
{
    /// <summary>
    /// Interaction logic for CreateRecordPage.xaml
    /// </summary>
    public partial class CreateRecordPage : Page
    {
        bool _isElementLoaded;
        public CreateRecordPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _isElementLoaded = false;
        }

        private void tabController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabController.SelectedItem == null)
            {
                return;
            }
            else if (!_isElementLoaded && tabController.SelectedItem == tabElement)
            {
                _isElementLoaded = true;
                frmElement.Navigate(new CreateElementPage());
            }
            else { }
        }
    }
}
