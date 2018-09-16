using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class SingleHead
    {
        private int singleHeadId;
        private DateTime time;
        private Region regionId;
        private User userId;
        private User userName;
        private int allBillCount;
        private double allTotalPrice;
        private double allRealPrice;
        private int type;
        /// <summary>
        /// 单头id
        /// </summary>
        public int SingleHeadId
        {
            get
            {
                return singleHeadId;
            }

            set
            {
                singleHeadId = value;
            }
        }
        /// <summary>
        /// 制单时间
        /// </summary>
        public DateTime Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
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
        /// 用户名
        /// </summary>
        public User UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }
        /// <summary>
        /// 单据总数量
        /// </summary>
        public int AllBillCount
        {
            get
            {
                return allBillCount;
            }

            set
            {
                allBillCount = value;
            }
        }
        /// <summary>
        /// 总码洋
        /// </summary>
        public double AllTotalPrice
        {
            get
            {
                return allTotalPrice;
            }

            set
            {
                allTotalPrice = value;
            }
        }
        /// <summary>
        /// 总实洋
        /// </summary>
        public double AllRealPrice
        {
            get
            {
                return allRealPrice;
            }

            set
            {
                allRealPrice = value;
            }
        }
        /// <summary>
        /// 状态（0为出库，1为入库）
        /// </summary>
        public int Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }



        /// <summary>
        /// 单头无参构造函数
        /// </summary>
        public SingleHead() { }

        /// <summary>
        /// 单头参数构造函数
        /// </summary>
        /// <param name="singleHeadId">单头id</param>
        /// <param name="time">制单时间</param>
        /// <param name="regionId">地区id'</param>
        /// <param name="userId">用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="allBillCount">单据总数量</param>
        /// <param name="allTotalPrice">总码洋</param>
        /// <param name="allRealPrice">总实洋</param>
        /// <param name="type">状态（0为出库，1为入库）</param>
        public SingleHead(int singleHeadId, DateTime time, Region regionId, User userId, User userName, int allBillCount, double allTotalPrice, double allRealPrice, int type)
        {
            this.SingleHeadId = singleHeadId;
            this.Time = time;
            this.RegionId = regionId;
            this.UserId = userId;
            this.UserName = userName;
            this.AllBillCount = allBillCount;
            this.AllTotalPrice = allTotalPrice;
            this.AllRealPrice = allRealPrice;
            this.Type = type;
        }
    }
}
