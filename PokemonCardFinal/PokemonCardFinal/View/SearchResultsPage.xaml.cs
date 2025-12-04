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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PokemonCardFinal.View
{

    /// <summary>
    /// Interaction logic for SearchResultsPage.xaml
    /// </summary>
    public partial class SearchResultsPage : Page
    {
        ICardManager _cardManager;
        List<CardVM> _cards;
        List<CardVM> _filteredCards;
        string _search;
        bool _isSearchMode;

        public SearchResultsPage()
        {
            InitializeComponent();
            _cardManager = new CardManager();
            _isSearchMode = false;
        }

        public SearchResultsPage(string search)
        {
            InitializeComponent();
            _cardManager = new CardManager();
            _search = search;
            _isSearchMode = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Load the filter combo boxes
            LoadBoosterComboBox();
            LoadRarityComboBox();
            LoadCardTypeComboBox();
            LoadElementTypeComboBox();

            LoadList();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _filteredCards = ApplyFilters();
                UpdateList(_filteredCards);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private List<CardVM> ApplyFilters()
        {
            _filteredCards = _cards;

            if (0 < cmbBooster.SelectedIndex)
            {
                string booster = cmbBooster.SelectedItem.ToString();
                _filteredCards = _cardManager.GetCardVMsByBoosterID(_filteredCards, booster).ToList();
            }

            if (0 < cmbRarity.SelectedIndex)
            {
                string rarity = cmbRarity.SelectedItem.ToString();
                _filteredCards = _cardManager.GetCardVMsByRarity(_filteredCards, rarity).ToList();
            }

            if (0 < cmbCardType.SelectedIndex)
            {
                string cardType = cmbCardType.SelectedItem.ToString();
                _filteredCards = _cardManager.GetCardVMsByCardType(_filteredCards, cardType).ToList();
            }

            if (0 < cmbElementType.SelectedIndex)
            {
                string element = cmbElementType.SelectedItem.ToString();
                _filteredCards = _cardManager.GetCardVMsByElementTypeID(_filteredCards, element).ToList();
            }


            return _filteredCards;
        }

        private void LoadList()
        {
            try
            {
                if (_isSearchMode)
                {
                    _cards = _cardManager.GetCardVMsByCardName(_search);
                }
                else
                {
                    _cards = _cardManager.GetCardVMs();
                }
                UpdateList(_cards);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void UpdateList(List<CardVM> cards)
        {
            datSearch.ItemsSource = cards;

            datSearch.Columns[0].Width = new DataGridLength(65);
            datSearch.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            datSearch.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            datSearch.Columns[3].Width = new DataGridLength(100);
            datSearch.Columns[4].Width = new DataGridLength(65);
            datSearch.Columns[5].Width = new DataGridLength(100);
            datSearch.Columns[6].Width = new DataGridLength(25);
        }

        private void LoadBoosterComboBox()
        {
            IBoosterManager boosterManager = new BoosterManager();
            try
            {
                List<string> boosterIDs = boosterManager.GetBoosterIDs();
                boosterIDs.Insert(0, "Booster Set");
                cmbBooster.ItemsSource = boosterIDs;
                cmbBooster.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to load booster packs." + "\n\n" + ex.Message);
            }
        }

        private void LoadRarityComboBox()
        {
            string[] rarities = { "Rarity", "Common", "Full Art", "Gallery", "Illustration Rare", "Rare", "Secret Rare", "Ultra Rare", "Uncommon" };

            try
            {
                cmbRarity.ItemsSource = rarities;
                cmbRarity.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to load rarities." + "\n\n" + ex.Message);
            }
        }

        private void LoadCardTypeComboBox()
        {
            // Could make a table for card type table
            string[] cardTypes = { "Card Type", "Item", "Pokemon", "Stage", "Trainer" };

            try
            {
                cmbCardType.ItemsSource = cardTypes;
                cmbCardType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to load card types." + "\n\n" + ex.Message);
            }
        }

        private void LoadElementTypeComboBox()
        {
            IElementManager elementManager = new ElementManager();
            try
            {
                List<string> elementTypeIDs = elementManager.GetElementTypeIDs();
                elementTypeIDs.Insert(0, "Element Type");
                cmbElementType.ItemsSource = elementTypeIDs;
                cmbElementType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to load element types." + "\n\n" + ex.Message);
            }
        }

        private void btnCollection_Click(object sender, RoutedEventArgs e)
        {
            // give user a prompt to add a card to a collection
        }

        private void datSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datSearch.SelectedItem == null)
            {
                return;
            }

            // load the detailed page
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            CardVM selectedCard = datSearch.SelectedItem as CardVM;
            mainWindow.frmMain.Navigate(new DetailedCardPage(selectedCard));
        }

    }
}
