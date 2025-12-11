using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;
using PokemonCardFinal.View.ListRecords;

namespace PokemonCardFinal.View.AddRecord
{
    /// <summary>
    /// Interaction logic for AddMovePage.xaml
    /// </summary>
    public partial class AddMovePage : Page
    {
        IMoveManager _moveManager;
        MoveVM _moveVM;
        AddEditContainerPage _containerPage;
        bool _isAddMode;

        public AddMovePage()
        {
            InitializeComponent();
            _moveManager = new MoveManager();
            _moveVM = new MoveVM();
            _isAddMode = true;
        }

        public AddMovePage(IMoveManager moveManager, MoveVM move,
            AddEditContainerPage containerPage)
        {
            InitializeComponent();
            _moveManager = moveManager;
            _moveVM = move;
            _containerPage = containerPage;
            _isAddMode = false;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadElementGrid();
            if (!_isAddMode)
            {
                txtName.IsEnabled = false;
                txtName.Text = _moveVM.Name;
                txtDamage.Text = _moveVM.Damage.ToString();
                txtDescription.Focus();
                txtDescription.Text = _moveVM.Description;
                DisplayCmbElement();
                btnClear.Content = "Go Back";

                // Disables all other tab items
                _containerPage.DisplayTabItems(false);
                _containerPage.tabMove.IsEnabled = true;
            }

            txtName.Focus();
            btnSave.IsDefault = true;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
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

        private void cmbElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbElement1.SelectedIndex > 0)
            {
                txtElement1.IsEnabled = true;
            }
            else
            {
                txtElement1.IsEnabled = false;
            }

            if (cmbElement2.SelectedIndex > 0)
            {
                txtElement2.IsEnabled = true;
            }
            else
            {
                txtElement2.IsEnabled = false;
            }

            if (cmbElement3.SelectedIndex > 0)
            {
                txtElement3.IsEnabled = true;
            }
            else
            {
                txtElement3.IsEnabled = false;
            }

        }

