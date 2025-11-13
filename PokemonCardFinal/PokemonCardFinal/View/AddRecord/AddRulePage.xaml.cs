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
    /// Interaction logic for AddRulePage.xaml
    /// </summary>
    public partial class AddRulePage : Page
    {
        IRuleManager _ruleManager;
        PokemonRule _rule;
        AddEditContainerPage _containerPage;
        bool _isAddMode;

        public AddRulePage()
        {
            InitializeComponent();
            _isAddMode = true;
        }

        public AddRulePage(PokemonRule rule, IRuleManager ruleManager, 
            AddEditContainerPage containerPage)
        {
            InitializeComponent();
            _rule = rule;
            _ruleManager = ruleManager;
            _containerPage = containerPage;
            _isAddMode = false;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
