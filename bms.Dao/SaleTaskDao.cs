using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class SaleTaskDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取销售任务数量
        /// </summary>
        /// <returns>数量</returns>
        public int countSaleTask()
        {
            string cmdText = "select count(saleTaskId) from T_SaleTask";
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, null, null));
            return row;
        }
        /// <summary>
        /// 查询有销售任务的客户
        /// </summary>
        /// <returns>客户id数据集</returns>
        public DataSet getcustomerID()
        {
            string cmdText = "select customerId from T_SaleTask";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            return ds;
        }
        /// <summary>
        /// 添加客户的销售任务总计
        /// </summary>
        /// <param name="customerId">客户id</param>
        /// <param name="allNumber">总数量</param>
        /// <param name="allPrice">总码洋</param>
        /// <param name="allKinds">品种数</param>
        /// <returns>受影响行数</returns>
        public int insertSaleStatistics(string customerId, int allNumber, double allPrice, int allKinds)
        {
            string cmdText = "insert into T_SaleStatistics(customerId,allNumber,allPrice,allKinds) values(@customerId,@allNumber,@allPrice,@allKinds)";
            string[] param = { "@customerId", "@allNumber", "@allPrice", "@allKinds" };
            object[] values = { customerId, allNumber, allPrice, allKinds };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        ///根据用户id获取他的所有销售记录
        /// </summary>
        /// <param name="customerId">客户id</param>
        /// <returns>返回数据集</returns>
        public DataSet SelectMonomers(string customerId)
        {
            string cmdText = "select number,totalPrice from V_SaleMonomer where customerId=@customerId";
            string[] param = { "@customerId" };
            object[] values = { customerId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///根据客户id 查询是否统计过该客户的销售任务总计
        /// </summary>
        /// <param name="customerId">客户id</param>
        /// <returns>影响行数</returns>
        public int SaleStatisticsIsExistence(string customerId)
        {
            string cmdText = "select count(customerId) from T_SaleStatistics where customerId=@customerId";
            string[] param = { "@customerId" };
            object[] values = { customerId };
            int row = int.Parse(db.ExecuteScalar(cmdText, param, values).ToString());
            return row;
        }

        /// <summary>
        /// 统计销售任务总种数
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns>返回总种数</returns>
        public int getkinds(string customerID)
        {
            string cmdText = "select bookNum,number from V_SaleMonomer where customerID=@customerID;";
            string[] param = { "@customerID" };
            object[] values = { customerID };
            float sltemp = 0;
            int zl = 0;
            DataTable dtcount = db.getkinds(cmdText, param, values);
            DataView dv = new DataView(dtcount);
            DataTable dttemp = dv.ToTable(true, "bookNum");
            for (int i = 0; i < dttemp.Rows.Count; i++)
            {
                string bn = dttemp.Rows[i]["bookNum"].ToString();
                DataRow[] dr = dtcount.Select("bookNum='" + bn + "'");
                for (int j = 0; j < dr.Length; j++)
                {
                    sltemp += float.Parse(dr[j]["number"].ToString().Trim());
                }
                if (sltemp > 0)
                {
                    zl++;
                }
            }
            return zl;
        }

        /// <summary>
        /// 根据销售任务id 获取客户id
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns></returns>
        public string getCustomerId(string saleTaskId)
        {
            string comText = "select customerId from T_SaleTask where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(comText, param, values);
            string customerId = ds.Tables[0].Rows[0]["customerId"].ToString();
            if (customerId != null || customerId.Length > 0)
            {
                return customerId;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 查询所有销售任务
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet Select()
        {
            string comText = "select saleTaskId,defaultDiscount,numberLimit,totalPriceLimit,startTime,finishTime,userId from T_SaleTask";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 添加销售任务
        /// </summary>
        /// <param name="task">销售任务实体</param>
        /// <returns>受影响行数</returns>
        public int Insert(SaleTask sale)
        {
            string cmdText = "insert into T_SaleTask(saleTaskId,userId,customerId,defaultDiscount,defaultCopy,numberLimit,priceLimit,totalPriceLimit,startTime,finishTime) values(@saleTaskId,@userId,@customreId,@defaultDiscount,@defaultCopy,@numberLimit,@priceLimit,@totalPriceLimit,@startTime,@finishTime)";
            string[] param = { "@saleTaskId", "@userId", "@customreId", "@defaultDiscount", "@defaultCopy", "@numberLimit", "@priceLimit", "@totalPriceLimit", "@startTime", "@finishTime" };
            object[] values = { sale.SaleTaskId, sale.UserId, sale.Customer.CustomerId, sale.DefaultDiscount, sale.DefaultCopy, sale.NumberLimit, sale.PriceLimit, sale.TotalPiceLimit, sale.StartTime, sale.FinishTime };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            if (row > 0)
            {
                return row;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 删除销售任务
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns>受影响行数</returns>
        public int Delete(string saleTaskId)
        {
            string cmdText = "update T_SaleTask set deleteState = 1 where saleTaskId=@saleTaskId";
            String[] param = { "@saleTaskId" };
            String[] values = { saleTaskId.ToString() };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 根据销售任务ID获取销售任务信息 
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns></returns>
        public DataSet SelectById(string saleTaskId)
        {
            string sql = "select defaultDiscount,numberLimit,userId from T_SaleTask where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 根据销售任务id获取数量、价格、总实洋限制
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns></returns>
        public DataSet SelectBysaleTaskId(string saleTaskId)
        {
            string sql = "select numberLimit,priceLimit,totalPriceLimit from T_SaleTask where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 更新销售任务数量 总码洋 单价上限
        /// </summary>
        /// <param name="numberLimit">数量</param>
        /// <param name="priceLimit">单价</param>
        /// <param name="totalPriceLimit">总码洋</param>
        /// <returns>受影响行数</returns>
        public int update(int numberLimit, double priceLimit, double totalPriceLimit)
        {
            string cmdText = "update T_Stock set numberLimit=@numberLimit where priceLimit=@priceLimit and totalPriceLimit=@totalPriceLimit";
            string[] param = { "@numberLimit", "@priceLimit", "@totalPriceLimit" };
            object[] values = { numberLimit, priceLimit, totalPriceLimit };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

    }
}
