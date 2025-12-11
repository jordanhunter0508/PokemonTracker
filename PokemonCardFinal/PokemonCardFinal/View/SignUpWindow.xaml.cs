using System;
using System.CodeDom;
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

namespace PokemonCardFinal.View
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        IUserManager _userManager;
        public UserVM AccessToken { get; set; }
        public SignUpWindow(IUserManager userManager)
        {
            _userManager = userManager;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnSignUp.IsDefault = true;
            txtGivenName.Focus();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string givenName = txtGivenName.Text;
            string surname = txtSurname.Text;
            string email = txtEmail.Text;
            string password = pwdPassword.Password;
            string retypePassword = pwdRetype.Password;

            if (givenName == "" || givenName == null || givenName.Any(char.IsDigit))
            {
                statMessage.Content = "Please enter a valid first name.";
                txtGivenName.Focus();
                return;
            }
            else if (surname == "" || surname == null || surname.Any(char.IsDigit))
            {
                statMessage.Content = "Please enter a valid last name.";
                txtSurname.Focus();
                return;
            }
            else if (email == "" || email == null || email.Length < 10)
            {
                statMessage.Content = "Please enter a valid email address.";
                txtEmail.Focus();
                return;
            }
            else if (password == "" || password == null)
            {
                statMessage.Content = "Please enter a password.";
                pwdPassword.Focus();
                return;
            }
            else if (retypePassword == "" || retypePassword == null)
            {
                statMessage.Content = "Please retype the password.";
                pwdRetype.Focus();
                return;
            }
            else if (password != retypePassword)
            {
                statMessage.Content = "Passwords do not match please retype them.";
                pwdPassword.Password = "";
                pwdRetype.Password = "";
                pwdPassword.Focus();
                return;
            }

            try
            {
                bool isRegistered = _userManager.CreateUserAccount(txtGivenName.Text, txtSurname.Text, txtEmail.Text, pwdPassword.Password);
                if (isRegistered)
                {
                    AccessToken = _userManager.LogInUser(email, password);
                    if (_userManager.AddUserToRole(AccessToken.UserID) &&
                        _userManager.AddDefaultUserCollections(AccessToken.UserID))
                    {
                        MessageBox.Show("Account was successfuly created.");
                        AccessToken.Collections = _userManager.GetCollectionsByUserID(AccessToken.UserID);
                        AccessToken.Roles = _userManager.GetRolesForUser(AccessToken.Email);
                        this.DialogResult = false;
                    }
                    else 
                    {
                        MessageBox.Show("There was an error creating your account.");
                    }
                }
                else
                {
                    statMessage.Content = "Invalid email address.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
