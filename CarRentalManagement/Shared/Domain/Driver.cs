using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grooviee.Shared.Domain
{
    public class Driver : BaseDomainModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DrivingLicense { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public int Rating { get; set; }
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public int FeedbackId { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
