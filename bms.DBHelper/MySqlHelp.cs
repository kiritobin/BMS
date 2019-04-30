using bms.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace bms.DBHelper
{
    public class MySqlHelp
    {
        private string strConn;
        private MySqlConnection sqlConn = null;

        public MySqlHelp()
        {
            strConn = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;
            sqlConn = new MySqlConnection(strConn);
            //sqlConn = new SqlConnection("Data Source=.;Initial Catalog=GTS;Integrated Security=True");
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        private void OpenConn()
        {
            try
            {
                if (sqlConn != null && sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        private void CloseConn()
        {
            if (sqlConn != null && sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }
        /// <summary>
        /// 构造操作命令
        /// </summary>
        /// <param name="cmdText">带参命令</param>
        /// <param name="param">参数数组</param>
        /// <param name="values">参数值数组</param>
        /// <returns></returns>
        public MySqlCommand CreateCommand(string cmdText, string[] param, object[] values)
        {
            MySqlCommand myCmd = new MySqlCommand(cmdText, sqlConn);
            for (int i = 0; i < param.Length; i++)
            {
                myCmd.Parameters.AddWithValue(param[i], values[i]);
            }
            return myCmd;
        }
        /// <summary>
        /// 根据SQL指令返回相应查询阅读器，在阅读器使用完后请及时关闭
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <param name="param">参数列表，无参可设置为null</param>
        /// <param name="values">参数值列表，只有当参数不为空时有效</param>
        /// <returns></returns>
        public MySqlDataReader ExecuteReader(string cmdText, string[] param, object[] values)
        {
            OpenConn();
            MySqlCommand myCmd;
            if (param != null)
            {
                myCmd = this.CreateCommand(cmdText, param, values);
            }
            else
            {
                myCmd = new MySqlCommand(cmdText, sqlConn);
            }
            return myCmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 根据存储过程返回相应查询阅读器，在阅读器使用完后请及时关闭
        /// </summary>
        /// <param name="cmdText">存储过程名</param>
        /// <param name="parms">参数列表</param>
        /// <returns></returns>
        public MySqlDataReader ExecuteReaderBySP(string cmdText, MySqlParameter[] parms)
        {
            OpenConn();
            MySqlCommand myCmd = new MySqlCommand(cmdText, sqlConn);
            myCmd.CommandType = CommandType.StoredProcedure;
            if (parms != null)
            {
                myCmd.Parameters.AddRange(parms);
            }
            return myCmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 根据SQL指令返回受影响行数，主要用于数据库的更新、插入、删除等操作
        /// </summary>
        /// <param name="cmdText">sql命令语句</param>
        /// <param name="param">参数数组，若没有参数可以设置为空</param>
        /// <param name="values">参数值数组，只有当param不为空时有效</param>
        /// <returns></returns>
        public int ExecuteNoneQuery(string cmdText, string[] param, object[] values)
        {
            OpenConn();
            MySqlCommand myCmd;
            if (param != null)
            {
                myCmd = this.CreateCommand(cmdText, param, values);
            }
            else
            {
                myCmd = new MySqlCommand(cmdText, sqlConn);
            }
            try
            {
                return myCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }

        /// <summary>
        /// 根据SQL指令返回第一行第一列结果
        /// </summary>
        /// <param name="cmdText">sql命令语句</param>
        /// <param name="param">参数数组，若没有参数可以设置为空</param>
        /// <param name="values">参数值数组，只有当param不为空时有效</param>
        /// <returns></returns>
        public object ExecuteScalar(string cmdText, string[] param, object[] values)
        {
            OpenConn();
            MySqlCommand myCmd;
            if (param != null)
            {
                myCmd = this.CreateCommand(cmdText, param, values);
            }
            else
            {
                myCmd = new MySqlCommand(cmdText, sqlConn);
            }
            try
            {
                return myCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }
        /// <summary>
        /// 带事务执行存储过程，该方法主要用于执行用于数据维护类的存储过程执行
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="parms">SQL参数数组</param>
        public int ExecuteNoneQueryBySP(string cmdText, MySqlParameter[] parms)
        {
            OpenConn();
            MySqlTransaction tran = sqlConn.BeginTransaction();
            MySqlCommand myCmd = new MySqlCommand(cmdText, sqlConn);
            myCmd.CommandType = CommandType.StoredProcedure;
            if (parms != null)
            {
                myCmd.Parameters.AddRange(parms);
            }
            myCmd.Transaction = tran;
            try
            {
                int result = myCmd.ExecuteNonQuery();
                tran.Commit();
                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }
        /// <summary>
        /// 根据命令语句返回数据集
        /// </summary>
        /// <param name="cmdText">命令语句</param>
        /// <param name="param">参数数组，若没有参数可以设置为空</param>
        /// <param name="values">参数值数组，只有当param不为空时有效</param>
        /// <returns></returns>
        public DataSet FillDataSet(string cmdText, string[] param, object[] values)
        {
            OpenConn();
            MySqlCommand myCmd;
            if (param != null)
            {
                myCmd = this.CreateCommand(cmdText, param, values);
            }
            else
            {
                myCmd = new MySqlCommand(cmdText, sqlConn);
            }
            MySqlDataAdapter myAdp = new MySqlDataAdapter(myCmd);
            DataSet ds = new DataSet();
            try
            {
                myAdp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }

        public DataTable getkinds(string cmdText, string[] param, object[] values)
        {
            OpenConn();
            MySqlCommand myCmd;
            if (param != null)
            {
                myCmd = this.CreateCommand(cmdText, param, values);
            }
            else
            {
                myCmd = new MySqlCommand(cmdText, sqlConn);
            }
            DataTable dtcount = new DataTable();
            MySqlDataAdapter myAdp = new MySqlDataAdapter(myCmd);

            try
            {
                myAdp.Fill(dtcount);
                myAdp.Dispose();
                return dtcount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }
        /// <summary>
        /// 执行特定存储过程并返回查询后的数据结果，该方法用于执行查询类的存储过程
        /// </summary>
        /// <param name="cmdText">存储过程名</param>
        /// <param name="parms">SQL参数数组,若没有参数可以设置为空</param>
        /// <returns></returns>
        public DataSet FillDataSetBySP(string cmdText, MySqlParameter[] parms)
        {
            OpenConn();
            MySqlCommand myCmd = new MySqlCommand(cmdText, sqlConn);
            myCmd.CommandType = CommandType.StoredProcedure;
            if (parms != null)
            {
                myCmd.Parameters.AddRange(parms);
            }
            MySqlDataAdapter myAdp = new MySqlDataAdapter(myCmd);
            DataSet ds = new DataSet();
            try
            {
                myAdp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }
        /// <summary>
        /// 执行存储过程返回输出参数
        /// </summary>
        /// <param name="cmdText">存储过程名</param>
        /// <param name="parms">参数数组</param>
        /// <returns>包含所有输出值的ArrayList</returns>
        public ArrayList ExecuteSp(string cmdText, MySqlParameter[] parms)
        {
            OpenConn();
            MySqlCommand myCmd = new MySqlCommand(cmdText, sqlConn);
            myCmd.CommandType = CommandType.StoredProcedure;
            if (parms != null)
            {
                myCmd.Parameters.AddRange(parms);
            }
            try
            {
                myCmd.ExecuteNonQuery();
                ArrayList al = new ArrayList();
                for (int i = 0; i < parms.Length; i++)
                {
                    if (parms[i].Direction == ParameterDirection.Output)
                    {
                        al.Add(parms[i]);
                    }
                }
                return al;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }

        /// <summary>
        /// 添加销售单体，并更新库存
        /// </summary>
        /// <param name="saleText">添加单体语句</param>
        /// <param name="stockText">查询库存语句</param>
        /// <param name="sale">销售单体</param>
        /// <returns></returns>
        public int addsale(string saleText,string stockText, SaleMonomer sale)
        {
            OpenConn();
            int error = 1;
            MySqlTransaction tran = sqlConn.BeginTransaction();
            MySqlCommand myCmd;
            myCmd= new MySqlCommand(saleText, sqlConn);
            myCmd.Transaction = tran;
            try
            {
                //添加单体信息
                myCmd.ExecuteNonQuery();
                //查询库存数据
                MySqlCommand myCmdQueryBookss = new MySqlCommand(stockText, sqlConn);

                MySqlDataAdapter myAdp = new MySqlDataAdapter(myCmdQueryBookss);
                DataSet ds = new DataSet();
                myAdp.Fill(ds);
                int number = sale.Number;
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    int stockNum = Convert.ToInt32(ds.Tables[0].Rows[j]["stockNum"]);
                    string goodsId = ds.Tables[0].Rows[j]["goodsShelvesId"].ToString();
                   
                    if (number <= stockNum)
                    {
                        int stockcount = stockNum - number;
                        //更新库存
                        string upcmd ="update T_Stock set stockNum=" + stockcount + " where goodsShelvesId=" + goodsId + " and bookNum='"+sale.BookNum+"'";
                        MySqlCommand myCmdupdate = new MySqlCommand(upcmd, sqlConn);
                        try
                        {
                            myCmdupdate.ExecuteNonQuery();
                            if ((number == stockNum)||number<0) break;
                        }
                        catch (Exception ex)
                        {
                            error = 0;
                            throw ex;
                        }
                    }
                    else
                    {
                        number = number - stockNum;
                        MySqlCommand myCmdupdate = new MySqlCommand("update T_Stock set stockNum=" + 0 + " where goodsShelvesId=" + goodsId + " and bookNum='" + sale.BookNum+"'", sqlConn);
                        try
                        {
                            myCmdupdate.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            error = 0;
                            throw ex;
                        }
                        if (number == 0)
                        {
                            break;
                        }
                    }
                }
                tran.Commit();
                return error;
            }
            catch (Exception ex)
            {
                error = 0;
                tran.Rollback();
                return error;
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }

    }

}
