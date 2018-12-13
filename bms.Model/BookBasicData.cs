using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class BookBasicData
    {
        private string bookNum;
        private string isbn;
        private string bookName;
        private DateTime publishTime;
        private double price;
        private string publisher;
        private string catalog;
        private string author;
        private string remarks;
        private string dentification;
        private string newBookNum;
        private string time;
        private string remarks1;
        private string remarks2;
        private string remarks3;

        /// <summary>
        /// 最新的书号
        /// </summary>
        public string NewBookNum
        {
            get
            {
                return newBookNum;
            }

            set
            {
                newBookNum = value;
            }
        }
        /// <summary>
        /// 书号
        /// </summary>
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
        public string Time
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
        /// 国际书号
        /// </summary>
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
        /// <summary>
        /// 书名
        /// </summary>
        public string BookName
        {
            get
            {
                return bookName;
            }

            set
            {
                bookName = value;
            }
        }
        /// <summary>
        /// 出版时间
        /// </summary>
        public DateTime PublishTime
        {
            get
            {
                return publishTime;
            }

            set
            {
                publishTime = value;
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
        /// 出版社
        /// </summary>
        public string Publisher
        {
            get
            {
                return publisher;
            }

            set
            {
                publisher = value;
            }
        }
        /// <summary>
        /// 编目
        /// </summary>
        public string Catalog
        {
            get
            {
                return catalog;
            }

            set
            {
                catalog = value;
            }
        }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                author = value;
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get
            {
                return remarks;
            }

            set
            {
                remarks = value;
            }
        }
        /// <summary>
        /// 标识
        /// </summary>
        public string Dentification
        {
            get
            {
                return dentification;
            }

            set
            {
                dentification = value;
            }
        }
        public string Remarks1
        {
            get
            {
                return remarks1;
            }

            set
            {
                remarks1 = value;
            }
        }
        public string Remarks2
        {
            get
            {
                return remarks2;
            }

            set
            {
                remarks2 = value;
            }
        }
        public string Remarks3
        {
            get
            {
                return remarks3;
            }

            set
            {
                remarks3 = value;
            }
        }

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
        public BookBasicData(string newBookNum, string bookNum, string isbn, string bookName, DateTime publishTime, double price, string publisher, string catalog, string author, string remarks, string dentification)
        {
            this.BookNum = bookNum;
            this.Isbn = isbn;
            this.BookName = bookName;
            this.PublishTime = publishTime;
            this.Price = price;
            this.Publisher = publisher;
            this.Catalog = catalog;
            this.Author = author;
            this.Remarks = remarks;
            this.Dentification = dentification;
            this.NewBookNum = newBookNum;
        }
    }
}
