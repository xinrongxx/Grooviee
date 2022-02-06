using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grooviee.Shared.Domain
{
    public class Feedback : BaseDomainModel
    {
        public string Rating { get; set; }
        public string Comment { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
