using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskuvo_tervezo.ViewModel
{
    public class DateView
    {
        public DateView(DateTime day, List<string> entrys)
        {
            Day = day.ToString("yyyy-MM-dd");

            for (int i = 0; i < entrys.Count; i++)
            {
                if (i + 1 < entrys.Count)
                    Entry += entrys[i] + Environment.NewLine;
                else
                    Entry += entrys[i];
            }
        }
        public string Day { get; set; }
        public string Entry { get; set; }
    }
}
