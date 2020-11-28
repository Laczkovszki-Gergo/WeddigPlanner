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
    /// Interaction logic for QuteModify.xaml
    /// </summary>
    public partial class QuoteModify : Window
    {
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Models.WeddingData Wedding;
        Home h;

        Functions f = new Functions();

        string[] ResourceNames;
        ResourceManager rm;

        public QuoteModify(ResourceManager _rm, Models.WeddingData  _Wedding, string[] _ResourceNames, Home _h)
        {
            InitializeComponent();
            rm = _rm;
            Wedding = _Wedding;
            ResourceNames = _ResourceNames;
            RTB_Entry.Document.Blocks.Clear();
            if(Wedding.Quote != null)
            RTB_Entry.Document.Blocks.Add(new Paragraph(new Run(Wedding.Quote.Trim())));
            h = _h;
            LoadFormats();
        }

        void LoadFormats()
        {
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);
                if (it is CheckBox)
                    (it as CheckBox).Content = rm.GetString(ResourceNames[i]);
                else if (it is Label)
                    (it as Label).Content = rm.GetString(ResourceNames[i]);
                else if (it is Button)
                    (it as Button).Content = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i].ToString() == "Title_Login")
                    this.Title = rm.GetString(ResourceNames[i].ToString());
                else if (it is TextBlock)
                    (it as TextBlock).Text = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i] == "Tooltip_InvalidNameCharacters")
                {
                    if (RTB_Entry.ToolTip != null)
                        RTB_Entry.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
            }
        }
        void Modification()
        {
            if (Wedding != null && f.IsNormalText(RTB_Entry, new TextRange(RTB_Entry.Document.ContentStart, RTB_Entry.Document.ContentEnd).Text.Trim(), rm))
            {
                Models.WeddingData wedd = WPE.WeddingData.FirstOrDefault(x => x.ID.Equals(Wedding.ID));
                string quote = new TextRange(RTB_Entry.Document.ContentStart, RTB_Entry.Document.ContentEnd).Text.Trim();
                if (quote.Trim().Length > 1000)
                    quote = quote.Trim().Substring(0, 1000);

                wedd.Quote = quote;
                WPE.SaveChanges();
                h.Tbl_Qoute.Text = quote.Trim();
                this.Close();
            }
        }

        void BT_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void BT_Modification_Click(object sender, RoutedEventArgs e)
        {
            Modification();
        }
        void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }
    }
}
