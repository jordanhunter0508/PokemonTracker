using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.VisualBasic;

namespace PokemonCardFinal.View
{
    /// <summary>
    /// Interaction logic for TESTWINDOW.xaml
    /// </summary>
    public partial class TESTWINDOW : Window
    {
        public TESTWINDOW()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TestArtist();
            //TestBooster();
            //TestPokemonRule();
            //TestAbility();
            //TestAlternateArt();

            MoveManager moveManager = new MoveManager();

            //Move move = moveManager.GetMoveByMoveID("shadow bind");
            //MoveVM moveVM = moveManager.GetMoveVMByMoveID("shadow bind");

            //lblName.Content = moveVM.MoveID + ", " + moveVM.Damage + ", " + moveVM.Description + "\n" +
            //moveVM.Costs[0].ElementType + ", " + moveVM.Costs[0].Quantity + "\n" +
            //moveVM.Costs[1].ElementType + ", " + moveVM.Costs[1].Quantity;
            try
            {
                List<MoveVM> moveVMs = moveManager.GetMoveVMs();
                MessageBox.Show(moveVMs.Count.ToString());

                foreach (MoveVM moveVM in moveVMs)
                {
                    if (moveVM.Costs.Count == 0)
                    {
                        lblName.Content += "\n" + moveVM.MoveID + ", " + moveVM.Damage + ", " + moveVM.Description;
                    }
                    else
                    {
                        lblName.Content += "\n" + moveVM.MoveID + ", " + moveVM.Damage + ", " + moveVM.Description + "\n" +
                        moveVM.Costs[0].ElementType + ", " + moveVM.Costs[0].Quantity;
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            //List<Move> moves = moveManager.GetMovesWithoutMoveCost();
            //foreach (Move move in moves)
            //{
            //    lblName.Content += "\n" + move.MoveID + ", " + move.Damage + ", " + move.Description;
            //}

        }

        private void TestAlternateArt()
        {
            IAltArtManager alternateArtManager = new AltArtManager();

            //AlternateArt alternateArt = alternateArtManager.GetAlternateArtByID("jumbo");
            //lblName.Content += alternateArt.AlternateArtID + " " + alternateArt.Description;

            //List<AlternateArt> alternateArts = alternateArtManager.GetAlternateArts();

            //foreach (AlternateArt element in alternateArts)
            //{
            //    lblName.Content += element.AlternateArtID + " " + element.Description + "\n";
            //}


            //AlternateArt alternateArt = new AlternateArt()
            //{
            //    AlternateArtID = "Add Test",
            //    Description = "This is test for the AddRule method."
            //};
            //if (alternateArtManager.AddAlternateArt(alternateArt))
            //{
            //    MessageBox.Show("Success");
            //}
            //else
            //{
            //    MessageBox.Show("Failed");
            //}

            //AlternateArt alternateArt = new AlternateArt()
            //{
            //    AlternateArtID = "Add Test",
            //    Description = "This is test for the editRule method."
            //};
            //if (alternateArtManager.EditAlternateArt(alternateArt))
            //{
            //    MessageBox.Show("Success");
            //}
            //else
            //{
            //    MessageBox.Show("Failed");
            //}

            //AlternateArt alternateArt = new AlternateArt()
            //{
            //    AlternateArtID = "Add Test",
            //    Description = "This is test for the AddRule method."
            //}
            //;
            //if (alternateArtManager.DeleteAlternateArt(alternateArt.AlternateArtID))
            //{
            //    MessageBox.Show("Success");
            //}
            //else
            //{
            //    MessageBox.Show("Failed");
            //}
        }

        private void TestAbility()
        {
            IAbilityManager abilityManager = new AbilityManager();

            //Ability ability = abilityManager.GetAbilityByAbilityID("invisible wall");
            //lblName.Content += ability.AbilityID + " " + ability.AbilityType;

            //List<Ability> abilities = abilityManager.GetAbilities();

            //foreach (Ability element in abilities)
            //{
            //    lblName.Content += element.AbilityID + " " + element.AbilityType + "\n";
            //}

            //List<Ability> abilities = abilityManager.GetAbilityByAbilityType("pokemon power");

            //foreach (Ability element in abilities)
            //{
            //    lblName.Content += element.AbilityID + " " + element.AbilityType + "\n";
            //}

            //Ability ability = new Ability()
            //{
            //    AbilityID = "Add Test",
            //    AbilityType = "Ability Type",
            //    Description = "This is test for the AddRule method."
            //};
            //if (abilityManager.AddAbility(ability))
            //{
            //    MessageBox.Show("Success");
            //}
            //else
            //{
            //    MessageBox.Show("Failed");
            //}

            //Ability ability = new Ability()
            //{
            //    AbilityID = "Add Test",
            //    AbilityType = "Ability new Type",
            //    Description = "This is test for the editRule method."
            //};
            //if (abilityManager.EditAbility(ability))
            //{
            //    MessageBox.Show("Success");
            //}
            //else
            //{
            //    MessageBox.Show("Failed");
            //}

            //Ability ability = new Ability()
            //{
            //    AbilityID = "Add Test",
            //    AbilityType = "Ability Type",
            //    Description = "This is test for the AddRule method."
            //};
            //if (abilityManager.DeleteAbility(ability.AbilityID))
            //{
            //    MessageBox.Show("Success");
            //}
            //else
            //{
            //    MessageBox.Show("Failed");
            //}
        }

        private void TestPokemonRule()
        {
            try
            {
                IRuleManager ruleManager = new RuleManager();

                //PokemonRule ruleByID = ruleManager.GetRuleByRuleID("pokemon-ex");
                //lblName.Content += ruleByID.RuleID + " " + ruleByID.Description;

                //List<PokemonRule> rules = ruleManager.GetRules();

                //foreach (PokemonRule rule in rules)
                //{
                //    lblName.Content += rule.RuleID + " " + rule.Description + "\n";
                //}

                //PokemonRule rule = new PokemonRule()
                //{
                //    RuleID = "Add Test",
                //    Description = "This is test for the AddRule method."
                //};
                //if (ruleManager.AddRule(rule))
                //{
                //    MessageBox.Show("Success");
                //}
                //else
                //{
                //    MessageBox.Show("Failed");
                //}

                //PokemonRule rule = new PokemonRule()
                //{
                //    RuleID = "invalid",
                //    Description = "This is an updated test for the EditRule method."
                //};
                //if (ruleManager.EditRule(rule))
                //{
                //    MessageBox.Show("Success");
                //}
                //else
                //{
                //    MessageBox.Show("Failed");
                //}

                //PokemonRule rule = new PokemonRule()
                //{
                //    RuleID = "Add Test",
                //    Description = "This is test for the AddRule method."
                //};
                //if (ruleManager.DeleteRule(rule.RuleID))
                //{
                //    MessageBox.Show("Success");
                //}
                //else
                //{
                //    MessageBox.Show("Failed");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TestBooster()
        {
            IBoosterManager boosterManager = new BoosterManager();

            //Booster booster = boosterManager.GetBoosterByBoosterID("151");

            //List<Booster> boosters = boosterManager.GetBoosters();

            //if (booster == null)
            //{
            //    MessageBox.Show("Booster is null");
            //    return;
            //}
            //lblName.Content = boosters.Count;

            //Booster newBooster = new Booster()
            //{
            //    BoosterID = "Meaga Evolution",
            //    Series = "Mega Evolution",
            //    ReleaseDate = DateTime.Parse("2025-05-04"),
            //    Abbreviation = "meg"
            //};

            //    if (boosterManager.AddBooster(newBooster))
            //    {
            //        MessageBox.Show("Success");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Failed to insert.");
            //    }


            if (boosterManager.DeleteBooster("Shrouded Fable"))
            {
                MessageBox.Show("success");
            }
            else
            {
                MessageBox.Show("failed");
            }
        }

        private void TestArtist()
        {
            IArtistManager artistManager = new ArtistManager();

            try
            {
                //Artist artist = artistManager.GetArtistByArtistID(56);
                //lblName.Content = artist.GivenName + ", " + artist.Surname;

                //Artist artist = artistManager.GetArtistByName("fujimoto", "gold");
                //lblName.Content = artist.GivenName + ", " + artist.Surname;

                //List<Artist> artists = artistManager.GetArtists();
                //foreach (Artist artist in artists)
                //{
                //    lblName.Content += artist.GivenName + ", " + artist.Surname + "\n";
                //}

                //if (artistManager.AddArtist("test", "person"))
                //{
                //    MessageBox.Show("Artist added.");
                //}
                //else
                //{
                //    MessageBox.Show("Failed to add artist.");
                //}

                if (artistManager.EditArtist(3, "Nothgin", ""))
                {
                    MessageBox.Show("Artist updated.");
                }
                else
                {
                    MessageBox.Show("Failed to upgggggggdate artist.");
                }

                //if (artistManager.DeleteArtistByArtistID(3))
                //{
                //    MessageBox.Show("Artist deleted.");
                //}
                //else
                //{
                //    MessageBox.Show("Failed to deleteggggd artist.");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n");
            }
        }

    }
}
