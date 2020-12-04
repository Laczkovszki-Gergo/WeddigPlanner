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
using System.Windows.Threading;

namespace Eskuvo_tervezo.Pages
{
    /// <summary>
    /// Interaction logic for Radio.xaml
    /// </summary>
    /// 
    public delegate void ChangeRadioChannel(string Url, bool sound);
    public delegate void ChangeRadioChannelVolume(int Volume);

    public partial class Radio : Page
    {
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Models.Login User;

        Windows.Functions f = new Windows.Functions();
        Windows.Home h;
        MainWindow main;

        List<Models.Radio> RadioStuffs = new List<Models.Radio>();
        DispatcherTimer Timer = new DispatcherTimer();

        string[] ResourceNames;
        object rm;
        bool Sound = true;
        bool IsVolIncrease = false;

        public Radio(Models.Login _User, ResourceManager _rm, string[] _Resourcenames, MainWindow _main, Windows.Home _h)
        {
            InitializeComponent();
            rm = _rm;
            ResourceNames = _Resourcenames;
            User = _User;
            main = _main;
            h = _h;
            Windows.RefreshRadioChannelList re = CB_Reload;


        }
        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFormats((rm as ResourceManager),ResourceNames);

            Models.Radio rad = WPE.Radio.FirstOrDefault(x => (x.UserID.Equals(User.IDLogin) && x.Chosen == true));
            int vol = rad != null ? (int)rad.Volume : 100;
            if (rad != null)
            {
                main.SetRadioPlayerVolume(vol);
                ScbVolume.Value = vol;
                LB_Volume.Content = LB_Volume.Content.ToString().Split(':')[0] + ": " + ScbVolume.Value;
                Sound = false;
                CB_RadioList.SelectedIndex = RadioStuffs.IndexOf(rad);
                Sound = true;
            }
            else
            {
                ScbVolume.Value = vol;
                LB_Volume.Content = LB_Volume.Content.ToString().Split(':')[0] + ": " + ScbVolume.Value;
            }
            Timer.Interval = TimeSpan.FromMilliseconds(100);
            Timer.Tick += Timer_Tick;
        }

        internal void LoadFormats(ResourceManager _rm, string[] ResourceNames)
        {
            rm = _rm;
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);

