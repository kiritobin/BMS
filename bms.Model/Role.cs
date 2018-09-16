using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class Role
    {
        private int roleId;
        private int roleName;

        /// <summary>
        /// 无参构造
        /// </summary>
        public Role() { }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="roleName">角色名称</param>
        public Role(int roleId, int roleName)
        {
            this.RoleId = roleId;
            this.RoleName = roleName;
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get => roleId; set => roleId = value; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public int RoleName { get => roleName; set => roleName = value; }
    }
}
