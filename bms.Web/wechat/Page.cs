using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bms.Web.wechat
{
    public class Page
    {
        public string data { get; set; }
        public string summarydata { get; set; }
        public int totalCount { get; set; }
        public int intPageCount { get; set; }
        public int currentPage { get; set; }
    }
}