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
    /// Interaction logic for AddAbilityPage.xaml
    /// </summary>
    public partial class AddAbilityPage : Page
    {
        IAbilityManager _abilityManager;
        Ability _ability;
        bool _isEditMode;

        public AddAbilityPage()
        {
            InitializeComponent();
            _abilityManager = new AbilityManager();
        }

        public AddAbilityPage(Ability ability, IAbilityManager abilityManager)
        {
            InitializeComponent();
            _ability = ability;
            _abilityManager = abilityManager;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ability == null)
            {
                _isEditMode = false;
            }
            else 
            {
                _isEditMode = true;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
