using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MembershipType
    {
        [Required]
        public string Name { get; set; }
        public byte Id { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        //if you use enum over here then you will have to cast it to byte inside the Mim18YearsIfAMember.
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;

    }
}