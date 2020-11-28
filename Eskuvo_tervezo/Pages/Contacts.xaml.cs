using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Windows;
using System.Windows.Controls;


namespace Eskuvo_tervezo.Pages
{
    /// <summary>
    /// Interaction logic for Contacts.xaml
    /// </summary>
    public partial class Contacts : Page
    {
        Models.Login User;
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        List<Models.Contacts> contList = new List<Models.Contacts>();
        Windows.Functions f = new Windows.Functions();

        object rm;
        string[] ResourceNames;

        public Contacts(Models.Login _User, ResourceManager _rm, string[] _Resourcenames)
        {
            InitializeComponent();
            rm = _rm;
            ResourceNames = _Resourcenames;
            User = _User;
            Windows.RefreshContactsList re = CreateContactList;
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WPE = new Models.WeddingPlannerEntities();
            LoadFormats((rm as ResourceManager), ResourceNames);
            BT_ExportToExcel.Visibility = Visibility.Hidden;
            CreateContactList();
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
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidPhoneCharacters")
                {
                    if (TB_Phone.ToolTip != null)
                        TB_Phone.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidEmailCharacters")
                {
                    if (TB_Email.ToolTip != null)
                        TB_Email.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNameCharacters")
                {
                    if (TB_Name.ToolTip != null)
                        TB_Name.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i].ToString());
                }
            }
        }
        void SaveContact()
            { 
            if(f.IsName(TB_Name, TB_Name.Text.Trim(), (rm as ResourceManager)) && f.IsPhoneNumber(TB_Phone, TB_Phone.Text.Trim(), (rm as ResourceManager)) && f.IsValidEmail(TB_Email,TB_Email.Text.Trim(),(rm as ResourceManager)) )
            {
                Models.Contacts cont = new Models.Contacts();
                cont.User_Id = User.IDLogin;
                cont.Con_Name = TB_Name.Text.Trim();
                cont.Con_Phone = TB_Phone.Text.Trim();
                cont.Con_Email = TB_Email.Text.Trim(); ;
                WPE.Contacts.Add(cont);
                WPE.SaveChanges();
                CreateContactList();
            }
        }  

        internal void CreateContactList()
        {
            WPE = new Models.WeddingPlannerEntities();
            contList = WPE.Contacts.Where(x=> x.User_Id.Equals(User.IDLogin)).OrderBy(x=>x.Con_Name).ToList();
            ContactItems.Children.Clear();

            foreach (var item in contList)
            {
                ViewModel.Cont it = new ViewModel.Cont(item.Con_Name.Trim(), item.Con_Phone.Trim(), item.Con_Email.Trim(), item.Con_ID.ToString());
                ContactItems.Children.Add(new UserControls.UsercontrolContacts(it, (rm as ResourceManager), ResourceNames, this));
            }
            if(ContactItems.Children.Count > 0)
                BT_ExportToExcel.Visibility = Visibility.Visible;
            else
                BT_ExportToExcel.Visibility = Visibility.Hidden;

            //foreach (var item in contList.Where(x => x.User_Id.Equals(User.IDLogin)).Reverse().ToList())
            //{
            //    var it = new ViewModel.Cont(item.Con_Name.Trim(), item.Con_Phone.Trim(), item.Con_Email.Trim(), item.Con_ID.ToString());
            //    ContactItems.Children.Add(new UserControls.UsercontrolContacts(it, (rm as ResourceManager), ResourceNames, this));
            //}
        }
        internal void CreateContactList(ResourceManager rm)
        {
            WPE = new Models.WeddingPlannerEntities();
            contList = WPE.Contacts.Where(x => x.User_Id.Equals(User.IDLogin)).OrderBy(x => x.Con_Name).ToList();
            ContactItems.Children.Clear();

            foreach (var item in contList)
            {
                ViewModel.Cont it = new ViewModel.Cont(item.Con_Name.Trim(), item.Con_Phone.Trim(), item.Con_Email.Trim(), item.Con_ID.ToString());
                ContactItems.Children.Add(new UserControls.UsercontrolContacts(it, (rm as ResourceManager), ResourceNames, this));
            }
            if (ContactItems.Children.Count > 0)
                BT_ExportToExcel.Visibility = Visibility.Visible;
            else
                BT_ExportToExcel.Visibility = Visibility.Hidden;


            //foreach (var item in contList.Where(x => x.User_Id.Equals(User.IDLogin)).Reverse().ToList())
            //{
            //    var it = new ViewModel.Cont(item.Con_Name.Trim(), item.Con_Phone.Trim(), item.Con_Email.Trim(), item.Con_ID.ToString());
            //    ContactItems.Children.Add(new UserControls.UsercontrolContacts(it, (rm as ResourceManager), ResourceNames, this));
            //}
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
                xlWorkSheet.Name = (rm as ResourceManager).GetString("Menu_Contact");

                Microsoft.Office.Interop.Excel.Range formatRange = xlWorkSheet.get_Range("A1:C1");
                formatRange.Font.Bold = true;
                formatRange.WrapText = true;

                string rang = "A1:" + "C" +(contList.Count+1);
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

                xlWorkSheet.Cells[1, 1] = LB_Name.Content;
                xlWorkSheet.Cells[1, 2] = LB_Phone.Content;
                xlWorkSheet.Cells[1, 3] = LB_Email.Content;

                for (int i = 0; i < contList.Count; i++)
                {
                    xlWorkSheet.Cells[2 + i, 1] = contList[i].Con_Name.Trim();
                    xlWorkSheet.Cells[2 + i, 2] = contList[i].Con_Phone.Trim();
                    xlWorkSheet.Cells[2 + i, 3] = contList[i].Con_Email.Trim();
                }

                xlWorkSheet.get_Range(rang).Columns.AutoFit();
                xlApp.DisplayAlerts = false;

                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.FileName = (rm as ResourceManager).GetString("Menu_Contact");
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

        void BT_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveContact();
        }
        internal void DeleteCLick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = 0;
            Int32.TryParse((string)(sender as PackIcon).DataContext, out id);

            ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems((rm as ResourceManager).GetString("Message_Delete_Title"), (rm as ResourceManager).GetString("Message_DeleteContact"), PackIconKind.WarningCircle);
            Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames,null);

            if (msb.ShowDialog() == true)
            {
                if (WPE.Contacts.FirstOrDefault(x => x.Con_ID.Equals(id)) != null)
                {
                    WPE.Dispose();
                    WPE = new Models.WeddingPlannerEntities();
                    var cont = new Models.Contacts { Con_ID = id };
                    WPE.Contacts.Attach(cont);
                    WPE.Contacts.Remove(cont);
                    WPE.SaveChanges();
                    CreateContactList();
                }
            }
        }
        void TB_Name_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
        void TB_Phone_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
        void TB_Email_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }

        void BT_ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            CreateExcelDocument();
        }
    }
}
