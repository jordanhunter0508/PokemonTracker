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


            //Card card = cardManager.GetCardByCardID(1);
            //if (card != null)
            //{
            //    lblName.Content = card.CardID + ", " + card.Name + ", " + card.BoosterID + ", " + card.BoosterNumber;
            //}
            //else
            //{
            //    MessageBox.Show("failed");
            //}


            //List<MoveVM> moveVMs = cardManager.GetMovesByCardID(1);
            //if (moveVMs != null)
            //{
            //    lblName.Content = moveVMs[0].MoveID + ", " + moveVMs[0].Damage + ", " + moveVMs[0].Description + "\n " + 
            //        moveVMs[0].Costs.Count +", " + moveVMs[0].TotalCost + ", " + moveVMs[0].ElementTypes + "\n\n\n";

            //    lblName.Content += moveVMs[1].MoveID + ", " + moveVMs[1].Damage + ", " + moveVMs[1].Description + "\n " + 
            //        moveVMs[1].Costs.Count +", " + moveVMs[1].TotalCost + ", " + moveVMs[1].ElementTypes;
            //}
            //else
            //{
            //    MessageBox.Show("failed");
            //}


            List<string> altArts = cardManager.GetAlternateArtsByCardID(1);
            lblName.Content += altArts[0] + "\n";
            lblName.Content += altArts[1] + "\n";
        }

    }
}
