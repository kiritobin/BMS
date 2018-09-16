﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class GoodsShelves
    {
        private int goodsShelvesId;
        private string shelvesName;
        private Region regionId;

        /// <summary>
        /// 无参构造
        /// </summary>
        public GoodsShelves() { }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="goodsShelvesId">货架ID</param>
        /// <param name="shelvesName">货架名称</param>
        /// <param name="regionId">地区ID</param>
        public GoodsShelves(int goodsShelvesId, string shelvesName, Region regionId)
        {
            this.GoodsShelvesId = goodsShelvesId;
            this.ShelvesName = shelvesName;
            this.RegionId = regionId;
        }

        /// <summary>
        /// 货架ID
        /// </summary>
        public int GoodsShelvesId { get => goodsShelvesId; set => goodsShelvesId = value; }
        /// <summary>
        /// 货架名称
        /// </summary>
        public string ShelvesName { get => shelvesName; set => shelvesName = value; }
        /// <summary>
        /// 地区ID
        /// </summary>
        public Region RegionId { get => regionId; set => regionId = value; }
    }
}
