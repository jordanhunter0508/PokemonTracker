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
using Microsoft.IdentityModel.Tokens;
using PokemonCardFinal.View.ListRecords;

namespace PokemonCardFinal.View.AddRecord
{
    /// <summary>
    /// Interaction logic for AddCardPage.xaml
    /// </summary>
    public partial class AddCardPage : Page
    {
        ICardManager _cardManager;
        CardVM _cardVM;
        AddEditContainerPage _containerPage;
        bool _isAddMode;

        public AddCardPage()
        {
            InitializeComponent();
            _cardManager = new CardManager();
            _cardVM = new CardVM();
            _isAddMode = true;
        }

        public AddCardPage(ICardManager cardManager, CardVM cardVM, AddEditContainerPage containerPage)
        {
            InitializeComponent();
            _cardManager = cardManager;
            _cardVM = cardVM;
            _containerPage = containerPage;
            _isAddMode = false;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBoxes();
            LoadListBox();

            if (_isAddMode)
            {
                ClearTextAreas();
            }
            else
            {
                DisplayCardVM();
                btnClear.Content = "Go Back";
                txtName.IsEnabled = false;

                // Disables all other tab items
                _containerPage.DisplayTabItems(false);
                _containerPage.tabCard.IsEnabled = true;
            }

            txtName.Focus();
            btnSave.IsDefault = true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateTextBoxes())
            {
                return;
            }

            if (_isAddMode)
            {
                CreateModeSaveButton();

            }
            else
            {
                EditModeSaveButton();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (_isAddMode)
            {
                ClearTextAreas();
                txtName.Focus();

            }
            else
            {
                DisplayListViewPage();
            }
        }

        private void LoadComboBoxes()
        {
            string[] cardTypes = { "Card Type", "Item", "Pokemon", "Stage", "Trainer" };
            string[] rarities = { "Rarity", "Common", "Full Art", "Gallery", "Illustration Rare", "Rare", "Secret Rare", "Ultra Rare", "Uncommon" };
            string[] stages = { "Stage", "Basic", "Stage 1", "Stage 2", "Mega", "VMAX", "VSTAR" };

            try
            {
                List<string> elements = new ElementManager().GetElementTypeIDs();

                // Sets all the combo box item sources
                cmbArtistID.ItemsSource = new ArtistManager()
                                            .GetArtists()
                                            .Prepend(new Artist
                                            {
                                                ArtistID = 0,
                                                GivenName = "Artists",
                                                Surname = ""
                                            });

                cmbAbility.ItemsSource = new AbilityManager()
                                            .GetActiveAbilities()
                                            .Prepend(new Ability()
                                            {
                                                AbilityID = "Abilities",
                                            });

                cmbBoosterID.ItemsSource = new BoosterManager()
                                            .GetBoosterIDs()
                                            .Prepend("Booster Set");


                cmbRule.ItemsSource = new RuleManager()
                                            .GetRules()
                                            .Prepend(new PokemonRule()
                                            {
                                                RuleID = "Rule"
                                            });

                cmbCardType.ItemsSource = cardTypes;
                cmbRarity.ItemsSource = rarities;
                cmbStage.ItemsSource = stages;

                cmbElementType.ItemsSource = elements.Prepend("Element Type").ToList();
                cmbWeaknessType.ItemsSource = elements.Prepend("Weakness Type").ToList();
                cmbResistanceType.ItemsSource = elements.Prepend("Resistance Type").ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load combo boxes.\n\n" + ex.Message);
            }
        }

        private void LoadListBox()
        {
            try
            {
                lstMove.ItemsSource = new MoveManager().GetMoveVMs().OrderBy(move => move.Name);
                lstAltArt.ItemsSource = new AltArtManager().GetAlternateArts();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to load list boxes.\n\n" + ex.Message);
            }
        }

        private void ClearTextAreas()
        {
            cmbArtistID.SelectedIndex = 0;
            cmbAbility.SelectedIndex = 0;
            cmbBoosterID.SelectedIndex = 0;
            cmbRule.SelectedIndex = 0;
            cmbCardType.SelectedIndex = 0;
            cmbRarity.SelectedIndex = 0;
            cmbStage.SelectedIndex = 0;
            cmbElementType.SelectedIndex = 0;
            cmbWeaknessType.SelectedIndex = 0;
            cmbResistanceType.SelectedIndex = 0;

            txtBoosterNumber.Text = "";
            txtHealth.Text = "";
            txtName.Text = "";
            txtResistanceValue.Text = "";
            txtRetreatCost.Text = "";
            txtWeaknessValue.Text = "";

            lstMove.UnselectAll();
            lstAltArt.UnselectAll();
        }

        private void DisplayListViewPage()
        {
            _containerPage.DisplayTabItems(true);
            _containerPage.IsListView = true;
            _containerPage.frmCard.Navigate(new CardRecordsPage());
        }

