using Eskuvo_tervezo.UserControls;
using Eskuvo_tervezo.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.IO;

namespace Eskuvo_tervezo.Windows
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public delegate void ChangePicture();

    public partial class Home : Window
    {
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Models.Login ActualUser = new Models.Login();
        Models.Calendar[] CalArray = null;
        Models.CalendarLogEntrys[] ClArray = null;
        Models.Radio rad = null;

        Functions f = new Functions();
        MainWindow main;

        List<DateTime> dates = new List<DateTime>();

        DateTime[] significantDates;
        string[] ResourceNames;

        DoubleAnimation doubleAnimation = new DoubleAnimation();
        ResourceManager rm;
        Style HighlightedDateStyle = null;
        Style BigDayStyle = null;
        Style NormalDateStyle = null;
        DateTime DayOfWedding;
        bool Hun = true;
        bool sound;

        public Home(ResourceManager _rm)
        {
            InitializeComponent();
            rm = _rm;
        }
        public Home(string[] _ResourceNames, bool hun, Models.Login _ActualUser,bool _sound, MainWindow _main)
        {
            InitializeComponent();
            ResourceNames = _ResourceNames;
            Hun = hun;
            ActualUser = _ActualUser;
            HighlightedDateStyle = (Style)FindResource("CalendarDayButtonStyleHighlighted");
            NormalDateStyle = (Style)FindResource("CalendarDayButtonStyle");
            BigDayStyle = (Style)FindResource("CalendarDayButtonStyleHighlightedBigDay");
            main = _main;
            sound = _sound;
            //TODO Framework háttérkép
            //FrameContent.Background = f.CreateImageBrush(@"D:\C#\Eskuvo_tervezo\Eskuvo_tervezo\bin\Debug\1.jpg");
            //FrameContent.BorderBrush = System.Windows.Media.Brushes.DarkGray;
            //FrameContent.BorderThickness = new Thickness(2);
        }
        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReadBasicRadioChannels();
            rad = WPE.Radio.FirstOrDefault(x => (x.UserID.Equals(ActualUser.IDLogin) && x.Chosen == true)) != null ? WPE.Radio.FirstOrDefault(x => (x.UserID.Equals(ActualUser.IDLogin) && x.Chosen == true)): WPE.Radio.First(x => (x.UserID.Equals(ActualUser.IDLogin)));
            if (rad != null)
            {
                main.PlayMusicFromURL(rad.StreamLink.Trim(),sound);
                main.RadioVolume = rad.Volume != null ? (int)rad.Volume : 100;

                Tbl_Radio.Text = "♫ " + rad.ChannelName.Trim() + " ♫";

                LeftToRightMarquee();
                if (sound)                
                    IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeHigh;              
                else
                {
                    RadioAnimationStop();
                    IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeOff;
                }
            }
            if (main.bgwRadioHigh.IsBusy)
            {
                main.bgwRadioHigh.CancelAsync();
                main.bgwRadioHigh = new System.ComponentModel.BackgroundWorker();
            }
            main.bgwRadioHigh.RunWorkerAsync();

            significantDates = WPE.Calendar.Where(x => x.UserID.Equals(ActualUser.IDLogin)).Select(x => x.Date).ToArray();

            calendarEdit.DisplayDate = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.Month);
            LB_Time.Content = DateTime.Now.ToLongTimeString();

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            FrameContent.Content = new Pages.Welcome(ActualUser, rm, ResourceNames);

            if(WPE.WeddingData.Any(x=>x.User_ID.Equals(ActualUser.IDLogin)))
                {
                if (WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(ActualUser.IDLogin)).Image != null)
                    ImageBetrothed.Source = f.CreateBitmapFromBytes(WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(ActualUser.IDLogin)).Image);
                }
          
            LoadFormats(Hun);
            RefreshCalendarArray();
        }
        void LoadFormats(bool hun)
        {
            WPE = new Models.WeddingPlannerEntities();
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            if (hun)
            {
                rm = new ResourceManager(typeof(Properties.ResourceHu));
                calendarEdit.Language = XmlLanguage.GetLanguage(CultureInfo.GetCultureInfoByIetfLanguageTag("Hu").IetfLanguageTag);
            }
            else
            {
                rm = new ResourceManager(typeof(Properties.ResourceEng));
                calendarEdit.Language = XmlLanguage.GetLanguage(CultureInfo.GetCultureInfoByIetfLanguageTag("Eng").IetfLanguageTag);
            }
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);

                if (it is CheckBox)
                    (it as CheckBox).Content = rm.GetString(ResourceNames[i]);
                else if (it is Label)
                    (it as Label).Content = rm.GetString(ResourceNames[i]);
                else if (it is Button)
                    (it as Button).Content = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i] == "Title_Home")
                    this.Title = rm.GetString(ResourceNames[i]);
                else if (it is TextBlock)
                    (it as TextBlock).Text = rm.GetString(ResourceNames[i]);
                else if (it is PackIcon)
                    (it as PackIcon).ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);

            }
            LB_TitleHome.Content += " " + ActualUser.User.Trim();
            CreateMenu();
            System.Windows.Input.Mouse.OverrideCursor = null;
        }
        
        internal void CreateMenu()
        {
            Menu.Children.Clear();
            var item0 = new ItemMenu("Menu_Title", rm.GetString("Menu_Title"), new Pages.Welcome(ActualUser,rm,ResourceNames), PackIconKind.ViewDashboard);
            var item1 = new ItemMenu("Menu_FirstSteps", rm.GetString("Menu_FirstSteps"), new Pages.FirstSteps(ActualUser, rm, ResourceNames,this), PackIconKind.FirstPage);

            Menu.Children.Add(new UserControlMenuItem(item0, this, rm));
            Menu.Children.Add(new UserControlMenuItem(item1, this, rm));

            if (FirstStepReady())
            {
                var item2 = new ItemMenu("Menu_DiaryDate", rm.GetString("Menu_DiaryDate"), new Pages.DateView(ActualUser, rm, ResourceNames, this), PackIconKind.BookAdd);
                var item3 = new ItemMenu("Menu_Contact", rm.GetString("Menu_Contact"), new Pages.Contacts(ActualUser, rm, ResourceNames), PackIconKind.Contact);
                var item4 = new ItemMenu("Menu_Guests", rm.GetString("Menu_Guests"), new Pages.Guests(ActualUser, rm, ResourceNames), PackIconKind.People);

                var menu_expenses = new List<SubItem>();
                menu_expenses.Add(new SubItem("Menu_Venue", rm.GetString("Menu_Venue"), new Pages.Venue(ActualUser, rm, ResourceNames)));                                                                                                             
                menu_expenses.Add(new SubItem("Menu_ExpensesPlanned", rm.GetString("Menu_ExpensesPlanned"), new Pages.Expenses(ActualUser,rm,ResourceNames)));
                var item5 = new ItemMenu("Menu_Expenses", rm.GetString("Menu_Expenses"), menu_expenses, PackIconKind.Money);

                var item6 = new ItemMenu("Menu_Comparsion", rm.GetString("Menu_Comparsion"), new Pages.Comparsion(ActualUser,rm,ResourceNames), PackIconKind.Compare);

                Menu.Children.Add(new UserControlMenuItem(item2, this, rm));
                Menu.Children.Add(new UserControlMenuItem(item3, this, rm));
                Menu.Children.Add(new UserControlMenuItem(item4, this, rm));
                Menu.Children.Add(new UserControlMenuItem(item5, this, rm));
                Menu.Children.Add(new UserControlMenuItem(item6, this, rm));
            }
            var item7 = new ItemMenu("Menu_Radio", rm.GetString("Menu_Radio"), new Pages.Radio(ActualUser,rm,ResourceNames,main,this), PackIconKind.Radio);
            var item8 = new ItemMenu("Menu_Settings", rm.GetString("Menu_Settings"), new Pages.Settings(ActualUser,rm, ResourceNames,this), PackIconKind.Settings);
            var item9 = new ItemMenu("Menu_Logout", rm.GetString("Menu_Logout"), new Page(), PackIconKind.Logout);
            var item10 = new ItemMenu("Menu_Exit", rm.GetString("Menu_Exit"), new Page(), PackIconKind.EmergencyExit);
            Menu.Children.Add(new UserControlMenuItem(item7, this, rm));
            Menu.Children.Add(new UserControlMenuItem(item8, this, rm)); 
            Menu.Children.Add(new UserControlMenuItem(item9, this, rm));
            Menu.Children.Add(new UserControlMenuItem(item10, this, rm));


        }
        internal void HighlightDay(CalendarDayButton button, DateTime date)
        {
            if (significantDates.Contains(date))
            {
                button.Style = HighlightedDateStyle;
                DateTooltips(button, date);
            }
            else if (DayOfWedding.Equals(date))
            {
                button.Style = BigDayStyle;
                button.ToolTip = rm.GetString("DateOfWedding");
            }
            else
            {
                button.Style = NormalDateStyle;
                button.ToolTip = null;
            }
        }
        internal void SwitchScreen(object sender)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            var screen = ((Page)sender);
            if (screen != null)
            {
                if (sender is Pages.DateView)
                {
                    (sender as Pages.DateView).ShowDates(null,null);
                    FrameContent.Content = null;
                    FrameContent.Content = screen;
                }
                else
                {
                    FrameContent.Content = null;
                    FrameContent.Content = screen;
                }
                System.Windows.Input.Mouse.OverrideCursor = null;
            }
        }
        internal void VolumeOnOff()
        {
            if (main.IsRadioPlaying())
            {
                IconVolumeOnOff.Kind = PackIconKind.VolumeOff;
                main.RadioPlayerStop();
                RadioAnimationStop();
                sound = false;
            }

            else
            {
                IconVolumeOnOff.Kind = PackIconKind.VolumeHigh;
                main.RadioPlayerPlay();
                RadioAnimationStart();
                sound = true;
            }
        }
        internal void LoadDayEntrys(DateTime Day)
        {
            try
            {
                Models.Calendar c = CalArray.FirstOrDefault(x => x.UserID.Equals(ActualUser.IDLogin) && x.Date.Equals(Day));

                if (c != null && FrameContent != null)
                    {
                        FrameContent.Content = new Pages.CalendarItems(c, rm, ResourceNames,this);
                    }
                    else if (FrameContent != null)
                    {
                        c = new Models.Calendar();
                        c.Date = calendarEdit.SelectedDate.Value;
                        c.UserID = ActualUser.IDLogin;
                        FrameContent.Content = new Pages.CalendarItems(c, rm, ResourceNames,this);
                    }
            }
            catch (Exception ex)
            {
                ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems("Error", ex.Message, PackIconKind.Error);
                Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames,false);
                msb.Show();
            }
        }
        internal void RefreshCalendarArray()
        {
            WPE = new Models.WeddingPlannerEntities();
            CalArray = WPE.Calendar.Where(x => x.UserID.Equals(ActualUser.IDLogin)).ToArray();
            ClArray = WPE.CalendarLogEntrys.ToArray();
            significantDates = WPE.Calendar.Where(x => x.UserID.Equals(ActualUser.IDLogin)).Select(x => x.Date).ToArray();
        }
        internal void RadioAnimationStart()
        {
            Tbl_Radio.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
        }
        internal void RadioAnimationStop()
        {
            Tbl_Radio.BeginAnimation(Canvas.LeftProperty, null);
        }

        bool FirstStepReady()
        {
            bool FirstStep = WPE.WeddingData.Any(x => x.User_ID.Equals(ActualUser.IDLogin));
            if (FirstStep)
            {
                WPE = new Models.WeddingPlannerEntities();
                DayOfWedding = WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(ActualUser.IDLogin)).Wedding_Date;
                LB_RemainingDays.Content = LB_RemainingDays.Content.ToString().Split(':')[0] + ": " + (int)(DayOfWedding - DateTime.Now).TotalDays;
            }
            return FirstStep;
        }

        void DateTooltips(CalendarDayButton button, DateTime date)
        {
            Models.Calendar c = CalArray.FirstOrDefault(x => x.Date.Equals(date));
            string[] l = ClArray.Where(X => X.CalID.Equals(c.ID)).Select(x => x.LogEntry.Trim()).Reverse().ToArray();
            button.ToolTip = null;
            for (int i = 0; i < l.Length; i++)
            {
                if (i < l.Length - 1)
                    button.ToolTip += l[i].Trim() + "\n";
                else
                    button.ToolTip += l[i].Trim();
            }
        }
        void LoadDayEntrys()
        {
            try
            {
                if (calendarEdit.SelectedDate.Value != null)
                {
                    Models.Calendar c = CalArray.FirstOrDefault(x => x.UserID.Equals(ActualUser.IDLogin) && x.Date.Equals(calendarEdit.SelectedDate.Value));
                    if (c != null && FrameContent != null)
                    {
                        FrameContent.Content = new Pages.CalendarItems(c, rm, ResourceNames, this);
                    }
                    else if (FrameContent != null)
                    {
                        c = new Models.Calendar();
                        c.Date = calendarEdit.SelectedDate.Value;
                        c.UserID = ActualUser.IDLogin;
                        FrameContent.Content = new Pages.CalendarItems(c, rm, ResourceNames, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems("Error", ex.Message, PackIconKind.Error);
                Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, false);
                msb.Show();

            }
        }
        void LeftToRightMarquee()
        {
            double height = canMain.ActualHeight - Tbl_Radio.ActualHeight;
            Tbl_Radio.Margin = new Thickness(0, height / 2, 0, 0);

            doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = -Tbl_Radio.ActualWidth;
            doubleAnimation.To = canMain.ActualWidth;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(3));
            RadioAnimationStart();
        }
        void ReadBasicRadioChannels()
        {         
            if(File.Exists("RadioChannels/RadioChannels.txt"))
            {
                StreamReader sr = new StreamReader("RadioChannels/Radiochannels.txt", System.Text.Encoding.UTF8);
                try
                {
                    while(!sr.EndOfStream)
                    {
                        string row = sr.ReadLine().Trim();
                        string rowStreamLink = row.Split(';')[0].Trim();
                        string rowChannelName = row.Split(';')[1].Trim();

                        if (!WPE.Radio.Any(x=> x.UserID.Equals(ActualUser.IDLogin) && x.StreamLink.Trim().Equals(rowStreamLink.Trim()) && x.ChannelName.Trim().Equals(rowChannelName.Trim())))
                        {
                            Models.Radio r = new Models.Radio();

                            r.UserID = ActualUser.IDLogin;
                            r.StreamLink = row.Split(';')[0].Trim();
                            r.ChannelName = row.Split(';')[1].Trim();
                            WPE.Radio.Add(r);
                        }
                    }
                    sr.Close();
                    WPE.SaveChanges();
                }
                catch(Exception ex)
                {
                    sr.Close();
                    ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems("Error", ex.Message, PackIconKind.Error);
                    Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, false);
                    msb.Show();
                }
            }
        }

        internal void Exit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems(rm.GetString("ExitWarning"), rm.GetString("Exit"), PackIconKind.QuestionMarkRhombus);
            Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, null);

            if (msb.ShowDialog() == true)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
        internal void Back_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems(rm.GetString("Message_LogoutTitle"), rm.GetString("Message_LogoutText"), PackIconKind.QuestionMarkRhombus);
            Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, null);

            if (msb.ShowDialog() == true)
            {
                main.RadioPlayerStop();
                MainWindow m = new MainWindow(ResourceNames, Hun, sound);
                m.Show();
                this.Close();
            }
        }
        void ImHU_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Hun = true;
            LoadFormats(Hun);
            if (FrameContent != null)
            {
                if (FrameContent.Content is Pages.CalendarItems)
                {
                    (FrameContent.Content as Pages.CalendarItems).LoadFormats(rm, ResourceNames);
                    (FrameContent.Content as Pages.CalendarItems).CreateList(rm);
                }
                else if (FrameContent.Content is Pages.Contacts)
                {
                    (FrameContent.Content as Pages.Contacts).LoadFormats(rm, ResourceNames);
                    (FrameContent.Content as Pages.Contacts).CreateContactList(rm);
                }
                else if (FrameContent.Content is Pages.DateView)
                {
                    (FrameContent.Content as Pages.DateView).LoadFormats(rm, ResourceNames);
                }
                else if (FrameContent.Content is Pages.Guests)
                {
                    (FrameContent.Content as Pages.Guests).LoadFormats(rm, ResourceNames);
                    (FrameContent.Content as Pages.Guests).CreateGuestList(rm);
                }
                else if (FrameContent.Content is Pages.FirstSteps)
                {
                    (FrameContent.Content as Pages.FirstSteps).LoadFormats(rm, ResourceNames);
                }
                else if (FrameContent.Content is Pages.Expenses)
                {
                    (FrameContent.Content as Pages.Expenses).LoadFormats(rm, ResourceNames);
                    (FrameContent.Content as Pages.Expenses).CreateExpenseList(rm);
                }
                else if (FrameContent.Content is Pages.Venue)
                {
                    (FrameContent.Content as Pages.Venue).LoadFormats(rm, ResourceNames);
                    (FrameContent.Content as Pages.Venue).ShowPics(rm);
                }
                else if (FrameContent.Content is Pages.Comparsion)
                {
                    (FrameContent.Content as Pages.Comparsion).LoadFormats(rm, ResourceNames);
                }
                else if (FrameContent.Content is Pages.Settings)
                {
                    (FrameContent.Content as Pages.Settings).LoadFormats(rm, ResourceNames);
                }
                else if (FrameContent.Content is Pages.Radio)
                {
                    (FrameContent.Content as Pages.Radio).LoadFormats(rm, ResourceNames);
                }
            }
            System.Windows.Input.Mouse.OverrideCursor = null;
        }
        void ImUK_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Hun = false;
            LoadFormats(Hun);
            if (FrameContent != null)
            {
                if (FrameContent.Content is Pages.CalendarItems)
                {
                    (FrameContent.Content as Pages.CalendarItems).LoadFormats(rm, ResourceNames);
                    (FrameContent.Content as Pages.CalendarItems).CreateList(rm);
                }
                else if (FrameContent.Content is Pages.Contacts)
                {
                    (FrameContent.Content as Pages.Contacts).LoadFormats(rm, ResourceNames);
                    (FrameContent.Content as Pages.Contacts).CreateContactList(rm);
                }
                else if (FrameContent.Content is Pages.DateView)
                {
                    (FrameContent.Content as Pages.DateView).LoadFormats(rm, ResourceNames);
                }
                else if (FrameContent.Content is Pages.Guests)
                {
                    (FrameContent.Content as Pages.Guests).LoadFormats(rm, ResourceNames);
                    (FrameContent.Content as Pages.Guests).CreateGuestList(rm);
                }
                else if (FrameContent.Content is Pages.FirstSteps)
                {
                    (FrameContent.Content as Pages.FirstSteps).LoadFormats(rm, ResourceNames);
                }
                else if (FrameContent.Content is Pages.Expenses)
                {
                    (FrameContent.Content as Pages.Expenses).LoadFormats(rm, ResourceNames);
                    (FrameContent.Content as Pages.Expenses).CreateExpenseList(rm);
                }
                else if (FrameContent.Content is Pages.Venue)
                {
                    (FrameContent.Content as Pages.Venue).LoadFormats(rm, ResourceNames);
                    (FrameContent.Content as Pages.Venue).ShowPics(rm);
                }
                else if (FrameContent.Content is Pages.Comparsion)
                {
                    (FrameContent.Content as Pages.Comparsion).LoadFormats(rm, ResourceNames);
                }
                else if (FrameContent.Content is Pages.Settings)
                {
                    (FrameContent.Content as Pages.Settings).LoadFormats(rm, ResourceNames);
                }
                else if (FrameContent.Content is Pages.Radio)
                {
                    (FrameContent.Content as Pages.Radio).LoadFormats(rm, ResourceNames);
                }
            }
            System.Windows.Input.Mouse.OverrideCursor = null;
        }
        void SelectedDates_Changed(object sender, SelectionChangedEventArgs e)
        {
            LoadDayEntrys();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            LB_Time.Content = DateTime.Now.ToLongTimeString();
        }
        void LB_Today_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            calendarEdit.DisplayMode = CalendarMode.Month;
            calendarEdit.DisplayDate = DateTime.Now;
            calendarEdit.SelectedDate = DateTime.Today;
        }
        void calendarButton_Loaded(object sender, EventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
            button.DataContextChanged += new DependencyPropertyChangedEventHandler(calendarButton_DataContextChanged);
        }
        void calendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DateTime? d = ((sender as CalendarDayButton).DataContext) as DateTime?;
            DateTime da = Convert.ToDateTime(d);

                CalendarDayButton button = (CalendarDayButton)sender;
                DateTime date = (DateTime)button.DataContext;
                HighlightDay(button, date);
        }
        void IconVolumeOnOff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VolumeOnOff();
        }
        void IconRadioChannelRight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WPE = new Models.WeddingPlannerEntities();
            if (WPE.Radio.Any(x => x.UserID.Equals(ActualUser.IDLogin)))
            {
                rad = WPE.Radio.Any(x => x.ID > rad.ID & x.UserID.Equals(ActualUser.IDLogin)) == true ? WPE.Radio.Where(x => x.ID > rad.ID & x.UserID.Equals(ActualUser.IDLogin)).OrderBy(x => x.ID).First() : WPE.Radio.Where(x => x.UserID.Equals(ActualUser.IDLogin)).First();
                main.PlayMusicFromURL(rad.StreamLink.Trim(), sound);
                main.RadioVolume = rad.Volume != null ? (int)rad.Volume : 100;

                Tbl_Radio.Text = "♫ " + rad.ChannelName.Trim() + " ♫";

                LeftToRightMarquee();
                if (sound)
                    IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeHigh;
                else
                {
                    RadioAnimationStop();
                    IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeOff;
                }
            }
        }
        void IconRadioChannelLeft_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WPE = new Models.WeddingPlannerEntities();
            if (WPE.Radio.Any(x=>x.UserID.Equals(ActualUser.IDLogin)))
            {
                rad = WPE.Radio.Any(x => x.ID < rad.ID & x.UserID.Equals(ActualUser.IDLogin)) == true ? WPE.Radio.Where(x => x.ID < rad.ID & x.UserID.Equals(ActualUser.IDLogin)).OrderByDescending(x => x.ID).First() : WPE.Radio.Where(x => x.UserID.Equals(ActualUser.IDLogin)).OrderByDescending(x => x.ID).First();
                main.PlayMusicFromURL(rad.StreamLink.Trim(), sound);
                main.RadioVolume = rad.Volume != null ? (int)rad.Volume : 100;

                Tbl_Radio.Text = "♫ " + rad.ChannelName.Trim() + " ♫";

                LeftToRightMarquee();
                if (sound)
                    IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeHigh;
                else
                {
                    RadioAnimationStop();
                    IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeOff;
                }
            }

        }
        void ImageBetrothed_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(WPE.WeddingData.Any(x=>x.User_ID.Equals(ActualUser.IDLogin)))
                {
                Pages.FirstSteps fi = new Pages.FirstSteps(ActualUser, rm, ResourceNames, this);
                ChangePicture re = fi.BrowsePictures;
                re();
                fi = null;
            }
        }
        private void ImageBetrothed_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Media.Imaging.BitmapImage[] allpics = new System.Windows.Media.Imaging.BitmapImage[1];
            Windows.ImageView i = new Windows.ImageView((sender as Image), allpics);
            this.Opacity = 0.5;
            bool? result = i.ShowDialog();
            if (result == true)
            {
                this.Opacity = 1;
            }
        }
    }
}
