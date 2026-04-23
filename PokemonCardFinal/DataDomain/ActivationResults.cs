using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    /// <summary>
    /// Used for activate or deacivate where more than 1 row will be affected.
    /// </summary>
    public class ActivationResults
    {
        public int UpdatedCount { get; set; } = 0;
        public int ExpectedCount { get; set; } = 1;
    }
}
