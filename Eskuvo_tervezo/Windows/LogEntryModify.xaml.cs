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
    /// Interaction logic for LogEntryModify.xaml
    /// </summary>
    public delegate void RefreshCalendarList();
    public partial class LogEntryModify : Window
    {
        Models.CalendarLogEntrys Entry = new Models.CalendarLogEntrys();
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Pages.CalendarItems cli;

        Functions f = new Functions();

        string[] ResourceNames;
        ResourceManager rm;
        public LogEntryModify(ResourceManager _rm, Models.CalendarLogEntrys _Entry, string[] _ResourceNames, Pages.CalendarItems _cli)
        {
            InitializeComponent();
            rm = _rm;
            Entry = _Entry;
            ResourceNames = _ResourceNames;
            RTB_Entry.Document.Blocks.Clear();
            RTB_Entry.Document.Blocks.Add(new Paragraph(new Run(Entry.LogEntry.Trim())));
            cli = _cli;
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
            var result = WPE.CalendarLogEntrys.SingleOrDefault(b => b.ID == Entry.ID);
            if (result != null && f.isNormalRichText(RTB_Entry, new TextRange(RTB_Entry.Document.ContentStart, RTB_Entry.Document.ContentEnd).Text.Trim(), rm))
            {
                result.LogEntry = new TextRange(RTB_Entry.Document.ContentStart, RTB_Entry.Document.ContentEnd).Text.Trim();
                WPE.SaveChanges();
                RefreshCalendarList re = cli.CreateList;
                re();
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


    }
}
