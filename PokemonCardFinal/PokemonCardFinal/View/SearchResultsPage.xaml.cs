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
        ISearchManager _searchManager;
        IFilterCardManager _filterCardManager;
        List<Card> _cards;
        List<Card> _filteredCards;
        UserVM _accessToken;
        string _search;
        bool _isSearchMode;

        public SearchResultsPage()
        {
            InitializeComponent();
            _cardManager = new CardManager();
            _searchManager = new SearchManager();
            _filterCardManager = new FilterCardManager();
            _isSearchMode = false;
        }

        public SearchResultsPage(string search)
        {
            InitializeComponent();
            _cardManager = new CardManager();
            _searchManager = new SearchManager();
            _filterCardManager = new FilterCardManager();
            _search = search;
            _isSearchMode = true;
        }

        public SearchResultsPage(UserVM accessToken)
        {
            InitializeComponent();
            _cardManager = new CardManager();
            _searchManager = new SearchManager();
            _filterCardManager = new FilterCardManager();
            _accessToken = accessToken;
            _isSearchMode = false;
        }

        public SearchResultsPage(string search, UserVM accessToken)
        {
            InitializeComponent();
            _cardManager = new CardManager();
            _searchManager = new SearchManager();
            _filterCardManager = new FilterCardManager();
            _search = search;
            _accessToken = accessToken;
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

            if (_accessToken == null)
            {
                grdButton.Visibility = Visibility.Collapsed;
            }
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

        private List<Card> ApplyFilters()
        {
            _filteredCards = _cards;

            if (0 < cmbBooster.SelectedIndex)
            {
                string booster = cmbBooster.SelectedItem.ToString();
                _filteredCards = _filterCardManager.FilterByBoosterID(_filteredCards, booster).ToList();
            }

            if (0 < cmbRarity.SelectedIndex)
            {
                string rarity = cmbRarity.SelectedItem.ToString();
                _filteredCards = _filterCardManager.FilterByRarity(_filteredCards, rarity).ToList();
            }

            if (0 < cmbCardType.SelectedIndex)
            {
                string cardType = cmbCardType.SelectedItem.ToString();
                _filteredCards = _filterCardManager.FilterByCardType(_filteredCards, cardType).ToList();
            }

            if (0 < cmbElementType.SelectedIndex)
            {
                string element = cmbElementType.SelectedItem.ToString();
                _filteredCards = _filterCardManager.FilterByElementTypeID(_filteredCards, element).ToList();
            }


            return _filteredCards;
        }

        private void LoadList()
        {
            try
            {
                if (_isSearchMode)
                {
                    _cards = _searchManager.SearchCardsByName(_search);
                }
                else
                {
                    _cards = _cardManager.GetAllCards();
                }
                UpdateList(_cards);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
            }
        }

        private void UpdateList(List<Card> cards)
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
            if (datSearch.SelectedItem == null) 
            {
                return;
            }

            if (_accessToken != null)
            {
                CardVM selectedCard = datSearch.SelectedItem as CardVM;
                AddCollectionCardWindow collectionWindow = new AddCollectionCardWindow(_accessToken, selectedCard);
                collectionWindow.Owner = Window.GetWindow(this);
                collectionWindow.ShowDialog();
            }
        }

        private void datSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datSearch.SelectedItem == null)
            {
                return;
            }

            // load the detailed page
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            Card selectedCard = datSearch.SelectedItem as Card;
            mainWindow.frmMain.Navigate(new DetailedCardPage(selectedCard.CardID, _accessToken));
        }

    }
}