        private void EditModeSaveButton()
        {
            try
            {
                string name = txtName.Text;
                BuildMoveVM();

                if (_moveManager.EditMoveVM(_moveVM))
                {
                    MessageBox.Show("The move " + name + " was successfully updated.");
                    DisplayListViewPage();
                }
                else
                {
                    MessageBox.Show("The move " + name + " was not updated.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateModeSaveButton()
        {
            try
            {
                string name = txtName.Text;
                BuildMoveVM();

                if (_moveManager.AddMoveVM(_moveVM))
                {
                    MessageBox.Show("The move " + name + " was successfully created.");
                    ClearTextAreas();
                    txtName.Focus();
                }
                else
                {
                    MessageBox.Show("The move " + name + " was not created.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadElementGrid()
        {
            IElementManager elementManager = null;
            List<ElementType> elements = null;
            try
            {
                elementManager = new ElementManager();
                elements = elementManager.GetElementTypes();

                List<string> elementTypes = (from element in elements
                                             select element.ElementTypeID.ToLower()).ToList();

                elementTypes.Insert(0, "Element Type");
                cmbElement1.ItemsSource = elementTypes;
                cmbElement1.SelectedIndex = 0;
                cmbElement2.ItemsSource = elementTypes;
                cmbElement2.SelectedIndex = 0;
                cmbElement3.ItemsSource = elementTypes;
                cmbElement3.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException);
            }
        }

        public bool ValidateInput()
        {
            bool isValid = true;
            string name = txtName.Text;
            string description = txtDescription.Text;
            int damage;
            int quantity;

            if (name.Replace(" ", "") == "" || name == null ||
                name.Length > 30 || name.Any(char.IsDigit))
            {
                MessageBox.Show("The element name entered was invalid.");
                txtName.SelectAll();
                txtName.Focus();
                isValid = false;
            }
            else if (description.Replace(" ", "") == "" || description == null || description.Length > 200)
            {
                MessageBox.Show("The description entered was invalid.");
                txtDescription.SelectAll();
                txtDescription.Focus();
                isValid = false;
            }
            else if (!int.TryParse(txtDamage.Text, out damage))
            {
                MessageBox.Show("Please enter a valid number for Damage.");
                txtDamage.SelectAll();
                txtDamage.Focus();
                isValid = false;
            }
            else if (txtElement1.IsEnabled && !int.TryParse(txtElement1.Text, out quantity))
            {
                MessageBox.Show("Quantity must be a whole number.");
                txtElement1.SelectAll();
                txtElement1.Focus();
                isValid = false;
            }
            else if (txtElement2.IsEnabled && !int.TryParse(txtElement2.Text, out quantity))
            {
                MessageBox.Show("Quantity must be a whole number.");
                txtElement2.SelectAll();
                txtElement2.Focus();
                isValid = false;
            }
            else if (txtElement3.IsEnabled && !int.TryParse(txtElement3.Text, out quantity))
            {
                MessageBox.Show("Quantity must be a whole number.");
                txtElement3.SelectAll();
                txtElement3.Focus();
                isValid = false;
            }
            // if two combo boxs are the same and not element 0 (the placeholder)
            // then they must be the same element
            else if ((cmbElement1.SelectedIndex == cmbElement2.SelectedIndex && cmbElement1.SelectedIndex != 0) ||
                (cmbElement1.SelectedIndex == cmbElement3.SelectedIndex && cmbElement1.SelectedIndex != 0) ||
                (cmbElement2.SelectedIndex == cmbElement3.SelectedIndex && cmbElement2.SelectedIndex != 0))
            {
                MessageBox.Show("Please make sure you haven't selected duplicate elements.");
                isValid = false;
            }

                return isValid;
        }

        private void ClearTextAreas()
        {
            txtName.Text = "";
            txtDamage.Text = "";
            txtDescription.Text = "";

            txtElement1.Text = "";
            txtElement2.Text = "";
            txtElement3.Text = "";

            cmbElement1.SelectedIndex = 0;
            cmbElement2.SelectedIndex = 0;
            cmbElement3.SelectedIndex = 0;
        }

        private void DisplayListViewPage()
        {
            _containerPage.DisplayTabItems(true);
            _containerPage.IsListView = true;
            _containerPage.frmMove.Navigate(new MoveRecordsPage());
        }

        private void BuildMoveVM()
        {
            string name = txtName.Text;
            string description = txtDescription.Text;
            int damage = Convert.ToInt32(txtDamage.Text);

            _moveVM.Name = name;
            _moveVM.Damage = damage;
            _moveVM.Description = description;
            _moveVM.Costs = CreateMoveCost();
        }

        private List<MoveCost> CreateMoveCost()
        {
            List<MoveCost> results = new List<MoveCost>();

            if (txtElement1.IsEnabled)
            {
                results.Add(new MoveCost()
                {
                    MoveID = 1,
                    ElementType = cmbElement1.SelectedItem as string,
                    Quantity = Convert.ToInt32(txtElement1.Text)
                });
            }
            if (txtElement2.IsEnabled)
            {
                results.Add(new MoveCost()
                {
                    MoveID = 1,
                    ElementType = cmbElement2.SelectedItem as string,
                    Quantity = Convert.ToInt32(txtElement2.Text)
                });
            }
            if (txtElement3.IsEnabled)
            {
                results.Add(new MoveCost()
                {
                    MoveID = 1,
                    ElementType = cmbElement3.SelectedItem as string,
                    Quantity = Convert.ToInt32(txtElement3.Text)
                });
            }
            return results;
        }

        private void DisplayCmbElement()
        {
            if (_moveVM.Costs.Count >= 1)
            {
                txtElement1.Text = _moveVM.Costs[0].Quantity.ToString();
                txtElement1.IsEnabled = true;
                cmbElement1.SelectedValue = _moveVM.Costs[0].ElementType.ToString().ToLower();
            }
            if (_moveVM.Costs.Count >= 2)
            {
                txtElement2.Text = _moveVM.Costs[1].Quantity.ToString();
                txtElement2.IsEnabled = true;
                cmbElement2.SelectedValue = _moveVM.Costs[1].ElementType.ToString().ToLower();
            }
            if (_moveVM.Costs.Count >= 3)
            {
                txtElement3.Text = _moveVM.Costs[2].Quantity.ToString();
                txtElement3.IsEnabled = true;
                cmbElement3.SelectedValue = _moveVM.Costs[2].ElementType.ToString().ToLower();
            }
        }
    }
}
