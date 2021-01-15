﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class AddressPost
    {
        [Required]
        public string name { get; set; }

        [Required]
        public int personId { get; set; }
    }
}
