using Eskuvo_tervezo.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eskuvo_tervezo
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Functions f = new Functions();
        MainWindow main;

        string[] ResourceNames;
        ResourceManager rm;
        bool Hun = true;
        bool sound;
        
        public Registration(string[] _ResourceNames, bool hun,bool _sound,MainWindow _main)
        {
            InitializeComponent();
            ResourceNames = _ResourceNames;
            Hun = hun;
            sound = _sound;
            main = _main;
            LoadFormats(Hun);
        }

        bool SignUp()
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Models.Login l = new Models.Login();
            List<Models.Login> LoginData = WPE.Login.ToList();
            if (f.isContactName(TB_user,TB_user.Text,rm) && f.IsPassword(T_passwd,T_passwd.Password, rm) && f.IsPasswordAreEqual(T_passwd,T_passwdAgain,T_passwd.Password,T_passwdAgain.Password,rm) && f.IsValidEmail(TB_email,TB_email.Text.Trim(),rm))
            {
                if (LoginData.FirstOrDefault(x => x.User.Trim().Equals(TB_user.Text)) == null)
                {
                    l.User = TB_user.Text.Trim();
                    l.Password = f.Encrypt(T_passwd.Password.Trim());
                    l.EmailAddress = TB_email.Text.Trim();
                    WPE.Login.Add(l);
                    WPE.SaveChanges();
                    System.Windows.Input.Mouse.OverrideCursor = null;
                    return true;
                }
                else
                {
                    ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems(rm.GetString("LB_Title_reg"), rm.GetString("Message_Reg"), MaterialDesignThemes.Wpf.PackIconKind.InformationCircleOutline);
                    Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames,null);
                    msb.Show();
                    System.Windows.Input.Mouse.OverrideCursor = null;
                    return false;
                }
            }
            else
            {
                System.Windows.Input.Mouse.OverrideCursor = null;
                return false;
            }
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
                else if (ResourceNames[i] == "Title_Reg")
                    this.Title = rm.GetString(ResourceNames[i]);
                else if (it is TextBlock)
                    (it as TextBlock).Text = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i] == "Tooltip_InvalidNameCharacters")
                    TB_user.ToolTip = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i] == "Tooltip_InvalidPasswordCharacters")
                    T_passwd.ToolTip = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i] == "Tooltip_PasswordsAreNotEqual")
                    T_passwd.ToolTip = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i] == "Tooltip_PasswordsAreNotEqual")
                    T_passwdAgain.ToolTip = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i] == "Tooltip_InvalidEmailCharacters")
                    TB_email.ToolTip = rm.GetString(ResourceNames[i]);
            }
            if(sound)
                IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeHigh;
            else
                IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeOff;

        }

        void BT_Reg_Click(object sender, RoutedEventArgs e)
        {
            if(SignUp())
            {
                main.RadioPlayerStop();
                main.Close();
                MainWindow m = new MainWindow(ResourceNames,Hun,sound);
                m.Show();
                this.Close();
            }
        }      
        void LB_Back_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            main.RadioPlayerStop();
            MainWindow m = new MainWindow(ResourceNames,Hun,sound);
            m.Show();
            this.Close();
        }
        void BT_Exit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems(rm.GetString("ExitWarning"), rm.GetString("Exit"), MaterialDesignThemes.Wpf.PackIconKind.QuestionMarkRhombus);
            Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames,null);

            if (msb.ShowDialog() == true)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
        void ImUK_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Hun = false;
            LoadFormats(Hun);
        }
        void ImHU_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Hun = true;
            LoadFormats(Hun);
        }
        void TB_user_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Reg.RaiseEvent(newEventArgs);
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                BT_Exit.RaiseEvent(newEventArgs);
            }
        }
        void TB_email_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Reg.RaiseEvent(newEventArgs);
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                BT_Exit.RaiseEvent(newEventArgs);
            }
        }
        void T_passwd_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Reg.RaiseEvent(newEventArgs);
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                BT_Exit.RaiseEvent(newEventArgs);
            }
        }
        void T_passwdAgain_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Reg.RaiseEvent(newEventArgs);
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                BT_Exit.RaiseEvent(newEventArgs);
            }
        }
        void IconVolumeOnOff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (main.IsRadioPlaying())
            {
                IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeOff;
                main.RadioPlayerStop();
                sound = false;
            }

            else
            {
                IconVolumeOnOff.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeHigh;
                main.RadioPlayerPlay();
                sound = true;
            }
        }
    }
}
