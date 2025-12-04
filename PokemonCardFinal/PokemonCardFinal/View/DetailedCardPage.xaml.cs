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
using PokemonCardFinal.View.Profile;

namespace PokemonCardFinal.View
{
    /// <summary>
    /// Interaction logic for DetailedCardPage.xaml
    /// </summary>
    public partial class DetailedCardPage : Page
    {
        public bool IsCollectionView = false;

        CardVM _card;

        public DetailedCardPage(CardVM card)
        {
            InitializeComponent();
            _card = card;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Used just incase a blank card is inserted
            if (_card == null || _card.Name == null)
            {
                MessageBox.Show("Failed to load card details.");
                // Hide all
                return;
            }

            if (IsCollectionView)
            { 
                btnAddCard.Visibility = Visibility.Collapsed;
            }

            DisplayCardInfo();
            DisplayCardStats();
            DisplayAltArt();
            DisplayMove();
            DisplayAbility();
        }

        private void DisplayCardInfo()
        {
            IBoosterManager boosterManager = new BoosterManager();
            try
            {
                string series = boosterManager.GetBoosterByBoosterID(_card.BoosterID).Series;
                Artist artist = new ArtistManager().GetArtistByArtistID(_card.ArtistID);

                lblCardName.Content = _card.Name;
                lblArtist.Content = "Artist: " + artist.GivenName + ", " + artist.Surname;
                lblRarity.Content = "Rarity: " + _card.Rarity;
                lblBooster.Content = series + ": " + _card.BoosterID + ", " + _card.BoosterNumber;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to get booster series.\n\n" + ex.Message);
            }            
        }

        private void DisplayCardStats()
        {
            if (_card.CardType.ToLower() != "pokemon")
            {
                // hide all card related
                grdCardStats.Visibility = Visibility.Collapsed;
                Grid.SetRow(grdAltArt, 1);
                return;
            }

            string resistanceType = char.ToUpper(_card.ResistanceType[0]) + _card.ResistanceType.Substring(1);
            string weaknessType = char.ToUpper(_card.WeaknessType[0]) + _card.WeaknessType.Substring(1);

            lblHealth.Content = "Health: " + _card.Health;
            lblStage.Content = "Stage: " + _card.Stage;
            lblResistance.Content = "Resistance: " + resistanceType;
            lblWeakness.Content = "Weakness: " + weaknessType;
            lblRetreatCost.Content = "Retreat Cost: " + _card.RetreatCost;

            if (_card.WeaknessType.ToLower() != "none")
            {

                lblWeakness.Content += " x" + _card.WeaknessValue;
            }

            if (_card.ResistanceType.ToLower() != "none")
            {

                lblResistance.Content += " -" + _card.ResistanceValue;
            }
        }

        private void DisplayAltArt()
        {
            try
            {
                lstAltArt.ItemsSource = _card.AlternateArts;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to load a list of alternate arts.\n\n" + ex.Message);
            }
        }
        
        private void DisplayMove()
        {
            if (_card.Moves.Count == 0)
            {
                // hide both
                grdMove.Visibility = Visibility.Collapsed;
                Grid.SetRow(grdAbility, 0);
            }
            else if (_card.Moves.Count == 1)
            {
                lblSecondMove.Visibility = Visibility.Collapsed;
                txtSecondMove.Visibility = Visibility.Collapsed;

                MoveVM move = _card.Moves[0];
                lblFirstMove.Content = move.Name+ ", " + move.TotalCost + " (" + move.ElementTypes + ")";
                txtFirstMove.Text = "Description: " + move.Description;
            }
            else
            {
                MoveVM move = _card.Moves[0];
                lblFirstMove.Content = move.Name + ", " + move.TotalCost + " (" + move.ElementTypes + ")";
                txtFirstMove.Text = "Description: " + move.Description;

                move = _card.Moves[1];
                lblSecondMove.Content = move.Name + ", " + move.TotalCost + " (" + move.ElementTypes + ")";
                txtSecondMove.Text = "Description: " + move.Description;
            }
        }

        private void DisplayAbility()
        {
            if (_card.AbilityID == "none")
            { 
                grdAbility.Visibility = Visibility.Collapsed;
                return;
            }

            IAbilityManager abilityManager = new AbilityManager();

            try
            {
                string description = abilityManager.GetAbilityByAbilityID(_card.AbilityID).Description;
                lblAbility.Content = _card.AbilityID;
                txtAbility.Text = description;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to get ability description.\n\n" + ex.Message);
            } 
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.frmMain.GoBack();
        }

        private void btnAddCard_Click(object sender, RoutedEventArgs e)
        {
            // save the card to user cards
        }
    }
}
