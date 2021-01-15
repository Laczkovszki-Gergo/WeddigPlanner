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
using System.Windows.Controls.Primitives;

namespace Eskuvo_tervezo.Pages
{
    /// <summary>
    /// Interaction logic for CalendarItems.xaml
    /// </summary>
    public partial class CalendarItems : Page
    {
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Models.Calendar Cal = new Models.Calendar();
        string[] Resourcenames;
        object rm;
        List<Models.CalendarLogEntrys> CalEntrys = new List<Models.CalendarLogEntrys>();
        Windows.Functions f = new Windows.Functions();
        Windows.Home h;

        public CalendarItems(Models.Calendar _Cal, ResourceManager _rm, string[] _Resourcenames, Windows.Home _h)
        {
            InitializeComponent();
            WPE = new Models.WeddingPlannerEntities();
            Cal = _Cal;
            Resourcenames = _Resourcenames;
            rm = _rm;
            h = _h;
            CalEntrys = WPE.CalendarLogEntrys.ToList();
            LoadFormats((rm as ResourceManager), Resourcenames);
            CreateList((rm as ResourceManager));
            LB_Date.Content = _Cal.Date.ToString("yyyy-MM-dd");
            Windows.RefreshCalendarList re = CreateList;
        }

        void SaveEntrys()
        {
            if (Cal.ID == 0)
            {
                WPE.Calendar.Add(Cal);
                WPE.SaveChanges();
            }
            if (Cal != null && f.IsNormalText(TB_LogEntry, TB_LogEntry.Text, (rm as ResourceManager)))
            {
                Models.CalendarLogEntrys cl = new Models.CalendarLogEntrys();
                cl.CalID = Cal.ID;
                cl.LogEntry = TB_LogEntry.Text.Trim();
                WPE.CalendarLogEntrys.Add(cl);
                WPE.SaveChanges();

            }
            CreateList();
        }
        internal void CreateList(ResourceManager _rm = null)
        {
            WPE = new Models.WeddingPlannerEntities();
            CalEntrys = WPE.CalendarLogEntrys.ToList();
            Items.Children.Clear();
            foreach (var item in CalEntrys.Where(x => x.CalID.Equals(Cal.ID)).Reverse().ToList())
                {
                    var it = new ViewModel.CalLogEntry(item.LogEntry.Trim(), item.ID.ToString());
                    Items.Children.Add(new UserControls.UserControlCalItems(it,Cal, (_rm as ResourceManager),Resourcenames, this));
                }
            h.RefreshCalendarArray();
        }
        internal void CreateList()
        {
            WPE = new Models.WeddingPlannerEntities();
            CalEntrys = WPE.CalendarLogEntrys.ToList();
            Items.Children.Clear();
            foreach (var item in CalEntrys.Where(x => x.CalID.Equals(Cal.ID)).Reverse().ToList())
            {
                var it = new ViewModel.CalLogEntry(item.LogEntry.Trim(), item.ID.ToString());
                Items.Children.Add(new UserControls.UserControlCalItems(it, Cal,(rm as ResourceManager), Resourcenames, this));
            }
            h.RefreshCalendarArray();
        }
        internal void LoadFormats(ResourceManager _rm, string[] ResourceNames)
        {
            rm = _rm;

            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);

                if (it is CheckBox)
                    (it as CheckBox).Content = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (it is Label)
                    (it as Label).Content = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (it is Button)
                    (it as Button).Content = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (it is TextBlock)
                    (it as TextBlock).Text = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNameCharacters")
                {
                    if (TB_LogEntry.ToolTip != null)
                        TB_LogEntry.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
            }
        }

        internal void DeleteCLick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = 0;
            Int32.TryParse((string)(sender as PackIcon).DataContext, out id);

            ViewModel.WinMessageBoxItem wmsb = new ViewModel.WinMessageBoxItem((rm as ResourceManager).GetString("Message_Delete_Title"), (rm as ResourceManager).GetString("Message_Delete"), PackIconKind.WarningCircle);
            Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), Resourcenames, true);

            if (msb.ShowDialog() == true)
            {
                if (WPE.CalendarLogEntrys.Any(x => x.ID.Equals(id)))
                {
                    WPE.Dispose();
                    WPE = new Models.WeddingPlannerEntities();
                    var Logentry = WPE.CalendarLogEntrys.FirstOrDefault(x => x.ID.Equals(id));
                    WPE.CalendarLogEntrys.Attach(Logentry);
                    WPE.CalendarLogEntrys.Remove(Logentry);
                    WPE.SaveChanges();

                    if (!WPE.CalendarLogEntrys.Any(x => x.CalID.Equals(Logentry.CalID)))
                    {

                        var cal = WPE.Calendar.FirstOrDefault(x => x.ID.Equals(Logentry.CalID));
                        WPE.Calendar.Attach(cal);
                        WPE.Calendar.Remove(cal);
                        WPE.SaveChanges();

                        int Userid = Cal.UserID;
                        DateTime Date = Cal.Date;
                        Cal = new Models.Calendar();
                        Cal.ID = 0;
                        Cal.UserID = Userid;
                        Cal.Date = Date;
                    }
                    CreateList();
                }
            }
        }
        void BT_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveEntrys();
        }
        void TB_LogEntry_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
    }
}
