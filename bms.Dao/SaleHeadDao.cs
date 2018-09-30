using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class SaleHeadDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 查询所有销售单头
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet Select()
        {
            string comText = "select saleHeadId,saleTaskId,kindsNum,number,allTotalPrice,allRealPrice,userId,regionId from T_SaleHead";
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
        /// 添加销售单头
        /// </summary>
        /// <param name="task">销售单头实体</param>
        /// <returns>受影响行数</returns>
        public int Insert(SaleHead salehead)
        {
            string cmdText = "insert into T_SaleHead(saleHeadId,saleTaskId,kindsNum,number,allTotalPrice,allRealPrice,userId,regionId) values(@saleHeadId,@saleTaskId,@kindsNum,@number,@allTotalPrice,@allRealPrice,@userId,@regionId)";
            string[] param = { "@saleHeadId", "@saleTaskId", "@kindsNum", "@number", "@allTotalPrice", "@allRealPrice", "@userId", "@regionId" };
            object[] values = { salehead.SaleHeadId, salehead.SaleTaskId, salehead.KindsNum, salehead.Number, salehead.AllTotalPrice, salehead.AllRealPrice, salehead.UserId, salehead.RegionId };
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
        /// 删除销售单头
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>受影响行数</returns>
        public int Delete(string saleTaskId, string saleHeadId)
        {
            string cmdText = "update T_SaleHead set deleteState = 1 where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
            String[] param = { "@saleTaskId", @saleHeadId };
            String[] values = { saleTaskId, saleHeadId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
    }

}
