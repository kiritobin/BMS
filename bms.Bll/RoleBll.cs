using bms.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bms.Bll;
using bms.Model;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class RoleBll
    {
        RoleDao roleDao = new RoleDao();
        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet select()
        {
            return roleDao.select();
        }

        /// <summary>
        /// 根据角色名获取角色id
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色id</returns>
        public int selectByroleName(string roleName)
        {
            return roleDao.selectByroleName(roleName);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Result Insert(Role role)
        {
            int row = roleDao.Insert(role);
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
        /// 批量添加角色功能关系
        /// </summary>
        /// <param name="sqlText">添加的值</param>
        /// <param name="count">添加的数量</param>
        /// <returns></returns>
        public Result InsertPer(string sqlText,int roleId,string remark)
        {
            int row = roleDao.InsertPer(sqlText, roleId,remark);
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
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Result Update(Role role)
        {
            int row = roleDao.Update(role);
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
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public Result Delete(int roleId)
        {
            int row = roleDao.Delete(roleId);
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
        /// 删除角色、功能的关系
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="count">删除记录数</param>
        /// <returns></returns>
        public Result DeletePer(int roleId,int count)
        {
            //关闭外键约束
            roleDao.changeKey(0);
            string row = roleDao.DeletePer(roleId, count);
            //开启外键约束
            roleDao.changeKey(1);
            if (row == "succ")
            {
                return Result.删除成功;
            }
            else
            {
                return Result.删除失败;
            }
        }

        /// <summary>
        /// 判断另外一张表中是否有引用
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="primarykeyname">主键列</param>
        /// <param name="primarykey">主键参数</param>
        /// <returns></returns>
        public Result isDelete(string table, string primarykeyname, string primarykey)
        {
            PublicProcedure procedure = new PublicProcedure();
            int row = procedure.isDelete(table, primarykeyname, primarykey);
            if (row > 0)
            {
                return Result.关联引用;
            }
            else
            {
                return Result.记录不存在;
            }
        }
    }
}
