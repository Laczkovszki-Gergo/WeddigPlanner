using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskuvo_tervezo.ViewModel
{
    public class Cont
    {
        public Cont(string name, string phone, string email, string iconId)
        {
            Name = name;
            Phone = phone;
            Email = email;
            IconId = iconId;
        }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IconId { get; set; }
    }
}
