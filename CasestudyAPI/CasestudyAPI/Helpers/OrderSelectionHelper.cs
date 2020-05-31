using CaseStudyAPI.DAL.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasestudyAPI.Helpers
{
    public class OrderSelectionHelper
    {
        public int Id { get; set; }
        public int Qty { get; set; }
        public OrderLineItem item { get; set; }
    }
}
