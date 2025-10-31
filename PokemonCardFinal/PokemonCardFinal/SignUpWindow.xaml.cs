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

namespace PokemonCardFinal
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

            if (!ValidateInput(givenName, surname, email, password, retypePassword))
            {
                return;
            }

            try
            {
                bool isRegistered = _userManager.CreateUserAccount(txtGivenName.Text, txtSurname.Text, txtEmail.Text, pwdPassword.Password);
                if (isRegistered)
                {
                    AccessToken = _userManager.LogInUser(email, password);
                    if (_userManager.AddUserToRole(AccessToken.UserID))
                    {
                        MessageBox.Show("There was an error");
                    }
                    MessageBox.Show("Account was successfuly created.");
                    this.DialogResult = false;
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

        /// <summary>
        /// Checks if the user input is valid
        /// </summary>
        /// <param name="givenName">input from txtGivenname</param>
        /// <param name="surname">input from txtSurname</param>
        /// <param name="email">input from txtEmail</param>
        /// <param name="password">input from pwdPassword</param>
        /// <param name="retypePassword">input from pwdRetype</param>
        /// <returns>False if any parameters are null or empty, true otherwise</returns>
        private bool ValidateInput(string givenName, string surname, string email,
                string password, string retypePassword)
        {
            bool result = true;

            if (givenName == "" || givenName == null || givenName.Any(char.IsDigit))
            {
                statMessage.Content = "Please enter a valid first name.";
                txtGivenName.Focus();
                result = false;
            }
            else if (surname == "" || surname == null || surname.Any(char.IsDigit))
            {
                statMessage.Content = "Please enter a valid last name.";
                txtSurname.Focus();
                result = false;
            }
            else if (email == "" || email == null || email.Length < 10)
            {
                statMessage.Content = "Please enter a valid email address.";
                txtEmail.Focus();
                result = false;
            }
            else if (password == "" || password == null)
            {
                statMessage.Content = "Please enter a password.";
                pwdPassword.Focus();
                result = false;
            }
            else if (retypePassword == "" || retypePassword == null)
            {
                statMessage.Content = "Please retype the password.";
                pwdRetype.Focus();
                result = false;
            }
            else if (password != retypePassword)
            {
                statMessage.Content = "Passwords do not match please retype them.";
                pwdPassword.Password = "";
                pwdRetype.Password = "";
                pwdPassword.Focus();
                result = false;
            }

            return result;
        }
    }
}
