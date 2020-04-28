using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class RentalDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }

        public CustomerDto Customer { get; set; }

        public MovieDto Movie { get; set; }

        public DateTime DateRented { get; set; }
        public string DateRentedFormatted { get { return this.DateRented.ToString("dd/MM/yyyy"); } }

        //? as it is nullable
        public DateTime? DateReturned { get; set; }
        public string DateReturnedFormatted { get { return this.DateReturned.HasValue? this.DateReturned.Value.ToString("dd/MM/yyyy") : string.Empty; } }

    }
}