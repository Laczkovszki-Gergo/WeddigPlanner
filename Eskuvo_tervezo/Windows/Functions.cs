using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Eskuvo_tervezo.Windows
{
    class Functions
    {
        internal string Encrypt(string value)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(value);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }

        internal ImageBrush CreateImageBrush(string ImagePath)
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
            return brush;
        }
        internal BitmapImage CreateBitmapFromBytes(Byte[] ImageBytes)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                stream.Write(ImageBytes, 0, ImageBytes.Length);
                stream.Position = 0;

                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                return bi;
            }
            catch { return null; }
        }

        internal void NumericTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back & e.Key != Key.Left & e.Key != Key.Right & e.Key != Key.Delete & e.Key != Key.Space & e.Key != Key.Tab)
                e.Handled = true;
        }
        internal void CurrencyFormat(object sender)
        {
            if ((sender as TextBox).Text != null && (sender as TextBox).Text.Length > 0)
                (sender as TextBox).Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:#,##0}", double.Parse((sender as TextBox).Text));
        }

        internal bool IsValidEmail(object sender,string emailaddress, System.Resources.ResourceManager rm)
        {
                if(new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(emailaddress))
                    {
                        (sender as TextBox).ClearValue(TextBox.ToolTipProperty);
                        (sender as TextBox).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                        return true;
                    }
                else
                    {
                        (sender as TextBox).ToolTip = rm.GetString("Tooltip_InvalidEmailCharacters");
                        (sender as TextBox).BorderBrush = Brushes.Red;
                        return false;
                    }
            }
        internal bool IsPhoneNumber(object sender,string number, System.Resources.ResourceManager rm)
        {
                var r = new Regex(@"^\(?([+]{1})?([0-9]{2})?([- ])?([0-9]{2})\)?[- ]?([0-9]{3})[- ]?([0-9]{4})$");
                if (r.IsMatch(number))
                {
                    (sender as TextBox).ClearValue(TextBox.ToolTipProperty);
                    (sender as TextBox).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                    return true;
                }
                else
                {
                    (sender as TextBox).ToolTip = rm.GetString("Tooltip_InvalidPhoneCharacters");
                    (sender as TextBox).BorderBrush = Brushes.Red;
                    return false;
                }
        }                 
        internal bool IsName(object sender, string name, System.Resources.ResourceManager rm)
        {
            Regex r = new Regex(@"^(.?[A-Za-z0-9u00C0-\u017F#?!@$%^&*-.äÄ€đĐłŁß;\s]){2,}$");

            if ((sender as TextBox).Text.Length < 3 && !r.IsMatch(name))
            {
                (sender as TextBox).ToolTip = rm.GetString("Tooltip_InvalidNameCharacters");
                (sender as TextBox).BorderBrush = Brushes.Red;
                return false;
            }
            else
            {
                (sender as TextBox).ClearValue(TextBox.ToolTipProperty);
                (sender as TextBox).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                return true;
            }
        }
        internal bool IsPassword(object sender, string password, System.Resources.ResourceManager rm)
        {
            var r = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            if (r.IsMatch(password))
            {
                (sender as PasswordBox).ClearValue(PasswordBox.ToolTipProperty);
                (sender as PasswordBox).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                return true;
            }
            else
            {
                (sender as PasswordBox).ToolTip = rm.GetString("Tooltip_InvalidPasswordCharacters");
                (sender as PasswordBox).BorderBrush = Brushes.Red;
                return false;
            }
        }
        internal bool IsNormalText(object sender, string text, System.Resources.ResourceManager rm)
        {
            Regex r = new Regex(@"^(.?[A-Za-z0-9u00C0-\u017F#?!@$%^&*-.äÄ€đĐłŁß;\s]){2,}$");
            if(sender is TextBox)
            {
                if (r.IsMatch(text))
                {
                    (sender as TextBox).ClearValue(TextBox.ToolTipProperty);
                    (sender as TextBox).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                    return true;

                }
                else
                {
                    (sender as TextBox).ToolTip = rm.GetString("Tooltip_InvalidNormalTextCharacters");
                    (sender as TextBox).BorderBrush = Brushes.Red;
                    return false;
                }
            }
            else if (sender is RichTextBox)
            {
                if (r.IsMatch(text))
                {
                    (sender as RichTextBox).ClearValue(RichTextBox.ToolTipProperty);
                    (sender as RichTextBox).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                    return true;
                }
                else
                {
                    (sender as RichTextBox).ToolTip = rm.GetString("Tooltip_InvalidNormalTextCharacters");
                    (sender as RichTextBox).BorderBrush = Brushes.Red;
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        internal bool isRadioLink(object sender, string text, System.Resources.ResourceManager rm)
        {
            Regex r = new Regex(@"^(.?[A-Za-z0-9u00C0-\u017F#?!@$%^&*-.äÄ€đĐłŁß/;\s]){2,}$");
            if (r.IsMatch(text))
            {
                (sender as TextBox).ClearValue(TextBox.ToolTipProperty);
                (sender as TextBox).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                return true;

            }
            else
            {
                (sender as TextBox).ToolTip = rm.GetString("Tooltip_InvalidNormalTextCharacters");
                (sender as TextBox).BorderBrush = Brushes.Red;
                return false;
            }
        }
        internal bool IsNumber(object sender, string text, System.Resources.ResourceManager rm)
        {
            var r = new Regex(@"^[0-9]+$");
            if (r.IsMatch(text))
            {
                (sender as TextBox).ClearValue(TextBox.ToolTipProperty);
                (sender as TextBox).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                return true;

            }
            else
            {
                (sender as TextBox).ToolTip = rm.GetString("Tooltip_InvalidNumberCharacters");
                (sender as TextBox).BorderBrush = Brushes.Red;
                return false;
            }
        }
        internal bool IsDatetime(object sender, string date, System.Resources.ResourceManager rm)
        {
            try
            {
                Convert.ToDateTime(date);
                (sender as DatePicker).ClearValue(DatePicker.ToolTipProperty);
                (sender as DatePicker).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                return true;
            }
            catch {
                (sender as DatePicker).ToolTip = rm.GetString("Tooltip_InvalidDateTimeCharacters");
                (sender as DatePicker).BorderBrush = Brushes.Red; return false; }
        }
        internal bool IsPasswordAreEqual(object firstsender,object secondsender, string firstpassword,string secondpassword, System.Resources.ResourceManager rm)
        {
            if(firstpassword.Equals(secondpassword))
            {
                (firstsender as PasswordBox).ClearValue(PasswordBox.ToolTipProperty);
                (firstsender as PasswordBox).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                (secondsender as PasswordBox).ClearValue(PasswordBox.ToolTipProperty);
                (secondsender as PasswordBox).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                return true;
            }
            else
            {
                (firstsender as PasswordBox).ToolTip = rm.GetString("Tooltip_PasswordsAreNotEqual");
                (firstsender as PasswordBox).BorderBrush = Brushes.Red;
                (secondsender as PasswordBox).ToolTip = rm.GetString("Tooltip_PasswordsAreNotEqual");
                (secondsender as PasswordBox).BorderBrush = Brushes.Red;
                return false;
            }
        }
        internal bool IsYourPassword(object sender, string text, Models.Login ActualUser, System.Resources.ResourceManager rm)
        {
            string ScryptedPassw = Encrypt(text);
            if (ActualUser.Password.Trim().Equals(ScryptedPassw.Trim()))
            {
                (sender as PasswordBox).ClearValue(PasswordBox.ToolTipProperty);
                (sender as PasswordBox).BorderBrush = (Brush)new BrushConverter().ConvertFromString("#EFE4EC");
                return true;
            }
            else
            {
                (sender as PasswordBox).ToolTip = rm.GetString("Tooltip_InvalidOldPassword");
                (sender as PasswordBox).BorderBrush = Brushes.Red;
                return false;
            }
        }
        internal bool IsFileLocked(FileInfo file, System.Resources.ResourceManager rm, string[] ResourceNames)
        {
            try
            {
                if (File.Exists(file.FullName))
                {
                    using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        stream.Close();
                    }
                }
            }
            catch (IOException)
            {
                ViewModel.WinMessageBoxItems wmsgi = new ViewModel.WinMessageBoxItems((rm as System.Resources.ResourceManager).GetString("Message_FileIsOpenTitle"), (rm as System.Resources.ResourceManager).GetString("Message_FileIsOpenText"), MaterialDesignThemes.Wpf.PackIconKind.WarningLights);
                Windows.WinMessageBox wmsgb = new WinMessageBox(wmsgi, rm, ResourceNames, false);
                wmsgb.Show();
                return true;
            }
            return false;
        }

        internal string CurrencyFormatInt(int amount)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:#,##0}", double.Parse(amount.ToString()));
        }
        internal string StringCurrencyFormat(string text)
        {
            if (text != null && text.Length > 0)
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:#,##0}", double.Parse(text));
            else
            {
                return null;
            }
        }
        internal string StringRemoveWhiteSpace(string text)
        {
            string temp = string.Empty;
            for (int i = 0; i < text.Length; i++)
            {
                if (!Char.IsWhiteSpace(text[i]))
                {
                    temp += text[i];
                }

            }
            return temp;
        }
        internal string StringSegmentationByChar(string Text, string Segmentation, int Characters)
        {
            string temp = string.Empty;
            for (int i = 0; i < Text.Length; i++)
            {
                temp += Text[i];
                if ((i+1)%Characters == 0)
                {
                    temp += Segmentation;
                }
            }
            return temp.Remove(temp.Length-1);
        }
        internal string GetMacAddress()
        {
            foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    return StringSegmentationByChar(nic.GetPhysicalAddress().ToString(), "-", 2);
                }
            }
            return null;
        }
        internal System.Net.IPAddress GetIPAddress(System.Resources.ResourceManager rm, string[] ResourceNames)
        {
            System.Net.IPAddress[] hostAddresses = System.Net.Dns.GetHostAddresses("");

            foreach (System.Net.IPAddress hostAddress in hostAddresses)
            {
                if (hostAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                    !System.Net.IPAddress.IsLoopback(hostAddress) &&
                    !hostAddress.ToString().StartsWith("169.254."))
                    return hostAddress;
            }
            ViewModel.WinMessageBoxItems wmgbi = new ViewModel.WinMessageBoxItems(rm.GetString("Message_InternetConnectionTitle"), rm.GetString("Message_InternetConnection"), MaterialDesignThemes.Wpf.PackIconKind.MicrosoftInternetExplorer);
            WinMessageBox wmsgb = new WinMessageBox(wmgbi,rm,ResourceNames,false);
            wmsgb.Show();
            return null;
        }

        //Nem használt függvények, eljárások
        internal ImageSource CreateViewImageDynamically(string ImagePath, double width, double height)
        {
            Image dynamicImage = new Image();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(ImagePath);
            bitmap.EndInit();

            dynamicImage.Source = bitmap;
            dynamicImage.Width = width;
            dynamicImage.Height = height;

            return dynamicImage.Source;
        }

        internal void SetAppDomainCultures(string name)
        {
            try
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture(name);
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture(name);
            }
            catch (CultureNotFoundException)
            {
                return;
            }
            catch (ArgumentException)
            {
                return;
            }
        }
        internal bool CheckForInternetConnection()
        {
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
