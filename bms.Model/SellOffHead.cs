using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class SellOffHead
    {
        private int sellOffHeadId;
        private SaleTask saleTaskId;
        private string kinds;
        private int count;
        private double totalPrice;
        private double realPrice;
        private User userId;
        private int state;
        private Region regionId;
        private DateTime makingTime;
        /// <summary>
        /// 销退单头id
        /// </summary>
        public int SellOffHeadId
        {
            get
            {
                return sellOffHeadId;
            }

            set
            {
                sellOffHeadId = value;
            }
        }
        /// <summary>
        /// 销售任务id
        /// </summary>
        public SaleTask SaleTaskId
        {
            get
            {
                return saleTaskId;
            }

            set
            {
                saleTaskId = value;
            }
        }
        /// <summary>
        /// 品种
        /// </summary>
        public string Kinds
        {
            get
            {
                return kinds;
            }

            set
            {
                kinds = value;
            }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }
        /// <summary>
        /// 码洋
        /// </summary>
        public double TotalPrice
        {
            get
            {
                return totalPrice;
            }

            set
            {
                totalPrice = value;
            }
        }
        /// <summary>
        /// 实洋
        /// </summary>
        public double RealPrice
        {
            get
            {
                return realPrice;
            }

            set
            {
                realPrice = value;
            }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public User UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }
        /// <summary>
        /// 状态（0为未处理，1为已处理）
        /// </summary>
        public int State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }
        /// <summary>
        /// 地区id
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
        /// 制单时间
        /// </summary>
        public DateTime MakingTime
        {
            get
            {
                return makingTime;
            }

            set
            {
                makingTime = value;
            }
        }

        /// <summary>
        /// 销退单头无参构造函数
        /// </summary>
        public SellOffHead() { }

        /// <summary>
        /// 销退单头参数构造函数
        /// </summary>
        /// <param name="sellOffHeadId">销退单头</param>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="kinds">种类</param>
        /// <param name="count">数量</param>
        /// <param name="totalPrice">码洋</param>
        /// <param name="realPrice">实洋</param>
        /// <param name="userId">用户id</param>
        /// <param name="state">状态（0为未处理，1为已处理）</param>
        /// <param name="regionId">地区id</param>
        /// <param name="makingTime">制单时间</param>
        public SellOffHead(int sellOffHeadId, SaleTask saleTaskId, string kinds, int count, double totalPrice, double realPrice, User userId, int state, Region regionId, DateTime makingTime)
        {
            this.sellOffHeadId = sellOffHeadId;
            this.saleTaskId = saleTaskId;
            this.kinds = kinds;
            this.count = count;
            this.totalPrice = totalPrice;
            this.realPrice = realPrice;
            this.userId = userId;
            this.state = state;
            this.regionId = regionId;
            this.makingTime = makingTime;
        }
    }
}
