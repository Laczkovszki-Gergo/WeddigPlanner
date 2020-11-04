using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.ComponentModel;
using MaterialDesignThemes.Wpf;

namespace Eskuvo_tervezo.Pages
{
    /// <summary>
    /// Interaction logic for Venue.xaml
    /// </summary>
    public partial class Venue : Page
    {
        Models.WeddingData Wedding;
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Models.Login User;

        Windows.Functions f = new Windows.Functions();

        List<Models.Guests> guestList = new List<Models.Guests>();
        List<ViewModel.VenueItems> exitems = new List<ViewModel.VenueItems>();
        List<ViewModel.Pictures> p = new List<ViewModel.Pictures>();
        List<BitmapImage> Bitimages = new List<BitmapImage>();

        Models.WeddingVenue[] Venues;

        ViewModel.VenueItems e;
        string[] ResourceNames;
        int[] VenueIDs;

        object rm;
        double picsbwid = 0;
        double picsbhei = 0;

        public Venue(Models.Login _User, ResourceManager _rm, string[] _ResourceNames)
        {
            InitializeComponent();
            WPE = new Models.WeddingPlannerEntities();
            rm = _rm;
            User = _User;
            ResourceNames = _ResourceNames;
        }
        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Wedding = WPE.WeddingData.FirstOrDefault(x => x.User_ID.Equals(User.IDLogin));
            LoadFormats((rm as ResourceManager), ResourceNames);
            CB_Reload();
            Disable_Enable_Control();
            BT_DownloadPicture.Visibility = Visibility.Hidden;
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
                else if (ResourceNames[i] == "Tooltip_InvalidNameCharacters")
                {
                    if (TB_Venue.ToolTip != null)
                        TB_Venue.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }

