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
    /// Interaction logic for GuestsModify.xaml
    /// </summary>
    public delegate void RefreshGuestList(ResourceManager rm);
    public partial class GuestsModify : Window
    {
        Models.Guests gue = new Models.Guests();
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Pages.Guests GuePage;

        Functions f = new Functions();
        ResourceManager rm;
        string[] ResourceNames;

        public GuestsModify(ResourceManager _rm, Models.Guests _gue, string[] _ResourceNames, Pages.Guests _GuePage)
        {
            InitializeComponent();
            rm = _rm;
            gue = _gue;
            ResourceNames = _ResourceNames;
            TB_Guest.Text = gue.Guest_Name.Trim();
            TB_GuestsCount.Text = gue.Guest_Count.ToString().Trim();
            GuePage = _GuePage;
            LoadFormats();
        }
        void LoadFormats()
        {
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                var it = this.FindName(ResourceNames[i]);

                if (it is Label)
                    (it as Label).Content = rm.GetString(ResourceNames[i]);
                else if (it is Button)
                    (it as Button).Content = rm.GetString(ResourceNames[i]);
                else if (it is TextBlock)
                    (it as TextBlock).Text = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i] == "Tooltip_InvalidNameCharacters")
                {
                    if (TB_Guest.ToolTip != null)
                        TB_Guest.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
                else if (ResourceNames[i] == "Tooltip_InvalidNumberCharacters")
                {
                    if (TB_GuestsCount.ToolTip != null)
                        TB_GuestsCount.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
            }
        }
        void Modification()
        {
            var result = WPE.Guests.SingleOrDefault(b => b.Guest_ID == gue.Guest_ID);
            if (result != null && f.isContactName(TB_Guest, TB_Guest.Text.Trim(), rm) && f.IsNumber(TB_GuestsCount, f.StringRemoveWhiteSpace(TB_GuestsCount.Text.Trim()), rm))
            {
                result.Guest_Name = TB_Guest.Text.Trim();
                result.Guest_Count = Convert.ToInt32(f.StringRemoveWhiteSpace(TB_GuestsCount.Text.Trim()));
                WPE.SaveChanges();
                RefreshGuestList re = GuePage.CreateGuestList;
                re((rm as ResourceManager));
                this.Close();
            }
        }

        void BT_Modification_Click(object sender, RoutedEventArgs e)
        {
            Modification();
        }
        void BT_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void TB_Guest_PreviewKeyDown(object sender, KeyEventArgs e)
        {           
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Modification_Click(sender, e);
            }
        }
        void TB_GuestsCount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            f.NumericTextBox_PreviewKeyDown(sender, e);
            if (f.StringRemoveWhiteSpace((sender as TextBox).Text).Length > 8)
                e.Handled = true;

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Modification_Click(sender, e);
            }
        }
    }
}
