using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Documenttype { get; set; }
        public string Documentnumber { get; set; }
        public byte[] Photo { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
    }
}
