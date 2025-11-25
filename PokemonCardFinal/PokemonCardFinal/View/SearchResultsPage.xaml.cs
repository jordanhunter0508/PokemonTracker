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

namespace PokemonCardFinal.View
{
    /// <summary>
    /// Interaction logic for SearchResultsPage.xaml
    /// </summary>
    public partial class SearchResultsPage : Page
    {
        ICardManager _cardManager;
        string _search;

        public SearchResultsPage(string search)
        {
            InitializeComponent();
            _cardManager = new CardManager();
            _search = search;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadRarityComboBox();
            LoadBoosterComboBox();
            //LoadCardTypeComboBox();
            //LoadElementTypeComboBox();
        }

        private void LoadRarityComboBox() 
        {
            string[] rarities = { "Common", "Gallery", "Illustration Rare", "Rare", "Secret Rare", "Ultra Rare", "Uncommon" };

            try
            {
                cmbRarity.Items.Clear();
                cmbRarity.ItemsSource = rarities;

                cmbRarity.Items.Add("Rarity");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to load rarities." + "\n\n" + ex.Message);
            }
        }

        private void LoadBoosterComboBox() 
        {
            IBoosterManager boosterManager = new BoosterManager();

            try
            {
                cmbBooster.Items.Clear();
                List<Booster> boosters = boosterManager.GetBoosters();
                cmbBooster.ItemsSource = boosters;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to load booster packs." + "\n\n" + ex.Message);
            }
        }

        private void LoadCardTypeComboBox() 
        {
            // Could make a table for card type table
            string[] cardTypes = { "Item", "Pokemon", "Stage", "Trainer" };

            try
            {
                cmbCardType.ItemsSource = cardTypes;
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
                cmbElementType.ItemsSource = elementManager.GetElementTypes();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to load element types." + "\n\n" + ex.Message);
            }
        }
    }
}
