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

namespace PokemonCardFinal.View.Profile
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfileContainerPage : Page
    {
        IUserManager _userManager;
        ICollectionManager _collectionManager;
        UserVM _accessToken;

        // Used to specify which page is loaded
        bool _isUserCardPage;
        bool _isUserDeckPage;
        bool _isUserFavoritePage;
        bool _isUserWishlistPage;
        bool _isAccountInfoPage;

        public ProfileContainerPage(UserVM accessToken, IUserManager userManager)
        {
            InitializeComponent();
            _userManager = userManager;
            _accessToken = accessToken;
            _collectionManager = new CollectionManager();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetAllLoaded(false);
        }

        public void tabController_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (tabController.SelectedItem == null)
            {
                return;
            }

            try
            {
                int collectionID = 0;

                if (tabController.SelectedItem == tabUserCard && !_isUserCardPage)
                {
                    SetAllLoaded(false);
                    _isUserCardPage = true;
                    collectionID = _collectionManager.GetCollectionIDByCollectionType(_accessToken, "user");
                    frmUserCard.Navigate(new UserCardsPage(_userManager, _accessToken, collectionID));
                }
                else if (tabController.SelectedItem == tabUserDeck && !_isUserDeckPage)
                {
                    SetAllLoaded(false);
                    _isUserDeckPage = true;
                    frmUserDeck.Navigate(new UserDeckPage());
                }
                else if (tabController.SelectedItem == tabFavorite && !_isUserFavoritePage)
                {
                    SetAllLoaded(false);
                    _isUserFavoritePage = true;
                    collectionID = _collectionManager.GetCollectionIDByCollectionType(_accessToken, "favorite");
                    frmFavorite.Navigate(new UserCardsPage(_userManager, _accessToken, collectionID));
                }
                else if (tabController.SelectedItem == tabWishlist && !_isUserWishlistPage)
                {
                    SetAllLoaded(false);
                    _isUserWishlistPage = true;
                    collectionID = _collectionManager.GetCollectionIDByCollectionType(_accessToken, "wishlist");
                    frmWishlist.Navigate(new UserCardsPage(_userManager, _accessToken, collectionID));
                }
                else if (tabController.SelectedItem == tabAccount && !_isAccountInfoPage)
                {
                    SetAllLoaded(false);
                    _isAccountInfoPage = true;
                    frmAccount.Navigate(new AccountInfoPage(_userManager, _accessToken));
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void SetAllLoaded(bool option = false)
        {
            _isUserCardPage = option;
            _isUserDeckPage = option;
            _isUserFavoritePage = option;
            _isUserWishlistPage = option;
            _isAccountInfoPage = option;
        }
    }
}