                else if (ResourceNames[i] == "Tooltip_InvalidNameCharacters")
                {
                    if (TB_Address.ToolTip != null)
                        TB_Address.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
            }
        }
        internal void ShowPics(ResourceManager _rm)
        {
            rm = _rm;
            if (CB_Venue.SelectedIndex > -1)
            {
                System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                PicsBox.ItemsSource = null;
                int index = VenueIDs[CB_Venue.SelectedIndex];
                List<Models.WeddingVenueImages> images = WPE.WeddingVenueImages.Where(x => x.WeddingVenueID.Equals(index)).ToList();

                for (int i = 0; i < images.Count; i++)
                {
                    if (!p.Any(x=>x.ID.Equals(images[i].ID)))
                    {
                        byte[] blob = images[i].Image;
                        MemoryStream stream = new MemoryStream();
                        stream.Write(blob, 0, blob.Length);
                        stream.Position = 0;

                        System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();

                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        ms.Seek(0, SeekOrigin.Begin);
                        bi.StreamSource = ms;
                        bi.EndInit();                        

                        picsbwid = (this.WindowWidth * 4 / 6 - 150) / 3;
                        picsbhei = (this.WindowHeight * 10 / 17) / 2;
                        ViewModel.Pictures pics = new ViewModel.Pictures(images[i].ID, images[i].ImageName.Trim(), bi, picsbwid, picsbhei);

                        Bitimages.Add(bi);
                        p.Add(pics);
                    }               
                }
                if (p.Count > 0)
                {
                    PicsBox.ItemsSource = p;
                    BT_DownloadPicture.Visibility = Visibility.Visible;
                }
                else
                    BT_DownloadPicture.Visibility = Visibility.Hidden;

                System.Windows.Input.Mouse.OverrideCursor = null;
            }
            else
                PicsBox.ItemsSource = null;
        }
        internal void CB_Reload()
        {
            if (WPE.WeddingVenue.Any(x => x.WeddingID.Equals(Wedding.ID)))
            {
                Venues = WPE.WeddingVenue.Where(x => x.WeddingID.Equals(Wedding.ID)).ToArray();
                VenueIDs = Venues.Select(x => x.ID).ToArray();
                exitems = new List<ViewModel.VenueItems>();
                for (int i = 0; i < Venues.Length; i++)
                {
                    e = new ViewModel.VenueItems(Venues[i].Wedding_Venue, Venues[i].Venue_Address);
                    exitems.Add(e);
                }
                CB_Venue.ItemsSource = exitems;
                p.Clear();
            }

        }
        internal void CB_Reload(int index)
        {
            WPE = new Models.WeddingPlannerEntities();
            if (WPE.WeddingVenue.Any(x => x.WeddingID.Equals(Wedding.ID)))
            {
                Venues = WPE.WeddingVenue.Where(x => x.WeddingID.Equals(Wedding.ID)).ToArray();
                VenueIDs = Venues.Select(x => x.ID).ToArray();
                exitems = new List<ViewModel.VenueItems>();
                for (int i = 0; i < Venues.Length; i++)
                {
                    e = new ViewModel.VenueItems(Venues[i].Wedding_Venue, Venues[i].Venue_Address);
                    exitems.Add(e);
                }
                CB_Venue.ItemsSource = exitems;
            }
            p.Clear();
            CB_Venue.SelectedIndex = index;
        }
        void insertImageData(string[] imageNames)
        {
            try
            {
                if (imageNames != null)
                {
                    System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    FileStream fs = null;
                    for (int i = 0; i < imageNames.Length; i++)
                    {
                        fs = new FileStream(imageNames[i], FileMode.Open, FileAccess.Read);
                        byte[] imgByteArr = new byte[fs.Length];
                        fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                        Models.WeddingVenueImages wvi = new Models.WeddingVenueImages();
                        wvi.WeddingVenueID = VenueIDs[CB_Venue.SelectedIndex];
                        wvi.Image = imgByteArr;
                        wvi.ImageName = imageNames[i].Split(new[] { @"\" }, StringSplitOptions.None).Last();
                        WPE.WeddingVenueImages.Add(wvi);
                        WPE.SaveChanges();
                    }
                    fs.Close();
                    ShowPics((rm as ResourceManager));
                    System.Windows.Input.Mouse.OverrideCursor = null;
                }
            }
            catch (Exception ex)
            {
                ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems("Error", ex.Message, PackIconKind.Error);
                Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, false);
                msb.Show();
                System.Windows.Input.Mouse.OverrideCursor = null;
            }
        }
        void Disable_Enable_Control()
        {
            if (CB_Venue.SelectedIndex > -1)
            {
                BT_BrowsePicture.IsEnabled = true;
                BT_Modification.IsEnabled = true;
                BT_Delete.IsEnabled = true;
            }
            else
            {
                BT_BrowsePicture.IsEnabled = false;
                BT_Modification.IsEnabled = false;
                BT_Delete.IsEnabled = false;
            }
        }
        void SaveVenues()
        {
            if (f.isContactName(TB_Venue, TB_Venue.Text.Trim(), (rm as ResourceManager)) && f.isContactName(TB_Address, TB_Address.Text.Trim(), (rm as ResourceManager)))
            {
                Models.WeddingVenue wv = new Models.WeddingVenue();
                wv.WeddingID = Wedding.ID;
                wv.Wedding_Venue = TB_Venue.Text.Trim();
                wv.Venue_Address = TB_Address.Text.Trim();
                WPE.WeddingVenue.Add(wv);
                WPE.SaveChanges();
                CB_Reload();
            }
        }
        void BrowsePictures()
        {
            try
            {
                OpenFileDialog fldlg = new OpenFileDialog();
                fldlg.Multiselect = true;
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                fldlg.Filter = (rm as ResourceManager).GetString("Dialog_ImageFiles") + "(*.jpg; *.jpeg; *.gif; *.bmp)| *.jpg; *.jpeg; *.gif; *.bmp";
                if (fldlg.ShowDialog() == true)
                {
                    insertImageData(fldlg.FileNames);
                }
                fldlg = null;
            }
            catch (Exception ex)
            {
                ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems("Error", ex.Message, PackIconKind.Error);
                Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, false);
                msb.Show();
            }
        }
        void DeletePicture(int id)
        {

            ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems((rm as ResourceManager).GetString("Message_Delete_Title"), (rm as ResourceManager).GetString("Message_DeletePics"), PackIconKind.WarningCircle);
            Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, null);

