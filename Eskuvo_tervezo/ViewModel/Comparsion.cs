using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskuvo_tervezo.ViewModel
{
    public class Comparsion
    {
        Windows.Functions f = new Windows.Functions();
        public Comparsion(string _ExpenseName, string _Expense, string Count)
        {
            ExpenseName = _ExpenseName;
            Expense = f.CurrencyFormatInt((Convert.ToInt32(f.StringRemoveWhiteSpace(_Expense).Trim()) * Convert.ToInt32(f.StringRemoveWhiteSpace(Count).Trim())));
        }
        public string ExpenseName { get; set; }
        public string Expense { get;}

    }
}
