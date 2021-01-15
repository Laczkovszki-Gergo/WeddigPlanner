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

namespace Eskuvo_tervezo.Pages
{
    /// <summary>
    /// Interaction logic for Guests.xaml
    /// </summary>
    public partial class Guests : Page
    {
        Models.WeddingData Wedding;
        Models.Login User;
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();

        Windows.Functions f = new Windows.Functions();

        List<Models.Guests> guestList = new List<Models.Guests>();

        string[] ResourceNames;
        object rm;
        int gr = 0;
        int br = 0;
        public Guests(Models.Login _User, ResourceManager _rm, string[] _Resourcenames)
        {
            InitializeComponent();
            WPE = new Models.WeddingPlannerEntities();
            rm = _rm;
            ResourceNames = _Resourcenames;
            User = _User;
        }
        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Wedding = WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(User.IDLogin));
            LoadFormats((rm as ResourceManager), ResourceNames);
            BT_ExportToExcel.Visibility = Visibility.Hidden;
            CreateGuestList((rm as ResourceManager));
        }

        internal void LoadFormats(ResourceManager _rm, string[] ResourceNames)
        {
            rm = _rm;

            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);

                if (it is Label)
                {
                    if((it as Label).Name == "LB_BrideGuestCount")
                        LB_BrideGuestCount.Content = (rm as ResourceManager).GetString(ResourceNames[i].ToString()) + ": " + br;
                    else if ((it as Label).Name == "LB_GroomGuestCount")
                        LB_GroomGuestCount.Content = (rm as ResourceManager).GetString(ResourceNames[i].ToString()) + ": " + gr;
                    else
                    (it as Label).Content = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
                else if (it is Button)
                    (it as Button).Content = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (it is TextBlock)
                    (it as TextBlock).Text = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNameCharacters")
                {
                    if (TB_BrideGuests.ToolTip != null)
                        TB_BrideGuests.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNameCharacters")
                {
                    if (TB_GroomGuests.ToolTip != null)
                        TB_GroomGuests.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNumberCharacters")
                {
                    if (TB_BrideGuestsCount.ToolTip != null)
                        TB_BrideGuestsCount.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNumberCharacters")
                {
                    if (TB_GroomGuestsCount.ToolTip != null)
                        TB_GroomGuestsCount.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
            }
            if(Wedding !=null)
            {
                if (Wedding.GroomName != null)
                    LB_Groom.Content = Wedding.GroomName.Trim();
                if (Wedding.BrideName != null)
                    LB_Bride.Content = Wedding.BrideName.Trim();
            }
        }
        internal void CreateGuestList(ResourceManager _rm)
        {
            rm = _rm;
            WPE = new Models.WeddingPlannerEntities();
            gr = 0;
            br = 0;

                guestList = WPE.Guests.Where(x => x.Wedding_ID.Equals(Wedding.ID)).ToList();
                BrideItems.Children.Clear();

                foreach (var item in guestList.Where(x => x.Bride_Groom.Equals(1)).OrderBy(x => x.Guest_Name).ToList())
                {
                    var it = new ViewModel.Guest(item.Guest_Name.Trim(), item.Guest_Count, item.Guest_ID.ToString());
                    BrideItems.Children.Add(new UserControls.UserControlGuests(it,this,ResourceNames, (rm as ResourceManager)));
                    br = br + it.Person;
                }          
                GroomItems.Children.Clear();
                foreach (var item in guestList.Where(x => x.Bride_Groom.Equals(0)).OrderBy(x=>x.Guest_Name).ToList())
                {
                    var it = new ViewModel.Guest(item.Guest_Name.Trim(), item.Guest_Count, item.Guest_ID.ToString());
                    GroomItems.Children.Add(new UserControls.UserControlGuests(it, this, ResourceNames, (rm as ResourceManager)));
                    gr = gr + it.Person;
                }
            LB_BrideGuestCount.Content = (rm as ResourceManager).GetString("LB_BrideGuestCount") + " " + br;
            LB_GroomGuestCount.Content = (rm as ResourceManager).GetString("LB_GroomGuestCount") + " " + gr;

            if(guestList.Count > 0)
                BT_ExportToExcel.Visibility = Visibility.Visible;
            else
                BT_ExportToExcel.Visibility = Visibility.Hidden;

        }
        void SaveGuests(object sender)
        {
            WPE = new Models.WeddingPlannerEntities();
            if((sender as Button).Name == "BT_SaveGroom")
            {
                if (f.IsName(TB_GroomGuests, TB_GroomGuests.Text.Trim(), (rm as ResourceManager)) && f.IsNumber(TB_GroomGuestsCount, f.StringRemoveWhiteSpace(TB_GroomGuestsCount.Text.Trim()), (rm as ResourceManager)))
                {
                    Models.Guests gue = new Models.Guests();
                    gue.Guest_Name = TB_GroomGuests.Text.Trim();
                    gue.Guest_Count = Convert.ToInt32(f.StringRemoveWhiteSpace(TB_GroomGuestsCount.Text.Trim()));
                    gue.Wedding_ID =Wedding.ID;
                    gue.Bride_Groom = 0;
                    WPE.Guests.Add(gue);
                    WPE.SaveChanges();
                    CreateGuestList((rm as ResourceManager));
                }
            }
            else if ((sender as Button).Name == "BT_SaveBride")
            {
                if (f.IsName(TB_BrideGuests, TB_BrideGuests.Text.Trim(), (rm as ResourceManager)) && f.IsNumber(TB_BrideGuestsCount, f.StringRemoveWhiteSpace(TB_BrideGuestsCount.Text.Trim()), (rm as ResourceManager)))
                {
                    Models.Guests gue = new Models.Guests();
                    gue.Guest_Name = TB_BrideGuests.Text.Trim();
                    gue.Guest_Count = Convert.ToInt32(f.StringRemoveWhiteSpace(TB_BrideGuestsCount.Text.Trim()));
                    gue.Wedding_ID = Wedding.ID;
                    gue.Bride_Groom = 1;
                    WPE.Guests.Add(gue);
                    WPE.SaveChanges();
                    CreateGuestList((rm as ResourceManager));
                }
            }       
        }
        void CreateExcelDocument()
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks xlWorkBooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlWorkBooks.Add(1);
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            try
            {
                xlWorkSheet.Name = LB_Guests.Content.ToString();
                xlWorkSheet.Cells[1, 1] = LB_Bride.Content;
                xlWorkSheet.Cells[1, 4] = LB_Groom.Content;

                Microsoft.Office.Interop.Excel.Range formatRange = xlWorkSheet.get_Range("A1", "D1");
                formatRange.Font.Bold = true;
                formatRange.WrapText = true;

                xlWorkSheet.Range["a1", "b1"].Merge();
                xlWorkSheet.Range["d1", "e1"].Merge();

                List<Models.Guests> GroomGuestList = guestList.Where(x => x.Bride_Groom.Equals(0)).ToList();
                List<Models.Guests> BrideGuestList = guestList.Where(x => x.Bride_Groom.Equals(1)).ToList();

                string rang = "A1:" + "E" + (BrideGuestList.Count >= GroomGuestList.Count ? (BrideGuestList.Count + 1).ToString() : (GroomGuestList.Count + 1).ToString());
                xlWorkSheet.get_Range(rang).Cells.Font.Name = "Comic Sans MS";
                xlWorkSheet.Range[rang].Font.Size = 16;
                xlWorkSheet.Range[rang].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Pink);
                xlWorkSheet.Range[rang].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                xlWorkSheet.Range[rang].Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                formatRange = xlWorkSheet.get_Range(rang);
                formatRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                xlWorkSheet.Range[rang].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                xlWorkSheet.Range[rang].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                for (int i = 0; i < BrideGuestList.Count; i++)
                {
                    xlWorkSheet.Cells[2 + i, 1] = BrideGuestList[i].Guest_Name.Trim();
                    xlWorkSheet.Cells[2 + i, 2] = BrideGuestList[i].Guest_Count;
                    xlWorkSheet.Cells[2 + i, 2].NumberFormat = "0";
                }

                for (int i = 0; i < GroomGuestList.Count; i++)
                {
                    xlWorkSheet.Cells[2 + i, 4] = GroomGuestList[i].Guest_Name.Trim();
                    xlWorkSheet.Cells[2 + i, 5] = GroomGuestList[i].Guest_Count;
                    xlWorkSheet.Cells[2 + i, 5].NumberFormat = "0";
                }
                xlWorkSheet.get_Range(rang).Columns.AutoFit();
                xlApp.DisplayAlerts = false;

                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.FileName = (rm as ResourceManager).GetString("Menu_Guests");
                saveFileDialog.Filter = (rm as ResourceManager).GetString("SaveFileDialogFilter");
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = (rm as ResourceManager).GetString("SaveFileDialogTitle");

                if (saveFileDialog.ShowDialog() == true)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(saveFileDialog.FileName);
                    if (f.IsFileLocked(file, (rm as ResourceManager), ResourceNames) == false)
                    {
                        xlWorkBook.SaveAs(saveFileDialog.FileName);
                        ViewModel.WinMessageBoxItem wmsgbi = new ViewModel.WinMessageBoxItem((rm as ResourceManager).GetString("MessageBoxSaveTitle"), (rm as ResourceManager).GetString("MessageBoxSaveText"), MaterialDesignThemes.Wpf.PackIconKind.InformationCircle);
                        Windows.WinMessageBox wmsg = new Windows.WinMessageBox(wmsgbi, (rm as ResourceManager), ResourceNames, false);
                        wmsg.Show();
                    }
                }
                xlWorkBook.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkSheet);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp);
            }
            catch (Exception ex)
            {
                System.Windows.Input.Mouse.OverrideCursor = null;
                ViewModel.WinMessageBoxItem wmsb = new ViewModel.WinMessageBoxItem("Error", ex.Message, MaterialDesignThemes.Wpf.PackIconKind.Error);
                Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, false);
                msb.Show();

                xlWorkBook.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkSheet);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp);
            }
            System.Windows.Input.Mouse.OverrideCursor = null;
        }

        internal void DeleteCLick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = 0;
            Int32.TryParse((string)(sender as PackIcon).DataContext, out id);
            ViewModel.WinMessageBoxItem wmsb = new ViewModel.WinMessageBoxItem((rm as ResourceManager).GetString("Message_Delete_Title"), (rm as ResourceManager).GetString("Message_DeleteGuest"), PackIconKind.WarningCircle);
            Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, true);

            if (msb.ShowDialog() == true)
            {
                if (WPE.Guests.FirstOrDefault(x => x.Guest_ID.Equals(id)) != null)
                {
                    WPE.Dispose();
                    WPE = new Models.WeddingPlannerEntities();
                    var gue = new Models.Guests { Guest_ID = id };
                    WPE.Guests.Attach(gue);
                    WPE.Guests.Remove(gue);
                    WPE.SaveChanges();
                    CreateGuestList((rm as ResourceManager));
                }
            }
        }
        void BT_SaveBride_Click(object sender, RoutedEventArgs e)
        {
            SaveGuests(sender);
        }
        void BT_SaveGroom_Click(object sender, RoutedEventArgs e)
        {
            SaveGuests(sender);
        }
        void TB_BrideGuests_PreviewKeyDown(object sender, KeyEventArgs e)
        {         
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_SaveBride_Click(sender, e);
            }
        }
        void TB_BrideGuestsCount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            f.NumericTextBox_PreviewKeyDown(sender, e);
            if (f.StringRemoveWhiteSpace((sender as TextBox).Text).Length > 8)
                e.Handled = true;
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_SaveBride_Click(sender, e);
            }
        }
        void TB_GroomGuests_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_SaveGroom_Click(sender, e);
            }
        }
        void TB_GroomGuestsCount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            f.NumericTextBox_PreviewKeyDown(sender, e);
            if (f.StringRemoveWhiteSpace((sender as TextBox).Text).Length > 8)
                e.Handled = true;
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_SaveGroom_Click(sender, e);
            }
        }
        void BT_ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            CreateExcelDocument();
        }
    }
}
