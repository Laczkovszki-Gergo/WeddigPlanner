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
using MaterialDesignThemes.Wpf;
using System.Resources;

namespace Eskuvo_tervezo.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlCalItems.xaml
    /// </summary>
    public partial class UserControlCalItems : UserControl
    {
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Models.Calendar Cal;

        Pages.CalendarItems cli;

        string[] ResourceNames;

        object rm = null;

        public UserControlCalItems(ViewModel.CalLogEntrys item, Models.Calendar _Cal, ResourceManager _rm, string[] _ResourceNames,Pages.CalendarItems _cli)
        {
            InitializeComponent();
            Cal = _Cal;
            rm = _rm;
            cli = _cli;
            ListViewItemMenu.Visibility = item.LogEntry != null ? Visibility.Visible : Visibility.Collapsed;
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
                    (it as PackIcon).ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
            }
        }

        void Icon_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            cli.DeleteCLick(sender, e);
        }
        void Icon2_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = 0;
            Int32.TryParse((string)(sender as PackIcon).DataContext, out id);
            Models.CalendarLogEntrys cle = WPE.CalendarLogEntrys.FirstOrDefault(x => x.ID == id);
            if(cle != null)
            {
                Windows.LogEntryModify lem = new Windows.LogEntryModify((rm as ResourceManager),cle, ResourceNames,cli);
                lem.Show();
            }
        }

    }

}
