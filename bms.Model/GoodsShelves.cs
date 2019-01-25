using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class GoodsShelves
    {
        private string goodsShelvesId;
        private string shelvesName;
        private Region regionId;
        /// <summary>
        /// 货架ID
        /// </summary>
        public string GoodsShelvesId
        {
            get
            {
                return goodsShelvesId;
            }

            set
            {
                goodsShelvesId = value;
            }
        }
        /// <summary>
        /// 货架名称
        /// </summary>
        public string ShelvesName
        {
            get
            {
                return shelvesName;
            }

            set
            {
                shelvesName = value;
            }
        }
        /// <summary>
        /// 地区Id
        /// </summary>
        public Region RegionId
        {
            get
            {
                return regionId;
            }

            set
            {
                regionId = value;
            }
        }

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
        public GoodsShelves(string goodsShelvesId, string shelvesName, Region regionId)
        {
            this.GoodsShelvesId = goodsShelvesId;
            this.ShelvesName = shelvesName;
            this.RegionId = regionId;
        }

        
    }
}
