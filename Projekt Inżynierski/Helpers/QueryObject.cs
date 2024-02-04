using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Helpers
{
    public class QueryObject
    {
        public string NameSearch { get; set; }
        public int? CategoryId { get; set; }
        public int? CompanyId { get; set; }

        public string SortBy { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 9;
    }
}