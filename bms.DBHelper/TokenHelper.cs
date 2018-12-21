using bms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.DBHelper
{
    public class TokenHelper
    {
        
        
        public string userName { get; set; }
        public long date { get; set; }
        
        public string Produce(string userName)
        {
            TokenModel token = new TokenModel();
            string str = "";
            try
            {
                TokenHelper tokenHelper = new TokenHelper();
                long date = Convert.ToInt64(DateTime.Now.AddDays(1).ToString("yyyyMMddHHmmss")); //时效性1天
                tokenHelper.userName = userName;
                tokenHelper.date = date;
                string json = JsonHelper.JsonSerializerBySingleData(tokenHelper);
                token.tokenId = EncryptHelper.AESEncrypt(json);
                token.succ = true;
                str = JsonHelper.JsonSerializerBySingleData(token);
            }
            catch
            {
                token.succ = false;
            }
            return str;
        }
        public static bool Analysis(string str)
        {
            if (str!=null&&str!="")
            {
                try
                {
                    long now = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    TokenModel jsonData = JsonHelper.JsonDeserializeBySingleData<TokenModel>(str);
                    string json = jsonData.tokenId;
                    TokenHelper data = JsonHelper.JsonDeserializeBySingleData<TokenHelper>(json);
                    string name = data.userName;
                    long date = data.date;
                    if (now<date&&(name!=null||name!=""))
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
