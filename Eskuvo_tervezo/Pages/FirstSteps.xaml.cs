using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
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
    /// Interaction logic for FirstSteps.xaml
    /// </summary>
    public partial class FirstSteps : Page
    {
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Models.Login User;
        Models.WeddingData wedd;

        Windows.Functions f = new Windows.Functions();
        Windows.Home h;

        string[] ResourceNames;
        Byte[] BetrothedImage = null;
        Image DefaultImage = new Image();
        object rm;

        public FirstSteps(Models.Login _User,ResourceManager _rm, string[] _ResourceNames, Windows.Home _h)
        {
            InitializeComponent();
            WPE = new Models.WeddingPlannerEntities();
            rm = _rm;
            ResourceNames = _ResourceNames;
            User = _User;
            h = _h;
            DefaultImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Betrothed.png", UriKind.Absolute));
            Windows.ChangePicture re = BrowsePictures;
            wedd = WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(User.IDLogin));
            //TODO DateTimepicker design
        }
        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFormats((rm as ResourceManager), ResourceNames);
            if (wedd != null)
            {
                TB_BrideName.Text = wedd.BrideName.Trim();
                TB_GroomName.Text = wedd.GroomName.Trim();
                TB_Budget.Text = wedd.Budget.ToString().Trim();
                BetrothedImage = wedd.Image;
                DateP.SelectedDate = wedd.Wedding_Date;
            }
        }
        internal void BrowsePictures()
        {
            try
            {
                OpenFileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                fldlg.Filter = (rm as ResourceManager).GetString("Dialog_ImageFiles") + "(*.jpg; *.jpeg; *.png; *.gif; *.bmp)| *.jpg; *.jpeg; *.png; *.gif; *.bmp";
                if (fldlg.ShowDialog() == true)
                {
                    System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                    System.IO.FileStream fs = null;
                        fs = new System.IO.FileStream(fldlg.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        byte[] imgByteArr = new byte[fs.Length];
                        fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));

                        h.ImageBetrothed.Source = f.CreateBitmapFromBytes(imgByteArr);
                    if(wedd != null)
                    {
                        Models.WeddingData wd = wedd;
                        wd.Image = imgByteArr;
                        WPE.SaveChanges();                 
                    }
                    BetrothedImage = imgByteArr;
                    fs.Close();
                    fldlg = null;

                    System.Windows.Input.Mouse.OverrideCursor = null;
                }
                fldlg = null;
            }
            catch (Exception ex)
            {
                System.Windows.Input.Mouse.OverrideCursor = null;
                ViewModel.WinMessageBoxItem wmsb = new ViewModel.WinMessageBoxItem("Error", ex.Message, MaterialDesignThemes.Wpf.PackIconKind.Error);
                Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, false);
                msb.Show();
            }
        }

        internal void LoadFormats(ResourceManager _rm, string[] ResourceNames)
        {
            rm = _rm;

            if ((rm as ResourceManager).BaseName.Substring((rm as ResourceManager).BaseName.Length - 2).Equals("Hu"))
            {
                rm = new ResourceManager(typeof(Properties.ResourceHu));
                DateP.Language = System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag("Hu").IetfLanguageTag);
            }
            else
            {
                rm = new ResourceManager(typeof(Properties.ResourceEng));
                DateP.Language = System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag("Eng").IetfLanguageTag);
            }
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);

                if (it is Label)
                    (it as Label).Content = (rm as ResourceManager).GetString(ResourceNames[i]);
                else if (it is Button)
                    (it as Button).Content = (rm as ResourceManager).GetString(ResourceNames[i]);
                else if (it is TextBlock)
                    (it as TextBlock).Text = (rm as ResourceManager).GetString(ResourceNames[i]);
                else if (it is PackIcon)
                    (it as PackIcon).ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNameCharacters")
                {
                    if (TB_BrideName.ToolTip != null)
                        TB_BrideName.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNameCharacters")
                {
                    if (TB_GroomName.ToolTip != null)
                        TB_GroomName.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidDateTimeCharacters")
                {
                    if (DateP.ToolTip != null)
                        DateP.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNumberCharacters")
                {
                    if (TB_Budget.ToolTip != null)
                        TB_Budget.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }

            }
            if (wedd != null)
            {
                TB_BrideName.Text = wedd.BrideName;
                TB_GroomName.Text = wedd.GroomName;
                DateP.SelectedDate = wedd.Wedding_Date;
            }
        }
        void Save()
        {
            if (f.IsName(TB_BrideName, TB_BrideName.Text.Trim(), (rm as ResourceManager)) && f.IsName(TB_GroomName, TB_GroomName.Text.Trim(), (rm as ResourceManager)) && 
                f.IsDatetime(DateP, DateP.Text.Trim(), (rm as ResourceManager)) && f.IsNumber(TB_Budget, f.StringRemoveWhiteSpace(TB_Budget.Text.Trim()), (rm as ResourceManager)))
            {
                System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                wedd = WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(User.IDLogin));
                if (wedd != null)
                    {
                    wedd.BrideName = TB_BrideName.Text.Trim();
                    wedd.GroomName = TB_GroomName.Text.Trim();
                    wedd.Wedding_Date = Convert.ToDateTime(DateP.Text.Trim());
                    wedd.Image = BetrothedImage;
                    wedd.Budget = Convert.ToInt32(f.StringRemoveWhiteSpace(TB_Budget.Text));

                    WPE.SaveChanges();
                    }
                else
                {
                    wedd = new Models.WeddingData();
                    wedd.User_ID = User.IDLogin;
                    wedd.BrideName = TB_BrideName.Text.Trim();
                    wedd.GroomName = TB_GroomName.Text.Trim();
                    wedd.Wedding_Date = Convert.ToDateTime(DateP.Text.Trim());
                    wedd.Budget = Convert.ToInt32(f.StringRemoveWhiteSpace(TB_Budget.Text));
                    wedd.Image = BetrothedImage;
                    WPE.WeddingData.Add(wedd);
                    WPE.SaveChanges();
                }
                h.CreateMenu();
                System.Windows.Input.Mouse.OverrideCursor = null;
            }
        }

        void BT_Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
        void TB_Budget_KeyDown(object sender, KeyEventArgs e)
        {
            f.NumericTextBox_PreviewKeyDown(sender, e);
            if (f.StringRemoveWhiteSpace((sender as TextBox).Text).Length > 9)
                e.Handled = true;
            
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
        void TB_Budget_LostFocus(object sender, RoutedEventArgs e)
        {
            f.CurrencyFormat(sender);
        }
        void TB_Budget_Loaded(object sender, RoutedEventArgs e)
        {
            f.CurrencyFormat(sender);
        }
        void TB_BrideName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
        void TB_GroomName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
        void DateP_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
        void DateP_CalendarOpened(object sender, RoutedEventArgs e)
        {
            FirstStepsGrid.Opacity = 0.2;
        }
        void DateP_CalendarClosed(object sender, RoutedEventArgs e)
        {
            FirstStepsGrid.Opacity = 1;
        }

        void BT_BrowsePicture_Click(object sender, RoutedEventArgs e)
        {
            BrowsePictures();
        }
        void Tooltip_ImageReset_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            h.ImageBetrothed.Source = DefaultImage.Source;
            if(wedd != null)
            {
                Models.WeddingData wd = wedd;
                wd.Image = null;
                WPE.SaveChanges();
            }
        }
    }
}
