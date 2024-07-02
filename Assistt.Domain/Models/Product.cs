﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Domain.Models
{
    public class Product : BaseEntity, ISort
    {
        public string Name { get; set; }
        public int SortOrder { get; set; }
    }
}
