using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using bms.Model;

namespace bms.Model
{
    public class replenishMentMonomer
    {
        private int rsMonomerID;
        private string bookNum;
        private string isbn;
        private string author;
        private int count;
        private string supplier;
        private string saleTaskId;
        private string saleHeadId;
        private int saleIdMonomerId;
        private int deleteState;
        private DateTime dateTime;

        /// <summary>
        /// 空的构造方法
        /// </summary>
        public replenishMentMonomer()
        {

        }
        /// <summary>
        /// 有参构造方法
        /// </summary>
        /// <param name="rsMonomerID">补货单体id</param>
        /// <param name="bookNum">书号</param>
        /// <param name="isbn">isbn</param>
        /// <param name="author">进货折扣</param>
        /// <param name="count">数量</param>
        /// <param name="supplier">出版社</param>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头id</param>
        /// <param name="saleIdMonomerId">销售单体id</param>
        /// <param name="deleteState">删除状态</param>
        /// <param name="dateTime">补货单生成时间</param>
        public replenishMentMonomer(int rsMonomerID, string bookNum, string isbn, string author, int count, string supplier, string saleTaskId, string saleHeadId, int saleIdMonomerId, int deleteState, DateTime dateTime)
        {
            this.rsMonomerID = rsMonomerID;
            this.bookNum = bookNum;
            this.isbn = isbn;
            this.author = author;
            this.count = count;
            this.supplier = supplier;
            this.saleTaskId = saleTaskId;
            this.saleHeadId = saleHeadId;
            this.saleIdMonomerId = saleIdMonomerId;
            this.deleteState = deleteState;
            this.dateTime = dateTime;
        }
        public int RsMonomerID
        {
            get
            {
                return rsMonomerID;
            }

            set
            {
                rsMonomerID = value;
            }
        }

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

        public string Supplier
        {
            get
            {
                return supplier;
            }

            set
            {
                supplier = value;
            }
        }

        public string SaleTaskId
        {
            get
            {
                return saleTaskId;
            }

            set
            {
                saleTaskId = value;
            }
        }

        public string SaleHeadId
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

        public int DeleteState
        {
            get
            {
                return deleteState;
            }

            set
            {
                deleteState = value;
            }
        }

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
    }
}
