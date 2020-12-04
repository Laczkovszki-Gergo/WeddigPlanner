using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskuvo_tervezo.ViewModel
{
    public class VenueItem
    {
        public string Venue
        {
            get;set;
        }
        public string Address
        {
            get;set;
        }
        public VenueItem(string venue, string address)
        {
            Venue = venue.Trim();
            Address = address.Trim();
        }
    }
}
