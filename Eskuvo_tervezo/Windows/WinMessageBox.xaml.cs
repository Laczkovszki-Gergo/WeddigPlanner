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
using System.Windows.Shapes;

namespace Eskuvo_tervezo.Windows
{
    /// <summary>
    /// Interaction logic for WinMessageBox.xaml
    /// </summary>
    public partial class WinMessageBox : Window
    {
        Functions f = new Functions();

        string[] ResourceNames;
        ResourceManager rm;

        public WinMessageBox(ViewModel.WinMessageBoxItems WMsbI, ResourceManager _rm, string[] _ResourceNames, bool? IsYesNo)
        {
            InitializeComponent();
            rm = _rm;
            ResourceNames = _ResourceNames;
            this.DataContext = WMsbI;

            if(IsYesNo == false)
            {
                BT_Yes.Visibility = Visibility.Hidden;
                BT_No.Visibility = Visibility.Hidden;
                BT_Ok.Visibility = Visibility.Visible;
            }
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFormats();
        }
        void LoadFormats()
        {
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                object it = this.FindName(ResourceNames[i]);
                if (it is Button)
                    (it as Button).Content = rm.GetString(ResourceNames[i]);
            }
        }

        void no()
        {
            DialogResult = false;
            this.Close();
        }
        void yes()
        {
            DialogResult = true;
            this.Close();
        }
        void ok()
        {
            this.Close();
        }

        void BT_No_Click(object sender, RoutedEventArgs e)
        {
            no();
        }
        void BT_Yes_Click(object sender, RoutedEventArgs e)
        {
            yes();
        }
        void BT_Ok_Click(object sender, RoutedEventArgs e)
        {
            ok();
        }

        void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter & BT_Yes.Visibility == Visibility.Visible)
                yes();
            else if (e.Key == System.Windows.Input.Key.Enter & BT_Ok.Visibility == Visibility.Visible)
                ok();
            else if (e.Key == System.Windows.Input.Key.Escape & BT_No.Visibility == Visibility.Visible)
                no();
        }
    }
}
