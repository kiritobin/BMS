using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class BookBasicData
    {
        private long bookNum;
        private string isbn;
        private string bookName;
        private DateTime publishTime;
        private double price;
        private string publisher;
        private string catalog;
        private string author;
        private string remarks;
        private string dentification;

        /// <summary>
        /// 书号
        /// </summary>
        public long BookNum { get => bookNum; set => bookNum = value; }
        /// <summary>
        /// 国际书号
        /// </summary>
        public string Isbn { get => isbn; set => isbn = value; }
        /// <summary>
        /// 书名
        /// </summary>
        public string BookName { get => bookName; set => bookName = value; }
        /// <summary>
        /// 出版日期
        /// </summary>
        public DateTime PublishTime { get => publishTime; set => publishTime = value; }
        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get => price; set => price = value; }
        /// <summary>
        /// 出版社
        /// </summary>
        public string Publisher { get => publisher; set => publisher = value; }
        /// <summary>
        /// 编目
        /// </summary>
        public string Catalog { get => catalog; set => catalog = value; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get => author; set => author = value; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get => remarks; set => remarks = value; }
        /// <summary>
        /// 标识
        /// </summary>
        public string Dentification { get => dentification; set => dentification = value; }
        /// <summary>
        /// 无参构造
        /// </summary>
        public BookBasicData() { }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="isbn">国际书号</param>
        /// <param name="bookName">书名</param>
        /// <param name="publishTime">出版日期</param>
        /// <param name="price">单价</param>
        /// <param name="publisher">出版社</param>
        /// <param name="catalog">编目</param>
        /// <param name="author">作者</param>
        /// <param name="remarks">备注</param>
        /// <param name="dentification">标识</param>
        public BookBasicData(long bookNum, string isbn, string bookName, DateTime publishTime, double price, string publisher, string catalog, string author, string remarks, string dentification)
        {
            this.bookNum = bookNum;
            this.isbn = isbn;
            this.bookName = bookName;
            this.publishTime = publishTime;
            this.price = price;
            this.publisher = publisher;
            this.catalog = catalog;
            this.author = author;
            this.remarks = remarks;
            this.dentification = dentification;
        }
    }
}
