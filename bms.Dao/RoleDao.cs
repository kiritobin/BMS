using bms.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bms.Model;


namespace bms.Dao
{
    public class RoleDao
    {
        MySqlHelp db = new MySqlHelp();
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
        /// 添加角色
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns>返回受影响的行数</returns>
        public int Insert(Role role)
        {
            string cmdText = "insert into T_Role(roleName) values(@roleName)";
            string[] param = { "@roleName" };
            object[] valuse = { role.RoleName };
            int row = db.ExecuteNoneQuery(cmdText, param, valuse);
            return row;
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public int Update(Role role)
        {
            string cmdText = "update T_Role set roleName=@roleName where roleId=@roleId";
            string[] param = { "@roleName", "@roleId" };
            object[] values = { role.RoleId, role.RoleName };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="functionId">功能Id</param>
        /// <returns></returns>
        public int UpdatePer(int roleId,int functionId)
        {
            string cmdText = "update T_Permission set functionId=@functionId where roleId=@roleId";
            string[] param = {"@roleId", "@functionId" };
            object[] values = { roleId, functionId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 根据角色Id来删除角色功能关系
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public int DeletePer(int roleId)
        {
            string cmdText = "delete from T_Permission where roleId=@roleId";
            string[] param = { "@roleId" };
            object[] values = { roleId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
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
            return row;
        }
    }
}
