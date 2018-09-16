using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class LibraryCollection
    {

        private int libraryId;
        private string isbn;
        private string bookName;
        private double price;
        private int collectionNum;
        private Customer customerId;

        /// <summary>
        /// 无参构造
        /// </summary>
        public LibraryCollection() { }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="libraryId">馆藏ID</param>
        /// <param name="isbn">国际书号</param>
        /// <param name="bookName">书名</param>
        /// <param name="price">单价</param>
        /// <param name="collectionNum">馆藏数量</param>
        /// <param name="customerId">客户ID</param>
        public LibraryCollection(int libraryId, string isbn, string bookName, double price, int collectionNum, Customer customerId)
        {
            this.LibraryId = libraryId;
            this.Isbn = isbn;
            this.BookName = bookName;
            this.Price = price;
            this.CollectionNum = collectionNum;
            this.CustomerId = customerId;
        }
        /// <summary>
        /// 馆藏ID
        /// </summary>
        public int LibraryId { get => libraryId; set => libraryId = value; }
        /// <summary>
        /// 国际书号
        /// </summary>
        public string Isbn { get => isbn; set => isbn = value; }
        /// <summary>
        /// 书名
        /// </summary>
        public string BookName { get => bookName; set => bookName = value; }
        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get => price; set => price = value; }
        /// <summary>
        /// 馆藏数量
        /// </summary>
        public int CollectionNum { get => collectionNum; set => collectionNum = value; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public Customer CustomerId { get => customerId; set => customerId = value; }
    }
}
