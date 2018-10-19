using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.Model;
using bms.Dao;
using System.Data;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class replenishMentBll
    {
        replenishMentDao dao = new replenishMentDao();
        /// <summary>
        /// 获取补货单体信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select()
        {
            DataSet ds = dao.Select();
            return ds;
        }
        /// <summary>
        /// 插入补货单体
        /// </summary>
        /// <param name="rm"></param>
        /// <returns></returns>
        public Result Insert(replenishMentMonomer rm)
        {
            int row = dao.Insert(rm);
            if (row > 0)
            {
                return Result.添加成功;
            }
            else
            {
                return Result.添加失败;
            }
        }
        /// <summary>
        /// 添加补货单头
        /// </summary>
        /// <param name="rd">补货单头实体</param>
        /// <returns>添加结果</returns>
        public Result InsertRsHead(replenishMentHead rd)
        {
            int row = dao.InsertRsHead(rd);
            if (row > 0)
            {
                return Result.添加成功;
            }
            else
            {
                return Result.添加失败;
            }
        }
        /// <summary>
        /// 更新补货单头的种数，数量，总码洋，总实洋
        /// </summary>
        /// <param name="rd">销售单头实体</param>
        /// <returns>结果</returns>
        public Result updateRsHead(replenishMentHead rd)
        {
            int row = dao.updateRsHead(rd);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else
            {
                return Result.更新失败;
            }
        }
        /// <summary>
        /// 获取单头数量
        /// </summary>
        /// <returns>行数</returns>
        public int countHead()
        {
            int row = dao.countHead();
            if (row > 0)
            {
                return row;
            }
            else
            {
                return row = 0;
            }
        }
        /// <summary>
        /// 获取制单日期
        /// </summary>
        /// <returns>时间字符串</returns>
        public string getRsHeadTime()
        {
            string time = dao.RsHeadTime();
            if (time != null || time == "")
            {
                return time;
            }
            else
            {
                return time = "";
            }
        }
        /// <summary>
        /// 根据销售任务或取补货头ID
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns>补货单头id</returns>
        public string getRsHeadID(string saleTaskId)
        {
            return dao.getRsHeadID(saleTaskId);
        }
        /// <summary>
        /// 获取该补货单头下的单体数量
        /// </summary>
        /// <param name="rsHeadID">补货单头id</param>
        /// <returns>数量</returns>
        public int countMon(string rsHeadID)
        {
            int row = dao.countMon(rsHeadID);
            if (row > 0)
            {
                return row;
            }
            else
            {
                return row = 0;
            }
        }
        /// <summary>
        /// 统计补货单头种数
        /// </summary>
        /// <param name="rsHeadID">补货单头id</param>
        /// <returns></returns>
        public int getkinds(string rsHeadID)
        {
            return dao.getkinds(rsHeadID);
        }
        /// <summary>
        /// 获取该单头id下的书本数量
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public int getsBookNumberSum(string rsHeadID)
        {
            return dao.getsBookNumberSum(rsHeadID);
        }
        /// <summary>
        /// 获取该单头id下的码洋
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public double getsBookTotalPrice(string rsHeadID)
        {
            return dao.getsBookTotalPrice(rsHeadID);
        }
        /// <summary>
        /// 获取该单头id下的实洋
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public double getsBookRealPrice(string rsHeadID)
        {
            return dao.getsBookRealPrice(rsHeadID);
        }
        /// <summary>
        /// 根据补货单头id判断其单体是否有数据
        /// </summary>
        /// <param name="rsHeadID">补货单头id</param>
        /// <returns>count</returns>
        public int getRsMonCount(string rsHeadID)
        {
            return dao.getRsMonCount(rsHeadID);
        }
        /// <summary>
        /// 删除补货单
        /// </summary>
        /// <param name="rsHeadID">补货单头id</param>
        /// <returns>受影响行数</returns>
        public Result Delete(string rsHeadID)
        {
            int row = dao.Delete(rsHeadID);
            if (row > 0)
            {
                return Result.删除成功;
            }
            else
            {
                return Result.删除失败;
            }
        }
        /// <summary>
        /// 根据补货单头id获取单头信息
        /// </summary>
        /// <param name="rsHeadId">补货单头id</param>
        /// <returns>数据集</returns>
        public DataSet getHeadMsg(string rsHeadId)
        {
            DataSet ds = dao.getHeadMsg(rsHeadId);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
    }
}
