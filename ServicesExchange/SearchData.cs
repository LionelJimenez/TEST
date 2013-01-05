using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesExchange
{
    public class SearchData
    {
        public string MC { get; set; }
        public string Cat { get; set; }

        public SearchData(string mc, string cat)
        {
            MC = mc;
            Cat = cat;
        }
    }
}