using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using bms.Model;

namespace bms.Model
{
    public class replenishMentMonomer
    {
        private int rsMonomerID;
        private string bookNum;
        private string isbn;
        private replenishMentHead rsHeadID;
        private double unitPrice;
        private int count;
        private double totalPrice;
        private double realDiscount;
        private double realPrice;
        private DateTime time;
        private int deleteState;
        /// <summary>
        //参数构造函数
        /// </summary>
        /// <param name="rsMonomerID">补货单体ID</param>
        /// <param name="bookNum">书号</param>
        /// <param name="isbn">国际书号</param>
        /// <param name="rsHeadID">补货单头ID</param>
        /// <param name="unitPrice">定价</param>
        /// <param name="count">数量</param>
        /// <param name="totalPrice">码洋</param>
        /// <param name="realDiscount">默认折扣</param>
        /// <param name="realPrice">实洋</param>
        /// <param name="time">补货时间</param>
        /// <param name="deleteState">删除状态</param>
        public replenishMentMonomer(int rsMonomerID, string bookNum, string isbn, replenishMentHead rsHeadID, double unitPrice, int count, double totalPrice, double realDiscount, double realPrice, DateTime time, int deleteState)
        {
            this.rsMonomerID = rsMonomerID;
            this.bookNum = bookNum;
            this.isbn = isbn;
            this.rsHeadID = rsHeadID;
            this.unitPrice = unitPrice;
            this.count = count;
            this.totalPrice = totalPrice;
            this.realDiscount = realDiscount;
            this.realPrice = realPrice;
            this.time = time;
            this.deleteState = deleteState;
        }

        public int RsMonomerID
        {
            get
            {
                return rsMonomerID;
            }

            set
            {
                rsMonomerID = value;
            }
        }

        public string BookNum
        {
            get
            {
                return bookNum;
            }

            set
            {
                bookNum = value;
            }
        }

        public string Isbn
        {
            get
            {
                return isbn;
            }

            set
            {
                isbn = value;
            }
        }

        public replenishMentHead RsHeadID
        {
            get
            {
                return rsHeadID;
            }

            set
            {
                rsHeadID = value;
            }
        }

        public double UnitPrice
        {
            get
            {
                return unitPrice;
            }

            set
            {
                unitPrice = value;
            }
        }

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

        public double RealDiscount
        {
            get
            {
                return realDiscount;
            }

            set
            {
                realDiscount = value;
            }
        }

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
    }
}
