using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace bms.DBHelper
{
    public class DataTableHelper
    {
        /// <summary>
        /// dt分页
        /// </summary>
        /// <param name="dt">记录集 DataTable</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="pagesize">一页的记录数</param>
        /// <returns></returns>
        public static DataTable SplitDataTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (dt == null)
            {
                return null;
            }
            if (PageIndex == 0)
                return dt;
            DataTable newdt = dt.Clone();
            //newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }

            return newdt;
        }

        /// <summary>
        /// 合并两结构不同的dt（两表列名不能相同）
        /// </summary>
        /// <param name="udt1">左表</param>
        /// <param name="udt2">右表</param>
        /// <returns></returns>
        public static DataTable UniteDataTable(DataTable udt1, DataTable udt2)
        {
            DataTable udt3 = udt1.Clone();
            int row1 = udt1.Rows.Count;
            int row2 = udt2.Rows.Count;
            int colum1 = udt1.Columns.Count;
            int colum2 = udt2.Columns.Count;
            int colum3 = udt3.Columns.Count;
            for (int i = 0; i < colum2; i++)
            {
                udt3.Columns.Add(udt2.Columns[i].ColumnName);
            }
            object[] obj = new object[colum3];
            for (int i = 0; i < row1; i++)
            {
                udt1.Rows[i].ItemArray.CopyTo(obj, 0);
                udt3.Rows.Add(obj);
            }

            if (row1 >= row2)
            {
                for (int i = 0; i < row2; i++)
                {
                    DataRow dataRow2 = udt2.Rows[i];
                    DataRow dataRow3 = udt3.Rows[i];
                    for (int j = 0; j < colum2; j++)
                    {
                        dataRow3[j + colum1] = dataRow2[j].ToString();
                    }
                }
            }
            else
            {
                DataRow dr3;
                for (int i = 0; i < row2 - row1; i++)
                {
                    dr3 = udt3.NewRow();
                    udt3.Rows.Add(dr3);
                }
                for (int i = 0; i < row2; i++)
                {
                    for (int j = 0; j < colum2; j++)
                    {
                        udt3.Rows[i][j + colum1] = udt2.Rows[i][j].ToString();
                    }
                }
            }
            return udt3;
        }

        /// <summary>
        /// 移除重复数据
        /// </summary>
        /// <param name="SourceDt">源表</param>
        /// <param name="field">根据条件移除的字段</param>
        /// <returns></returns>
        public static DataTable GetDistinctSelf(DataTable SourceDt, string field)
        {
            int j = SourceDt.Rows.Count;
            if (j > 1)
            {
                int k = j - 2;
                int i = 1;
                while (i <= k)
                {
                    DataRow dr = SourceDt.Rows[i];
                    string fieldName = dr[field].ToString();
                    DataRow[] rows = SourceDt.Select(string.Format("{0}='{1}'", field, fieldName));
                    if (rows.Length > 1)
                    {
                        SourceDt.Rows.RemoveAt(i); //存在重复就移除
                        k = k - 1;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return SourceDt;
        }

        /// <summary>
        /// 判断dt是否存在重复记录，true为存在
        /// </summary>
        /// <param name="dt">源表</param>
        /// <param name="strComuns">需要判断的字段</param>
        /// <returns></returns>
        public static bool checkDt(DataTable dt, string[] strComuns)
        {
            // string[] strComuns = { "ISBN", "书名", "单价" };
            DataView myDataView = new DataView(dt);
            int i = myDataView.ToTable(true, strComuns).Rows.Count;
            int j = dt.Rows.Count;
            if (i < j)
            {
                return true; //存在重复记录
            }
            return false; //不存在重复记录
        }

        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable jsonToDt(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        //Columns
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        //Rows
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
    }
}
