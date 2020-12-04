using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskuvo_tervezo.ViewModel;

namespace Eskuvo_tervezo.ViewModel
{
    public class CalLogEntry
    {

        public CalLogEntry(string logEntry, string iconId)
        {
            LogEntry = logEntry;
            IconId = iconId;
        }
        public string LogEntry { get; set; }
        public string IconId { get; set; }
    }
}
