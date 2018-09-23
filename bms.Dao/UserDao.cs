using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class UserDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user">用户实体对象</param>
        /// <returns>受影响行数</returns>
        public int Insert(User user)
        {
            int row;
            int userId = user.UserId;
            string cmd = "select userId from T_User where userId=@userId";
            string[] param1 = { "@userId" };
            object[] values1 = { userId };
            DataSet ds = db.FillDataSet(cmd, param1, values1);
            if (ds != null)
            {
                return row = 0;
            }
            else
            {
                string comText = "insert into T_User(userID,userPwd,userName,regionId,roleId) values(@userID, @userPwd,@userName,@regionId,@roleId)";
                string[] param = { "@userID", "@userName", "@regionId", "@roleId", "@userPwd" };
                object[] values = { user.UserId, user.UserName, user.ReginId.RegionId, user.RoleId.RoleId, user.Pwd };
                row = db.ExecuteNoneQuery(comText, param, values);
                return row;
            }

        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户实体对象</param>
        /// <returns>受影响行数</returns>
        public int Update(User user)
        {
            string comText = "update T_User set userName=@userName,regionId=@regionId,roleId=@roleId";
            string[] param = { "@userName", "@regionId", "@roleId" };
            object[] values = { user.UserName, user.ReginId.RegionId, user.RoleId.RoleId };
            int row = db.ExecuteNoneQuery(comText, param, values);
            return row;
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="user">用户实体对象</param>
        /// <returns>受影响行数</returns>
        public int UpdatePwd(User user)
        {
            string comText = "update T_User set userPwd=@userPwd where userID = @userID";
            string[] param = { "@userPwd", "@userID" };
            object[] values = { user.Pwd, user.UserId };
            int row = db.ExecuteNoneQuery(comText, param, values);
            return row;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <returns></returns>
        public int Delete(int userID)
        {
            string comText = "update T_User set deleteState=1 where userID = @userID";
            string[] param = { "@userID" };
            object[] values = { userID };
            int row = db.ExecuteNoneQuery(comText, param, values);
            return row;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select()
        {
            string comText = "select userId,userName,regionName,roleName from V_User";
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
        /// 根据地区id获取用户信息
        /// </summary>
        /// <param name="regionId">地区id</param>
        /// <returns></returns>
        public DataSet selectByRegion(int regionId)
        {
            string comText = "select userId,userName,regionName,roleName from V_User where regionId=@regionId";
            string[] param = { "@regionId" };
            object[] values = { regionId };
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
    }
}
