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
    /// Interaction logic for VenueModify.xaml
    /// </summary>
    public delegate void RefreshVenueCombobox(int index);
    public partial class VenueModify : Window
    {
        Models.WeddingVenue ven= new Models.WeddingVenue();
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Pages.Venue VenPage;

        Functions f = new Functions();
        ResourceManager rm;
        string[] ResourceNames;
        int cbindex = 0;

        public VenueModify(ResourceManager _rm, Models.WeddingVenue _ven, string[] _ResourceNames, Pages.Venue _VenPage, int _cbindex)
        {
            InitializeComponent();
            rm = _rm;
            ven = _ven;
            ResourceNames = _ResourceNames;
            TB_Venue.Text = ven.Wedding_Venue.Trim();
            TB_Address.Text = ven.Venue_Address.Trim();
            VenPage = _VenPage;
            cbindex = _cbindex;
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
        void Modification()
        {
            var result = WPE.WeddingVenue.SingleOrDefault(b => b.ID == ven.ID);
            if (result != null && f.IsName(TB_Venue, TB_Venue.Text.Trim(), rm) && f.IsName(TB_Address, TB_Address.Text.Trim(), rm))
            {
                result.Wedding_Venue = TB_Venue.Text.Trim();
                result.Venue_Address = TB_Address.Text.Trim();
                WPE.SaveChanges();
                RefreshVenueCombobox re = VenPage.CB_Reload;

                re(cbindex);
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
        void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }
    }
}
