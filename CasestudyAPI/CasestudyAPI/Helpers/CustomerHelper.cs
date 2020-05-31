using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasestudyAPI.Helpers
{
    public class CustomerHelper
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? token { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

    }
}
