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
            string userId = user.UserId;
            string cmd = "select count(userId) from T_User where userId=@userId";
            string[] param1 = { "@userId" };
            object[] values1 = { userId };
            row = Convert.ToInt32(db.ExecuteScalar(cmd, param1, values1));
            if (row > 0)
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
            string comText = "update T_User set userName=@userName,regionId=@regionId,roleId=@roleId where UserId=@UserId";
            string[] param = { "@userName", "@regionId", "@roleId", "@UserId" };
            object[] values = { user.UserName, user.ReginId.RegionId, user.RoleId.RoleId,user.UserId };
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
        public int Delete(string userID)
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
        /// <summary>
        /// 根据用户ID获取用户基础信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>数据集</returns>
        public DataSet selectByUserId(string  userId)
        {
            string comText = "select userId,userName,regionName,roleName,regionId from V_User where userId=@userId";
            string[] param = { "@userId" };
            object[] values = { userId };
            DataSet ds = db.FillDataSet(comText, param, values);
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
        /// 判断该用户是否已经被删除
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回受影响的行数</returns>
        public DataSet SelectDeleteState(string userId)
        {
            string cmdText = "select deleteState=1 from T_User where userID=@userId";
            string[] param = { "@userId" };
            object[] values = { userId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }

        /// <summary>
        /// 判断用户存不存在
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int isUser(string userId)
        {
            string cmdText = "select count(userID) from T_User where userID=@userId and deleteState=0";
            string[] param = { "@userId" };
            object[] values = { userId };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return row;
        }
        /// <summary>
        /// 判断客户存不存在
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int isCustomer(string userId)
        {
            string cmdText = "select count(customerID) from T_Customer where customerID=@userId and deleteState=0";
            string[] param = { "@userId" };
            object[] values = { userId };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return row;
        }

        /// <summary>
        /// 判断客户是否有未完成的销售任务
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CustomersaletaskIsNull(string userId)
        { 
            string cmdText = "select count(customerID) from t_saletask where customerID=@userId and deleteState=0 and ISNULL(finishTime)";
            string[] param = { "@userId" };
            object[] values = { userId };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return row;
        }
        /// <summary>
        /// 根据客户ID获取销售任务ID和销售任务时间
        /// </summary>
        /// <param name="userId">根据客户ID</param>
        /// <returns></returns>
        public DataSet  getCustomersaletaskID(string userId)
        {
            //SELECT regionId,regionName from v_saletask where customerId='87100127' and ISNULL(finishTime) 
            string cmdText = "select saleTaskId,startTime,regionId,regionName from v_saletask where customerID=@userId and deleteState=0 and ISNULL(finishTime)";
            string[] param = { "@userId" };
            object[] values = { userId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }
        /// <summary>
        /// 根据销售任务ID获取销售任务时间
        /// </summary>
        /// <param name="saletaskId">销售任务ID</param>
        /// <returns></returns>
        public DataSet getsaletasktime(string saletaskId)
        {
            string cmdText = "select saleTaskId,startTime,regionId,regionName from v_saletask where saleTaskId=@saletaskId and deleteState=0 and ISNULL(finishTime)";
            string[] param = { "@saletaskId" };
            object[] values = { saletaskId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }
    }
}
