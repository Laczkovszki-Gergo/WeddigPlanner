using Eskuvo_tervezo.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.EntityClient;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Windows;
using System.Windows.Controls;

namespace Eskuvo_tervezo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal System.ComponentModel.BackgroundWorker bgwRadioHigh = new System.ComponentModel.BackgroundWorker();

        WMPLib.WindowsMediaPlayer RadioPlayer = new WMPLib.WindowsMediaPlayer();
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        ResourceManager rm;
        Models.Login lgin = new Models.Login();
        Functions f = new Functions();

        List<string> ResourceNamesList = new List<string>();
        string[] ResourceNames;
        bool Hun = true;
        internal int RadioVolume = 100;
        int vol = 0;
        bool sound = true;

        public MainWindow()
        {
            InitializeComponent();
            Models.Login acc = new Models.Login();
            ReadResources();
            Hun = true;
            bgwRadioHigh.DoWork += BgwRadioHigh_DoWork;
            bgwRadioHigh.WorkerSupportsCancellation = true;
        }
        public MainWindow(string[] _ResourceNames, bool hun,bool _sound)
        {
            InitializeComponent();
            ResourceNames = _ResourceNames;
            Hun = hun;
            sound = _sound;
            bgwRadioHigh.DoWork += BgwRadioHigh_DoWork;
            bgwRadioHigh.WorkerSupportsCancellation = true;
        }

        void Login_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFormats(Hun);
            Tbl_RePassword.Visibility = Visibility.Hidden;
            if(sound)          
                IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeHigh;
            else
                IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeOff;

            PlayMusicFromURL("http://stream.radio1.hu/high.mp3.xspf", sound);
            bgwRadioHigh.RunWorkerAsync();
        }
        void LoadFormats(bool hun)
        {           
            if (hun == true)
                rm = new ResourceManager(typeof(Properties.ResourceHu));           
            else
                rm = new ResourceManager(typeof(Properties.ResourceEng));
                           
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);

                if (it is CheckBox)
                    (it as CheckBox).Content = rm.GetString(ResourceNames[i]);
                else if (it is Label)
                    (it as Label).Content = rm.GetString(ResourceNames[i]);
                else if (it is Button)
                    (it as Button).Content = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i] == "Title_Login")
                    this.Title = rm.GetString(ResourceNames[i]);
                else if (it is TextBlock)
                    (it as TextBlock).Text = rm.GetString(ResourceNames[i]);
            }
        }
        void ReadResources()
        {
            ResourceManager MyResourceClass =
                new ResourceManager(typeof(Properties.Resources));

            ResourceSet resourceSet =
                MyResourceClass.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

            foreach (DictionaryEntry entry in resourceSet)
            {
                //string resourceKey = entry.Key.ToString();
                //object resource = entry.Value;
                ResourceNamesList.Add(entry.Key.ToString());
            }
            Pages.ChangeRadioChannel cha = PlayMusicFromURL;
            Pages.ChangeRadioChannelVolume chav = SetRadioPlayerVolume;

            ResourceNames = ResourceNamesList.ToArray();
        }
        void LogIn()
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            string ScryptedPassw = f.Encrypt(T_passwd.Password);
            List<Models.Login> LoginData = WPE.Login.ToList();

            if (LoginData.FirstOrDefault(x => x.User.Trim().Equals(TB_user.Text.Trim()) && x.Password.Trim().Equals(ScryptedPassw.Trim())) != null)
            {
                bgwRadioHigh.CancelAsync();
                lgin = LoginData.FirstOrDefault(x => x.User.Trim().Equals(TB_user.Text) && x.Password.Trim().Equals(ScryptedPassw));
                LogDatas(lgin.IDLogin);
                Windows.Home h = new Windows.Home(ResourceNames, Hun, lgin,sound, this);
                h.Show();
                this.Hide();
            }
            else
            {
                Tbl_Error.Visibility = Visibility.Visible;
                Tbl_RePassword.Visibility = Visibility.Visible;
            }
            System.Windows.Input.Mouse.OverrideCursor = null;
        }
        void LogDatas(int UserId)
        {
            Models.Log l = new Models.Log();
            l.TimeStamp = DateTime.Now;
            l.UserID = UserId;
            l.WindowsUser = Environment.UserName;
            l.IP_Address = f.GetIPAddress(rm, ResourceNames) != null ? f.GetIPAddress(rm, ResourceNames).ToString() : null;
            l.Mac_Address = f.GetMacAddress();
            WPE.Log.Add(l);
            WPE.SaveChanges();
        }
        void ReNewPassword()
        {
            if (f.IsName(TB_user, TB_user.Text.Trim(), rm))
            {
                ViewModel.WinMessageBoxItem wmsb = new ViewModel.WinMessageBoxItem(rm.GetString("Message_RenewPasswordTitle"), rm.GetString("Message_RenewPassword"), MaterialDesignThemes.Wpf.PackIconKind.WarningCircle);
                Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, null);

                if (msb.ShowDialog() == true)
                {
                    System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    string GeneratedPassword = GeneratePassword();
                    if (PasswordModification(TB_user.Text.Trim(), GeneratedPassword))
                    {
                        string Useremail = WPE.Login.FirstOrDefault(x => x.User.Equals(TB_user.Text)).EmailAddress.Trim();
                        var fromAddress = new System.Net.Mail.MailAddress(rm.GetString("Send_EmailFrom").Trim(), "Wedding Planner");
                        var toAddress = new System.Net.Mail.MailAddress(Useremail.Trim(), "To Name");
                        const string fromPassword = "WeddingPlanner01";
                        string subject = rm.GetString("Send_EmailSubject").Trim();
                        string body = rm.GetString("Send_EmailBody1") + TB_user.Text.Trim() + "!\n" + rm.GetString("Send_EmailBody2") + "\n\n" + GeneratedPassword + "\n\n" + rm.GetString("Send_EmailBody3");

                        var smtp = new System.Net.Mail.SmtpClient
                        {
                            Host = rm.GetString("Send_EmailHost").Trim(),
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword),
                            Timeout = 20000

                        };
                        using (var message = new System.Net.Mail.MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = body
                        })
                        {
                            smtp.Send(message);
                        }
                    }
                }
            }
            System.Windows.Input.Mouse.OverrideCursor = null;
        }

        string GeneratePassword()
        {
            string dictionaryString1 = "abcdefghijklmnopqrstuvwxyz";
            string dictionaryString2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string dictionaryString3 = "0123456789";
            Random rnd = new Random();
            System.Text.StringBuilder resultStringBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                resultStringBuilder.Append(dictionaryString1[rnd.Next(0, dictionaryString1.Length)]);
                resultStringBuilder.Append(dictionaryString2[rnd.Next(0, dictionaryString2.Length)]);
                resultStringBuilder.Append(dictionaryString3[rnd.Next(0, dictionaryString3.Length)]);
            }
            return resultStringBuilder.ToString();
        }
        bool PasswordModification(string UserName, string GeneratedPassword)
        {
            var result = WPE.Login.SingleOrDefault(b => b.User == UserName.Trim());
            if (result != null && UserName.Equals(result.User.Trim()))
            {
                result.Password = f.Encrypt(GeneratedPassword.Trim());
                WPE.SaveChanges();
                return true;
            }
            else
                return false;
        }

        internal bool IsRadioPlaying()
        {
            return (int)RadioPlayer.playState == 3 ? true : false;
        }
        internal void PlayMusicFromURL(string url, bool _sound)
        {
            RadioPlayer.URL = url;

            if (_sound)
                RadioPlayerPlay();
            else
                RadioPlayerStop();

        }
        internal void RadioPlayerStop()
        {
            RadioPlayer.controls.stop();
        }
        internal void SetRadioPlayerVolume(int volume)
        {
            try
            {
                    RadioPlayer.settings.volume = volume;
                    RadioVolume = volume;
            }
            catch
            {

            }
        }
        internal void RadioPlayerPlay()
        {
            RadioPlayer.controls.play();
        }

        internal void BgwRadioHigh_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            vol = RadioVolume;
            SetRadioPlayerVolume(0);
            for (int i = 1; i < vol; i++)
            {
                if (bgwRadioHigh.CancellationPending == true)
                {
                    e.Cancel = true;
                    //SetRadioPlayerVolume(vol);
                    return;
                }
                SetRadioPlayerVolume(i);
                System.Threading.Thread.Sleep(30);
            }
        }

        void BT_Login_Click(object sender, RoutedEventArgs e)
        {
            LogIn();
        }
        void Tbl_Reg_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Registration reg = new Registration(ResourceNames,Hun, sound,this);
            this.Hide();
            reg.Show();
        }
        void BT_Exit_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.WinMessageBoxItem wmsb = new ViewModel.WinMessageBoxItem(rm.GetString("ExitWarning"), rm.GetString("Exit"), MaterialDesignThemes.Wpf.PackIconKind.QuestionMarkRhombus);
            Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames,null);

            if (msb.ShowDialog() == true)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
        void TB_user_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Login.RaiseEvent(newEventArgs);
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                BT_Exit.RaiseEvent(newEventArgs);
            }
        }
        void T_passwd_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Login.RaiseEvent(newEventArgs);
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                BT_Exit.RaiseEvent(newEventArgs);
            }
        }
        void ImHU_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Hun = true;
            LoadFormats(Hun);
        }
        void ImUK_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Hun = false;
            LoadFormats(Hun);
        }
        void Tbl_RePassword_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ReNewPassword();
        }
        void IconVolumeOnOff_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsRadioPlaying())
            {
                IconVolumeOnOff.Kind = PackIconKind.VolumeOff;
                sound = false;
                RadioPlayerStop();
            }

            else
            {
                IconVolumeOnOff.Kind = PackIconKind.VolumeHigh;
                sound = true;
                RadioPlayerPlay();
            }
        }
    }
}
