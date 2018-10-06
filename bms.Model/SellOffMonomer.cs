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
        private string sellOffHead;
        private long bookNum;
        private string ISBN;
        private double price;
        private int count;
        private double totalPrice;
        private double realPrice;
        private DateTime dateTime;
        private int deleteSate;
        private int savaSate;
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
        public string SellOffHead
        {
            get
            {
                return sellOffHead;
            }

            set
            {
                sellOffHead = value;
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
        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }

            set
            {
                dateTime = value;
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
        public SellOffMonomer(string sellOffMonomerId, string sellOffHead, long bookNum, string iSBN, double price, int count, double totalPrice, double realPrice, DateTime dateTime, int deleteSate, int savaSate)
        {
            this.SellOffMonomerId = sellOffMonomerId;
            this.SellOffHead = sellOffHead;
            this.BookNum = bookNum;
            ISBN1 = iSBN;
            this.Price = price;
            this.Count = count;
            this.TotalPrice = totalPrice;
            this.RealPrice = realPrice;
            this.DateTime = dateTime;
            this.DeleteSate = deleteSate;
            this.SavaSate = savaSate;
        }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SellOffMonomer() { }
    }
}
