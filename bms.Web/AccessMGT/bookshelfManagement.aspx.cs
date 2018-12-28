using bms.Bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.BasicInfor
{
    using Model;
    using System.Data.OleDb;
    using System.Text;
    using System.Web.Security;
    using Result = Enums.OpResult;
    public partial class bookshelfManagement : System.Web.UI.Page
    {
        public string userName,regionName;
        public int totalCount, intPageCount, PageSize=10,row;
        public User user = new User();
        public DataSet regionDs, ds,dsPer;
        GoodsShelvesBll shelvesbll = new GoodsShelvesBll();
        RegionBll rbll = new RegionBll();
        UserBll userBll = new UserBll();
        DataTable except = new DataTable();//接受差集
        DataTable excel = new DataTable();
        RoleBll roleBll = new RoleBll();
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail,isAdmin, funcBookStock;
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
            getData();
            string op = Request["op"];
            if (op == "add")
            {
                int regionId;
                if (user.RoleId.RoleName == "超级管理员")
                {
                    regionId = int.Parse(Request["regionId"]);
                }
                else
                {
                    regionId = user.ReginId.RegionId;
                }
                string shelfName = Request["shelfName"];

                Region reg = new Region()
                {
                    RegionId = regionId
                };
                GoodsShelves shelves = new GoodsShelves()
                {
                    ShelvesName = shelfName,
                    RegionId = reg
                };
                Result row = shelvesbll.selectByName(shelves);
                if (row == Result.记录不存在)
                {
                    Result result = shelvesbll.Insert(shelves);
                    if (result == Result.添加成功)
                    {
                        Response.Write("添加成功");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("添加成功");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("货架名已存在");
                    Response.End();
                }
            }
            if (op == "del")
            {
                int shelfId = int.Parse(Request["shelfId"]);
                //Result result = isDelete();
                //if (result == Result.记录不存在)
                //{
                    Result row = shelvesbll.Delete(shelfId);
                    if (row == Result.删除成功)
                    {
                        Response.Write("删除成功");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("删除失败");
                        Response.End();
                    }
                //}
                //else
                //{
                //    Response.Write("在其它表里关联引用，不能删除");
                //    Response.End();
                //}
            }
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
            if (op=="import")
            {
                DataTable dtInsert = new DataTable();
                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                differentDt();
                dtInsert = except; //赋给新table
                TimeSpan ts = watch.Elapsed;
                dtInsert.TableName = "T_GoodsShelves"; //导入的表名
                int a = userBll.BulkInsert(dtInsert);
                watch.Stop();
                double minute = ts.TotalMinutes; //计时
                string m = minute.ToString("0.00");
                int cf = row - a;
                if (a > 0)
                {
                    Response.Write("导入成功，总数据有" + row + "条，共导入" + a + "条数据" + "，共用时：" + m + "分钟");
                    Response.End();
                }
                else
                {
                    Response.Write("导入失败，总数据有" + row + "条，共导入" + a + "条数据，重复数据有"+cf);
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 判断在其他表中是否有关联
        /// </summary>
        /// <returns></returns>
        public Result isDelete()
        {
            string shelfId = Request["shelfId"];
            Result row;
            if (shelvesbll.isDelete("T_Monomers", "goodsShelvesId", shelfId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            else
            {
                row = Result.记录不存在;
            }
            if (shelvesbll.isDelete("T_Stock", "goodsShelvesId", shelfId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            else
            {
                row = Result.记录不存在;
            }
            return row;
        }
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            //获取分页数据
            User user = (User)Session["user"];
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string goods = Request["goods"];
            string region = Request["region"];
            string search;
            if ((region == "" || region == null) && (goods == "" || goods == null))
            {
                search = "deleteState=0";
            }
            else if ((goods != null || goods != "") && (region == "" || region == null))
            {

                search = String.Format("shelvesName='{0}' and deleteState=0", goods);
            }
            else if ((goods == null || goods == "") && (region != "" || region != null))
            {
                search = String.Format("regionName='{0}' and deleteState=0", region);
            }
            else
            {
                search = String.Format("regionName='{0}' and shelvesName='{1}' and deleteState=0", region, goods);
            }

            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_GoodsShelves";
            tb.OrderBy = "goodsShelvesId";
            tb.StrColumnlist = "goodsShelvesId,shelvesName,regionId,regionName";
            tb.IntPageSize = PageSize;
            tb.IntPageNum = currentPage;
            if (user.RoleId.RoleName == "超级管理员")
            {
                tb.StrWhere = search;
            }
            else
            {
                tb.StrWhere = "regionId=" + user.ReginId.RegionId + " and " + search;
            }
            //获取展示的客户数据
            ds = shelvesbll.selectByPage(tb, out totalCount, out intPageCount);
            //获取地区下拉数据
            regionDs = rbll.select();
            //生成table
            StringBuilder strb = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * PageSize)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["goodsShelvesId"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["shelvesName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</td>");
                strb.Append("<td>" + "<button class='btn btn-danger btn-sm btn_delete'>" + "<i class='fa fa-trash-o fa-lg'></i>" + "</button>" + " </td></tr>");
            }
            strb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }

        //excel读到table
        private DataTable excelToDt()
        {
            int regId;
            if (user.RoleId.RoleName == "超级管理员")
            {
                regId = Convert.ToInt32(Request["regId"]);
            }
            else
            {
                regId = user.ReginId.RegionId;
            }
            DataTable dt1 = new DataTable();
            string path = Session["path"].ToString();
            string strConn = "";
            //文件类型判断
            string[] sArray = path.Split('.');
            int count = sArray.Length - 1;
            if (sArray[count] == "xls")
            {
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (sArray[count] == "xlsx")
            {
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string strExcel1 = "select 货架名称 from [Sheet1$]";
                OleDbDataAdapter oda1 = new OleDbDataAdapter(strExcel1, strConn);
                oda1.Fill(dt1);
                DataColumn dc = new DataColumn("地区ID", typeof(int));
                dc.DefaultValue = regId; 
                dt1.Columns.Add(dc);

                row = dt1.Rows.Count; //获取总数
                //GetDistinctSelf(dt1, "货架名称");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }
            finally
            {
                conn.Close();
            }
            return dt1;
        }
        private DataTable npioDt()
        {
            DataTable dt1 = new DataTable();
            int regId;
            if (user.RoleId.RoleName == "超级管理员")
            {
                regId = Convert.ToInt32(Request["regId"]);
            }
            else
            {
                regId = user.ReginId.RegionId;
            }
            string path = Session["path"].ToString();
            try
            {
                dt1 = ExcelHelper.GetDataTable(path);
                DataColumn dc = new DataColumn("地区ID", typeof(int));
                dc.DefaultValue = regId;
                dt1.Columns.Add(dc);
                dt1.Columns.Add("id").SetOrdinal(0);
                row = dt1.Rows.Count;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                Response.End();
            }
            return dt1;
        }
        //某字段table去重方法
        private DataTable GetDistinctSelf(DataTable SourceDt, string field)
        {
            int j = SourceDt.Rows.Count;
            if (j > 1)
            {
                int k = j - 2;
                int i = 1;
                while (i <= k)
                {
                    DataRow dr = SourceDt.Rows[i];
                    string isbn = dr[field].ToString();
                    DataRow[] rows = SourceDt.Select(string.Format("{0}='{1}'", field, isbn));
                    if (rows.Length > 1)
                    {
                        SourceDt.Rows.RemoveAt(i);
                        k = k - 1;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return SourceDt;
        }

        //取差集
        private void differentDt()
        {
            //excel = excelToDt();
            excel = npioDt();
            int regId;
            if (user.RoleId.RoleName == "超级管理员")
            {
                regId = Convert.ToInt32(Request["regId"]);
            }
            else
            {
                regId = user.ReginId.RegionId;
            }
            int j = shelvesbll.isGoodsShelves(regId).Tables[0].Rows.Count;
            //数据库无数据时直接导入excel
            if (j <= 0)
            {
                //except = excelToDt();
                except=GetDistinctSelf(excel, "货架名称");
            }
            else
            {
                except.Columns.Add("id", typeof(string));
                except.Columns.Add("货架名称", typeof(string));
                except.Columns.Add("地区ID", typeof(string));
                DataSet dataSet = shelvesbll.isGoodsShelves(regId);
                DataRowCollection count = excel.Rows;
                foreach (DataRow row in count)//遍历excel数据集
                {
                    try
                    {
                        string goodsName = ToSBC(row[1].ToString());
                        DataRow[] rows = dataSet.Tables[0].Select(string.Format("shelvesName='{0}'", goodsName));
                        if (rows.Length == 0)//判断如果DataRow.Length为0，即该行excel数据不存在于表A中，就插入到dt3
                        {
                            except.Rows.Add(row[0], row[1], row[2]);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex);
                        Response.End();
                    }
                }
            }
        }
        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="input">需要转换的字符串</param>
        /// <returns></returns>
        private String ToSBC(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }
        protected void permission()
        {
            FunctionBll functionBll = new FunctionBll();
            user = (User)Session["user"];
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
            for (int i=0;i<dsPer.Tables[0].Rows.Count;i++)
            {
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) ==1)
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
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 15)
                {
                    funcBookStock = true;
                }
            }
        }
    }
}