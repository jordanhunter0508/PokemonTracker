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
using DataDomain;

namespace PokemonCardFinal
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        UserVM _accessToken;
        public ProfilePage(UserVM accessToken)
        {
            _accessToken = accessToken;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtGivenName.Text = _accessToken.GivenName;
            txtSurname.Text = _accessToken.Surname;
            txtEmail.Text = _accessToken.Email;
            pwdPassword.Password = "***********";
        }

        private void btnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            UpdatePasswordWindow updatePassword = new UpdatePasswordWindow();
            updatePassword.Owner = Window.GetWindow(this);
            updatePassword.ShowDialog();
        }
    }
}
