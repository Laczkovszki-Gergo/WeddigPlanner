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
    /// Interaction logic for Expenses.xaml
    /// </summary>
    public partial class Expenses : Page
    {
        Models.WeddingData Wedding;
        Models.WeddingExpenses Exp;
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Models.Login User;
        

        ViewModel.VenueItem e;
        Windows.Functions f = new Windows.Functions();

        List<Models.WeddingExpenses> expList = new List<Models.WeddingExpenses>();
        List<ViewModel.VenueItem> exitems = new List<ViewModel.VenueItem>();

        string[] ResourceNames;
        int[] VenueIDs;

        Int64? SumCost = 0;
        object rm;
        int? Budget = 0;
        System.Windows.Media.Brush TextColor = null;

        public Expenses(Models.Login _User, ResourceManager _rm, string[] _Resourcenames)
        {
            InitializeComponent();
            TextColor = LB_Offer.Foreground;
            WPE = new Models.WeddingPlannerEntities();
            rm = _rm;
            User = _User;
            ResourceNames = _Resourcenames;
            Windows.RefreshExpenseList re = CreateExpenseList;
        }
        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Wedding = WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(User.IDLogin));
            LoadFormats((rm as ResourceManager), ResourceNames);
            CB_Reload();
            LB_Expense.IsEnabled = false;
            LB_Cost.IsEnabled = false;
            LB_Count.IsEnabled = false;
            BT_Save.IsEnabled = false;
            TB_Expense.IsEnabled = false;
            TB_Count.IsEnabled = false;
            TB_Cost.IsEnabled = false;
            BT_ExportToExcel.Visibility = Visibility.Hidden;

        }
        internal void CreateExpenseList(ResourceManager _rm)
        {
            rm = _rm;
            if(CB_Offer.SelectedIndex>-1)
            {
                LB_Expense.IsEnabled = true;
                LB_Cost.IsEnabled =    true;
                LB_Count.IsEnabled =   true;
                BT_Save.IsEnabled =    true;
                TB_Expense.IsEnabled = true;
                TB_Count.IsEnabled =   true;
                TB_Cost.IsEnabled =    true;

                int id = VenueIDs[CB_Offer.SelectedIndex];
                WPE = new Models.WeddingPlannerEntities();
                expList = WPE.WeddingExpenses.Where(x => x.VenueID.Equals(id)).OrderBy(x => x.ExpenseName).ToList();
                ExpenseItems.Children.Clear();
                foreach (var item in expList)
                {
                    var exp = new ViewModel.Expense(item.ExpenseName.Trim(), f.StringCurrencyFormat(item.Expense.ToString().Trim()), item.ID, f.StringCurrencyFormat(item.Count.ToString().Trim()));
                    ExpenseItems.Children.Add(new UserControls.UserControlExpenses(exp, (rm as ResourceManager), ResourceNames, this));
                }
                Amount_Reload();

                if (ExpenseItems.Children.Count > 0)
                    BT_ExportToExcel.Visibility = Visibility.Visible;
                else
                    BT_ExportToExcel.Visibility = Visibility.Hidden;
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

                xlWorkSheet.Name = (rm as ResourceManager).GetString("LB_Expenses");
                xlWorkSheet.Cells[1, 1] = exitems[CB_Offer.SelectedIndex].Venue.Trim() + " - " +exitems[CB_Offer.SelectedIndex].Address.Trim();

                Microsoft.Office.Interop.Excel.Range formatRange = xlWorkSheet.get_Range("A1", "D1");
                formatRange.Font.Bold = true;
                formatRange.WrapText = true;

                xlWorkSheet.Range["a1", "D1"].Merge();

                string rang = "A1:" + "D" + (expList.Count +3);
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

                xlWorkSheet.Cells[2, 1] = LB_Expense.Content;
                xlWorkSheet.Cells[2, 2] = LB_Cost.Content;
                xlWorkSheet.Cells[2, 3] = LB_Count.Content;
                xlWorkSheet.Cells[2, 4] = (rm as ResourceManager).GetString("LB_Amount");

                int sum = 0;
                for (int i = 0; i < expList.Count; i++)
                {
                    xlWorkSheet.Cells[3 + i, 1] = expList[i].ExpenseName.Trim(); ;
                    xlWorkSheet.Cells[3 + i, 2] = f.StringCurrencyFormat(expList[i].Expense.ToString());
                    xlWorkSheet.Cells[3 + i, 3] = f.StringCurrencyFormat(expList[i].Count.ToString());
                    xlWorkSheet.Cells[3 + i, 4] = f.StringCurrencyFormat((expList[i].Expense * expList[i].Count).ToString());
                    xlWorkSheet.Cells[3 + i, 2].NumberFormat = "0";
                    xlWorkSheet.Cells[3 + i, 3].NumberFormat = "0";
                    xlWorkSheet.Cells[3 + i, 4].NumberFormat = "0";
                sum += expList[i].Expense * expList[i].Count;
                }

            xlWorkSheet.Range["A"+ (expList.Count + 3), "C"+ (expList.Count+3)].Merge();

            xlWorkSheet.Cells[expList.Count +3,1] = (rm as ResourceManager).GetString("LB_Amount");
            xlWorkSheet.Cells[expList.Count + 3,4] = f.StringCurrencyFormat(sum.ToString());

            xlWorkSheet.get_Range(rang).Columns.AutoFit();

            rang = "A" + (expList.Count + 3) + ":" + "D" + (expList.Count + 3);
            formatRange = xlWorkSheet.get_Range(rang);
            formatRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
            Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
            xlWorkSheet.Range[rang].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            xlWorkSheet.Range[rang].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            xlApp.DisplayAlerts = false;

                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

                int venlenght = exitems[CB_Offer.SelectedIndex].Venue.Length;
                int addlenght = exitems[CB_Offer.SelectedIndex].Address.Length;

                saveFileDialog.FileName = (rm as ResourceManager).GetString("LB_Expenses") + "_" + exitems[CB_Offer.SelectedIndex].Venue.Substring(0, venlenght < 15 ? venlenght:15) + "_" + exitems[CB_Offer.SelectedIndex].Address.Substring(0, addlenght <15 ? addlenght:15);
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

        internal void LoadFormats(ResourceManager _rm, string[] ResourceNames)
        {
            rm = _rm;
            LB_Amount.Content = string.Empty;
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);

                if (it is Label)
                    (it as Label).Content = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (it is Button)
                    (it as Button).Content = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (it is TextBlock)
                    (it as TextBlock).Text = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNameCharacters")
                {
                    if (TB_Expense.ToolTip != null)
                        TB_Expense.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNumberCharacters")
                {
                    if (TB_Cost.ToolTip != null)
                        TB_Cost.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNumberCharacters")
                {
                    if (TB_Count.ToolTip != null)
                        TB_Count.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
            }
            Amount_Reload();
        }

        void CB_Reload()
        {
            if (WPE.WeddingVenue.Any(x => x.WeddingID.Equals(Wedding.ID)))
            {
                Models.WeddingVenue[] Venues = WPE.WeddingVenue.Where(x => x.WeddingID.Equals(Wedding.ID)).ToArray();
                VenueIDs = Venues.Select(x => x.ID).ToArray();
                exitems = new List<ViewModel.VenueItem>();
                for (int i = 0; i < Venues.Length; i++)
                {
                    e = new ViewModel.VenueItem(Venues[i].Wedding_Venue, Venues[i].Venue_Address);
                    exitems.Add(e);
                }
                CB_Offer.ItemsSource = exitems;
                ExpenseItems.Children.Clear();
            }
        }
        void Amount_Reload()
        {
            LB_Amount.Content = (rm as ResourceManager).GetString(LB_Amount.Name);
            if (CB_Offer.SelectedIndex > -1)
            {
                int venID = VenueIDs[CB_Offer.SelectedIndex];
                SumCost = WPE.WeddingExpenses.Any(x => x.VenueID.Equals(venID)) ? (Int64)WPE.WeddingExpenses.Where(x => x.VenueID.Equals(venID)).Select(x => (Int64)x.Expense * (Int64)x.Count).Sum() : 0;
                Budget = WPE.WeddingData.Any(x => x.User_ID.Equals(Wedding.User_ID)) ? WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(Wedding.User_ID)).Budget : 0;

                LB_Amount.Content += SumCost != 0 && Budget != 0 ? " " + f.StringCurrencyFormat(SumCost.ToString().Trim()) + " / " + f.StringCurrencyFormat(Budget.ToString()) : " 0 / " + f.StringCurrencyFormat(Budget.ToString());
            }
            else
                LB_Amount.Content += " 0 / " + f.StringCurrencyFormat(WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(Wedding.User_ID)).Budget.ToString());

            if (Budget >= SumCost)
                LB_Amount.Foreground = TextColor;
            else
                LB_Amount.Foreground = Brushes.Red;
        }
        void SaveExpense()
        {
            if (CB_Offer.SelectedIndex > -1)
            {
                if (f.IsName(TB_Expense, TB_Expense.Text.Trim(), (rm as ResourceManager)) && f.IsNumber(TB_Cost, f.StringRemoveWhiteSpace(TB_Cost.Text.Trim()), (rm as ResourceManager)) && f.IsNumber(TB_Count, f.StringRemoveWhiteSpace(TB_Count.Text.Trim()), (rm as ResourceManager)))
                {
                    Exp = WPE.WeddingExpenses.FirstOrDefault(x => x.ID.Equals(Wedding.ID));
                    Models.WeddingExpenses wexp = new Models.WeddingExpenses();
                    wexp.ExpenseName = TB_Expense.Text.Trim();
                    wexp.Expense = Convert.ToInt32(f.StringRemoveWhiteSpace(TB_Cost.Text.Trim()));
                    wexp.VenueID = VenueIDs[CB_Offer.SelectedIndex];
                    wexp.Count = Convert.ToInt32(f.StringRemoveWhiteSpace(TB_Count.Text));
                    WPE.WeddingExpenses.Add(wexp);
                    WPE.SaveChanges();
                    CreateExpenseList((rm as ResourceManager));
                }
            }
        }

        internal void DeleteExpense(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = 0;
            Int32.TryParse((sender as PackIcon).DataContext.ToString(), out id);

            ViewModel.WinMessageBoxItem wmsb = new ViewModel.WinMessageBoxItem((rm as ResourceManager).GetString("Message_Delete_Title"), (rm as ResourceManager).GetString("Message_DeleteExp"), PackIconKind.WarningCircle);
            Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, true);

            if (msb.ShowDialog() == true)
            {
                if (WPE.WeddingExpenses.Any(x => x.ID.Equals(id)))
                {
                    WPE.Dispose();
                    WPE = new Models.WeddingPlannerEntities();
                    var ex = new Models.WeddingExpenses { ID = id };
                    WPE.WeddingExpenses.Attach(ex);
                    WPE.WeddingExpenses.Remove(ex);
                    WPE.SaveChanges();
                    CreateExpenseList((rm as ResourceManager));
                }
            }
        }
        void BT_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveExpense();
        }
        void CB_Offer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateExpenseList((rm as ResourceManager));
        }
        void TB_Count_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            f.NumericTextBox_PreviewKeyDown(sender, e);

            if (f.StringRemoveWhiteSpace((sender as TextBox).Text).Length > 8)
                e.Handled = true;

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
        void TB_Cost_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            f.NumericTextBox_PreviewKeyDown(sender, e);

            if (f.StringRemoveWhiteSpace((sender as TextBox).Text).Length > 8)
                e.Handled = true;

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
        void TB_Expense_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
        void CB_Offer_DropDownOpened(object sender, EventArgs e)
        {
            ExpenseItems.Opacity = 0.2;
        }
        void CB_Offer_DropDownClosed(object sender, EventArgs e)
        {
            ExpenseItems.Opacity = 1;
        }
        void BT_ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            CreateExcelDocument();
        }
    }
}
