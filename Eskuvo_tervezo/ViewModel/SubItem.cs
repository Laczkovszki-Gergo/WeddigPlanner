using System.Windows.Controls;


namespace Eskuvo_tervezo.ViewModel
{
    public class SubItem
    {
        public SubItem(string subItemResourceName, string name, Page screen = null)
        {
            Name = name;
            Screen = screen;
            SubItemResourceName = subItemResourceName;
        }

        public string SubItemResourceName { get; set; }
        public string Name { get; set; }
        public Page Screen { get; set; }
    }
}
