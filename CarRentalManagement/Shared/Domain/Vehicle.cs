using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grooviee.Shared.Domain
{
    public class Vehicle : BaseDomainModel
    {
        [Required]
        [DataType(DataType.Date)]
        public int Year { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z]{3}\d{4}[A-Za-z]", ErrorMessage = "License Plate Number does not meet requirements")]
        public string LicensePlateNumber { get; set; }
        public int? MakeId { get; set; }
        public virtual Make Make { get; set; }
        public int? ModelId { get; set; }
        public virtual Model Model { get; set; }
        public int? ColourId { get; set; }
        public virtual Colour Colour { get; set; }
        public virtual List<Booking> Bookings { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double RentalRate { get; set; }
    }
}
