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
    /// Interaction logic for RadioChannelModify.xaml
    /// </summary>
    public delegate void RefreshRadioChannelList();
    public partial class RadioChannelModify : Window
    {
        Models.Radio rad = new Models.Radio();
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Pages.Radio RadPage;

        Functions f = new Functions();
        ResourceManager rm;

        string[] ResourceNames;

        public RadioChannelModify(ResourceManager _rm, Models.Radio _rad , string[] _ResourceNames, Pages.Radio _RadPage)
        {
            InitializeComponent();
            rm = _rm;
            rad = _rad;
            ResourceNames = _ResourceNames;
            TB_RadioChannelName.Text = rad.ChannelName.Trim();
            TB_StreamLink.Text = rad.StreamLink.Trim();
            RadPage = _RadPage;
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
                else if (ResourceNames[i] == "Tooltip_InvalidNormalTextCharacters")
                {
                    if (TB_RadioChannelName.ToolTip != null)
                        TB_RadioChannelName.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
                else if (ResourceNames[i] == "Tooltip_InvalidNormalTextCharacters")
                {
                    if (TB_StreamLink.ToolTip != null)
                        TB_StreamLink.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
            }
        }

        void Modification()
        {
            Models.Radio result = WPE.Radio.SingleOrDefault(b => b.ID == rad.ID);
            if (result != null && f.isNormalText(TB_StreamLink, TB_StreamLink.Text.Trim(), rm) && f.isNormalText(TB_RadioChannelName, TB_RadioChannelName.Text.Trim(), rm))
            {
                result.StreamLink = TB_StreamLink.Text.Trim();
                result.ChannelName = TB_RadioChannelName.Text.Trim();
                WPE.SaveChanges();
                RefreshRadioChannelList re = RadPage.CB_Reload;
                re();
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
        void TB_RadioChannelName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Modification_Click(sender, e);
            }
        }
        void TB_StreamLink_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Modification_Click(sender, e);
            }
        }
    }
}
