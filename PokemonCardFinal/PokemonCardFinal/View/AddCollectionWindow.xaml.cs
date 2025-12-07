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

namespace PokemonCardFinal.View
{
    /// <summary>
    /// Interaction logic for AddCollectionWindow.xaml
    /// </summary>
    public partial class AddCollectionWindow : Window
    {
        ICollectionManager _collectionManager;
        IUserManager _userManager;
        UserVM _accessToken;
        public AddCollectionWindow(UserVM accessToken)
        {
            InitializeComponent();
            _collectionManager = new CollectionManager();
            _userManager = new UserManager();
            _accessToken = accessToken;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.IsDefault = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            string name = txtName.Text;
            string desciption = txtDescription.Text;

            try
            {
                Collection collection = new Collection()
                {
                    CollectionID = 1,
                    UserID = _accessToken.UserID,
                    CollectionTypeID = "Deck",
                    Name = name,
                    Description = desciption,
                };

                if (_collectionManager.AddCollection(collection))
                {
                    MessageBox.Show("The collection was created successfully.");
                    _accessToken.Collections = _userManager.GetCollectionsByUserID(_accessToken.UserID);
                    this.DialogResult = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private bool ValidateInput()
        {
            bool isValid = true;

            string name = txtName.Text;
            string desciption = txtDescription.Text;

            if (name == "" || name == null || name.Length > 25)
            {
                MessageBox.Show("The deck name entered was invalid.");
                txtName.Focus();
                isValid = false;
            }
            if (desciption == "" || desciption == null || desciption.Length > 150)
            {
                MessageBox.Show("The description entered was invalid.");
                txtDescription.Focus();
                isValid = false;
            }

            return isValid;
        }
    }
}


// need to reget the list of collecions for user before closing window