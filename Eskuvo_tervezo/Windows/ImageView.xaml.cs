using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Image.xaml
    /// </summary>
internal class Images
    {
    public Images(BitmapImage imageBitmap)
         {
                ImageBitmap = imageBitmap;
         }
    internal BitmapImage ImageBitmap { get; set; }
    }

public partial class ImageView : Window
    {
        BitmapImage[] Allpics;
        Image Pics;
        int index = 0;
        double wid = 0;
        double hei = 0;

        public ImageView(Image pics, BitmapImage[] allpics)
        {
            InitializeComponent();
            ImagePics.Source = pics.Source;
            Pics = pics;
            Allpics = allpics;

            if (allpics.Length == 1)
                index = 0;
            else
                index = Array.FindIndex(Allpics, x => x.Equals(Pics.Source));
            wid = ImagePics.Width;
            hei = ImagePics.Height;

            if (!(index > 0))
                IconBack.Visibility = Visibility.Collapsed;
            if (!(index < Allpics.Length-1))
                IconNext.Visibility = Visibility.Collapsed;
            LB_Pics.Content = (index + 1) + " / " + Allpics.Length;
        }

        void next()
        {
            if (index < Allpics.Length - 1)
            {
                index++;
                ImagePics.Source = Allpics[index];
                IconNext.Visibility = Visibility.Visible;
                if (!(index < Allpics.Length - 1))
                    IconNext.Visibility = Visibility.Collapsed;
                LB_Pics.Content = (index + 1) + " / " + Allpics.Length;
            }
            else
                IconNext.Visibility = Visibility.Collapsed;

            if (index > 0)
                IconBack.Visibility = Visibility.Visible;
        }
        void back()
        {
            if (index > 0)
            {
                index--;
                ImagePics.Source = Allpics[index];
                IconBack.Visibility = Visibility.Visible;
                if (!(index > 0))
                    IconBack.Visibility = Visibility.Collapsed;
                LB_Pics.Content = (index + 1) + " / " + Allpics.Length;
            }
            else
                IconBack.Visibility = Visibility.Collapsed;

            if ((index < Allpics.Length - 1))
                IconNext.Visibility = Visibility.Visible;
        }
        void escape()
        {
            DialogResult = true;
            this.Close();
        }

        void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(ImagePics.Width != Pics.Width)
            {
                ImagePics.Width = Pics.Width;
                ImagePics.Height = Pics.Height;
            }
            else
            {
                ImagePics.Width = wid;
                ImagePics.Height = hei;
            }
        }
        void IconEscape_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            escape();
        }
        void IconNext_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            next();
        }
        void IconBack_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            back();
        }
        void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Right)            
                next();
            else if (e.Key == System.Windows.Input.Key.Left)
                    back();
            else if (e.Key == System.Windows.Input.Key.Escape)
                escape();
        }
    }
}
