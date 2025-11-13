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
using LogicLayerInterfaces;

namespace PokemonCardFinal.View.AddRecord
{
    /// <summary>
    /// Interaction logic for AddBoosterPage.xaml
    /// </summary>
    public partial class AddBoosterPage : Page
    {
        public AddBoosterPage()
        {
            InitializeComponent();
        }

        public AddBoosterPage(Booster booster, IBoosterManager boosterManager, 
            AddEditContainerPage containerPage)
        {
            InitializeComponent();
        }
    }
}
