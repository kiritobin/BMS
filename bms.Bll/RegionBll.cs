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
        /// 获取操作员
        /// </summary>
        /// <returns></returns>
        public DataSet selectUser()
        {
            return regionDao.selectUser();
        }

        /// <summary>
        /// 根据地区ID获取地区信息
        /// </summary>
        /// <returns></returns>
        public string selectById(int regionId)
        {
            return regionDao.selectById(regionId);
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
        /// <summary>
        /// 添加组织的同时分配名为“未上架”的货架
        /// </summary>
        /// <param name="tabInsert">添加类实体</param>
        /// <param name="count">输入的参数</param>
        /// <returns></returns>
        public Result InsertManyTable(TableInsertion tabInsert, out int count)
        {
            PublicProcedure pb = new PublicProcedure();
            int ds = pb.InsertManyTable(tabInsert, out count);
            if (count > 0)
            {
                return Result.添加成功;
            }
            else
            {
                return Result.添加失败;
            }
        }
        /// <summary>
        /// 判断组织是否存在
        /// </summary>
        /// <param name="regionName">组织名称</param>
        /// <returns></returns>
        public Result isExit(string regionName)
        {
            int row = regionDao.isExit(regionName);
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
        /// 更新组织
        /// </summary>
        /// <param name="region">组织实体</param>
        /// <returns>返回结果</returns>
        public Result Update(Region region)
        {
            int row = regionDao.Update(region);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else
            {
                return Result.更新失败;
            }
        }
    }
}
