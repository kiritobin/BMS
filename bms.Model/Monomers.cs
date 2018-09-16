using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class Monomers
    {
        private int monomersId;
        private BookBasicData bookNum;
        private BookBasicData isbn;
        private int number;
        private BookBasicData uPrice;
        private double totalPrice;
        private double realPrice;
        private double discount;
        private GoodsShelves goodsShelvesId;
        private int type;
        private SingleHead singleHeadId;

        /// <summary>
        /// 无参构造
        /// </summary>
        public Monomers() { }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="monomersId">单体ID</param>
        /// <param name="bookNum">书号</param>
        /// <param name="isbn">国际书号</param>
        /// <param name="number">数量</param>
        /// <param name="uPrice">单价</param>
        /// <param name="totalPrice">码洋</param>
        /// <param name="realPrice">实洋</param>
        /// <param name="discount">折扣</param>
        /// <param name="goodsShelvesId">货架ID</param>
        /// <param name="type">0为出库，1入库</param>
        /// <param name="singleHeadId">单头ID</param>
        public Monomers(int monomersId, BookBasicData bookNum, BookBasicData isbn, int number, BookBasicData uPrice, double totalPrice, double realPrice, double discount, GoodsShelves goodsShelvesId, int type, SingleHead singleHeadId)
        {
            this.MonomersId = monomersId;
            this.BookNum = bookNum;
            this.Isbn = isbn;
            this.Number = number;
            this.UPrice = uPrice;
            this.TotalPrice = totalPrice;
            this.RealPrice = realPrice;
            this.Discount = discount;
            this.GoodsShelvesId = goodsShelvesId;
            this.Type = type;
            this.singleHeadId = singleHeadId;
        }
        /// <summary>
        /// 单体ID
        /// </summary>
        public int MonomersId { get => monomersId; set => monomersId = value; }
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
        public int Number { get => number; set => number = value; }
        /// <summary>
        /// 单价
        /// </summary>
        public BookBasicData UPrice { get => uPrice; set => uPrice = value; }
        /// <summary>
        /// 码洋
        /// </summary>
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
        /// <summary>
        /// 实洋
        /// </summary>
        public double RealPrice { get => realPrice; set => realPrice = value; }
        /// <summary>
        /// 折扣
        /// </summary>
        public double Discount { get => discount; set => discount = value; }
        /// <summary>
        /// 货架ID
        /// </summary>
        public GoodsShelves GoodsShelvesId { get => goodsShelvesId; set => goodsShelvesId = value; }
        /// <summary>
        /// 0为出库，1入库
        /// </summary>
        public int Type { get => type; set => type = value; }
        /// <summary>
        /// 单头ID
        /// </summary>
        public SingleHead SingleHeadId { get => singleHeadId; set => singleHeadId = value; }
    }
}
