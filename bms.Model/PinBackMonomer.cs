using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class PinBackMonomer
    {
        private int pinBackMonomerId;
        private BookBasicData bookNum;
        private BookBasicData isbn;
        private int count;
        private BookBasicData unitPrice;
        private double totalPrice;
        private double realPrice;
        private double realDiscount;
        private DateTime acquisitionTime;

        /// <summary>
        /// 无参构造
        /// </summary>
        public PinBackMonomer() { }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="pinBackMonomerId">销退单体ID</param>
        /// <param name="bookNum">书号</param>
        /// <param name="isbn">国际书号</param>
        /// <param name="count">数量</param>
        /// <param name="unitPrice">单价</param>
        /// <param name="totalPrice">码洋</param>
        /// <param name="realPrice">实洋</param>
        /// <param name="realDiscount">实际折扣</param>
        /// <param name="acquisitionTime">采集时间</param>
        public PinBackMonomer(int pinBackMonomerId, BookBasicData bookNum, BookBasicData isbn, int count, BookBasicData unitPrice, double totalPrice, double realPrice, double realDiscount, DateTime acquisitionTime)
        {
            this.PinBackMonomerId = pinBackMonomerId;
            this.BookNum = bookNum;
            this.Isbn = isbn;
            this.Count = count;
            this.UnitPrice = unitPrice;
            this.TotalPrice = totalPrice;
            this.RealPrice = realPrice;
            this.RealDiscount = realDiscount;
            this.AcquisitionTime = acquisitionTime;
        }
        /// <summary>
        /// 销退单体ID
        /// </summary>
        public int PinBackMonomerId { get => pinBackMonomerId; set => pinBackMonomerId = value; }
        /// <summary>
        /// 书号
        /// </summary>
        public BookBasicData BookNum { get => bookNum; set => bookNum = value; }
        /// <summary>
        /// 国际书号
        /// </summary>
        public BookBasicData Isbn { get => isbn; set => isbn = value; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get => count; set => count = value; }
        /// <summary>
        /// 单价
        /// </summary>
        public BookBasicData UnitPrice { get => unitPrice; set => unitPrice = value; }
        /// <summary>
        /// 码洋
        /// </summary>
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
        /// <summary>
        /// 实洋
        /// </summary>
        public double RealPrice { get => realPrice; set => realPrice = value; }
        /// <summary>
        /// 实际折扣
        /// </summary>
        public double RealDiscount { get => realDiscount; set => realDiscount = value; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime AcquisitionTime { get => acquisitionTime; set => acquisitionTime = value; }
    }
}
