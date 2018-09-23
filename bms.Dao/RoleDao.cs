using bms.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bms.Model;
using MySql.Data.MySqlClient;

namespace bms.Dao
{
    public class RoleDao
    {
        MySqlHelp db = new MySqlHelp();
        public MySqlTransaction transaction;

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet select()
        {
            string comText = "select roleId,roleName,functionName from V_Permission";
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
        /// 根据角色名获取角色id
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色id</returns>
        public int selectByroleName(string roleName)
        {
            string cmdText = "select roleId from T_Role where roleName = @roleName";
            string[] param = { "@roleName" };
            object[] values = { roleName };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int role = Convert.ToInt32(ds.Tables[0].Rows[0]["roleId"]);
                return role;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns>返回受影响的行数</returns>
        public int Insert(Role role)
        {
            string cmdText = "insert into T_Role(roleName) values(@roleName)";
            string[] param = { "@roleName" };
            object[] values = { role.RoleName };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            if (row > 0) { 
                return row;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 添加角色功能关系
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="functionId">功能Id</param>
        /// <returns>返回受影响的行数</returns>
        public int InsertPer(string sqlText, int count)
        {
            string cmdText = "insert into T_Permission(roleId,functionId) values" + sqlText;
            int row = db.ExecuteNoneQuery(cmdText,null,null);
            if (row == count)
            {
                return row;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public int Update(Role role)
        {
            string cmdText = "update T_Role set roleName=@roleName where roleId=@roleId";
            string[] param = { "@roleId","@roleName" };
            object[] values = { role.RoleId, role.RoleName };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 根据角色Id来删除角色功能关系
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public string DeletePer(int roleId,int count)
        {
            string cmdText = "delete from T_Permission where roleId=@roleId";
            string[] param = { "@roleId" };
            object[] values = { roleId };
            int row = db.ExecuteNoneQuery(cmdText,param,values);
            if (row == count)
            {
                return "succ";
            }
            else
            {
                return "failure";
            }
        }

        /// <summary>
        /// 根据角色Id来删除角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public int Delete(int roleId)
        {
            string cmdText = "delete from T_Role where roleId=@roleId";
            string[] param = { "@roleId" };
            object[] values = { roleId };
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
        /// 改变外键约束状态
        /// </summary>
        /// <param name="state">0为关，1为开</param>
        public void changeKey(int state)
        {
            string cmdText = "SET FOREIGN_KEY_CHECKS="+ state;
            int row = db.ExecuteNoneQuery(cmdText, null, null);
        }
    }
}