        private bool ValidateTextBoxes()
        {
            bool isValid = true;
            int value = 0;

            if (txtName.Text.IsNullOrEmpty() || txtName.Text.Length > 50)
            {
                txtName.Focus();
                txtName.SelectAll();
                isValid = false;
                MessageBox.Show("Invalid Card Name.");
            }
            else if (txtBoosterNumber.Text.IsNullOrEmpty() || !int.TryParse(txtBoosterNumber.Text, out value))
            {
                txtBoosterNumber.Focus();
                txtBoosterNumber.SelectAll();
                isValid = false;
                MessageBox.Show("Invalid Booster Number.");
            }
            else if (txtWeaknessValue.Text.IsNullOrEmpty() || !int.TryParse(txtWeaknessValue.Text, out value))
            {
                txtWeaknessValue.Focus();
                txtWeaknessValue.SelectAll();
                isValid = false;
                MessageBox.Show("Invalid Weakness Value.");
            }
            else if (txtResistanceValue.Text.IsNullOrEmpty() || !int.TryParse(txtResistanceValue.Text, out value))
            {
                txtResistanceValue.Focus();
                txtResistanceValue.SelectAll();
                isValid = false;
                MessageBox.Show("Invalid Resistance Value.");
            }
            else if (txtHealth.Text.IsNullOrEmpty() || !int.TryParse(txtHealth.Text, out value))
            {
                txtHealth.Focus();
                txtHealth.SelectAll();
                isValid = false;
                MessageBox.Show("Invalid Health Value.");
            }
            else if (txtRetreatCost.Text.IsNullOrEmpty() || !int.TryParse(txtRetreatCost.Text, out value))
            {
                txtRetreatCost.Focus();
                txtRetreatCost.SelectAll();
                isValid = false;
                MessageBox.Show("Invalid Retreat Cost.");
            }
            else if (cmbArtistID.SelectedIndex < 1)
            {
                isValid = false;
                MessageBox.Show("Please select an artist.");
            }
            else if (cmbRarity.SelectedIndex < 1)
            {
                isValid = false;
                MessageBox.Show("Please select a rarity.");
            }
            else if (cmbElementType.SelectedIndex < 1)
            {
                isValid = false;
                MessageBox.Show("Please select an element type.");
            }
            else if (cmbCardType.SelectedIndex < 1)
            {
                isValid = false;
                MessageBox.Show("Please select a card type.");
            }
            else if (cmbBoosterID.SelectedIndex < 1)
            {
                isValid = false;
                MessageBox.Show("Please select a booster set.");
            }
            else if (cmbStage.SelectedIndex < 1)
            {
                isValid = false;
                MessageBox.Show("Please select a stage.");
            }

            return isValid;
        }

