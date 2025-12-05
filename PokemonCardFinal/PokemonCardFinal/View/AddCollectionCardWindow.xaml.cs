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
    /// Interaction logic for AddCollectionCardWindow.xaml
    /// </summary>
    public partial class AddCollectionCardWindow : Window
    {
        ICollectionManager _collectionManager;
        UserVM _accessToken;
        CardVM _card;
        IEnumerable<string> collectionNames;

        public AddCollectionCardWindow(UserVM accessToken, CardVM card)
        {
            InitializeComponent();
            _accessToken = accessToken;
            _card = card;
            _collectionManager = new CollectionManager();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Creates a list of the collection names
            // just incase the Collections is null
            try
            {
                collectionNames = from collection in _accessToken.Collections
                                  select collection.Name;

                cmbCollection.ItemsSource = collectionNames;
                cmbCollection.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to get collection names.\n\n" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // gets the collection
            Collection collection = _accessToken.Collections.ElementAt(cmbCollection.SelectedIndex);
            int quantity;

            try
            {
                quantity = Convert.ToInt32(txtQuantity.Text);

                if (quantity <= 0)
                {
                    throw new Exception("Quantity must be greater than 0.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                CollectionCard collectionCard = new CollectionCard()
                {
                    Card = _card,
                    CollectionID = collection.CollectionID,
                    Quantity = quantity,
                    Owned = (bool)chkOwned.IsChecked,
                };

                if (_collectionManager.AddCollectionCard(collectionCard))
                {
                    MessageBox.Show("Successfully added " + _card.Name + " into " + collection.Name + ".");
                    this.DialogResult = false;
                }
                else
                {
                    MessageBox.Show("Failed to add " + _card.Name + " into " + collection.Name + ".");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
