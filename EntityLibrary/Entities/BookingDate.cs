using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary.Entities
{
    public class BookingDate
    {
        [Key]
        public int BookingDateId { get; set; }

        public DateTime Date { get; set; }

        public int BookingId { get; set; }

        public Booking Booking { get; set; }
    }
}
