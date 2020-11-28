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
    /// Interaction logic for ContactsModify.xaml
    /// </summary>
    public delegate void RefreshContactsList();
    public partial class ContactsModify : Window
    {

        Models.Contacts Con = new Models.Contacts();
        Models.WeddingPlannerEntities WPE = new Models.WeddingPlannerEntities();
        Pages.Contacts conpage;

        Functions f = new Functions();

        ResourceManager rm;
        string[] ResourceNames;

        public ContactsModify(ResourceManager _rm, Models.Contacts _Con, string[] _ResourceNames, Pages.Contacts _conpage)
        {
            InitializeComponent();
            rm = _rm;
            Con = _Con;
            ResourceNames = _ResourceNames;
            Tb_Name.Clear();
            Tb_Name.Text = Con.Con_Name.Trim();
            Tb_Phone.Clear();
            Tb_Phone.Text = Con.Con_Phone.Trim();
            Tb_Email.Clear();
            Tb_Email.Text = Con.Con_Email.Trim();
            conpage = _conpage;
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
                else if (ResourceNames[i] == "Title_Login")
                    this.Title = rm.GetString(ResourceNames[i].ToString());
                else if (it is TextBlock)
                    (it as TextBlock).Text = rm.GetString(ResourceNames[i]);
                else if (ResourceNames[i] == "Tooltip_InvalidPhoneCharacters")
                {
                    if (Tb_Phone.ToolTip != null)
                        Tb_Phone.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidEmailCharacters")
                {
                    if (Tb_Email.ToolTip != null)
                        Tb_Email.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
                else if (ResourceNames[i].ToString() == "Tooltip_InvalidNameCharacters")
                {
                    if (Tb_Name.ToolTip != null)
                        Tb_Name.ToolTip = (rm as ResourceManager).GetString(ResourceNames[i]);
                }
            }
        }
        void Modification()
        {
            var result = WPE.Contacts.SingleOrDefault(b => b.Con_ID == Con.Con_ID);
            if (result != null && f.IsName(Tb_Name, Tb_Name.Text.Trim(), (rm as ResourceManager)) && f.IsValidEmail(Tb_Email, Tb_Email.Text.Trim(), (rm as ResourceManager)) && f.IsPhoneNumber(Tb_Phone, Tb_Phone.Text.Trim(), (rm as ResourceManager)))
            {
                result.Con_Name = Tb_Name.Text.Trim();
                result.Con_Phone = Tb_Phone.Text.Trim();
                result.Con_Email = Tb_Email.Text.Trim();
                WPE.SaveChanges();
                RefreshContactsList re = conpage.CreateContactList;
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
        void Tb_Name_PreviewKeyDown(object sender, KeyEventArgs e)
        {           
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Modification_Click(sender, e);
            }
        }
        void Tb_Phone_PreviewKeyDown(object sender, KeyEventArgs e)
        {          
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Modification_Click(sender, e);
            }
        }
        void Tb_Email_PreviewKeyDown(object sender, KeyEventArgs e)
        {          
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BT_Modification_Click(sender, e);
            }
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
