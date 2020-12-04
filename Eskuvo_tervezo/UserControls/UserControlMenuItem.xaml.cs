using Eskuvo_tervezo.ViewModel;
using Eskuvo_tervezo.UserControls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Resources;

namespace Eskuvo_tervezo.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlMenuItem.xaml
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        Windows.Home home;
        object rm = null;
        public UserControlMenuItem(ItemMenu itemMenu, Windows.Home _home, ResourceManager _rm)
        {
            InitializeComponent();      
            home = _home;
            rm = _rm;
            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;
            this.DataContext = itemMenu;
        }

        void MenuItemclick(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < home.Menu.Children.Count; i++)
            {
                if (home.Menu.Children[i] is UserControlMenuItem)
                    (home.Menu.Children[i] as UserControlMenuItem).ListViewItemMenu.Foreground = Brushes.Yellow;
            }
            //TODO kiválasztott menü elemek piros betűszíne

            if (sender is ListBoxItem)
            {
                ItemMenu it = ((ItemMenu)((ListBoxItem)sender).DataContext);
                (sender as ListBoxItem).Foreground = Brushes.Red;

                if ((string)(sender as ListBoxItem).Content == (rm as ResourceManager).GetString("Menu_Exit"))
                {
                    home.Exit_Click(sender, e);
                }
                else if ((string)(sender as ListBoxItem).Content == (rm as ResourceManager).GetString("Menu_Logout"))
                {
                    home.Back_Click(sender, e);
                }
                else
                    home.SwitchScreen(it.Screen);

            }
            ExpanderMenu.IsExpanded = true;
            ListViewMenu.SelectedItem = null;
        }
        void MenuSubItemClick(object sender)
        {
            if (ListViewMenu.SelectedItem != null)
                home.SwitchScreen(((SubItem)((ListView)sender).SelectedItem).Screen);
        }

        void ListViewItemMenu_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MenuItemclick(sender,e);
        }
        void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MenuSubItemClick(sender);
           
        }
    }
}
