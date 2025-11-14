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

namespace PokemonCardFinal.View.ListRecords
{
    /// <summary>
    /// Interaction logic for MoveRecordsPage.xaml
    /// </summary>
    public partial class MoveRecordsPage : Page
    {
        List<MoveVM> _moveVMs;
        IMoveManager _moveManager;
        MoveVM _selectedMoveVM;

        public MoveRecordsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _moveManager = new MoveManager();
            LoadList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void datMove_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedMoveVM = datMove.SelectedItem as MoveVM;
        }

        private void LoadList() 
        {
            try
            {
                _moveVMs = _moveManager.GetMoveVMs();
                _selectedMoveVM = _moveVMs[0];
                datMove.AutoGenerateColumns = false;
                datMove.ItemsSource = _moveVMs;

                datMove.Columns[0].Width = new DataGridLength(125);
                datMove.Columns[1].Width = new DataGridLength(75);
                datMove.Columns[2].Width = new DataGridLength(140);
                datMove.Columns[3].Width = new DataGridLength(75);
                datMove.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load list of moves.\n" + ex.Message);
            }
        }
    }
}
