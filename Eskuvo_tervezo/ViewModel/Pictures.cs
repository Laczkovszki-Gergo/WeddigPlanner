using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Eskuvo_tervezo.ViewModel
{
    public class Pictures
    {
        public Pictures(int id,string title, BitmapImage imageData, double wid, double hei)
        {
            ID = id;
            Title = title;
            ImageData = imageData;
            Wid = wid;
            Hei = hei;

        }

        public int ID { get; set; }
        public string Title { get; set; }
        public BitmapImage ImageData { get; set; }

        public double Wid { get; set; }
        public double Hei { get; set; }

    }
}