        private void CreateModeSaveButton()
        {
            try
            {
                string name = txtName.Text;
                BuildCardVM();

                if (_cardManager.AddCardVM(_cardVM))
                {
                    MessageBox.Show("The card " + name + " was successfully created.");
                    ClearTextAreas();
                    txtName.Focus();
                }
                else
                {
                    MessageBox.Show("The card " + name + " was not created.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditModeSaveButton()
        {
            try
            {
                BuildCardVM();
                if (_cardManager.EditCardVM(_cardVM))
                {
                    MessageBox.Show("The card " + _cardVM.Name + " was successfully updated.");
                    DisplayListViewPage();
                }
                else
                {
                    MessageBox.Show("The card " + _cardVM.Name + " was not successfully updated.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BuildCardVM()
        {
            try
            {
                // Bas Card Section
                _cardVM.ArtistID = ((Artist)cmbArtistID.SelectedItem).ArtistID;
                _cardVM.BoosterID = (string)cmbBoosterID.SelectedItem;
                _cardVM.ElementTypeID = (string)cmbElementType.SelectedItem;
                _cardVM.Name = txtName.Text;
                _cardVM.BoosterNumber = Convert.ToInt32(txtBoosterNumber.Text);
                _cardVM.CardType = (string)cmbCardType.SelectedItem;
                _cardVM.Rarity = (string)cmbRarity.SelectedItem;
                _cardVM.RetreatCost = Convert.ToInt32(txtRetreatCost.Text);
                _cardVM.Health = Convert.ToInt32(txtHealth.Text);
                _cardVM.Stage = (string)cmbStage.SelectedItem;

                // If unselected assumes the card doesn't have an ability
                if (cmbAbility.SelectedIndex == 0)
                {
                    _cardVM.AbilityID = "none";
                }
                else
                {
                    _cardVM.AbilityID = ((Ability)cmbAbility.SelectedItem).AbilityID;
                }

                // If unselected assumes the card doesn't have a pokemon rule
                if (cmbRule.SelectedIndex == 0)
                {
                    _cardVM.PokemonRuleID = "none";
                }
                else
                {
                    _cardVM.PokemonRuleID = ((PokemonRule)cmbRule.SelectedItem).RuleID;
                }

                // If unselected assumes the card doesn't have a weakness type
                if (cmbWeaknessType.SelectedIndex == 0)
                {
                    _cardVM.WeaknessType = "none";
                    _cardVM.WeaknessValue = 0;
                }
                else
                {
                    _cardVM.WeaknessType = (string)cmbWeaknessType.SelectedItem;
                    _cardVM.WeaknessValue = Convert.ToInt32(txtWeaknessValue.Text);
                }

                // If unselected assumes the card doesn't have a resistance type
                if (cmbResistanceType.SelectedIndex == 0)
                {
                    _cardVM.ResistanceType = "none";
                    _cardVM.ResistanceValue = 0;
                }
                else
                {
                    _cardVM.ResistanceType = (string)cmbResistanceType.SelectedItem;
                    _cardVM.ResistanceValue = Convert.ToInt32(txtResistanceValue.Text);
                }

                // VM Section
                _cardVM.Moves = lstMove.SelectedItems.Cast<MoveVM>().ToList();

                if (lstAltArt.SelectedItems.Count == 0)
                {
                    _cardVM.AlternateArts = new List<string>() { "none" };
                }
                else
                {
                    _cardVM.AlternateArts = lstAltArt.SelectedItems.Cast<AlternateArt>().Select(altArt => altArt.AlternateArtID).ToList();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to build the move.\n\n" + ex.Message);
            }
        }

        private void DisplayCardVM()
        {
            // .Items get the full list of Items in the combo box
            // .OfType<T>() gets only the T elements (needed for the next step)
            // .FirstOrDefault gets the first instance where the condition matches
            // Returns a type T object

            cmbArtistID.SelectedItem = cmbArtistID.Items
                                        .OfType<Artist>()
                                        .FirstOrDefault(a => a.ArtistID == _cardVM.ArtistID);

            


            if (_cardVM.PokemonRuleID.ToLower() == "none")
            {
                cmbRule.SelectedIndex = 0;
            }
            else
            {
                cmbRule.SelectedItem = cmbRule.Items
                                        .OfType<PokemonRule>()
                                        .FirstOrDefault(r => r.RuleID == _cardVM.PokemonRuleID);
            }

            if (_cardVM.AbilityID.ToLower() == "none")
            {
                cmbAbility.SelectedIndex = 0;
            }
            else
            {
                cmbAbility.SelectedItem = cmbAbility.Items
                                        .OfType<Ability>()
                                        .FirstOrDefault(a => a.AbilityID == _cardVM.AbilityID);
            }

            if (_cardVM.WeaknessType.ToLower() == "none")
            {
                cmbWeaknessType.SelectedIndex = 0;
            }
            else
            {
                cmbWeaknessType.SelectedItem = cmbWeaknessType.Items
                    .OfType<string>()
                    .FirstOrDefault(x => x.ToLower() == _cardVM.WeaknessType.ToLower());
            }

            if (_cardVM.ResistanceType.ToLower() == "none")
            {
                cmbResistanceType.SelectedIndex = 0;
            }
            else
            {
                cmbResistanceType.SelectedItem = cmbResistanceType.Items
                                        .OfType<string>()
                                        .FirstOrDefault(x => x.ToLower() == _cardVM.ResistanceType.ToLower());
            }




            cmbElementType.SelectedItem = cmbElementType.Items
                                        .OfType<string>()
                                        .FirstOrDefault(x => x.ToLower() == _cardVM.ElementTypeID.ToLower());


            cmbBoosterID.SelectedItem = _cardVM.BoosterID;
            cmbCardType.SelectedItem = _cardVM.CardType;
            cmbRarity.SelectedItem = _cardVM.Rarity;
            cmbStage.SelectedItem = _cardVM.Stage;

            

            // TextBoxes
            txtBoosterNumber.Text = _cardVM.BoosterNumber.ToString();
            txtHealth.Text = _cardVM.Health.ToString();
            txtName.Text = _cardVM.Name;
            txtResistanceValue.Text = _cardVM.ResistanceValue.ToString();
            txtRetreatCost.Text = _cardVM.RetreatCost.ToString();
            txtWeaknessValue.Text = _cardVM.WeaknessValue.ToString();

            // Same as ArtistID comboBox but needs a loop because
            // Card.AlternateArts is a List
            foreach (MoveVM move in _cardVM.Moves)
            {
                Move match = lstMove.Items.OfType<MoveVM>()
                    .FirstOrDefault(m => m.MoveID == move.MoveID);

                if (match != null)
                {
                    lstMove.SelectedItems.Add(match);
                }
            }

            foreach (string altArtIDs in _cardVM.AlternateArts)
            {
                AlternateArt match = lstAltArt.Items.OfType<AlternateArt>()
                    .FirstOrDefault(a => a.AlternateArtID == altArtIDs);

                if (match != null)
                {
                    lstAltArt.SelectedItems.Add(match);
                }
            }
        }
    }
}
