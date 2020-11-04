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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        Models.Login ActualUser = new Models.Login();
        Models.WeddingPlannerEntities WPE;
        Windows.Home h;
        Windows.Functions f = new Windows.Functions();

        RoutedEventArgs newEventArgs;

        string[] Resourcenames;
        object rm;

        public Settings(Models.Login _ActualUser,ResourceManager _rm, string[] _Resourcenames, Windows.Home _h)
        {
            InitializeComponent();
            Resourcenames = _Resourcenames;
            rm = _rm;
            ActualUser = _ActualUser;
            h = _h;
        }
        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WPE = new Models.WeddingPlannerEntities();
            LoadFormats((rm as ResourceManager),Resourcenames);
            ReadUserDatas();
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
                else if (ResourceNames[i] == "Tooltip_InvalidNameCharacters")
                {
                    if(TB_user.ToolTip != null)
                    TB_user.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }

                else if (ResourceNames[i] == "Tooltip_InvalidPasswordCharacters")
                {
                    if(T_passwd.ToolTip != null)
                    T_passwd.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }

                else if (ResourceNames[i] == "Tooltip_PasswordsAreNotEqual")
                {
                    if(T_passwd.ToolTip != null)
                    T_passwd.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }

                else if (ResourceNames[i] == "Tooltip_PasswordsAreNotEqual")
                {
                    if(T_passwdAgain.ToolTip != null)
                    T_passwdAgain.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }

                else if (ResourceNames[i] == "Tooltip_InvalidEmailCharacters")
                {
                    if(TB_email.ToolTip != null)
                    TB_email.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }

                else if (ResourceNames[i] == "Tooltip_InvalidOldPassword")
                {
                    if(T_OldPassword.ToolTip != null)
                    T_OldPassword.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }

            }
        }

        void Modification()
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            var result = WPE.Login.SingleOrDefault(b => b.IDLogin == ActualUser.IDLogin);

            if(result != null && f.IsYourPassword(T_OldPassword, T_OldPassword.Password, ActualUser, (rm as ResourceManager)))
            {

                if (TB_user.Text != string.Empty)
                {
                    if (f.isContactName(TB_user, TB_user.Text.Trim(), (rm as ResourceManager)))
                        result.User = TB_user.Text.Trim();
                }
                if(T_passwd.Password != string.Empty || T_passwdAgain.Password != string.Empty)
                {
                    if (f.IsPassword(T_passwd, T_passwd.Password, (rm as ResourceManager)) && f.IsPasswordAreEqual(T_passwd, T_passwdAgain, T_passwd.Password, T_passwdAgain.Password, (rm as ResourceManager)))
                        result.Password = f.Encrypt(T_passwd.Password.Trim());

                }
                if(TB_email.Text != string.Empty)
                {
                    if (f.IsValidEmail(TB_email, TB_email.Text.Trim(), (rm as ResourceManager)))
                        result.EmailAddress = TB_email.Text.Trim();
                }
                WPE.SaveChanges();
                WPE = new Models.WeddingPlannerEntities();
                ActualUser = WPE.Login.FirstOrDefault(x => x.IDLogin.Equals(ActualUser.IDLogin));
                h.LB_TitleHome.Content = h.LB_TitleHome.Content.ToString().Split(' ')[0] + " " + ActualUser.User.Trim();
            }
            System.Windows.Input.Mouse.OverrideCursor = null;
        }
        void ReadUserDatas()
        {
            TB_user.Text = ActualUser.User.Trim();
            TB_email.Text = ActualUser.EmailAddress.Trim();
        }

        void BT_Save_Click(object sender, RoutedEventArgs e)
        {
            Modification();
        }
        void TB_user_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            newEventArgs = new RoutedEventArgs(Button.ClickEvent);
            if (e.Key == System.Windows.Input.Key.Enter)           
                BT_Save.RaiseEvent(newEventArgs);
          
        }
        void TB_email_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            newEventArgs = new RoutedEventArgs(Button.ClickEvent);

            if (e.Key == System.Windows.Input.Key.Enter)
                BT_Save.RaiseEvent(newEventArgs);

        }
        void T_passwd_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            newEventArgs = new RoutedEventArgs(Button.ClickEvent);
            if (e.Key == System.Windows.Input.Key.Enter)
                BT_Save.RaiseEvent(newEventArgs);

        }
        void T_passwdAgain_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            newEventArgs = new RoutedEventArgs(Button.ClickEvent);

            if (e.Key == System.Windows.Input.Key.Enter)
                BT_Save.RaiseEvent(newEventArgs);
        }
        void T_OldPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            newEventArgs = new RoutedEventArgs(Button.ClickEvent);

            if (e.Key == System.Windows.Input.Key.Enter)
                BT_Save.RaiseEvent(newEventArgs);
        }
    }
}
