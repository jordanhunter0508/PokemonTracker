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
        public UserVM AccessToken { get; set; }

        public LogInWindow(IUserManager userManager) 
        {
            _userManager = userManager;
            InitializeComponent();
            
        }

        /// <summary>
        /// When the window loads set the focus to txtEmail
        /// and set btnLogIn to default
        /// </summary>
        private void txtEmail_Loaded(object sender, RoutedEventArgs e)
        {
            txtEmail.Focus();
            btnLogIn.IsDefault = true;
        }

        /// <summary>
        /// Attempts to log in the user based on the content of
        /// txtEmail and pwdPassword. Check if they are empty or null
        /// if so return if not attempt to log in.
        /// </summary>
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = pwdPassword.Password;
            try
            {
                
                if (email == "" || email == null)
                {
                    txtEmail.Focus();
                    statMessage.Content = "Please enter an email.";
                    return;
                }
                if (password == "" || password == null)
                {
                    pwdPassword.Focus();
                    statMessage.Content = "Please enter a password.";
                    return;
                }

                AccessToken = _userManager.LogInUser(email, password);

                // Close window if the log in was succesfull
                if (AccessToken != null)
                {
                    this.DialogResult = false;
                }

                else
                {
                    MessageBox.Show("Log in attempt has failed.\nPlease try again.");
                    txtEmail.Text = "";
                    pwdPassword.Password = "";
                    statMessage.Content = "";
                    txtEmail.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);

            }
        }

        /// <summary>
        /// Closes the window if clicked
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
