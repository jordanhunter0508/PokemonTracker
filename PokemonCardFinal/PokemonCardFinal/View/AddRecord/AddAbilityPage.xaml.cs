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
using PokemonCardFinal.View.ListRecords;

namespace PokemonCardFinal.View.AddRecord
{
    /// <summary>
    /// Interaction logic for AddAbilityPage.xaml
    /// </summary>
    public partial class AddAbilityPage : Page
    {
        IAbilityManager _abilityManager;
        Ability _ability;
        AddEditContainerPage _containerPage;
        bool _isAddMode;

        public AddAbilityPage()
        {
            InitializeComponent();
            _abilityManager = new AbilityManager();
            _isAddMode = true;
        }

        public AddAbilityPage(Ability ability, IAbilityManager abilityManager, AddEditContainerPage containerPage)
        {
            InitializeComponent();
            _ability = ability;
            _abilityManager = abilityManager;
            _containerPage = containerPage;
            _isAddMode = false;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isAddMode)
            {
                txtAbilityName.Focus();
            }
            else 
            {
                btnClear.Content = "Go Back";

                txtAbilityName.Text = _ability.AbilityID;
                txtAbilityType.Text = _ability.AbilityType;
                txtDescription.Text = _ability.Description;

                txtAbilityName.IsEnabled = false;
                txtAbilityType.Focus();

                // Disables all other tab items
                _containerPage.DisplayTabItems(false);
                _containerPage.tabAbility.IsEnabled = true;
            }

            btnSave.IsDefault = true;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (_isAddMode)
            {
                ClearTextAreas();
                txtAbilityName.Focus();

            }
            else
            {
                DisplayListViewPage();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_isAddMode)
            {
                CreateModeSaveButton();

            }
            else
            {
                EditModeSaveButton();
            }
        }
        
        private void CreateModeSaveButton()
        {
            string abilityID = txtAbilityName.Text;
            string abilityType = txtAbilityType.Text;
            string description = txtDescription.Text;

            if (abilityID.Replace(" ", "") == "" || abilityID == null ||
                    abilityID.Length > 30)
            {
                MessageBox.Show("The ability name entered was invalid.");
                txtAbilityName.SelectAll();
                txtAbilityName.Focus();
                return;
            }
            if (abilityType.Replace(" ", "") == "" || abilityType == null ||
                        abilityType.Length > 25 || abilityType.Any(char.IsDigit))
            {
                MessageBox.Show("The ability type entered was invalid.");
                txtAbilityType.SelectAll();
                txtAbilityType.Focus();
                return;
            }
            if (description.Replace(" ", "") == "" || description == null ||
                        description.Length > 25)
            {
                MessageBox.Show("The ability description entered was invalid.");
                txtDescription.SelectAll();
                txtDescription.Focus();
                return;
            }

            Ability ability = new Ability()
            { 
                AbilityID = abilityID,
                AbilityType = abilityType,
                Description = description,
            };

            try
            {
                if (_abilityManager.AddAbility(ability))
                {
                    MessageBox.Show("The ability " + abilityID + " was created.");
                    ClearTextAreas();
                    txtAbilityName.Focus();
                }
                else
                {
                    MessageBox.Show("The ability " + abilityID + " was not created.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void EditModeSaveButton()
        {
            string abilityType = txtAbilityType.Text;
            string description = txtDescription.Text;

            if (abilityType.Replace(" ", "") == "" || abilityType == null ||
                        abilityType.Length > 25 || abilityType.Any(char.IsDigit))
            {
                MessageBox.Show("The ability type entered was invalid.");
                txtAbilityType.SelectAll();
                txtAbilityType.Focus();
                return;
            }
            if (description.Replace(" ", "") == "" || description == null ||
                        description.Length > 25)
            {
                MessageBox.Show("The ability description entered was invalid.");
                txtDescription.SelectAll();
                txtDescription.Focus();
                return;
            }

            _ability.AbilityType = abilityType;
            _ability.Description = description;

            try
            {
                if (_abilityManager.EditAbility(_ability))
                {
                    MessageBox.Show("The ability " + _ability.AbilityID + " was updated.");
                    DisplayListViewPage();
                }
                else
                {
                    MessageBox.Show("The ability " + _ability.AbilityID + " was not updated.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextAreas()
        {
            txtAbilityName.Text = "";
            txtAbilityType.Text = "";
            txtDescription.Text = "";
        }

        private void DisplayListViewPage()
        {
            _containerPage.DisplayTabItems(true);
            _containerPage.IsListView = true;
            _containerPage.frmAbility.Navigate(new AbilityRecordsPage());
        }
    }
}
