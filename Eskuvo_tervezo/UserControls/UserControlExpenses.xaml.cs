using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eskuvo_tervezo.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlExpenses.xaml
    /// </summary>
    public partial class UserControlExpenses : UserControl
    {
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        ViewModel.Expense Exp;
        Pages.Expenses expPage;

        string[] ResourceNames;
        object rm = null;

        public UserControlExpenses(ViewModel.Expense _Exp,ResourceManager _rm, string[] _ResourceNames, Pages.Expenses _expPage)
        {        
            InitializeComponent();
            Exp = _Exp;
            rm = _rm;
            expPage = _expPage;
            ListViewItemMenu1.Visibility = Exp.Expanse != null ? Visibility.Visible : Visibility.Collapsed;
            ListViewItemMenu2.Visibility = Exp.Cost != null ? Visibility.Visible : Visibility.Collapsed;
            ListViewItemMenu3.Visibility = Exp.Count != null ? Visibility.Visible : Visibility.Collapsed;
            ResourceNames = _ResourceNames;
            this.DataContext = Exp;
            LoadFormats();
        }

        void LoadFormats()
        {
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);
                if (it is PackIcon)
                    (it as PackIcon).ToolTip = (rm as System.Resources.ResourceManager).GetString(ResourceNames[i]);
            }
        }

        void IconDelete(object sender, MouseButtonEventArgs e)
        {
            expPage.DeleteExpense(sender, e);
        }
        void IconModify(object sender, MouseButtonEventArgs e)
        {
            int id = 0;
            Int32.TryParse((sender as PackIcon).DataContext.ToString(), out id);
            Models.WeddingExpenses ex = WPE.WeddingExpenses.FirstOrDefault(x => x.ID == id);
            if (ex!= null)
            {
                Windows.ExpenseModify expm = new Windows.ExpenseModify((rm as ResourceManager), ex, ResourceNames, expPage);
                expm.Show();
            }
        }
    }
}
