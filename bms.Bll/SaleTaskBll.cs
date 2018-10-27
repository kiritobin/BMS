using bms.Dao;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    using System.Data;
    using Result = Enums.OpResult;
    public class SaleTaskBll
    {
        readonly SaleTaskDao saleDao = new SaleTaskDao();
        /// <summary>
        /// 获取销售任务id数量
        /// </summary>
        /// <param name="saleTaskId">销售任务</param>
        /// <returns>行数</returns>
        public int getCount()
        {
            int count = saleDao.countSaleTask();
            if (count > 0)
            {
                return count;
            }
            else
            {
                return count = 0;
            }

        }
        /// <summary>
        /// 根据销售任务id获取数量、价格、总实洋限制
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns></returns>
        public DataSet SelectBysaleTaskId(string saleTaskId)
        {
            return saleDao.SelectBysaleTaskId(saleTaskId);

        }

        /// <summary>
        /// 添加客户的销售任务总计
        /// </summary>
        /// <param name="customerId">客户id</param>
        /// <param name="allNumber">总数量</param>
        /// <param name="allPrice">总码洋</param>
        /// <param name="allKinds">品种数</param>
        /// <returns>返回结果</returns>
        public Result insertSaleStatistics(string customerId, int allNumber, double allPrice, int allKinds)
        {
            int row = saleDao.insertSaleStatistics(customerId, allNumber, allPrice, allKinds);
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
        ///根据客户id 查询是否统计过该客户的销售任务总计
        /// </summary>
        /// <param name="customerId">客户id</param>
        /// <returns>影响行数</returns>
        public Result SaleStatisticsIsExistence(string customerId)
        {
            int row = saleDao.SaleStatisticsIsExistence(customerId);
            if (row > 0)
            {

                return Result.记录存在;
            }
            else
            {
                return Result.记录不存在;
            }
        }

        /// <summary>
        ///根据用户id获取他的所有销售记录
        /// </summary>
        /// <param name="customerId">客户id</param>
        /// <returns>返回结果</returns>
        public DataSet SelectMonomers(string customerId)
        {
            return saleDao.SelectMonomers(customerId);

        }

        /// <summary>
        /// 查询有销售任务的客户
        /// </summary>
        /// <returns>客户id数据集</returns>
        public DataSet getcustomerID()
        {
            return saleDao.getcustomerID();
        }

        /// <summary>
        /// 统计销售任务总种数
        /// </summary>
        /// <param name="customerID">客户id</param>
        /// <returns>返回总种数</returns>
        public int getkinds(string customerID)
        {
            return saleDao.getkinds(customerID);
        }

        /// <summary>
        /// 根据销售任务id 获取客户id
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns></returns>
        public string getCustomerId(string saleTaskId)
        {
            return saleDao.getCustomerId(saleTaskId);
        }

        /// <summary>
        /// 添加销售任务方法
        /// </summary>
        /// <param name="task">实体销售任务</param>
        /// <returns>返回结果</returns>
        public Result insert(SaleTask sale)
        {
            int row = saleDao.Insert(sale);
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
        /// 销售任务删除
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns>返回结果</returns>
        public Result Delete(string saleTaskId)
        {
            int row = saleDao.Delete(saleTaskId);
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
        /// 判断是否关联引用
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="primarykeyname">主键名</param>
        /// <param name="primarykey">主键</param>
        /// <returns></returns>
        public Result IsDelete(string table, string primarykeyname, string primarykey)
        {
            PublicProcedure pp = new PublicProcedure();
            int row = pp.isDelete(table, primarykeyname, primarykey);
            if (row > 0)
            {
                return Result.关联引用;
            }
            else
            {
                return Result.记录不存在;
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tablebuilder"></param>
        /// <param name="totalCount"></param>
        /// <param name="intPageCount"></param>
        /// <returns></returns>
        public DataSet selectBypage(TableBuilder tablebuilder, out int totalCount, out int intPageCount)
        {
            PublicProcedure procedure = new PublicProcedure();
            DataSet ds = procedure.SelectBypage(tablebuilder, out totalCount, out intPageCount);
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
        /// 根据销售任务ID获取销售任务信息 
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns></returns>
        public SaleTask selectById(string saleTaskId)
        {
            DataSet ds = saleDao.SelectById(saleTaskId);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                double discount = double.Parse(ds.Tables[0].Rows[0]["defaultDiscount"].ToString());
                int numberlimit = Convert.ToInt32(ds.Tables[0].Rows[0]["numberLimit"].ToString());
                string userId = ds.Tables[0].Rows[0]["userId"].ToString();
                SaleTask st = new SaleTask()
                {
                    DefaultDiscount = discount,
                    NumberLimit = numberlimit,
                    UserId = userId
                };
                return st;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更新销售任务数量 总码洋 单价上限
        /// </summary>
        /// <param name="numberLimit">数量</param>
        /// <param name="priceLimit">单价</param>
        /// <param name="totalPriceLimit">总码洋</param>
        /// <returns>受影响行数</returns>
        public int update(int numberLimit, double priceLimit, double totalPriceLimit, double defaultDiscount, string defaultCopy, string saleId)
        {
            return saleDao.update(numberLimit, priceLimit, totalPriceLimit, defaultDiscount, defaultCopy, saleId);
        }
        /// <summary>
        /// 更新任务完成时间
        /// </summary>
        /// <param name="finishtime">时间</param>
        /// <param name="saleId">销售任务id</param>
        /// <returns>受影响行数</returns>
        public int updatefinishTime(DateTime finishtime, string saleId)
        {
            return saleDao.updatefinishTime(finishtime, saleId);
        }
        /// <summary>
        /// 获取该销售任务下的所有销售单状态
        /// </summary>
        /// <param name="saleid">销售任务id</param>
        /// <returns></returns>
        public DataSet SelectHeadStateBySaleId(string saleId)
        {
            return saleDao.SelectHeadStateBySaleId(saleId);
        }

        /// <summary>
        /// 获取日期来判断是否是当天的第一单
        /// </summary>
        /// <returns>时间字符串</returns>
        public string getSaleTaskTime()
        {
            return saleDao.getSaleTaskTime();
        }
        /// <summary>
        /// 获取完成日期
        /// </summary>
        /// <returns>时间字符串</returns>
        public string getSaleTaskFinishTime(string saleId)
        {
            return saleDao.getSaleFinishTime(saleId);
        }
        /// <summary>
        /// 根据销售任务ID，统计销售任务的总数量，总码洋，总实洋
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns>数据集</returns>
        public DataSet getSaleTaskStatistics(string saleTaskId)
        {
            return saleDao.getSaleTaskStatistics(saleTaskId);
        }
        /// <summary>
        /// 根据销售任务id获取操作员
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns>操作员名称</returns>
        public DataSet getcustomerName(string saleTaskId)
        {
            return saleDao.getcustomerName(saleTaskId);
        }
        /// <summary>
        /// 销售统计计划
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public DataSet salesTaskStatistics(string saleTaskId)
        {
            return saleDao.salesTaskStatistics(saleTaskId);
        }
        /// <summary>
        /// 通过销售任务Id统计种数
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns></returns>
        public int getkindsBySaleTaskId(string saleTaskId)
        {
            return saleDao.getkindsBySaleTaskId(saleTaskId);
        }
        /// <summary>
        /// 根据客户id获取他是否有过销售任务
        /// </summary>
        /// <param name="CustmerId">客户id</param>
        /// <returns>0该客户还没有销售任务,1该客户已有销售任务但未完成,2已完成，可以添加</returns>
        public string getcustomermsg(int CustmerId, int regionId)
        {
            return saleDao.getcustomermsg(CustmerId, regionId);
        }
        /// <summary>
        /// 获取当天时间的所有销售计划的总实洋，码洋，数量
        /// </summary>
        /// <returns></returns>
        public DataSet getAllprice(string dateTime)
        {
            return saleDao.getAllprice(dateTime);
        }
        public int getAllkinds(string dateTime)
        {
            return saleDao.getAllkinds(dateTime);
        }

        /// <summary>
        ///根据当天时间 获取所有销售任务的总实洋，书籍总数，总码洋 地区
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet getAllpriceRegion(string dateTime, int regionId)
        {
            return saleDao.getAllpriceRegion(dateTime, regionId);
        }
        /// <summary>
        /// 统计当天种数
        /// </summary>
        /// <returns></returns>
        public int getAllkindsRegion(string dateTime, int regionId)
        {
            return saleDao.getAllkindsRegion(dateTime, regionId);
        }
        /// <summary>
        /// 导出表格
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataTable ExportExcel(string strWhere)
        {
            DataTable dt = saleDao.ExportExcel(strWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }
    }
}
