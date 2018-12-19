using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bms.Web.wechat
{
    public class loginmsg
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 销售任务ID
        /// </summary>
        public string saletaskID { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public string customID { get; set; }
    }
}