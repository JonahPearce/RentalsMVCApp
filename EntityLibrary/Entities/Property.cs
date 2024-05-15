using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary.Entities
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Please enter your address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your City.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your Province or State.")]
        [RegularExpression("^[a-zA-Z]{2}$", ErrorMessage = "Your Province or State should be a 2-letter code.")]
        public string ProvinceOrState { get; set; } = null!;

        [Required(ErrorMessage = "Please enter your Zip or Postal code.")]
        [RegularExpression(@"^(?!.*[DFIOQU])[A-VXY]\d[A-Z] ?\d[A-Z]\d$|^\d{5}(-\d{4})?$", ErrorMessage = "Your code should be a valid zip/postal code.")]
        public string ZipOrPostalCode { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the number of Bedrooms.")]
        [RegularExpression("^[1-9]\\d{0,1}$", ErrorMessage = "Please provide a reasonable number of bedrooms.")]
        public int Bedrooms { get; set; }

        [Required(ErrorMessage = "Please enter the number of Bathrooms.")]
        [RegularExpression("^[1-9]\\d{0,1}$", ErrorMessage = "Please provide a reasonable number of bedrooms.")]
        public int Bathrooms { get; set; }

        [Required(ErrorMessage = "Please enter the price you want.")]
        [RegularExpression("^(?:[1-9]\\d{0,2}(?:,\\d{3})+|[1-9]\\d{0,2}(?:\\.\\d{1,2})?|[1-9]\\d{0,})$",
            ErrorMessage = "Please provide a reasonable property renting price.")]
        public double PricePerNight { get; set; }

        public string? Description { get; set; }

        [NotMapped]
        public List<string>? Appliances { get; set; }

        public int OwnerID { get; set; }

        public List<Booking>? Bookings { get; set; }
    }
}
