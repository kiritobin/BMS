using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bms.Dao;
using bms.Model;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class UserBll
    {
        UserDao userDao = new UserDao();
        /// <summary>
        /// 添加用户方法
        /// </summary>
        /// <param name="user">用户实体对象</param>
        /// <returns></returns>
        public Result Insert(User user)
        {
            int row = userDao.Insert(user);
            if (row == 0)
            {
                return Result.记录存在;
            }
            else
            {
                if (row > 0)
                {
                    return Result.添加成功;
                }
                else
                {
                    return Result.添加失败;
                }
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户实体对象</param>
        /// <returns></returns>
        public Result Update(User user)
        {
            int row = userDao.Update(user);
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
        /// 更新用户密码
        /// </summary>
        /// <param name="user">用户实体对象</param>
        /// <returns>受影响行数</returns>
        public Result UpdatePwd(User user)
        {
            int row = userDao.UpdatePwd(user);
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
        /// 删除用户信息
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <returns></returns>
        public Result Delete(int userID)
        {
            int row = userDao.Delete(userID);
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
        /// 获取所有用户信息
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet Select()
        {
            return userDao.Select();
        }

        /// <summary>
        /// 根据地区id获取用户信息
        /// </summary>
        /// <param name="regionId">地区id</param>
        /// <returns>数据集</returns>
        public DataSet selectByRegion(int regionId)
        {
            return userDao.selectByRegion(regionId);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public DataSet selectByPage(TableBuilder tablebuilder, out int totalCount, out int intPageCount)
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
        /// 批量导入
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int BulkInsert(DataTable table)
        {
            bulkLoad bulkLoad = new bulkLoad();
            return bulkLoad.BulkInsert(table);
        }
        /// <summary>
        /// 判断该账号是否已经被删除
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result SelectDeleteState(int userId)
        {
            DataSet ds = userDao.SelectDeleteState(userId);
            int row = Convert.ToInt32(ds.Tables[0].Rows[0]["deleteState=1"].ToString());
            if (row > 0)
            {
                return Result.记录不存在;
            }
            else
            {
                return Result.记录存在;
            }
        }
    }
}
