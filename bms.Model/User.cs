using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class User
    {
        private int userId;
        private string userName;
        private string pwd;
        private Regin reginId;
        private Role roleId;

        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get => userId; set => userId = value; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get => userName; set => userName = value; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get => pwd; set => pwd = value; }
        /// <summary>
        /// 地区id
        /// </summary>
        public Regin ReginId { get => reginId; set => reginId = value; }
        /// <summary>
        /// 角色id
        /// </summary>
        public Role RoleId { get => roleId; set => roleId = value; }

        /// <summary>
        /// 用户无参构造函数
        /// </summary>
        public User() { }

        /// <summary>
        /// 用户参数构造函数
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="userName">用户名称</param>
        /// <param name="pwd">密码</param>
        /// <param name="reginId">地区id</param>
        /// <param name="roleId">角色id</param>
        public User(int userId, string userName, string pwd, Regin reginId, Role roleId)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.Pwd = pwd;
            this.ReginId = reginId;
            this.RoleId = roleId;
        }
    }
}
