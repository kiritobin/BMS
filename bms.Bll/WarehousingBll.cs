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
        /// 获取出库单头的所有信息
        /// </summary>
        /// <param name="type">1为入库，0为出库，2为退货</param>
        /// <returns></returns>
        public DataTable SelectSingleHead(int type,string singleHeadId)
        {
            return monoDao.SelectSingleHead(type, singleHeadId);
        }

        /// <summary>
        /// 假删除单头信息
        /// </summary>
        /// <param name="singleHeadId">单头id</param>
        /// <returns></returns>
        public Result deleteHead(string singleHeadId)
        {
            int row = monoDao.deleteHead(singleHeadId);
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
    }
}
