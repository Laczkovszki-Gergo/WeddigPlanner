using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Eskuvo_tervezo.ViewModel
{
    public class WinMessageBoxItem
    {

        public WinMessageBoxItem(string header,string text,PackIconKind icon) 
        {
            Header = header;
            Text = text;
            Icon = icon;
        }
        public string Header { get; set; }
        public PackIconKind Icon { get; set; }
        public string Text { get; set; }
    }
}
