using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class Stock
    {
        private int stockId;
        private int stockNum;
        private BookBasicData bookNum;
        private BookBasicData iSBN;
        private Region regionId;
        private GoodsShelves goodsShelvesId;

        public int StockId
        {
            get
            {
                return stockId;
            }

            set
            {
                stockId = value;
            }
        }
        /// <summary>
        /// 库存id
        /// </summary>
        public int StockNum
        {
            get
            {
                return stockNum;
            }

            set
            {
                stockNum = value;
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
        public BookBasicData ISBN
        {
            get
            {
                return iSBN;
            }

            set
            {
                iSBN = value;
            }
        }
        /// <summary>
        /// 地区Id
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
        /// 货架id
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
        /// 库存无参构造函数
        /// </summary>
        public Stock() { }

        /// <summary>
        /// 库存参数构造函数
        /// </summary>
        /// <param name="stockId">库存id</param>
        /// <param name="stockNum">库存名称</param>
        /// <param name="bookNum">书号</param>
        /// <param name="iSBN">国际书号</param>
        /// <param name="regionId">地区id</param>
        /// <param name="goodsShelvesId">货架id</param>
        public Stock(int stockId, int stockNum, BookBasicData bookNum, BookBasicData iSBN, Region regionId, GoodsShelves goodsShelvesId)
        {
            this.StockId = stockId;
            this.StockNum = stockNum;
            this.BookNum = bookNum;
            this.ISBN = iSBN;
            this.reginId = reginId;
            this.GoodsShelvesId = goodsShelvesId;
        }
    }
}
