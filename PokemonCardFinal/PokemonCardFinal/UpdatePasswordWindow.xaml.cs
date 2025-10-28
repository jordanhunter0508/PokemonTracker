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

namespace PokemonCardFinal
{
    /// <summary>
    /// Interaction logic for UpdatePasswordWindow.xaml
    /// </summary>
    public partial class UpdatePasswordWindow : Window
    {
        public UpdatePasswordWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
