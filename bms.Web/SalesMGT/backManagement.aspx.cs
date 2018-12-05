using System;
using System.Data;
using bms.Bll;
using bms.Model;
using System.Text;
using System.Web.Security;

namespace bms.Web.SalesMGT
{
    using System.Globalization;
    using Result = Enums.OpResult;
    public partial class backManagement : System.Web.UI.Page
    {
        protected DataSet ds;
        sellOffHeadBll soBll = new sellOffHeadBll();
        UserBll uBll = new UserBll();
        SellOffMonomerBll smBll = new SellOffMonomerBll();
        protected int totalCount,intPageCount;
        protected string userName,regionName;
        public DataSet cutds, dsPer;
        protected double discount;
        RoleBll roleBll = new RoleBll();
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin;
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
            string op = Request["op"];
            getData();
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
            if (op == "add")
            {
                Insert();
            }
            if (op == "delete")//删除单头
            {
                Delete();
            }
            if (op == "addMonomer")//跳转到添加销售单体页面
            {
                string sellId = Request["sohId"];
                string state = Request["state"];
                Session["sellId"] = sellId;
                Session["type"] = "add";
                if (state == "已完成")
                {
                    Response.Write("单据已完成，无法进行修改");
                    Response.End();
                }
                else
                {
                    Response.Write("处理中");
                    Response.End();
                }
            }
            if(op== "searchMonomer")
            {
                string sellId = Request["sohId"];
                Session["sellId"] = sellId;
                Session["type"] = "search";
            }
        }
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public String getData()
        {
            string saleId = Session["saleId"].ToString();
            int pagesize = 20;
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string search = "";
            //string stockId = Request["stockId"];
            string sellId = Request["sellId"];
            string cutomerName = Request["customer"];
            if ((sellId == "" || sellId == null) && (cutomerName == "" || cutomerName == null))
            {
                search = "saleTaskId='" + saleId + "' and deleteState=0";
            }
            else if (sellId != "" && sellId != null && (cutomerName == "" || cutomerName == null))
            {
                search = "saleTaskId='" + saleId + "' and deleteState=0 and sellOffHeadID=" + "'" + sellId + "'";
            }
            else if ((sellId == "" || sellId == null) && cutomerName != "" && cutomerName != null)
            {
                search = "saleTaskId='" + saleId + "' and deleteState=0 and customerName like " + "'%" + cutomerName + "%'";
            }
            else
            {
                search = "saleTaskId='" + saleId + "' and deleteState=0 and customerName like " + "'%" + cutomerName + "%'" + " and sellOffHeadID=" + "'" + sellId + "'";
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SellOffHead";
            tb.OrderBy = "saleTaskId";
            tb.StrColumnlist = "sellOffHeadID,saleTaskId,kinds,count,totalPrice,realPrice,userName,customerName,makingTime,defaultDiscount,state";
            tb.IntPageSize = pagesize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = search;
            ds = uBll.selectByPage(tb, out totalCount, out intPageCount);
            StringBuilder strb = new StringBuilder();
            int row = 0;//判断销退单头中是否有单体
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //strb.Append("<tr><td>" + ds.Tables[0].Rows[i]["saleTaskId"].ToString() + "</td>");
                int state = int.Parse(ds.Tables[0].Rows[i]["state"].ToString());
                string dc = ds.Tables[0].Rows[i]["defaultDiscount"].ToString();
                double defaultDiscount = double.Parse(dc); //* 100;
                string headId = ds.Tables[0].Rows[i]["sellOffHeadID"].ToString();
                strb.Append("<tr>");
                strb.Append("<td class='sellId'>" + headId + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["userName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td>");
                strb.Append("<td>" + (state > 0 ? "已完成" : "处理中") + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["kinds"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["count"].ToString() + "</td>");
                strb.Append("<td>" + defaultDiscount + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["makingTime"].ToString() + "</td>");
                //strb.Append("<td>" + "<button class='btn btn-success btn-sm btn_add'><i class='fa fa-plus fa-lg'></i></button>" + "<button class='btn btn-info btn-sm search_back'><i class='fa fa-search'></i></button>" + "<button class='btn btn-danger btn-sm btndelete'><i class='fa fa-trash'></i></button>" + "</td></tr>");
                strb.Append("<td>");
                if (state == 0)
                {
                    strb.Append("<button class='btn btn-success btn-sm btn_add'><i class='fa fa-plus fa-lg'></i></button>");
                }
                if (state == 1)
                {
                    strb.Append("<button class='btn btn-info btn-sm search_back'><i class='fa fa-search'></i></button>");
                }
                row = smBll.GetCount(headId);
                if (row == 0)
                {
                    strb.Append("<button class='btn btn-danger btn-sm btndelete'><i class='fa fa-trash'></i></button>");
                }
                strb.Append("</td>");
            }
            strb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            strb.Append("<input type='hidden' value='" + Session["saleId"].ToString() + "' id='saleTaskId' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }
        /// <summary>
        /// 添加销退单头
        /// </summary>
        public void Insert()
        {
            string saleTaskId = Session["saleId"].ToString();
            SaleTaskBll saleBll = new SaleTaskBll();
            SaleTask sale = saleBll.selectById(saleTaskId);
            User user = new User();
            user.UserId = sale.UserId;//用户Id
            string headId;
            string sellId;//单头Id
            sellOffHeadBll sellBll = new sellOffHeadBll();
            DateTime nowTime = DateTime.Now;
            string nowDt = nowTime.ToString("yyyy-MM-dd");
            long count = 0;
            //判断数据库中是否已经有记录
            DataSet backds = soBll.getAllTime();
            if (backds != null && backds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < backds.Tables[0].Rows.Count; i++)
                {
                    string time = backds.Tables[0].Rows[i]["makingTime"].ToString();
                    DateTime dt = Convert.ToDateTime(time);
                    string sqlTime = dt.ToString("yyyy-MM-dd");
                    if (sqlTime == nowDt)
                    {
                        //count += 1;
                        string id = backds.Tables[0].Rows[i]["sellOffHeadID"].ToString();
                        string st1 = id.Substring(2);
                        count = long.Parse(st1);
                        headId = (count + 1).ToString();
                        //生成流水号
                        if (count > 0)
                        {
                            sellId = "XT" + headId;
                        }
                        else
                        {
                            count = 1;
                            sellId = "XT" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                        }
                        SaleTask st = new SaleTask()
                        {
                            SaleTaskId = saleTaskId
                        };
                        SellOffHead sell = new SellOffHead()
                        {
                            SellOffHeadId = sellId,
                            SaleTaskId = st,
                            MakingTime = nowTime,
                            User = user
                        };
                        Result row = sellBll.Insert(sell);
                        if (row == Result.添加成功)
                        {
                            Response.Write("添加成功");
                            Response.End();
                        }
                        else
                        {
                            Response.Write("添加失败");
                            Response.End();
                        }
                        break;
                    }
                    else
                    {
                        count = 1;
                        sellId = "XT" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                        SaleTask st = new SaleTask()
                        {
                            SaleTaskId = saleTaskId
                        };
                        SellOffHead sell = new SellOffHead()
                        {
                            SellOffHeadId = sellId,
                            SaleTaskId = st,
                            MakingTime = nowTime,
                            User = user
                        };
                        Result row = sellBll.Insert(sell);
                        if (row == Result.添加成功)
                        {
                            Response.Write("添加成功");
                            Response.End();
                        }
                        else
                        {
                            Response.Write("添加失败");
                            Response.End();
                        }
                    }
                }
            }
            else
            {
                count = 1;
                sellId = "XT" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                SaleTask st = new SaleTask()
                {
                    SaleTaskId = saleTaskId
                };
                SellOffHead sell = new SellOffHead()
                {
                    SellOffHeadId = sellId,
                    SaleTaskId = st,
                    MakingTime = nowTime,
                    User = user
                };
                Result row = sellBll.Insert(sell);
                if (row == Result.添加成功)
                {
                    Response.Write("添加成功");
                    Response.End();
                }
                else
                {
                    Response.Write("添加失败");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
            string sellId = Request["sohId"];
            int row = smBll.GetCount(sellId);
            if (row > 0)
            {
                Response.Write("该单据中存在数据，不能删除");
                Response.End();
            }
            else
            {
                Result result = soBll.Delete(sellId);
                if(result == Result.删除成功)
                {
                    Response.Write("删除成功");
                    Response.End();
                }
                else
                {
                    Response.Write("删除失败");
                    Response.End();
                }
            }
        }
        protected void permission()
        {
            FunctionBll functionBll = new FunctionBll();
            User user = (User)Session["user"];
            userName = user.UserName;
            regionName = user.ReginId.RegionName;
            Role role = new Role();
            role = user.RoleId;
            int roleId = role.RoleId;
            dsPer = functionBll.SelectByRoleId(roleId);
            string userId = user.UserId;
            DataSet dsRole = roleBll.selectRole(userId);
            string roleName = dsRole.Tables[0].Rows[0]["roleName"].ToString();
            if (roleName == "超级管理员")
            {
                isAdmin = true;
            }
            for (int i = 0; i < dsPer.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 1)
                {
                    funcOrg = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 2)
                {
                    funcRole = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 3)
                {
                    funcUser = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 4)
                {
                    funcGoods = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 5)
                {
                    funcCustom = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 6)
                {
                    funcLibrary = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 7)
                {
                    funcBook = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 8)
                {
                    funcPut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 9)
                {
                    funcOut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 10)
                {
                    funcSale = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 11)
                {
                    funcSaleOff = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 12)
                {
                    funcReturn = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 13)
                {
                    funcSupply = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 14)
                {
                    funcRetail = true;
                }
            }
        }

    }
}