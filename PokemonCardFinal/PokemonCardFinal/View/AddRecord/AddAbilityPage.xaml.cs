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
        
        private void CreateModeSaveButton()
        {
            Ability ability = new Ability()
            { 
                AbilityID = txtAbilityName.Text,
                AbilityType = txtAbilityType.Text,
                Description = txtDescription.Text
            };

            try
            {
                if (_abilityManager.AddAbility(ability))
                {
                    MessageBox.Show("The ability " + ability.AbilityID + " was created.");
                    ClearTextAreas();
                    txtAbilityName.Focus();
                }
                else
                {
                    MessageBox.Show("The ability " + ability.AbilityID + " was not created.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void EditModeSaveButton()
        {
            Ability ability = new Ability()
            {
                AbilityID = _ability.AbilityID,
                AbilityType = txtAbilityType.Text,
                Description = txtDescription.Text
            };

            try
            {
                if (_abilityManager.EditAbility(ability))
                {
                    MessageBox.Show("The ability " + ability.AbilityID + " was updated.");
                    DisplayListViewPage();
                }
                else
                {
                    MessageBox.Show("The ability " + ability.AbilityID + " was not updated.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool ValidateInput() 
        {
            bool isValid = true;
            string abilityID = txtAbilityName.Text;
            string abilityType = txtAbilityType.Text;
            string description = txtDescription.Text;

            if (abilityID.Replace(" ", "") == "" || abilityID == null ||
                    abilityID.Length > 30)
            {
                MessageBox.Show("The ability name entered was invalid.");
                txtAbilityName.SelectAll();
                txtAbilityName.Focus();
                isValid = false;
            }

            else if (abilityType.Replace(" ", "") == "" || abilityType == null ||
                        abilityType.Length > 25 || abilityType.Any(char.IsDigit))
            {
                MessageBox.Show("The ability type entered was invalid.");
                txtAbilityType.SelectAll();
                txtAbilityType.Focus();
                isValid = false;
            }

            else if (description.Replace(" ", "") == "" || description == null ||
                        description.Length > 650)
            {
                MessageBox.Show("The ability description entered was invalid.");
                txtDescription.SelectAll();
                txtDescription.Focus();
                isValid = false;
            }

            return isValid;
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
