using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    /// <summary>
    /// 销退单头
    /// </summary>
    public class SellOffMonomer
    {
        private string sellOffMonomerId;
        private string sellOffHeadId;
        private long bookNum;
        private string ISBN;
        private double price;
        private int count;
        private double totalPrice;
        private double realPrice;
        private DateTime time;
        private int deleteSate;
        private int savaSate;
        private double discount;
        /// <summary>
        /// 销退单体Id
        /// </summary>
        public string SellOffMonomerId
        {
            get
            {
                return sellOffMonomerId;
            }

            set
            {
                sellOffMonomerId = value;
            }
        }
        /// <summary>
        /// 销退单头Id
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
        /// 书号
        /// </summary>
        public long BookNum
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
        /// <summary>
        /// 国际书号
        /// </summary>
        public string ISBN1
        {
            get
            {
                return ISBN;
            }

            set
            {
                ISBN = value;
            }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
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
        /// 删除状态
        /// </summary>
        public int DeleteSate
        {
            get
            {
                return deleteSate;
            }

            set
            {
                deleteSate = value;
            }
        }
        /// <summary>
        /// 保存状态
        /// </summary>
        public int SavaSate
        {
            get
            {
                return savaSate;
            }

            set
            {
                savaSate = value;
            }
        }
        //实际折扣
        public double Discount
        {
            get
            {
                return discount;
            }

            set
            {
                discount = value;
            }
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SellOffMonomer() { }
        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="sellOffMonomerId">销退单体Id</param>
        /// <param name="sellOffHead">销退单头Id</param>
        /// <param name="bookNum">书号</param>
        /// <param name="iSBN">国际书号</param>
        /// <param name="price">定价</param>
        /// <param name="count">数量</param>
        /// <param name="totalPrice">码洋</param>
        /// <param name="realPrice">实洋</param>
        /// <param name="dateTime">制单时间</param>
        /// <param name="deleteSate">删除状态</param>
        /// <param name="savaSate">保存状态</param>
        public SellOffMonomer(string sellOffMonomerId, string sellOffHeadId, long bookNum, string iSBN, double price, int count, double totalPrice, double realPrice, DateTime time, int deleteSate, int savaSate, double discount)
        {
            this.sellOffMonomerId = sellOffMonomerId;
            this.sellOffHeadId = sellOffHeadId;
            this.bookNum = bookNum;
            ISBN = iSBN;
            this.price = price;
            this.count = count;
            this.totalPrice = totalPrice;
            this.realPrice = realPrice;
            this.time = time;
            this.deleteSate = deleteSate;
            this.savaSate = savaSate;
            this.discount = discount;
        }
    }
}
