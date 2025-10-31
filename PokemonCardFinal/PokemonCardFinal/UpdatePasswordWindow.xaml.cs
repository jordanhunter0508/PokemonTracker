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
    /// Interaction logic for UpdatePasswordWindow.xaml
    /// </summary>
    public partial class UpdatePasswordWindow : Window
    {
        IUserManager _userManager;
        UserVM _accessToken;
        public UpdatePasswordWindow(UserVM accessToken,IUserManager userManager)
        {
            _userManager = userManager;
            _accessToken = accessToken;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.IsDefault = true;
            txtEmail.Text = _accessToken.Email;
            txtEmail.IsEnabled = false;
            pwdCurrentPassword.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string currentPassword = pwdCurrentPassword.Password;
            string newPassword = pwdNewPassword.Password;
            string retypePassword = pwdRetypePassword.Password;

            if (email == "" || email == null) 
            {
                txtEmail.Focus();
                statMessage.Content = "Please enter an email address.";
                return;
            }
            if (currentPassword == "" || currentPassword == null)
            { 
                pwdCurrentPassword.Focus();
                statMessage.Content = "Please enter your current password.";
                return;
            }
            if (newPassword == "" || newPassword == null)
            {
                pwdNewPassword.Focus();
                statMessage.Content = "Please enter a new password.";
                return;
            }
            if (retypePassword == "" || retypePassword == null)
            {
                pwdRetypePassword.Focus();
                statMessage.Content = "Please retype your ne password password.";
                return;
            }
            if (newPassword != retypePassword)
            {
                pwdNewPassword.Focus();
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                statMessage.Content = "Passwords do not match please retype them.";
                return;
            }

            try
            {
                if (_userManager.ResetPassword(email, currentPassword, newPassword))
                {
                    MessageBox.Show("Password Updated.");
                    this.DialogResult = false;
                }
                else 
                {
                    statMessage.Content = "Could not update password. Please reenter your passwords.";
                    pwdCurrentPassword.Password = "";
                    pwdNewPassword.Password = "";
                    pwdRetypePassword.Password = "";
                    pwdCurrentPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Failed." + "\n\n" + ex.InnerException.Message);
            }
        }

    }
}
