using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.wechat
{
    using Result = Enums.OpResult;
    public partial class insertUserByRegin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RegionBll regionBll = new RegionBll();
            UserBll userBll = new UserBll();
            RoleBll roleBll = new RoleBll();
            RSACryptoService rsa = new RSACryptoService();

            string roleName = "微信零售";
            int row = roleBll.selectByroleName(roleName);
            int roleId = 0;
            if (row <= 0)
            {
                Role role = new Role();
                role.RoleName = roleName;
                Result insert = roleBll.Insert(role);
                if (insert == Result.添加成功)
                {
                    roleId = roleBll.selectByroleName(roleName);
                    string sqlText = "(" + roleId + "," + 14 + ")";
                    Result inserts = roleBll.InsertPer(sqlText, roleId, "添加");
                    if (inserts == Result.添加失败)
                    {
                        Response.Write("添加失败");
                        Response.End();
                    }
                }
            }
            else
            {
                roleId = row;
            }
            DataSet ds = regionBll.select();
            int count = ds.Tables[0].Rows.Count;
            if (count > 0 && ds != null)
            {
                for(int i=0;i< count; i++)
                {
                    string regionId = ds.Tables[0].Rows[i]["regionId"].ToString();
                    string regionName = ds.Tables[0].Rows[i]["regionName"].ToString();
                    Region region = new Region();
                    region.RegionId = Convert.ToInt32(regionId);
                    Role role = new Role();
                    role.RoleId = roleId;
                    User user = new User();
                    user.UserId = regionId + "01";
                    user.UserName = regionName + "微信零售";
                    user.ReginId = region;
                    user.Pwd = rsa.Encrypt("000000");
                    user.RoleId = role;
                    Result rows = userBll.Insert(user);
                    if(rows == Result.添加失败)
                    {
                        Response.Write("添加失败：" + regionId);
                    }
                }
                Response.Write("添加成功");
            }
            else
            {
                Response.Write("未查到任何组织信息");
            }
        }
    }
}