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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Azure.Core;
using DataDomain;
using LogicLayerInterfaces;

namespace PokemonCardFinal.View.Profile
{
    /// <summary>
    /// Interaction logic for AccountInfoPage.xaml
    /// </summary>
    public partial class AccountInfoPage : Page
    {
        IUserManager _userManager;
        UserVM _accessToken;

        public AccountInfoPage(IUserManager userManager, UserVM accessToken)
        {
            InitializeComponent();
            _userManager = userManager;
            _accessToken = accessToken;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtGivenName.Text = _accessToken.GivenName;
            txtSurname.Text = _accessToken.Surname;
            txtEmail.Text = _accessToken.Email;
            pwdPassword.Password = "***********";
        }

        public void btnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            UpdatePasswordWindow updatePassword = new UpdatePasswordWindow(_accessToken, _userManager);
            updatePassword.Owner = Window.GetWindow(this);
            updatePassword.ShowDialog();
        }
    }
}
