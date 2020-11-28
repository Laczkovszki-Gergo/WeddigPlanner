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
    /// Interaction logic for UserControlGuests.xaml
    /// </summary>
    public partial class UserControlGuests : UserControl
    {
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        object rm = null;
        Pages.Guests gue;
        string[] ResourceNames;
        public UserControlGuests(ViewModel.Guests item, Pages.Guests _gue, string[] _ResourceNames, ResourceManager _rm)
        {
            InitializeComponent();
            ListViewItemMenu1.Visibility = item.Name != null ? Visibility.Visible : Visibility.Collapsed;
            ListViewItemMenu2.Visibility = item.Person.ToString() != null ? Visibility.Visible : Visibility.Collapsed;
            this.DataContext = item;
            rm = _rm;
            gue = _gue;
            ResourceNames = _ResourceNames;
            LoadFormats();
        }

        void IconDelete_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            gue.DeleteCLick(sender, e);
        }
        void IconModify_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = 0;
            Int32.TryParse((string)(sender as PackIcon).DataContext, out id);
            Models.Guests c = WPE.Guests.FirstOrDefault(x => x.Guest_ID == id);
            if (c != null)
            {
                Windows.GuestsModify gu = new Windows.GuestsModify((rm as ResourceManager), c, ResourceNames, gue);
                gu.Show();
            }

        }
        void LoadFormats()
        {
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);
                if (it is PackIcon)
                    (it as PackIcon).ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
            }
        }
    }
}
