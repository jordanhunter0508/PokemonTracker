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
        public SignUpWindow()
        {
            _userManager = new UserManager();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnSignUp.IsDefault = true;
            txtGivenName.Focus();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            // need a stored procedure to set the role of the new user to general

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
                bool isRegistered = _userManager.RegisterUserAccount(txtGivenName.Text, txtSurname.Text, txtEmail.Text, pwdPassword.Password);
                if (isRegistered)
                {
                    MessageBox.Show("Account created");
                    AccessToken = _userManager.LogInUser(email, password);
                    // _userManager.AddRole("General" ,accessToke.ID)

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

            if (givenName == "" || givenName == null)
            {
                statMessage.Content = "Please enter a first name.";
                txtGivenName.Focus();
                result = false;
            }
            else if (surname == "" || surname == null)
            {
                statMessage.Content = "Please enter a last name.";
                txtSurname.Focus();
                result = false;
            }
            else if (email == "" || email == null)
            {
                statMessage.Content = "Please enter an email address.";
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
