using bms.Dao;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class WarehousingBll
    {
        WarehousingDao monoDao = new WarehousingDao();
        /// <summary>
        /// 添加单头信息
        /// </summary>
        /// <param name="single">单头实体对象</param>
        /// <returns></returns>
        public Result insertHead(SingleHead single)
        {
            int row = monoDao.insertHead(single);
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
        /// 添加单体信息
        /// </summary>
        /// <param name="monomers">单体实体对象</param>
        /// <returns></returns>
        public Result insertMono(Monomers monomers)
        {
            int row = monoDao.insertMono(monomers);
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
        /// 更新单头数量，码洋，实洋
        /// </summary>
        /// <param name="single"></param>
        /// <returns></returns>
        public Result updateHead(SingleHead single)
        {
            int row = monoDao.updateHead(single);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else {
                return Result.更新失败;
            }
        }

            /// <summary>
            /// 获取行数
            /// </summary>
            /// <param name="monId">单头id</param>
            /// <returns>返回行数</returns>
            public long getCount(string singleHeadId)
        {
            long row = monoDao.SelectBymonId(singleHeadId);
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
        /// 通过书号查询在单体中是否存在记录
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="type">单体类型（0：出库，1：入库，2：退货）</param>
        /// <returns></returns>
        public Result SelectBybookNum(string singleHeadId,string bookNum,int type)
        {
            int row = monoDao.SelectBybookNum(singleHeadId,bookNum, type);
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
        /// 获取出库单头的所有信息
        /// </summary>
        /// <param name="type">1为入库，0为出库，2为退货</param>
        /// <returns></returns>
        public DataTable SelectSingleHead(string singleHeadId)
        {
            return monoDao.SelectSingleHead(singleHeadId);
        }

        /// <summary>
        /// 获取出库单体的所有信息
        /// </summary>
        /// <param name="type">1为入库，0为出库，2为退货</param>
        /// <returns></returns>
        public DataTable SelectMonomers(string singleHeadId)
        {
            return monoDao.SelectMonomers(singleHeadId);
        }

            /// <summary>
            /// 假删除单头信息
            /// </summary>
            /// <param name="singleHeadId">单头id</param>
            /// <returns></returns>
            public Result deleteHead(string singleHeadId,int type)
        {
            int row = monoDao.deleteHead(singleHeadId,type);
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
        /// 查看单头数量
        /// </summary>
        /// <param name="type">类型，0：出库，1：入库，2：退货</param>
        /// <returns></returns>
        public int countHead(int type)
        {
            return monoDao.countHead(type);
        }
       
        /// <summary>
        /// 假删除单体信息
        /// </summary>
        /// <param name="singleHeadId">单头id</param>
        /// <param name="monId">单体id</param>
        /// <param name="type">单体类型（0：出库，1：入库，2：退货）</param>
        /// <returns></returns>
        public Result deleteMonomer(string singleHeadId,int monId, int type)
        {
            int row = monoDao.deleteMonomer(singleHeadId,monId,type);
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
        /// 读取isbn
        /// </summary>
        /// <returns></returns>
        public DataTable getISBNbook()
        {
            return monoDao.getISBNbook();
        }

        public Monomers getDiscount()
        {
            return monoDao.getDiscount();
        }

        /// <summary>
        /// 更新折扣
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        public Result updateDiscount(double discount)
        {
            int row = monoDao.updateDiscount(discount);
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
        /// 导出成Excel表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>返回一个DataTable的选题记录集合</returns>
        public DataTable ExportExcel(string strWhere)
        {
            DataTable dt = monoDao.ExportExcel(strWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        /// <summary>
        /// 获取该销退单头下所有的单据号和制单时间
        /// </summary>
        /// <returns></returns>
        public DataSet getAllTime(int type)
        {
            DataSet ds = monoDao.getAllTime(type);
            return ds;
        }
        /// <summary>
        /// 根据入库单头ID获取单体合计
        /// </summary>
        /// <param name="singleHeadId"></param>
        public DataSet addupRKMonomer(string singleHeadId)
        {
            DataSet ds = monoDao.addupRKMonomer(singleHeadId);
            return ds;
        }
        /// <summary>
        /// 补货查看
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public DataSet checkRs(string saleTaskId)
        {
            return monoDao.checkRs(saleTaskId);
        }
        public DataSet regionRs(int regionId)
        {
            return monoDao.regionRs(regionId);
        }
        public DataSet customerRs(int customerId)
        {
            return monoDao.customerRs(customerId);
        }
        public DataSet getKinds(string time, int type)
        {
            return monoDao.getKinds(time,type);
        }
        public DataSet getAllprice(string dateTime, int type)
        {
            return monoDao.getAllprice(dateTime,type);
        }
        public int getAllkinds(string dateTime, int type)
        {
            return monoDao.getAllkinds(dateTime,type);
        }
        public DataSet getAllpriceRegion(string dateTime, int regionId, int type)
        {
            return monoDao.getAllpriceRegion(dateTime,regionId,type);
        }
        public int getAllkindsRegion(string dateTime, int regionId, int type)
        {
            return monoDao.getAllkindsRegion(dateTime,regionId,type);
        }
    }
}
