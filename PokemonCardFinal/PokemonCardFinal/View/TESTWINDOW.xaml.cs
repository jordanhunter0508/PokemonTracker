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


            //Dictionary<int, Card> cards = cardManager.GetCards();
            //datGrid.ItemsSource = cards.Values.ToList();

            //Dictionary<int, List<MoveVM>> moves = cardManager.GetCardMoves();
            //datGrid.ItemsSource = moves.Values.AsEnumerable();

            //Dictionary<int, List<string>> altArts = cardManager.GetCardAlternateArts();
            //datGrid.ItemsSource = altArts.Values.AsEnumerable();
            Dictionary<int, List<string>> cards = cardManager.GetCardAlternateArtsByCardName("r");
            List<string> cardList = new List<string>();

            //foreach (var card in cards)
            //{
            //    cardList.Add(cards.Value);
            //}

            datGrid.ItemsSource = cardManager.GetCardVMsByCardName("r");
        }

    }
}
