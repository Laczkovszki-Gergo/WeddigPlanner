using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Resources;
using System.Windows;

namespace Eskuvo_tervezo.Pages
{
    /// <summary>
    /// Interaction logic for Comparsion.xaml
    /// </summary>
    public partial class Comparsion : Page
    {
        Models.WeddingData Wedding;
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Models.Login User;
        ViewModel.VenueItems e;
        Windows.Functions f = new Windows.Functions();

        List<ViewModel.Comparsion> FirstComp = new List<ViewModel.Comparsion>();
        List<ViewModel.Comparsion> SecondComp = new List<ViewModel.Comparsion>();
        List<Models.WeddingExpenses> expList = new List<Models.WeddingExpenses>();
        List<ViewModel.VenueItems> exitems = new List<ViewModel.VenueItems>();
        Int64? SumCost = 0;
        object rm;
        string[] ResourceNames;
        int[] VenueIDs;
        int? Budget = 0;

        public Comparsion(Models.Login _User, ResourceManager _rm, string[] _Resourcenames)
        {
            InitializeComponent();
            WPE = new Models.WeddingPlannerEntities();
            rm = _rm;
            User = _User;
            ResourceNames = _Resourcenames;
        }
        void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Wedding = WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(User.IDLogin));
            LoadFormats((rm as ResourceManager), ResourceNames);
            BT_ExportToExcel.Visibility = Visibility.Hidden;
            CB_Reload();
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
            }
            CB_Reload();
        }

        void ShowComparsion()
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            if (CB_FirstOffer.SelectedIndex > -1 & CB_SecondOffer.SelectedIndex > -1)
            {
                BT_ExportToExcel.Visibility = Visibility.Visible;
                int firstID = VenueIDs[CB_FirstOffer.SelectedIndex];
                int secondID = VenueIDs[CB_SecondOffer.SelectedIndex];
                WPE = new Models.WeddingPlannerEntities();
                expList = WPE.WeddingExpenses.ToList();
                ComparsionItemsFirst.Children.Clear();
                ComparsionItemsSecond.Children.Clear();
                FirstComp.Clear();
                SecondComp.Clear();

                foreach (var item in expList.Where(x => x.VenueID.Equals(firstID)).OrderBy(x => x.ExpenseName.Trim()).ToList())
                {
                    ViewModel.Comparsion com = new ViewModel.Comparsion(item.ExpenseName.Trim(), f.StringCurrencyFormat(item.Expense.ToString()), f.StringCurrencyFormat(item.Count.ToString()));
                    ComparsionItemsFirst.Children.Add(new UserControls.UserControlComparsion(com));
                    FirstComp.Add(com);
                }
                foreach (var item in expList.Where(x => x.VenueID.Equals(secondID)).OrderBy(x => x.ExpenseName.Trim()).ToList())
                {
                    ViewModel.Comparsion com = new ViewModel.Comparsion(item.ExpenseName.Trim(), f.StringCurrencyFormat(item.Expense.ToString().Trim()), f.StringCurrencyFormat(item.Count.ToString().Trim()));
                    ComparsionItemsSecond.Children.Add(new UserControls.UserControlComparsion(com));
                    SecondComp.Add(com);
                }
                Amount_Reload();
            }
            System.Windows.Input.Mouse.OverrideCursor = null;
        }
        void CB_Reload()
        {
            ComparsionItemsFirst.Children.Clear();
            ComparsionItemsSecond.Children.Clear();
            BT_ExportToExcel.Visibility = Visibility.Hidden;
            if (WPE.WeddingVenue.Any(x => x.WeddingID.Equals(Wedding.ID)))
            {
                ComparsionItemsFirst.Children.Clear();
                ComparsionItemsSecond.Children.Clear();
                Models.WeddingVenue[] Venues = WPE.WeddingVenue.Where(x => x.WeddingID.Equals(Wedding.ID)).ToArray();
                VenueIDs = Venues.Select(x => x.ID).ToArray();
                exitems = new List<ViewModel.VenueItems>();
                for (int i = 0; i < Venues.Length; i++)
                {
                    e = new ViewModel.VenueItems(Venues[i].Wedding_Venue.Trim(), Venues[i].Venue_Address.Trim());
                    exitems.Add(e);
                }
                CB_FirstOffer.ItemsSource = exitems;
                CB_SecondOffer.ItemsSource = exitems;
            }
        }
        void Amount_Reload()
        {
            LB_FirstAmount.Content = (rm as ResourceManager).GetString(LB_FirstAmount.Name);
            LB_SecondAmount.Content = (rm as ResourceManager).GetString(LB_SecondAmount.Name);
            if (CB_FirstOffer.SelectedIndex > -1 & CB_SecondOffer.SelectedIndex > -1)
            {
                int venID = VenueIDs[CB_FirstOffer.SelectedIndex];
                SumCost = WPE.WeddingExpenses.Any(x => x.VenueID.Equals(venID)) ? (Int64)WPE.WeddingExpenses.Where(x => x.VenueID.Equals(venID)).Select(x => (Int64)x.Expense * (Int64)x.Count).Sum() : 0;
                Budget = WPE.WeddingData.Any(x => x.User_ID.Equals(Wedding.User_ID)) ? WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(Wedding.User_ID)).Budget : 0;
                LB_FirstAmount.Content += SumCost != 0 && Budget != 0 ? " " + f.StringCurrencyFormat(SumCost.ToString().Trim()) + " / " + f.StringCurrencyFormat(Budget.ToString()) : " 0 / " + f.StringCurrencyFormat(Budget.ToString());

                venID = VenueIDs[CB_SecondOffer.SelectedIndex];
                SumCost = WPE.WeddingExpenses.Any(x => x.VenueID.Equals(venID)) ? (Int64)WPE.WeddingExpenses.Where(x => x.VenueID.Equals(venID)).Select(x => (Int64)x.Expense * (Int64)x.Count).Sum() : 0;
                Budget = WPE.WeddingData.Any(x => x.User_ID.Equals(Wedding.User_ID)) ? WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(Wedding.User_ID)).Budget : 0;
                LB_SecondAmount.Content += SumCost != 0 && Budget != 0 ? " " + f.StringCurrencyFormat(SumCost.ToString().Trim()) + " / " + f.StringCurrencyFormat(Budget.ToString()) : " 0 / " + f.StringCurrencyFormat(Budget.ToString());
            }
            else
            {
                LB_FirstOffer.Content += " 0 / " + f.StringCurrencyFormat(WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(Wedding.User_ID)).Budget.ToString());
                LB_SecondOffer.Content += " 0 / " + f.StringCurrencyFormat(WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(Wedding.User_ID)).Budget.ToString());
            }
            if (Budget >= SumCost)
            {
                LB_FirstOffer.Foreground = System.Windows.Media.Brushes.White;
                LB_SecondOffer.Foreground = System.Windows.Media.Brushes.White;
            }
            else
            {
                LB_FirstOffer.Foreground = System.Windows.Media.Brushes.Red;
                LB_SecondOffer.Foreground = System.Windows.Media.Brushes.Red;
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
                string FirstVenue = exitems[CB_FirstOffer.SelectedIndex].Venue + "_" + exitems[CB_FirstOffer.SelectedIndex].Address;
                string SecondVenue = exitems[CB_SecondOffer.SelectedIndex].Venue + "_" + exitems[CB_SecondOffer.SelectedIndex].Address;

                xlWorkSheet.Name = FirstVenue.Substring(0, 14) + " - " + SecondVenue.Substring(0, 14);
                xlWorkSheet.Cells[1, 1] = FirstVenue;
                xlWorkSheet.Cells[1, 4] = SecondVenue;

                Microsoft.Office.Interop.Excel.Range formatRange = xlWorkSheet.get_Range("A1", "D1");
                formatRange.Font.Bold = true;
                formatRange.WrapText = true;

                xlWorkSheet.Range["a1", "b1"].Merge();
                xlWorkSheet.Range["d1", "e1"].Merge();

                string rang = "A1:" + "E" + (SecondComp.Count >= FirstComp.Count ? (SecondComp.Count + 2).ToString() : (FirstComp.Count + 2).ToString());
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

                for (int i = 0; i < FirstComp.Count; i++)
                {
                    xlWorkSheet.Cells[2 + i, 1] = FirstComp[i].ExpenseName;
                    xlWorkSheet.Cells[2 + i, 2] = FirstComp[i].Expense;
                    xlWorkSheet.Cells[2 + i, 2].NumberFormat = "0";
                }

                for (int i = 0; i < SecondComp.Count; i++)
                {
                    xlWorkSheet.Cells[2 + i, 4] = SecondComp[i].ExpenseName;
                    xlWorkSheet.Cells[2 + i, 5] = SecondComp[i].Expense;
                    xlWorkSheet.Cells[2 + i, 5].NumberFormat = "0";
                }

                xlWorkSheet.Cells[(SecondComp.Count >= FirstComp.Count ? (SecondComp.Count + 2) : (FirstComp.Count + 2)), 1] = LB_FirstAmount.Content.ToString().Split(' ')[0];
                xlWorkSheet.Cells[(SecondComp.Count >= FirstComp.Count ? (SecondComp.Count + 2) : (FirstComp.Count + 2)), 2] = LB_FirstAmount.Content.ToString().Split(' ')[1];
                xlWorkSheet.Cells[(SecondComp.Count >= FirstComp.Count ? (SecondComp.Count + 2) : (FirstComp.Count + 2)), 4] = LB_SecondAmount.Content.ToString().Split(' ')[0];
                xlWorkSheet.Cells[(SecondComp.Count >= FirstComp.Count ? (SecondComp.Count + 2) : (FirstComp.Count + 2)), 5] = LB_SecondAmount.Content.ToString().Split(' ')[1];

                xlWorkSheet.get_Range(rang).Columns.AutoFit();

                rang = "A" + (SecondComp.Count >= FirstComp.Count ? (SecondComp.Count + 2).ToString() : (FirstComp.Count + 2).ToString()) + ":" + "E" + (SecondComp.Count >= FirstComp.Count ? (SecondComp.Count + 2).ToString() : (FirstComp.Count + 2).ToString());
                formatRange = xlWorkSheet.get_Range(rang);
                formatRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                xlWorkSheet.Range[rang].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                xlWorkSheet.Range[rang].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                xlApp.DisplayAlerts = false;

                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

                int fvenue = FirstVenue.Length;
                int svenue = SecondVenue.Length;

                saveFileDialog.FileName = (rm as ResourceManager).GetString("Menu_Comparsion") + "_" + FirstVenue.Substring(0, fvenue<15?fvenue:15) + "_" + SecondVenue.Substring(0, svenue<15?svenue:15);
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
                        ViewModel.WinMessageBoxItems wmsgbi = new ViewModel.WinMessageBoxItems((rm as ResourceManager).GetString("MessageBoxSaveTitle"), (rm as ResourceManager).GetString("MessageBoxSaveText"), MaterialDesignThemes.Wpf.PackIconKind.InformationCircle);
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
                ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems("Error", ex.Message, MaterialDesignThemes.Wpf.PackIconKind.Error);
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

        void CB_FirstOffer_DropDownOpened(object sender, EventArgs e)
        {
            ComparsionItemsFirst.Opacity = 0.2;
            ComparsionItemsSecond.Opacity = 0.2;
        }
        void CB_FirstOffer_DropDownClosed(object sender, EventArgs e)
        {
            ComparsionItemsFirst.Opacity = 1;
            ComparsionItemsSecond.Opacity = 1;
        }
        void CB_FirstOffer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowComparsion();
        }
        void CB_SecondOffer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowComparsion();
        }
        void CB_SecondOffer_DropDownOpened(object sender, EventArgs e)
        {
            ComparsionItemsFirst.Opacity = 0.2;
            ComparsionItemsSecond.Opacity = 0.2;
        }
        void CB_SecondOffer_DropDownClosed(object sender, EventArgs e)
        {
            ComparsionItemsFirst.Opacity = 1;
            ComparsionItemsSecond.Opacity = 1;
        }
        void BT_ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            CreateExcelDocument();
        }
        
    }
}
