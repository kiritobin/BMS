using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class Function
    {
        private int functionId;
        private string functionName;
        private string roleId;

        /// <summary>
        /// 无参构造
        /// </summary>
        public Function() { }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="functionId">功能ID</param>
        /// <param name="functionName">功能名称</param>
        /// <param name="roleId">角色ID</param>
        public Function(int functionId, string functionName, string roleId)
        {
            this.FunctionId = functionId;
            this.FunctionName = functionName;
            this.RoleId = roleId;
        }
        /// <summary>
        /// 功能ID
        /// </summary>
        public int FunctionId
        {
            get
            {
                return functionId;
            }

            set
            {
                functionId = value;
            }
        }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName
        {
            get
            {
                return functionName;
            }

            set
            {
                functionName = value;
            }
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId
        {
            get
            {
                return roleId;
            }

            set
            {
                roleId = value;
            }
        }
    }
}
