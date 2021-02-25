using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Enities
{
   public class Booking
    {
        public int ID { get; set; }
        public int AccomodationID { get; set; }
        public Accomodation Accomodation { get; set; }
        public decimal FromDate { get; set; }
        public int Duration { get; set; }

    }
}
