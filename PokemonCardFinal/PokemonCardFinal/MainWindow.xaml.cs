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
using PokemonCardFinal.AddView;
using PokemonCardFinal.ViewRecords;

namespace PokemonCardFinal
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

        // Clears the search box when the user enters a value a character
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Equals("Search..."))
            {
                txtSearch.Text = string.Empty;
            }
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
                frmMain.Navigate(new ProfilePage(_accessToken,_userManager));
            }
        }

        private void mnuCreateRecord_Click(object sender, RoutedEventArgs e)
        {
            // Open the create record page
            frmMain.Navigate(new AddRecordPage());
        }

        private void mnuEditRecord_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new ViewRecordsPage());
        }
    }
}