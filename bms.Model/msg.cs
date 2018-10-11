using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{

    public class msg
    {
        private string dataTable;
        private string dataTable1;
        private string allKinds;
        private string number;
        private string alltotalPrice;
        private string allrealPrice;
        private string messege;


        /// <summary>
        /// 无参构造
        /// </summary>
        public msg() { }

        public msg(string dataTable, string allKinds,string number,string alltotalPrice,string allrealPrice, string messege, string dataTable1)
        {
            this.dataTable = dataTable;
            this.allKinds = allKinds;
            this.number = number;
            this.alltotalPrice = alltotalPrice;
            this.allrealPrice = allrealPrice;
            this.messege = messege;
            this.dataTable1 = dataTable1;
        }

        /// <summary>
        /// 表格数据
        /// </summary>
        public string DataTable
        {
            get
            {
                return dataTable;
            }

            set
            {
                dataTable = value;
            }
        }
        /// <summary>
        /// 所有种类
        /// </summary>
        public string AllKinds
        {
            get
            {
                return allKinds;
            }

            set
            {
                allKinds = value;
            }
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public string Number
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
        /// 总码洋
        /// </summary>
        public string AlltotalPrice
        {
            get
            {
                return alltotalPrice;
            }

            set
            {
                alltotalPrice = value;
            }
        }
        /// <summary>
        /// 总实洋
        /// </summary>
        public string AllrealPrice
        {
            get
            {
                return allrealPrice;
            }

            set
            {
                allrealPrice = value;
            }
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Messege
        {
            get
            {
                return messege;
            }

            set
            {
                messege = value;
            }
        }

        public string DataTable1
        {
            get
            {
                return dataTable1;
            }

            set
            {
                dataTable1 = value;
            }
        }
    }
}
