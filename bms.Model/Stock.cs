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
        /// <summary>
        /// 库存id
        /// </summary>
        public int StockId { get => stockId; set => stockId = value; }
        /// <summary>
        /// 库存名称
        /// </summary>
        public int StockNum { get => stockNum; set => stockNum = value; }
        /// <summary>
        /// 书号
        /// </summary>
        public BookBasicData BookNum { get => bookNum; set => bookNum = value; }
        /// <summary>
        /// 国际书号
        /// </summary>
        public BookBasicData ISBN { get => iSBN; set => iSBN = value; }
        /// <summary>
        /// 地区id
        /// </summary>
        public Region ReginId { get => regionId; set => reginId = value; }
        /// <summary>
        /// 货架id
        /// </summary>
        public GoodsShelves GoodsShelvesId { get => goodsShelvesId; set => goodsShelvesId = value; }

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
            this.stockId = stockId;
            this.stockNum = stockNum;
            this.bookNum = bookNum;
            this.iSBN = iSBN;
            this.reginId = reginId;
            this.goodsShelvesId = goodsShelvesId;
        }
    }
}
