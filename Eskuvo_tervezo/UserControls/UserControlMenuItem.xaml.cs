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
        private void ListViewItemMenu_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in home.Menu.Children)
            {
                if (item is UserControlMenuItem)
                    (item as UserControlMenuItem).ListViewItemMenu.Foreground = Brushes.Yellow;         
            }
            //TODO
            //if (sender is ListView)
            //{

            //    ListViewItem myListViewItem =
            //    (ListViewItem)((sender as ListView).ItemContainerGenerator.ContainerFromItem((sender as ListView).Items.CurrentItem));

            //    ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListViewItem);
            //    DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            //    TextBlock myTextBlock = (TextBlock)myDataTemplate.FindName("Tbl_Text", myContentPresenter);
            //    myTextBlock.Foreground = Brushes.Red;

            //    (sender as ListView).ItemTemplate.FindName("Tbl_Text", myContentPresenter);
            //}

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
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewMenu.SelectedItem != null)
                home.SwitchScreen(((SubItem)((ListView)sender).SelectedItem).Screen);
           
        }

        childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
