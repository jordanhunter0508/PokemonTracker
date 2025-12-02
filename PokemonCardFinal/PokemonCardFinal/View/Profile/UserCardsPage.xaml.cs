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
using Azure.Core;
using DataDomain;
using LogicLayerInterfaces;

namespace PokemonCardFinal.View.Profile
{
    /// <summary>
    /// Interaction logic for UserCardsPage.xaml
    /// </summary>
    public partial class UserCardsPage : Page
    {
        ICollectionManager _collectionManager;
        CollectionVM _collectionVM;
        int _collectionID;
        public UserCardsPage(ICollectionManager collectionManager, int collectionID)
        {
            InitializeComponent();
            _collectionManager = collectionManager;
            _collectionID = collectionID;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _collectionVM = _collectionManager.GetCollectionVMByCollectionID(_collectionID);

                List<CollectionCardVM> collectionCardVM = _collectionManager.ConvertCollectionCardToVM(_collectionVM.Cards);

                if (collectionCardVM == null || collectionCardVM.Count == 0)
                {
                    datCard.Visibility = Visibility.Collapsed;
                    grdEmpty.Visibility = Visibility.Visible;
                }

                datCard.AutoGenerateColumns = false;
                datCard.ItemsSource = collectionCardVM;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