            if (msb.ShowDialog() == true)
            {
                System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                if (WPE.WeddingVenueImages.Any(x => x.ID.Equals(id)))
                {
                    WPE.Dispose();
                    WPE = new Models.WeddingPlannerEntities();
                    Models.WeddingVenueImages wvi = WPE.WeddingVenueImages.FirstOrDefault(x => x.ID.Equals(id));
                    WPE.WeddingVenueImages.Attach(wvi);
                    WPE.WeddingVenueImages.Remove(wvi);
                    WPE.SaveChanges();
                    int delindex = p.FindIndex(x => x.ID.Equals(wvi.ID));
                    p.Remove(p.FirstOrDefault(x => x.ID.Equals(wvi.ID)));
                    Bitimages.RemoveAt(delindex);
                    PicsBox.ItemsSource = null;
                    PicsBox.ItemsSource = p;
                }
            }
            System.Windows.Input.Mouse.OverrideCursor = null;

        }
        void DeleteVenueAndPictures()
        {
            if (CB_Venue.SelectedIndex > -1)
            {
                int VenID = VenueIDs[CB_Venue.SelectedIndex];
                ViewModel.WinMessageBoxItems wmsb = new ViewModel.WinMessageBoxItems((rm as ResourceManager).GetString("Message_Delete_Title"), (rm as ResourceManager).GetString("Message_DeleteVenue"), PackIconKind.WarningCircle);
                Windows.WinMessageBox msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, null);

                if (msb.ShowDialog() == true)
                {
                    if (WPE.WeddingVenue.Any(x => x.ID.Equals(VenID)))
                    {
                        WPE.Dispose();
                        WPE = new Models.WeddingPlannerEntities();
                        var ven = new Models.WeddingVenue { ID = VenID };

                        if (WPE.WeddingExpenses.Any(x => x.VenueID.Equals(ven.ID)))
                        {
                            wmsb = new ViewModel.WinMessageBoxItems((rm as ResourceManager).GetString("Message_Delete_Title"), (rm as ResourceManager).GetString("Message_DeleteExpense"), PackIconKind.WarningCircle);
                            msb = new Windows.WinMessageBox(wmsb, (rm as ResourceManager), ResourceNames, null);

                            if (msb.ShowDialog() == true)
                            {
                                WPE.WeddingExpenses.RemoveRange(WPE.WeddingExpenses.Where(X => X.VenueID.Equals(ven.ID)));
                                WPE.SaveChanges();
                            }
                            else
                            {
                                return;
                            }
                        }
                        if (WPE.WeddingVenueImages.Any(x => x.WeddingVenueID.Equals(ven.ID)))
                        {
                            WPE.WeddingVenueImages.RemoveRange(WPE.WeddingVenueImages.Where(x => x.WeddingVenueID.Equals(ven.ID)));
                            WPE.SaveChanges();
                        }
                        WPE.WeddingVenue.Attach(ven);
                        WPE.WeddingVenue.Remove(ven);
                        p.Clear();
                        Bitimages.Clear();
                        WPE.SaveChanges();
                        CB_Reload();
                    }
                }
            }
        }
        void DownloadPicture(int id)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = p.FirstOrDefault(x => x.ID.Equals(id)).Title;
            dialog.Filter = "Jpeg Image(.jpeg) | *.jpeg |Png Image(.png) | *.png |Bitmap Image (.bmp) | *.bmp |Gif Image(.gif) | *.gif |Tiff Image(.tiff) | *.tiff |Wmf Image(.wmf) | *.wmf";
            dialog.DefaultExt = "jpeg";
            if (dialog.ShowDialog()==true)
            {
                BitmapImage drawImage = Bitimages[p.FindIndex(x => x.ID.Equals(id))];
                MemoryStream outStream = new MemoryStream();
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(drawImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                System.Drawing.Imaging.ImageFormat iformat;

                switch (dialog.FilterIndex)
                {
                    case 1:
                        iformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    case 2:
                        iformat = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                    case 3:
                        iformat = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                    case 4:
                        iformat = System.Drawing.Imaging.ImageFormat.Gif;
                        break;
                    case 5:
                        iformat = System.Drawing.Imaging.ImageFormat.Tiff;
                        break;
                    case 6:
                        iformat = System.Drawing.Imaging.ImageFormat.Wmf;
                        break;
                    default:
                        iformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                }

                bitmap.Save(dialog.FileName,iformat);
                ViewModel.WinMessageBoxItems wmsgbi = new ViewModel.WinMessageBoxItems((rm as ResourceManager).GetString("MessageBoxSaveTitle"), (rm as ResourceManager).GetString("MessageBoxSaveText"), MaterialDesignThemes.Wpf.PackIconKind.InformationCircle);
                Windows.WinMessageBox wmsg = new Windows.WinMessageBox(wmsgbi, (rm as ResourceManager), ResourceNames, false);
                wmsg.Show();
            }
            System.Windows.Input.Mouse.OverrideCursor = null;

        }
        void DownloadAllVenuePicture()
        {
            if(CB_Venue.SelectedIndex >-1 )
            {
                SaveFileDialog dialog = new SaveFileDialog();
                int venid = VenueIDs[CB_Venue.SelectedIndex];
                string folder = Venues.FirstOrDefault(x => x.ID.Equals(venid)).Wedding_Venue.Trim();
                dialog.FileName = folder.Split('.')[0];
                dialog.Filter = "Jpeg Image(.jpeg) | *.jpeg |Png Image(.png) | *.png |Bitmap Image (.bmp) | *.bmp |Gif Image(.gif) | *.gif |Tiff Image(.tiff) | *.tiff |Wmf Image(.wmf) | *.wmf";
                dialog.DefaultExt = "Jpeg";
              
                if (dialog.ShowDialog() == true)
                {

                    System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                    System.Drawing.Imaging.ImageFormat iformat;

                    switch (dialog.FilterIndex)
                    {
                        case 1:
                            iformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                            break;
                        case 2:
                            iformat = System.Drawing.Imaging.ImageFormat.Png;
                            break;
                        case 3:
                            iformat = System.Drawing.Imaging.ImageFormat.Bmp;
                            break;
                        case 4:
                            iformat = System.Drawing.Imaging.ImageFormat.Gif;
                            break;
                        case 5:
                            iformat = System.Drawing.Imaging.ImageFormat.Tiff;
                            break;
                        case 6:
                            iformat = System.Drawing.Imaging.ImageFormat.Wmf;
                            break;
                        default:
                            iformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                            break;
                    }

                    for (int i = 0; i < Bitimages.Count; i++)
                    {
                        if (!File.Exists(dialog.FileName.Split('.')[0]))
                            System.IO.Directory.CreateDirectory(dialog.FileName.Split('.')[0]);

                        BitmapImage drawImage = Bitimages[i];
                        MemoryStream outStream = new MemoryStream();
                        BitmapEncoder enc = new BmpBitmapEncoder();
                        enc.Frames.Add(BitmapFrame.Create(drawImage));
                        enc.Save(outStream);
                        System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                        bitmap.Save(dialog.FileName.Split('.')[0] + @"\"+p[i].Title.Split('.')[0] +"."+ iformat.ToString(), iformat);
                    }
                    ViewModel.WinMessageBoxItems wmsgbi = new ViewModel.WinMessageBoxItems((rm as ResourceManager).GetString("MessageBoxSaveTitle"), (rm as ResourceManager).GetString("MessageBoxSaveText"), MaterialDesignThemes.Wpf.PackIconKind.InformationCircle);
                    Windows.WinMessageBox wmsg = new Windows.WinMessageBox(wmsgbi, (rm as ResourceManager), ResourceNames, false);
                    wmsg.Show();
                    System.Windows.Input.Mouse.OverrideCursor = null;
                }

            }
        }

        void CB_Venue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            p.Clear();
            Bitimages.Clear();
            Disable_Enable_Control();
            ShowPics((rm as ResourceManager));
        }
        void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BitmapImage[] allpics = p.Select(x => x.ImageData).ToArray();
            Windows.ImageView i = new Windows.ImageView((sender as Image),allpics);
            this.Opacity = 0.5;
            bool? result = i.ShowDialog();
            if (result == true)
            {
                this.Opacity = 1;
            }
        }
        void BT_Modification_Click(object sender, RoutedEventArgs e)
        {
            if (CB_Venue.SelectedIndex > -1)
            {
                int id = VenueIDs[CB_Venue.SelectedIndex];
                Models.WeddingVenue c = WPE.WeddingVenue.FirstOrDefault(x => x.ID == id);
                if (c != null)
                {
                    Windows.VenueModify ve = new Windows.VenueModify((rm as ResourceManager), c, ResourceNames, this, CB_Venue.SelectedIndex);
                    ve.Show();
                }
            }
        }
        void BT_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteVenueAndPictures();
        }
        void PackIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int id = (int)(sender as PackIcon).DataContext;
            DeletePicture(id);
        }
        void TB_Venue_PreviewKeyDown(object sender, KeyEventArgs e)
        {         
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
        void TB_Address_PreviewKeyDown(object sender, KeyEventArgs e)
        {          
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Save_Click(sender, e);
            }
        }
        void CB_Venue_DropDownOpened(object sender, EventArgs e)
        {
            PicsBox.Opacity = 0.2;
        }
        void CB_Venue_DropDownClosed(object sender, EventArgs e)
        {
            PicsBox.Opacity = 1;
        }
        void BT_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveVenues();
        }
        void Bt_Browse_Click(object sender, RoutedEventArgs e)
        {
            BrowsePictures();
        }
        void Tooltip_Delete_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as PackIcon).ToolTip = (rm as ResourceManager).GetString("Tooltip_Delete");
        }
        void Tooltip_Delete_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as PackIcon).ToolTip = null;
        }
        void Tooltip_Download_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as PackIcon).ToolTip = (rm as ResourceManager).GetString("Tooltip_Download");

        }
        void Tooltip_Download_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as PackIcon).ToolTip = null;

        }
        void Tooltip_Download_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DownloadPicture((int)(sender as PackIcon).DataContext);
        }
        void BT_DownloadPicture_Click(object sender, RoutedEventArgs e)
        {
            DownloadAllVenuePicture();
        }
    }
}
