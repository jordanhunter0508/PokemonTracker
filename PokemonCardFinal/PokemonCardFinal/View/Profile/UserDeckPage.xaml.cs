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

namespace PokemonCardFinal.View.Profile
{
    /// <summary>
    /// Interaction logic for UserDeckPage.xaml
    /// </summary>
    public partial class UserDeckPage : Page
    {
        ICollectionManager _collectionManager;
        UserVM _accessToken;

        public UserDeckPage(ICollectionManager collectionManager, UserVM accessToken)
        {
            InitializeComponent();
            _collectionManager = collectionManager;
            _accessToken = accessToken;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }

        private void LoadDataGrid() 
        {
            //datDeck.ItemsSource
        }
    }
}
