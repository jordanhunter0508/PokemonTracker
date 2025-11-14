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
    /// Interaction logic for AddRulePage.xaml
    /// </summary>
    public partial class AddRulePage : Page
    {
        IRuleManager _ruleManager;
        PokemonRule _rule;
        AddEditContainerPage _containerPage;
        bool _isAddMode;

        public AddRulePage()
        {
            InitializeComponent();
            _isAddMode = true;
        }

        public AddRulePage(PokemonRule rule, IRuleManager ruleManager, 
            AddEditContainerPage containerPage)
        {
            InitializeComponent();
            _rule = rule;
            _ruleManager = ruleManager;
            _containerPage = containerPage;
            _isAddMode = false;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isAddMode)
            {
                txtRuleID.Focus();
            }
            else
            {
                txtDescription.Focus();
                btnClear.Content = "Go Back";
                txtRuleID.Text = _rule.RuleID;
                txtDescription.Text = _rule.Description;
                txtRuleID.IsEnabled = false;

                // Disables all other tab items
                _containerPage.DisplayTabItems(false);
                _containerPage.tabRule.IsEnabled = true;
            }

            btnSave.IsDefault = true;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (_isAddMode)
            {
                ClearTextAreas();
                txtRuleID.Focus();

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
            PokemonRule rule = new PokemonRule()
            {
                RuleID = txtRuleID.Text,
                Description = txtDescription.Text,
            };

            try
            {
                if (_ruleManager.AddRule(rule))
                {
                    MessageBox.Show("The rule " + rule.RuleID + " was successfully created.");
                    ClearTextAreas();
                    txtRuleID.Focus();
                }
                else
                {
                    MessageBox.Show("The rule " + rule.RuleID + " was not created.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditModeSaveButton() 
        {
            PokemonRule rule = new PokemonRule()
            {
                RuleID = _rule.RuleID,
                Description = txtDescription.Text,
            };

            try
            {
                if (_ruleManager.EditRule(rule))
                {
                    MessageBox.Show("The rule " + rule.RuleID + " was successfully updated.");

                    // Brings the user back to the RuleRecordsPage
                    DisplayListViewPage();
                }
                else
                {
                    MessageBox.Show("The rule " + rule.RuleID + " was not successfully updated.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidateInput() 
        {
            bool isValid = true;

            string ruleID = txtRuleID.Text;
            string description = txtDescription.Text;

            if (ruleID.Replace(" ", "") == "" || ruleID == null ||
                ruleID.Length > 50 || ruleID.Any(char.IsDigit))
            {
                MessageBox.Show("The rule name entered was invalid.");
                txtRuleID.SelectAll();
                txtRuleID.Focus();
                isValid = false;
            }

            else if (description.Replace(" ", "") == "" || description == null || 
                description.Length > 150)
            {
                MessageBox.Show("The description entered was invalid.");
                txtDescription.SelectAll();
                txtDescription.Focus();
                isValid = false;
            }

            return isValid;
        }

        private void ClearTextAreas()
        {
            txtRuleID.Text = "";
            txtDescription.Text = "";
        }

        private void DisplayListViewPage()
        {
            _containerPage.DisplayTabItems(true);
            _containerPage.IsListView = true;
            _containerPage.frmRule.Navigate(new RuleRecordsPage());
        }
    }
}
