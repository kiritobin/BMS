using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.CustomerMGT
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\daobin\Desktop\marc\L云南省店20180911处理.iso";
            DataTable dt = MarcTodt(path);
            int c = dt.Rows.Count;
            Response.Write(c);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private static StreamReader FileReadStream;
        private static System.IO.FileStream CheckStream;
        private static string FilePath;

        public DataTable MarcTodt(string Path)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ISBN", System.Type.GetType("System.String"));
            dt.Columns.Add("书名", System.Type.GetType("System.String"));
            dt.Columns.Add("定价", System.Type.GetType("System.String"));
            dt.Columns.Add("馆藏数量", System.Type.GetType("System.String"));
            //DataColumn ISBN = new DataColumn("ISBN", System.Type.GetType("System.String"));
            //dt.Columns.Add(ISBN);
            //dt.Columns.Add("ID", System.Type.GetType("System.String"));
            //DataColumn ID = new DataColumn("ID", System.Type.GetType("System.String"));

            //dt.Columns.Add("BookName", System.Type.GetType("System.String"));
            //dt.Columns.Add("Author", System.Type.GetType("System.String"));

            //dt.Columns.Add("Price", System.Type.GetType("System.String"));
            //dt.Columns.Add("Press", System.Type.GetType("System.String"));
            //dt.Columns.Add("PressYear", System.Type.GetType("System.String"));
            //dt.Columns.Add("Abstract", System.Type.GetType("System.String"));
            //dt.Columns.Add("外文书名", System.Type.GetType("System.String"));
            //dt.Columns.Add("Number",
            DataColumn[] pris = new DataColumn[1];

            CheckStream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
            FileReadStream = new StreamReader(Path, System.Text.Encoding.Default);
            FilePath = Path;
            string BuffStr;
            int CurrLine = 1;
            while ((int)(FileReadStream.Peek()) >= 0)
            {
                try
                {
                    BuffStr = FileReadStream.ReadLine();//读取FileReadStream中一行字符，并赋值给BuffStr
                    if (BuffStr.Length > 0)
                    {
                        dt.Rows.Add(GetData(BuffStr)); //加入到dt中
                        CurrLine++;

                    }
                }
                catch
                {

                    CurrLine--;
                }
            }
            FileReadStream.Close();
            dt.TableName = Path;
            return dt;
        }

        private Boolean IsInteger(String strSrc)
        {
            Boolean bRet = false;
            if (!String.IsNullOrEmpty(strSrc))
            {
                if (Regex.IsMatch(strSrc, @"[+-]?\d+"))
                {
                    try
                    {
                        Int64 iTemp = Convert.ToInt64(strSrc);
                        bRet = true;
                    }
                    catch
                    {
                    }
                }
            }
            return bRet;
        }

        private string[] GetData(string Buff)
        {
            if (Buff.Length == 0)
            {
                return new string[8];
            }
            string isbn = "";
            string bookName = "";
            string price = "";
            string num = "0";
            //string ID = "无"; //识别码
            //string BookName = "无"; //书名
            //string Author = "无"; //作者
            //string ISBN = "无";  //ISBN
            //string Price = "无"; //价格
            //string Press = "无"; //出版社
            //string PressYear = "无"; //出版年
            //string Abstract = "无"; //摘要
            //string eng = "无";
            int IndexLen = int.Parse(Buff.Substring(12, 5)) - 25;
            string IndexRecord = Buff.Substring(24, IndexLen); //目次区内容
            int Data_Address = int.Parse(Buff.Substring(12, 5)); //数据区地址
            isbn = GetRecord(Buff, "010", "a", IndexRecord, Data_Address).Trim().Replace("-","");
            bookName = GetRecord(Buff, "200", "a", IndexRecord, Data_Address).Trim();
            price = GetRecord(Buff, "010", "d", IndexRecord, Data_Address).Trim().Replace("CNY", "");
            num = GetRecord(Buff, "010", "d", IndexRecord, Data_Address).Trim();
            if (!IsInteger(num))
            {
                num = "0";
            }
            else
            {
                num = GetRecord(Buff, "010", "d", IndexRecord, Data_Address).Trim();
            }
            //ISBN
            //ISBN = GetRecord(Buff, "010", "a", IndexRecord, Data_Address).Trim();
            ////识别码
            //ID = GetRecord(Buff, "001", "", IndexRecord, Data_Address).Trim();
            ////书名
            //BookName = GetRecord(Buff, "200", "a", IndexRecord, Data_Address).Trim();
            ////作者
            //Author = GetRecord(Buff, "200", "f", IndexRecord, Data_Address).Trim();

            ////价格
            //Price = GetRecord(Buff, "010", "d", IndexRecord, Data_Address).Trim();
            ////出版社
            //Press = GetRecord(Buff, "210", "c", IndexRecord, Data_Address).Trim();
            ////出版年
            //PressYear = GetRecord(Buff, "210", "d", IndexRecord, Data_Address).Trim();
            ////摘要
            //Abstract = GetRecord(Buff, "330", "a", IndexRecord, Data_Address).Trim();
            //eng = GetRecord(Buff, "200", "d", IndexRecord, Data_Address).Trim();
            //string[] ret = { ISBN, ID, BookName, Author, Price, Press, PressYear, Abstract,eng };
            string[] ret = { isbn, bookName,price,num };
            return ret;
        }
        /// <summary>
        /// 返回Buff中的字段号为 Member， 子字段号为 ZiMember 的数据
        /// </summary>
        /// <param name="Buff"></param>
        /// <param name="Member">字段号</param>
        /// <param name="SubMember">子字段号</param>
        /// <param name="Data_Addr">地址区基地址</param>
        /// <returns></returns>
        public string GetRecord(string Buff, string Member, string SubMember, string IndexRecord, int Data_Addr)
        {
            string temp1 = ((char)30).ToString(); //子记录分隔符
            string temp2 = ((char)31).ToString(); //子字段分隔符
            if (SubMember != "")
            {
                SubMember = temp2 + SubMember;
            }
            string SubMbrData = "";
            int Address;
            int Len;
            string Sub;
            int SubMbrIndex;
            int SubMbrLen;
            for (int i = 0; i < IndexRecord.Length / 12; i++)
            {
                if (Member == IndexRecord.Substring(i * 12, 3))
                {
                    //子记录相对于数据区偏移地址
                    Address = int.Parse(IndexRecord.Substring(i * 12 + 7, 5));
                    //子记录长度
                    Len = int.Parse(IndexRecord.Substring(i * 12 + 3, 4));
                    //子记录数据
                    int test = GetOffSet(Buff, 0, Address + Data_Addr);
                    Len -= GetOffSet(Buff, Data_Addr + Address - test, Data_Addr + Address - test + Len);

                    Sub = Buff.Substring(Data_Addr + Address - test, Len);
                    int a = Sub.Length;
                    test = Sub.IndexOf(temp2);
                    if (Sub.IndexOf(temp2) == Sub.Length - 1)
                    {
                        //如果该子记录无子字段
                        return Sub.Substring(0, Sub.Length - 1);
                    }
                    //子字段开始位置0
                    SubMbrIndex = Sub.IndexOf(SubMember) + SubMember.Length;
                    //子字段长度

                    int test11 = Sub.IndexOf(temp2, SubMbrIndex + 2);


                    bool IsEnd = (Sub.IndexOf(temp2, SubMbrIndex) == -1);
                    if (!IsEnd)
                    {
                        SubMbrLen = Sub.IndexOf(temp2, SubMbrIndex) - SubMbrIndex;
                    }
                    else
                    {
                        SubMbrLen = Sub.IndexOf(temp1, SubMbrIndex) - SubMbrIndex;
                    }
                    //子字段数据
                    int NextIndex = Sub.IndexOf(temp2, SubMbrIndex + 1); //下一个子字段分隔符的位置
                    if (NextIndex == -1)
                    {
                        SubMbrData = Sub.Substring(SubMbrIndex, SubMbrLen);
                    }
                    else
                    {
                        SubMbrData = Sub.Substring(SubMbrIndex, SubMbrLen);
                    }

                    break;
                }
            }
            char[] test22 = SubMbrData.ToCharArray();
            return SubMbrData;
        }

        /// <summary>
        /// 计算由汉字引起的偏移地址误差，返回汉字个数
        /// </summary>
        ///<param name="Buff">待统计的字符串</param>
        ///<param name="StartIndex">开始位置</param>
        ///<param name="EndIndex">截至位置</param>
        /// <returns></returns>
        public int GetOffSet(string Buff, int StartIndex, int EndIndex)
        {
            char[] temp = Buff.ToCharArray();
            int LetterCount = 0, HanZiCount = 0;
            for (int i = StartIndex; i < EndIndex; i++)
            {
                int test = (int)temp[i];
                if (test < 128)
                {
                    LetterCount++;
                }
                else
                {
                    HanZiCount++;
                }
                if (HanZiCount * 2 + LetterCount == EndIndex - StartIndex)
                {
                    return HanZiCount;
                }
            }
            return HanZiCount;
        }

    }
}