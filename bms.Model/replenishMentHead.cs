using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class replenishMentHead
    {
        //private string rsHeadID;
        private string saleTaskId;
        private int kindsNum;
        private int number;
        private double allTotalPrice;
        private double allRealPrice;
        private int userId;
        private int state;
        private DateTime time;
        private int deleteState;
        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="rsHeadID">补货单头ID</param>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <param name="kindsNum">品种数</param>
        /// <param name="number">总数量</param>
        /// <param name="allTotalPrice">总码洋</param>
        /// <param name="allRealPrice">总实洋</param>
        /// <param name="userId">用户Id</param>
        /// <param name="state">状态（0为采集中，1为以完成）</param>
        /// <param name="time">制单时间</param>
        /// <param name="deleteState">删除状态</param>
        public replenishMentHead( string saleTaskId, int kindsNum, int number, double allTotalPrice, double allRealPrice, int userId, int state, DateTime time, int deleteState)
        {
            //this.rsHeadID = rsHeadID;
            this.saleTaskId = saleTaskId;
            this.kindsNum = kindsNum;
            this.number = number;
            this.allTotalPrice = allTotalPrice;
            this.allRealPrice = allRealPrice;
            this.userId = userId;
            this.state = state;
            this.time = time;
            this.deleteState = deleteState;
        }
        //public string RsHeadID
        //{
        //    get
        //    {
        //        return rsHeadID;
        //    }

        //    set
        //    {
        //        rsHeadID = value;
        //    }
        //}

        public string SaleTaskId
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

        public int KindsNum
        {
            get
            {
                return kindsNum;
            }

            set
            {
                kindsNum = value;
            }
        }

        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
            }
        }

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
        /// 空的构造
        /// </summary>
        public replenishMentHead() {

        }
    }
}
