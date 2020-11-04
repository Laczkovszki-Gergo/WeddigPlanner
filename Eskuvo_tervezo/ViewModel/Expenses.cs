using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskuvo_tervezo.ViewModel
{
    public class Expenses
    {
        Windows.Functions f = new Windows.Functions();
        public Expenses(string expanse, string cost, int id, string count)
        {
            ID = id;
            Expanse = expanse;
            Cost = cost;
            Count = count;
            Amount = f.CurrencyFormatInt((Convert.ToInt32(f.StringRemoveWhiteSpace(Cost).Trim()) * Convert.ToInt32(f.StringRemoveWhiteSpace(Count).Trim())));
        }
        public int ID { get; set; }
        public string Count { get; set; }
        public string Expanse { get; set; }
        public string Cost { get; set; }
        public string Amount { get; }
    }
}
