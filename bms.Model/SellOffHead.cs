using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class SellOffHead
    {
        private string sellOffHeadId;
        private SaleTask saleTaskId;
        private int kinds;
        private int count;
        private double totalPrice;
        private double realPrice;
        private User user;
        private int state;
        private DateTime makingTime;
        private int deleteState;
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SellOffHead() { }
        /// <summary>
        /// 销退ID
        /// </summary>
        public string SellOffHeadId
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
        /// 品种数
        /// </summary>
        public int Kinds
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
        /// 总数量
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
        /// 用户
        /// </summary>
        public User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }
        /// <summary>
        /// 状态(0:新建单据 状态为“采集中”;1:单据完成后 状态为“已完成”)
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
        /// 删除状态
        /// </summary>
        public int DeleteState
        {
            get
            {
                return deleteState;
            }

            set
            {
                deleteState = value;
            }
        }
        /// <summary>
        /// 销售任务Id
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
        /// 参数构造函数
        /// </summary>
        /// <param name="sellOffHeadId">销退ID</param>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <param name="kinds">品种数</param>
        /// <param name="count">总数量</param>
        /// <param name="totalPrice">码洋</param>
        /// <param name="realPrice">实洋</param>
        /// <param name="user">操作员</param>
        /// <param name="state">状态(0:新建单据 状态为“采集中”;1:单据完成后 状态为“已完成”)</param>
        /// <param name="makingTime">制单时间</param>
        /// <param name="deleteState">删除状态</param>
        public SellOffHead(string sellOffHeadId, SaleTask saleTaskId, int kinds, int count, double totalPrice, double realPrice, User user, int state, DateTime makingTime, int deleteState)
        {
            this.SellOffHeadId = sellOffHeadId;
            this.SaleTaskId = saleTaskId;
            this.Kinds = kinds;
            this.Count = count;
            this.TotalPrice = totalPrice;
            this.RealPrice = realPrice;
            this.User = user;
            this.State = state;
            this.MakingTime = makingTime;
            this.DeleteState = deleteState;
        }
    }
}
