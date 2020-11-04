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
    /// Interaction logic for UserControlComparsion.xaml
    /// </summary>
    public partial class UserControlComparsion : UserControl
    {
        public UserControlComparsion(ViewModel.Comparsion Comp)
        {
            InitializeComponent();

            ListViewItemMenu1.Visibility = Comp.ExpenseName!= null ? Visibility.Visible : Visibility.Collapsed;
            ListViewItemMenu2.Visibility = Comp.Expense != null ? Visibility.Visible : Visibility.Collapsed;
            this.DataContext = Comp;
        }
    }
}
