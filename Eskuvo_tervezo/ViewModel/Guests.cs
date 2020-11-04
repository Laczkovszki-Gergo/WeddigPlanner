using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskuvo_tervezo.ViewModel
{
    public class Guests
    {
        public Guests(string name, int person, string iconID)
        {
            Name = name;
            Person = person;
            IconId = iconID;
        }
        public string Name { get; set; }
        public int Person { get; set; }

        public string IconId { get; set; }
    }

}
