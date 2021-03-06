﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bms.Web.wechat
{
    public class retailM
    {
        /// <summary>
        /// 数据
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public string regionId { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        public string regionName { get; set; }
        /// <summary>
        /// 数据成功类型
        /// </summary>
        public string type { get; set; }
        ///// <summary>
        ///// 书号
        ///// </summary>
        //public string bookNum { get; set; }
        ///// <summary>
        ///// 书名
        ///// </summary>
        //public string bookName { get; set; }
        ///// <summary>
        ///// 单价
        ///// </summary>
        //public string price { get; set; }
        ///// <summary>
        ///// isbn
        ///// </summary>
        //public string ISBN { get; set; }
        /// <summary>
        /// 码洋
        /// </summary>
        public string allTotalPrice { get; set; }
        /// <summary>
        /// 实洋
        /// </summary>
        public string allRealPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string allNumber { get; set; }
        /// <summary>
        /// 种类
        /// </summary>
        public string kindsNum { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public string discount { get; set; }
        /// <summary>
        /// 零售单头ID
        /// </summary>
        public string retailHeadId { get; set; }

    }
}