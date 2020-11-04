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

namespace Eskuvo_tervezo.UserControls
{
    public delegate void CalCalender(DateTime day);
    /// <summary>
    /// Interaction logic for UsercontrolDates.xaml
    /// </summary>
    public partial class UsercontrolDates : UserControl
    {
        Windows.Home h;

        public UsercontrolDates(ViewModel.DateView item, Pages.DateView _dat, Windows.Home _h)
        {
            InitializeComponent();
            h = _h;
            ListViewItemMenu1.Visibility = item.Day != null ? Visibility.Visible : Visibility.Collapsed;
            ListViewItemMenu2.Visibility = item.Entry != null ? Visibility.Visible : Visibility.Collapsed;
            this.DataContext = item;
        }

        void ListViewItemMenu1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CalCalender cal = h.LoadDayEntrys;
            cal(Convert.ToDateTime((((sender as ListBoxItem).DataContext) as ViewModel.DateView).Day));
        }
    }
}