                if (it is Label)
                    (it as Label).Content = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (it is Button)
                    (it as Button).Content = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (it is TextBlock)
                    (it as TextBlock).Text = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (it is MaterialDesignThemes.Wpf.PackIcon)
                    (it as MaterialDesignThemes.Wpf.PackIcon).ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
            }
            IconVisibility();
            CB_Reload();
        }
        internal void CB_Reload()
        {
            WPE = new Models.WeddingPlannerEntities();
            CB_RadioList.Items.Clear();
            RadioStuffs.Clear();
            if (WPE.Radio.Any(x => (x.UserID.Equals(User.IDLogin) || x.UserID.Equals(0))))
            {
                foreach (var item in WPE.Radio.Where(x=> (x.UserID.Equals(User.IDLogin) || x.UserID.Equals(0))))
                {
                    CB_RadioList.Items.Add(item.ChannelName.Trim());
                    RadioStuffs.Add(item);
                }
            }
        }
        internal void RadioChannelChange(int RadioIndex)
        {
            ChangeRadioChannel cha = main.PlayMusicFromURL;
            cha(RadioStuffs[RadioIndex].StreamLink.Trim(), Sound);
            if (main.bgwRadioHigh.IsBusy)
            {
                main.bgwRadioHigh.CancelAsync();
                main.bgwRadioHigh = new System.ComponentModel.BackgroundWorker();
            }
            main.bgwRadioHigh.RunWorkerAsync();
            h.Tbl_Radio.Text = "♫ " + RadioStuffs[RadioIndex].ChannelName.Trim() + " ♫";

            if (!main.IsRadioPlaying())
            {
                h.IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeHigh;
                h.RadioAnimationStart();
            }
        }

        void Save_Channel()
        {
            if (f.isRadioLink(TB_StreamLink, TB_StreamLink.Text.Trim(), (rm as ResourceManager)) && f.IsNormalText(TB_RadioChannelName, TB_RadioChannelName.Text.Trim(), (rm as ResourceManager)))
            {
                if(!WPE.Radio.Any(x=> x.UserID.Equals(User.IDLogin) && x.StreamLink.Trim().Equals(TB_StreamLink.Text.Trim()) && x.ChannelName.Trim().Equals(TB_RadioChannelName.Text.Trim())))
                    {
                    Models.Radio rad = new Models.Radio();
                    rad.UserID = User.IDLogin;
                    rad.StreamLink = TB_StreamLink.Text.Trim();
                    rad.ChannelName = TB_RadioChannelName.Text.Trim();
                    WPE.Radio.Add(rad);
                    WPE.SaveChanges();
                    CB_Reload();
                    }
            }
        }
        void RadioChannelChoose(int id)
        {
            foreach (var item in WPE.Radio.Where(x=>x.UserID.Equals(User.IDLogin)).ToList())
            {
                if (item.ID == id)
                {
                    item.Chosen = true;
                    item.Volume = (int)ScbVolume.Value;
                }
                else
                item.Chosen = false;
                WPE.SaveChanges();
            }          
        }
        void RefreshVolumeLabel(int volume)
        {
            ChangeRadioChannelVolume chav = main.SetRadioPlayerVolume;
            chav(volume);
            LB_Volume.Content = LB_Volume.Content.ToString().Split(':')[0] + ": " + volume;
        }
        void IconVisibility()
        {
            if (CB_RadioList.SelectedItem == null)
            {
                Tooltip_Delete.Visibility = Visibility.Hidden;
                Tooltip_Modification.Visibility = Visibility.Hidden;
            }
            else
            {
                Tooltip_Delete.Visibility = Visibility.Visible;
                Tooltip_Modification.Visibility = Visibility.Visible;
            }
        }

        void TB_StreamLink_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_SaveChannel_Click(sender, e);
            }
        }
        void TB_RadioChannelName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_SaveChannel_Click(sender, e);
            }
        }
        void BT_SaveChannel_Click(object sender, RoutedEventArgs e)
        {
            Save_Channel();
        }
        void BT_SaveChosenChannel_Click(object sender, RoutedEventArgs e)
        {
            if (CB_RadioList.SelectedIndex > -1)
                RadioChannelChoose(RadioStuffs[CB_RadioList.SelectedIndex].ID);
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            if (IsVolIncrease)
            {
                ScbVolume.Value = ScbVolume.Value + 1;
                RefreshVolumeLabel((int)ScbVolume.Value);
            }
            else
            {
                ScbVolume.Value = ScbVolume.Value - 1;
                RefreshVolumeLabel((int)ScbVolume.Value);
            }
        }
        void ScbVolume_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            RefreshVolumeLabel((int)ScbVolume.Value);
        }
        void CB_RadioList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_RadioList.SelectedIndex > -1 && Sound == true)
            {
                RadioChannelChange(CB_RadioList.SelectedIndex);
                IconVisibility();
            }
        }
        void CB_RadioList_DropDownOpened(object sender, EventArgs e)
        {
            LB_Volume.Opacity = 0.2;
            ScbVolume.Opacity = 0.2;
            BT_SaveChosenChannel.Opacity = 0.2;
            IconIncrease.Opacity = 0.2;
            IconDecrease.Opacity = 0.2;
        }
        void CB_RadioList_DropDownClosed(object sender, EventArgs e)
        {
            LB_Volume.Opacity = 1;
            ScbVolume.Opacity = 1;
            BT_SaveChosenChannel.Opacity = 1;
            IconDecrease.Opacity = 1;
            IconIncrease.Opacity = 1;
        }
        void IconDecrease_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ScbVolume.Value = ScbVolume.Value - 1;
            RefreshVolumeLabel((int)ScbVolume.Value);
            IsVolIncrease = false;
            Timer.Start();
        }
        void IconIncrease_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ScbVolume.Value = ScbVolume.Value + 1;
            RefreshVolumeLabel((int)ScbVolume.Value);
            IsVolIncrease = true;
            Timer.Start();
        }
        void IconDecrease_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Timer.Stop();
        }
        void IconIncrease_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Timer.Stop();
        }
        void IconIncrease_MouseLeave(object sender, MouseEventArgs e)
        {
            Timer.Stop();
        }
        void IconDecrease_MouseLeave(object sender, MouseEventArgs e)
        {
            Timer.Stop();
        }
        void Tooltip_Delete_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.WinMessageBoxItem wmsb = new ViewModel.WinMessageBoxItem((rm as ResourceManager).GetString("Message_Delete_Title"), (rm as ResourceManager).GetString("Message_DeleteRadioChannel"), MaterialDesignThemes.Wpf.PackIconKind.WarningCircle);
            Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, true);

            if (msb.ShowDialog() == true)
            {
                    int indexofChannel = CB_RadioList.SelectedIndex;
                    Models.Radio rad = RadioStuffs[indexofChannel];
                    WPE.Radio.Attach(rad);
                    WPE.Radio.Remove(rad);
                    WPE.SaveChanges();
                    CB_Reload();
                if(CB_RadioList.Items.Count > 0)
                {
                    CB_RadioList.SelectedIndex = indexofChannel > 0 ? indexofChannel - 1 : 0;
                    h.Tbl_Radio.Text = "♫ " + RadioStuffs[CB_RadioList.SelectedIndex].ChannelName.Trim() + " ♫";
                }
            }
        }
        void Tooltip_Modification_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int radID = RadioStuffs[CB_RadioList.SelectedIndex].ID;
            Models.Radio r = WPE.Radio.FirstOrDefault(x => x.ID == radID);
            if (r != null)
            {
                Windows.RadioChannelModify rad = new Windows.RadioChannelModify((rm as ResourceManager), r, ResourceNames, this);
                rad.Show();
            }
        }
    }
    }
