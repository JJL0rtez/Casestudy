﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasestudyAPI.Helpers
{
    public class OrderHelper
    {
        public string Email { get; set; }
        public OrderSelectionHelper[] Selections { 
            get; 
            set;
        }

        
    }
}
