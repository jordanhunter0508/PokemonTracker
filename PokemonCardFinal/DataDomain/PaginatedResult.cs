using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    /// <summary>
    /// Stores a list of items and other details 
    /// related to how many pages the items can make.
    /// </summary>
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalCount { get; set; } = 0;
        public int TotalPages { get; set; } = 1;
    }
}