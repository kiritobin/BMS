using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using bms.Dao;
using bms.Model;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class RegionBll
    {
        RegionDao regionDao = new RegionDao();
        /// <summary>
        /// 获取所有地区信息
        /// </summary>
        /// <returns></returns>
        public DataSet select()
        {
            return regionDao.select();
        }

        /// <summary>
        /// 添加分公司
        /// </summary>
        /// <param name="regionName">分公司名称</param>
        /// <returns></returns>
        public Result insert(string regionName)
        {
            int row = regionDao.insert(regionName);
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
        /// 删除分公司
        /// </summary>
        /// <param name="regionId">分公司id</param>
        /// <returns></returns>
        public Result delete(int regionId)
        {
            int row = regionDao.delete(regionId);
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
        /// 判断在另外一张表中是否有数据
        /// </summary>
        ///<param name = "table" > 表名 </ param >
        /// <param name="primarykeyname">主键列</param>
        /// <param name="primarykey">主键参数</param>
        /// <returns>管理引用代表数据存在不可删除，记录不存在表示可以删除</returns>
        public Result IsDelete(string table, string primarykeyname, string primarykey)
        {
            PublicProcedure procedure = new PublicProcedure();
            int row = procedure.isDelete(table, primarykeyname, primarykey);
            if (row > 0)
            {
                return Enums.OpResult.关联引用;
            }
            else
            {
                return Enums.OpResult.记录不存在;
            }
        }
    }
}
