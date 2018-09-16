using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class Region
    {
        private int regionId;
        private string regionName;

        /// <summary>
        /// 无参构造
        /// </summary>
        public Region() { }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="regionId">地区ID</param>
        /// <param name="regionName">地区名称</param>
        public Region(int regionId, string regionName)
        {
            this.RegionId = regionId;
            this.RegionName = regionName;
        }
        /// <summary>
        /// 地区ID
        /// </summary>
        public int RegionId { get => regionId; set => regionId = value; }
        /// <summary>
        /// 地区名称
        /// </summary>
        public string RegionName { get => regionName; set => regionName = value; }
    }
}
