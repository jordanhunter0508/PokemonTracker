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
            try
            {
                if (_userManager.ResetPassword(email, currentPassword, newPassword))
                {
                    MessageBox.Show("Password Update.");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Failed" + "\n\n" + ex.InnerException.Message);
            }
        }
    }
}
