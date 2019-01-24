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
        private string shelvesId;
        /// <summary>
        /// 单体
        /// </summary>
        public int MonomersId
        {
            get
            {
                return monomersId;
            }

            set
            {
                monomersId = value;
            }
        }
        /// <summary>
        /// 书号
        /// </summary>
        public BookBasicData BookNum
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
        public BookBasicData Isbn
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
        /// 单价
        /// </summary>
        public BookBasicData UPrice
        {
            get
            {
                return uPrice;
            }

            set
            {
                uPrice = value;
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
        /// 折扣
        /// </summary>
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
        /// 货架ID
        /// </summary>
        public GoodsShelves GoodsShelvesId
        {
            get
            {
                return goodsShelvesId;
            }

            set
            {
                goodsShelvesId = value;
            }
        }
        /// <summary>
        /// 0为出库，1入库
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
        /// 单头ID
        /// </summary>
        public SingleHead SingleHeadId
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
        /// 货架ID
        /// </summary>
        public string ShelvesId
        {
            get
            {
                return shelvesId;
            }

            set
            {
                shelvesId = value;
            }
        }

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
        public Monomers(int monomersId, BookBasicData bookNum, BookBasicData isbn, int number, BookBasicData uPrice, double totalPrice, double realPrice, double discount, GoodsShelves goodsShelvesId, int type, SingleHead singleHeadId,string shelvesId)
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
            this.SingleHeadId = singleHeadId;
            this.ShelvesId = shelvesId;
        }

    }
}
