using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    public class Enums
    {
        /// <summary>
        /// 数据库操作结果
        /// </summary>
        public enum OpResult
        {
            添加成功,
            添加失败,
            达到上限,
            /// <summary>
            /// 在添加记录时发现存在相同记录信息
            /// </summary>
            记录存在,
            删除成功,
            删除失败,
            /// <summary>
            /// 在删除记录时发现存在外键关联
            /// </summary>
            关联引用,
            更新成功,
            更新失败,
            /// <summary>
            /// 在删除或者更新的时候没有发现该记录
            /// </summary>
            记录不存在,
            /// <summary>
            /// 在修改密码时输入密码和老密码不匹配
            /// </summary>
            密码不匹配
        }
    }
}
