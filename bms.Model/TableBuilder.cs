using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class TableBuilder
    {
        private string strColumnlist;
        private string strTable;
        private string strWhere;
        private string orderBy;
        private int intPageNum;
        private int intPageSize;
        /// <summary>
        /// 要查询的字段用逗号隔开
        /// </summary>
        public string StrColumnlist
        {
            get
            {
                return strColumnlist;
            }

            set
            {
                strColumnlist = value;
            }
        }
        /// <summary>
        /// 要查询的表
        /// </summary>
        public string StrTable
        {
            get
            {
                return strTable;
            }

            set
            {
                strTable = value;
            }
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string StrWhere
        {
            get
            {
                return strWhere;
            }

            set
            {
                strWhere = value;
            }
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy
        {
            get
            {
                return orderBy;
            }

            set
            {
                orderBy = value;
            }
        }
        /// <summary>
        /// 当前页计数从1开始
        /// </summary>
        public int IntPageNum
        {
            get
            {
                return intPageNum;
            }

            set
            {
                intPageNum = value;
            }
        }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int IntPageSize
        {
            get
            {
                return intPageSize;
            }

            set
            {
                intPageSize = value;
            }
        }

        /// <summary>
        /// 无参构造
        /// </summary>
        public TableBuilder() { }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="strColumnlist">要查询的字段用逗号隔开</param>
        /// <param name="strTable">要查询的表</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="intPageNum">当前页计数从1开始</param>
        /// <param name="intPageSize">每页大小</param>
        public TableBuilder(string strColumnlist, string strTable, string strWhere, string orderBy, int intPageNum, int intPageSize)
        {
            this.StrColumnlist = strColumnlist;
            this.StrTable = strTable;
            this.StrWhere = strWhere;
            this.OrderBy = orderBy;
            this.IntPageNum = intPageNum;
            this.IntPageSize = intPageSize;
        }
    }
}
