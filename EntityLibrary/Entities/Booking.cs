using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary.Entities
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int PropertyId { get; set; }

        public int RenterId { get; set; }

        public int NumberOfGuests { get; set; }

        public int AmoundOwned { get; set; }

        public int AmountPayed { get; set; }

        public List<BookingDate> BookingDates { get; set; }

        public Boolean Status { get; set; }

        public Boolean Cancelled { get; set; }
    }
}
