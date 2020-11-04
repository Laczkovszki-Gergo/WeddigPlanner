using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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

namespace Eskuvo_tervezo.Windows
{
    /// <summary>
    /// Interaction logic for ExpenseModify.xaml
    /// </summary>
    public delegate void RefreshExpenseList(ResourceManager rm);
    public partial class ExpenseModify : Window
    {

        Models.WeddingExpenses exp = new Models.WeddingExpenses();
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Pages.Expenses ExpPage;
        Functions f = new Functions();

        ResourceManager rm;
        string[] ResourceNames;

        public ExpenseModify(ResourceManager _rm, Models.WeddingExpenses _exp, string[] _ResourceNames, Pages.Expenses _ExpPage)
        {
            InitializeComponent();
            rm = _rm;
            exp = _exp;
            ResourceNames = _ResourceNames;
            TB_Expense.Text = exp.ExpenseName.Trim();
            TB_Cost.Text = exp.Expense.ToString().Trim();
            TB_Count.Text = exp.Count.ToString().Trim();
            ExpPage = _ExpPage;
            LoadFormats();
        }

        void LoadFormats()
        {
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);

                if (it is Label)
                    (it as Label).Content = rm.GetString(ResourceNames[i]);
                else if (it is Button)
                    (it as Button).Content = rm.GetString(ResourceNames[i]);
                else if (it is TextBlock)
                    (it as TextBlock).Text = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i] == "Tooltip_InvalidNameCharacters")
                {
                    if (TB_Expense.ToolTip != null)
                        TB_Expense.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
                else if (ResourceNames[i] == "Tooltip_InvalidNumberCharacters")
                {
                    if (TB_Cost.ToolTip != null)
                        TB_Cost.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
                else if (ResourceNames[i] == "Tooltip_InvalidNumberCharacters")
                {
                    if (TB_Count.ToolTip != null)
                        TB_Count.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
            }
        }
        void Modification()
        {
            var result = WPE.WeddingExpenses.SingleOrDefault(b => b.ID == exp.ID);
            if (result != null && f.isContactName(TB_Expense, TB_Expense.Text.Trim(), rm) && f.IsNumber(TB_Cost, f.StringRemoveWhiteSpace(TB_Cost.Text.Trim()), rm) && f.IsNumber(TB_Count, f.StringRemoveWhiteSpace(TB_Count.Text.Trim()), rm))
            {
                result.ExpenseName = TB_Expense.Text.Trim();
                result.Expense = Convert.ToInt32(f.StringRemoveWhiteSpace(TB_Cost.Text.Trim()));
                result.Count = Convert.ToInt32(f.StringRemoveWhiteSpace(TB_Count.Text.Trim()));
                WPE.SaveChanges();
                RefreshExpenseList re = ExpPage.CreateExpenseList;
                re((rm as ResourceManager));
                this.Close();
            }
        }

        void BT_Modification_Click(object sender, RoutedEventArgs e)
        {
            Modification();
        }
        void BT_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void TB_Cost_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            f.NumericTextBox_PreviewKeyDown(sender, e);
            if (f.StringRemoveWhiteSpace((sender as TextBox).Text).Length > 8)
                e.Handled = true;
         
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Modification_Click(sender, e);
            }
        }
        void TB_Count_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            f.NumericTextBox_PreviewKeyDown(sender, e);
            if (f.StringRemoveWhiteSpace((sender as TextBox).Text).Length > 8)
                e.Handled = true;

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Modification_Click(sender, e);
            }
        }
        void TB_Expense_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Modification_Click(sender, e);
            }
        }
    }
}
