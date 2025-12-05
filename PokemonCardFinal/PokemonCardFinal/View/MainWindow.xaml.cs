using System.Text;
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
using PokemonCardFinal.View.AddRecord;
using PokemonCardFinal.View.ListRecords;
using PokemonCardFinal.View.Profile;

namespace PokemonCardFinal.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUserManager _userManager;
        UserVM _accessToken;

        public MainWindow()
        {
            _userManager = new UserManager();
            InitializeComponent();
        }

        private void winHome_Loaded(object sender, RoutedEventArgs e)
        {
            LoggedOutView();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogIn.Content == "Log In")
            {
                LogInWindow logInWindow = new LogInWindow(_userManager);
                logInWindow.Owner = this;
                logInWindow.ShowDialog();
                UserLoggedIn(logInWindow.AccessToken);
            }
            else if (btnLogIn.Content == "Log Out")
            {
                UserLoggedOut();
            }

        }

        /// <summary>
        /// Sets this classes accessToken
        /// from the one in logInWindow
        /// </summary>
        private void UserLoggedIn(UserVM accessToken)
        {
            _accessToken = accessToken;

            if (_accessToken == null)
            {
                LoggedOutView();
                return;
            }
            else
            {
                LoggedInView();
            }
        }

        /// <summary>
        /// Updates the display of the main window 
        /// to match what a user should see when logging in
        /// </summary>
        private void LoggedInView()
        {

            btnLogIn.Content = "Log Out";
            btnSignUp.Content = "View Profile";

            if (_accessToken.Roles.Contains("Admin"))
            {
                mnuCreateRecord.Visibility = Visibility.Visible;
                mnuEditRecord.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Changes the access token to null
        /// to log the user out
        /// </summary>
        private void UserLoggedOut()
        {
            if (_accessToken != null)
            {
                _accessToken = null;
                LoggedOutView();
            }
        }

        /// <summary>
        /// Updates the disaply of the main window
        /// to match what a user should see when logged out
        /// </summary>
        private void LoggedOutView() 
        {
            btnLogIn.Content = "Log In";
            btnSignUp.Content = "Sign Up";
            mnuCreateRecord.Visibility = Visibility.Collapsed;
            mnuEditRecord.Visibility = Visibility.Collapsed;
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (btnSignUp.Content == "Sign Up")
            {
                SignUpWindow signUpWindow = new SignUpWindow(_userManager);
                signUpWindow.Owner = this;
                signUpWindow.ShowDialog();
                UserLoggedIn(signUpWindow.AccessToken);

            }
            if (btnSignUp.Content == "View Profile")
            {
                frmMain.Navigate(new ProfileContainerPage(_accessToken,_userManager));
            }
        }

        private void mnuCreateRecord_Click(object sender, RoutedEventArgs e)
        {
            AddEditContainerPage addEditContainer = new AddEditContainerPage();
            addEditContainer.Loaded += (s, args) =>
            {
                addEditContainer.IsListView = false;
                addEditContainer.frmAbility.Navigate(new AddAbilityPage());
            };
            frmMain.Navigate(addEditContainer);
        }

        private void mnuEditRecord_Click(object sender, RoutedEventArgs e)
        {
            AddEditContainerPage addEditContainer = new AddEditContainerPage();
            addEditContainer.Loaded += (s, args) =>
            {
                addEditContainer.IsListView = true;
                addEditContainer.frmAbility.Navigate(new AbilityRecordsPage());
            };
            frmMain.Navigate(addEditContainer);
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            //txtSearch.SelectAll();
            if (txtSearch.Text == "Search...")
            {
                txtSearch.Text = "";
            }
            btnSearch.IsDefault = true;
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            btnSearch.IsDefault = false;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtSearch.Text;

            if (search.Replace(" ", "") == "" || search == null
                || search == "Search...")
            {
                return;
            }

            if (_accessToken != null)
            {
                frmMain.Navigate(new SearchResultsPage(search, _accessToken));
            }
            else
            {
                frmMain.Navigate(new SearchResultsPage(search));
            }

        }

        private void mnuAllCards_Click(object sender, RoutedEventArgs e)
        {
            if (_accessToken != null)
            {
                frmMain.Navigate(new SearchResultsPage(_accessToken));
            }
            else
            {
                frmMain.Navigate(new SearchResultsPage());
            }
        }
    }
}