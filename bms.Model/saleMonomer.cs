using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class SaleMonomer
    {
        private int saleIdMonomerId;
        private long  bookNum;
        private string ISBN;
        private int saleHeadId;
        private double unitPrice;
        private int number;
        private double totalPrice;
        private double realDiscount;
        private double realPrice;
        private DateTime datetime;

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="saleIdMonomerId">销售单体ID</param>
        /// <param name="bookNum">书号</param>
        /// <param name="iSBN">ISBN号</param>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <param name="unitPrice">单价</param>
        /// <param name="number">数量</param>
        /// <param name="totalPrice">码洋</param>
        /// <param name="realDiscount">实际折扣</param>
        /// <param name="realPrice">实洋</param>
        /// <param name="datetime">采集时间</param>
        public SaleMonomer(int saleIdMonomerId, long bookNum, string iSBN, int saleHeadId, double unitPrice, int number, double totalPrice, double realDiscount, double realPrice, DateTime datetime)
        {
            this.saleIdMonomerId = saleIdMonomerId;
            this.bookNum = bookNum;
            ISBN = iSBN;
            this.saleHeadId = saleHeadId;
            this.unitPrice = unitPrice;
            this.number = number;
            this.totalPrice = totalPrice;
            this.realDiscount = realDiscount;
            this.realPrice = realPrice;
            this.datetime = datetime;
        }

        /// <summary>
        /// 销售单体Id
        /// </summary>
        public int SaleIdMonomerId
        {
            get
            {
                return saleIdMonomerId;
            }

            set
            {
                saleIdMonomerId = value;
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
        /// ISBN号
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
        /// 销售单头id
        /// </summary>
        public int SaleHeadId
        {
            get
            {
                return saleHeadId;
            }

            set
            {
                saleHeadId = value;
            }
        }
        /// <summary>
        /// 单价
        /// </summary>
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
        /// <summary>
        /// 数量
        /// </summary>
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
        /// 实际折扣
        /// </summary>
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
        public DateTime Datetime
        {
            get
            {
                return datetime;
            }

            set
            {
                datetime = value;
            }
        }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SaleMonomer() { }
    }
}
