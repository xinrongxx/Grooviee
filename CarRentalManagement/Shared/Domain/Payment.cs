using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grooviee.Shared.Domain
{
    public class Payment : BaseDomainModel
    {
        public int Amount { get; set; }
        public string Type { get; set; }
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
