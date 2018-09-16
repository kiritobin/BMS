using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    /// <summary>
    /// 销售任务实体类
    /// </summary>
    public class SaleTask
    {
        private int saleTaskId;
        private int userId;
        private double defaultDiscount;
        private string defaultCopy;
        private int numberLimit;
        private double priceLimit;
        private double totalPiceLimit;
        private DateTime startTime;
        private DateTime finishTime;

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <param name="userId">操作员Id</param>
        /// <param name="defaultDiscount">默认折扣</param>
        /// <param name="defaultCopy">默认复本</param>
        /// <param name="numberLimit">最大采购数量</param>
        /// <param name="priceLimit">单价上限</param>
        /// <param name="totalPiceLimit">码洋上限</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="finishTime">结束时间</param>
        public SaleTask(int saleTaskId, int userId, double defaultDiscount, string defaultCopy, int numberLimit, double priceLimit, double totalPiceLimit, DateTime startTime, DateTime finishTime)
        {
            this.saleTaskId = saleTaskId;
            this.userId = userId;
            this.defaultDiscount = defaultDiscount;
            this.defaultCopy = defaultCopy;
            this.numberLimit = numberLimit;
            this.priceLimit = priceLimit;
            this.totalPiceLimit = totalPiceLimit;
            this.startTime = startTime;
            this.finishTime = finishTime;
        }
        /// <summary>
        /// 销售任务ID
        /// </summary>
        public int SaleTaskId
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
        /// 操作员Id
        /// </summary>
        public int UserId
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
        /// 默认折扣
        /// </summary>
        public double DefaultDiscount
        {
            get
            {
                return defaultDiscount;
            }

            set
            {
                defaultDiscount = value;
            }
        }
        /// <summary>
        /// 默认复本
        /// </summary>
        public string DefaultCopy
        {
            get
            {
                return defaultCopy;
            }

            set
            {
                defaultCopy = value;
            }
        }
        /// <summary>
        /// 最大采购数量
        /// </summary>
        public int NumberLimit
        {
            get
            {
                return numberLimit;
            }

            set
            {
                numberLimit = value;
            }
        }
        /// <summary>
        /// 单价上限
        /// </summary>
        public double PriceLimit
        {
            get
            {
                return priceLimit;
            }

            set
            {
                priceLimit = value;
            }
        }
        /// <summary>
        /// 码洋上限
        /// </summary>
        public double TotalPiceLimit
        {
            get
            {
                return totalPiceLimit;
            }

            set
            {
                totalPiceLimit = value;
            }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
            }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime FinishTime
        {
            get
            {
                return finishTime;
            }

            set
            {
                finishTime = value;
            }
        }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SaleTask() { }
    }
}
