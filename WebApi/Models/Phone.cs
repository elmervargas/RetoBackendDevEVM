﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Phone
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual Person Person { get; set; }
    }
}
