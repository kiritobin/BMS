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
        public int SingleHeadId { get => singleHeadId; set => singleHeadId = value; }
        /// <summary>
        /// 制单时间
        /// </summary>
        public DateTime Time { get => time; set => time = value; }
        /// <summary>
        /// 地区id
        /// </summary>
        public Region RegionId { get => regionId; set => regionId = value; }
        /// <summary>
        /// 用户id
        /// </summary>
        public User UserId { get => userId; set => userId = value; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public User UserName { get => userName; set => userName = value; }
        /// <summary>
        /// 单据总数量
        /// </summary>
        public int AllBillCount { get => allBillCount; set => allBillCount = value; }
        /// <summary>
        /// 总码洋
        /// </summary>
        public double AllTotalPrice { get => allTotalPrice; set => allTotalPrice = value; }
        /// <summary>
        /// 总实洋
        /// </summary>
        public double AllRealPrice { get => allRealPrice; set => allRealPrice = value; }

        public int Type { get => type; set => type = value; }

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
            this.type = type;
        }
    }
}
