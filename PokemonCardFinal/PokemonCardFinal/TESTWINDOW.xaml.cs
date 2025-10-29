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
using System.Windows.Shapes;
using DataDomain;
using LogicLayer;

namespace PokemonCardFinal
{
    /// <summary>
    /// Interaction logic for TESTWINDOW.xaml
    /// </summary>
    public partial class TESTWINDOW : Window
    {
        public TESTWINDOW()
        {
            InitializeComponent();
            string input = "shadow bind";
            MoveManager moveManager = new MoveManager();
            Move move = moveManager.GetMoveByMoveID(input);
            if (move != null)
            {
                lblName.Content = move.MoveID + " does " + move.Damage + ": " + move.Description;
            }
            else { lblName.Content = "There is no moved named " + input; }
        }
    }
}
