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
            InitializeComponent();
            CardManager cardManager = new CardManager();

            Card card = new Card()
            {
                CardID = 31,
                ArtistID = 2,
                AbilityID = "none",
                BoosterID = "destined rivals",
                PokemonRuleID = "none",
                ElementTypeID = "fire",
                Name = "test edit",
                BoosterNumber = 32,
                CardType = "Pokemon",
                Rarity = "common",
                WeaknessType = "water",
                ResistanceType = "none",
                WeaknessValue = 2,
                ResistanceValue = 0,
                RetreatCost = 1,
                Health = 70,
                Stage = "Basic"
            };

            cardManager.EditCard(card);           
        }

    }
}
