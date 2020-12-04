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
    /// Interaction logic for UsercontrolContacts.xaml
    /// </summary>
    public partial class UsercontrolContacts : UserControl
    {
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Pages.Contacts con;

        string[] ResourceNames;
        object rm = null;

        public UsercontrolContacts(ViewModel.Contact item, ResourceManager _rm, string[] _ResourceNames, Pages.Contacts _con)
        {
            InitializeComponent();
            rm = _rm;
            con = _con;
            ListViewItemMenu1.Visibility = item.Name != null ? Visibility.Visible : Visibility.Collapsed;
            ListViewItemMenu2.Visibility = item.Phone != null ? Visibility.Visible : Visibility.Collapsed;
            ListViewItemMenu3.Visibility = item.Email != null ? Visibility.Visible : Visibility.Collapsed;
            ResourceNames = _ResourceNames;
            this.DataContext = item;
            LoadFormats();
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

        void IconDelete_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            con.DeleteCLick(sender, e);
        }
        void IconModify_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = 0;
            Int32.TryParse((string)(sender as PackIcon).DataContext, out id);
            Models.Contacts c = WPE.Contacts.FirstOrDefault(x => x.Con_ID == id);
            if (c != null)
            {
                Windows.ContactsModify cm = new Windows.ContactsModify((rm as ResourceManager), c, ResourceNames, con);
                cm.Show();
            }
        }

    }
}
