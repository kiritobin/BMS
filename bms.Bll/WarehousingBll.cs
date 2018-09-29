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
        /// 获取出库单头的所有信息
        /// </summary>
        /// <param name="type">1为入库，0为出库，2为退货</param>
        /// <returns></returns>
        public DataTable SelectSingleHead(int type, string singleHeadId)
        {
            return monoDao.SelectSingleHead(type, singleHeadId);
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
        public DataTable getISBN()
        {
            return monoDao.getISBN();
        }
    }
}
