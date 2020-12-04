using System.Windows.Controls;


namespace Eskuvo_tervezo.ViewModel
{
    public class SubItem
    {
        public SubItem(string name, Page screen = null)
        {
            Name = name;
            Screen = screen;
        }

        public string Name { get; set; }
        public Page Screen { get; set; }
    }
}
