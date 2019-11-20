﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cupcakes.Models
{
    public class Bakery
    {
        [Key]
        public int BakeryId { get; set; }
        [StringLength(50, MinimumLength = 4)]
        public string BakeryName { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Cupcake> Cupcakes { get; set; }
    }
}
