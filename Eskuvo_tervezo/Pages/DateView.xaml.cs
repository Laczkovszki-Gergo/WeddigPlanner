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

namespace Eskuvo_tervezo.Pages
{
    /// <summary>
    /// Interaction logic for DateView.xaml
    /// </summary>
    /// 
    public partial class DateView : Page
    {
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Models.Login User;
        Models.Calendar Cal = new Models.Calendar();
        List<Models.CalendarLogEntrys> CalEntrys = new List<Models.CalendarLogEntrys>();
        List<Models.Calendar> Days = new List<Models.Calendar>();

        Windows.Functions f = new Windows.Functions();
        Windows.Home h;

        object rm;
        string[] ResourceNames;

        public DateView(Models.Login _User, ResourceManager _rm, string[] _Resourcenames, Windows.Home _h)
        {
            InitializeComponent();
            WPE = new Models.WeddingPlannerEntities();
            rm = _rm;
            ResourceNames = _Resourcenames;
            User = _User;
            h = _h;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RB_Actual.IsChecked = true;
            Disable_Enable_DatePickers();
            Days = WPE.Calendar.Where(x => x.UserID.Equals(User.IDLogin)).ToList();
            CalEntrys = WPE.CalendarLogEntrys.ToList();
            LoadFormats((rm as ResourceManager), ResourceNames);
            ShowDates(Convert.ToDateTime(DateTime.Now.ToShortDateString()), null);
        }

        internal void LoadFormats(ResourceManager _rm, string[] ResourceNames) 
        {
            rm = _rm;
            string[] ResourcenamesBlock = ResourceNames.ToArray();

            if ((rm as ResourceManager).BaseName.Substring((rm as ResourceManager).BaseName.Length - 2).Equals("Hu"))
            {
                rm = new ResourceManager(typeof(Properties.ResourceHu));
                DateStart.Language = System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag("Hu").IetfLanguageTag);
                DateEnd.Language = System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag("Hu").IetfLanguageTag);
            }
            else
            {
                rm = new ResourceManager(typeof(Properties.ResourceEng));
                DateStart.Language = System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag("Eng").IetfLanguageTag);
                DateEnd.Language = System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag("Hu").IetfLanguageTag);
            }
            for (int i = 0; i < ResourcenamesBlock.Length; i++)
            {
                var it = this.FindName(ResourcenamesBlock[i]);

                if (it is Label)
                    (it as Label).Content = (rm as ResourceManager).GetString(ResourcenamesBlock[i].ToString());
                else if (it is Button)
                    (it as Button).Content = (rm as ResourceManager).GetString(ResourcenamesBlock[i].ToString());
                else if (it is RadioButton)
                    (it as RadioButton).Content = (rm as ResourceManager).GetString(ResourcenamesBlock[i].ToString());
            }
        }
        internal void ShowDates(DateTime? start, DateTime? end)
        {
            WPE = new Models.WeddingPlannerEntities();     
            DataViewItems.Children.Clear();
            if(start == null && end == null)
            {
                foreach (var day in Days.Where(x => x.UserID.Equals(User.IDLogin)).OrderBy(x => x.Date).Reverse().ToList())
                {
                    var it = new ViewModel.DateView(day.Date, CalEntrys.Where(x => x.CalID.Equals(day.ID)).Select(x => x.LogEntry.Trim()).Reverse().ToList());
                    DataViewItems.Children.Add(new UserControls.UsercontrolDates(it, this, h));
                }
            }
            else if (end == null)
            {
                foreach (var day in Days.Where(x => x.UserID.Equals(User.IDLogin) & x.Date >= start).OrderBy(x => x.Date).Reverse().ToList())
                {
                    var it = new ViewModel.DateView(day.Date, CalEntrys.Where(x => x.CalID.Equals(day.ID)).Select(x => x.LogEntry.Trim()).Reverse().ToList());
                    DataViewItems.Children.Add(new UserControls.UsercontrolDates(it, this, h));
                }
            }
            else
            {
                foreach (var day in Days.Where(x => x.UserID.Equals(User.IDLogin) & x.Date >= start & x.Date <= end).OrderBy(x => x.Date).Reverse().ToList())
                {
                    var it = new ViewModel.DateView(day.Date, CalEntrys.Where(x => x.CalID.Equals(day.ID)).Select(x => x.LogEntry.Trim()).Reverse().ToList());
                    DataViewItems.Children.Add(new UserControls.UsercontrolDates(it, this, h));
                }
            }
        }
        void Disable_Enable_DatePickers()
        {
            if (RB_Interval.IsChecked == true)
            {
                DateStart.IsEnabled = true;
                DateEnd.IsEnabled = true;
                BT_Search.IsEnabled = true;
                LB_EndingInterval.IsEnabled = true;
                LB_StartingInterval.IsEnabled = true;
            }
            else
            {
                DateStart.IsEnabled = false;
                DateEnd.IsEnabled = false;
                BT_Search.IsEnabled = false;
                LB_EndingInterval.IsEnabled = false;
                LB_StartingInterval.IsEnabled = false;
            }
        }
        void RB_Actual_Checked(object sender, RoutedEventArgs e)
        {
                DateStart.IsEnabled = false;
                DateEnd.IsEnabled = false;
                BT_Search.IsEnabled = false;
                LB_EndingInterval.IsEnabled = false;
                LB_StartingInterval.IsEnabled = false;

                DateStart.SelectedDate = null;
                DateEnd.SelectedDate = null;
                ShowDates(Convert.ToDateTime(DateTime.Now.ToShortDateString()), null);
        }
        void RB_All_Checked(object sender, RoutedEventArgs e)
        {
                DateStart.IsEnabled= false;
                DateEnd.IsEnabled = false;
                BT_Search.IsEnabled = false;
                LB_EndingInterval.IsEnabled = false;
                LB_StartingInterval.IsEnabled = false;

                DateStart.SelectedDate = null;
                DateEnd.SelectedDate = null;
                ShowDates(null, null);
        }
        void RB_Interval_Checked(object sender, RoutedEventArgs e)
        {
            DateStart.IsEnabled = true;
            DateEnd.IsEnabled = true;
            BT_Search.IsEnabled = true;
            LB_EndingInterval.IsEnabled = true;
            LB_StartingInterval.IsEnabled = true;
            DataViewItems.Children.Clear();
        }
        void BT_Search_Click(object sender, RoutedEventArgs e)
        {
            ShowDates(DateStart.SelectedDate, DateEnd.SelectedDate);
        }

        void DateP_CalendarOpened(object sender, RoutedEventArgs e)
        {
           DataViewItems.Opacity = 0.2;
        }
        void DateP_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DataViewItems.Opacity = 1;
        }
    }
}
