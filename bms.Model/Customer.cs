using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class Customer
    {
        private int customerId;
        private string customerName;
        private Region regionId;
        
        /// <summary>
        /// 无参构造
        /// </summary>
        public Customer() { }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="customerName">客户姓名</param>
        /// <param name="regionId">地区ID</param>
        public Customer(int customerId, string customerName, Region regionId)
        {
            this.CustomerId = customerId;
            this.CustomerName = customerName;
            this.RegionId = regionId;
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerId
        {
            get
            {
                return customerId;
            }

            set
            {
                customerId = value;
            }
        }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName
        {
            get
            {
                return customerName;
            }

            set
            {
                customerName = value;
            }
        }
        /// <summary>
        /// 地区ID
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
    }
}
