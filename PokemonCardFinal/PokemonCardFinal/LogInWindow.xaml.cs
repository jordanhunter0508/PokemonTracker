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
using LogicLayerInterfaces;

namespace PokemonCardFinal
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        IUserManager _userManager;
        UserVM _accessToken;

        public LogInWindow()
        {
            _userManager = new UserManager();
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = pwdPassword.Password;
            try
            {
                _accessToken = _userManager.LogInUser(email, password);

                if (_accessToken != null)
                {
                    MessageBox.Show("Welcome Back " + _accessToken.GivenName + "!\n" +
                            "You are logged in as " + _accessToken.Roles[0] + " user.");
                }

                else
                {
                    MessageBox.Show("Failed to log in the user.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
