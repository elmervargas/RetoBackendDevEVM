using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class PersonPost
    {
        [Required]
        public string firstname { get; set; }

        [Required]
        public string lastname { get; set; }

        [Required]
        public string birthdate { get; set; }

        [Required]
        public string documenttype { get; set; }

        [Required]
        public string documentnumber { get; set; }

        public virtual List<AddressPost> addresses { get; set; }
        public virtual List<EmailPost> emails { get; set; }
        public virtual List<PhonePost> phones { get; set; }
    }
}
