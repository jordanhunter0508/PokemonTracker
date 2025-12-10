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
using LogicLayer;
using LogicLayerInterfaces;
using PokemonCardFinal.View.AddRecord;

namespace PokemonCardFinal.View.ListRecords
{
    /// <summary>
    /// Interaction logic for CardRecordsPage.xaml
    /// </summary>
    public partial class CardRecordsPage : Page
    {
        ICardManager _cardManager;
        CardVM _selectedCard;

        public CardRecordsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _cardManager = new CardManager();
            LoadList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCard == null)
            {
                return;
            }

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            // Navigate the main frame to the new outer page
            AddEditContainerPage containerPage = new AddEditContainerPage();
            mainWindow.frmMain.Navigate(containerPage);

            // When the outer page is loaded change the inner page
            // to AddCardPage
            containerPage.Loaded += (s, args) =>
            {
                containerPage.IsListView = false;
                containerPage.tabController.SelectedItem = containerPage.tabCard;
                containerPage.frmCard.Navigate
                (
                    new AddCardPage(_cardManager,_selectedCard,containerPage)
                );
            };
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCard == null)
            {
                return;
            }

            // Pop up window to confirm if the admin wants to delete the record
            MessageBoxResult conformationWindow = MessageBox.Show
            (
                "Are you sure you want to delete " + _selectedCard.Name + ".",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (conformationWindow != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                if (_cardManager.DeleteCard(_selectedCard.CardID))
                {
                    MessageBox.Show("The card was successfully deleted");
                    LoadList();
                }
                else
                {
                    MessageBox.Show("The card could not be deleted.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void datCard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedCard = datCard.SelectedItem as CardVM;
        }

        private void LoadList()
        {
            try
            {
                datCard.AutoGenerateColumns = false;
                datCard.ItemsSource = _cardManager.GetCardVMs();
                _selectedCard = datCard.SelectedItem as CardVM;

                datCard.Columns[0].Width = new DataGridLength(75);
                datCard.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                datCard.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                datCard.Columns[3].Width = new DataGridLength(100);
                datCard.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
    }
}
