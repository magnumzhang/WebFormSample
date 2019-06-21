using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebFormSample.Lib
{
    public class JSonUtility
    {
        //将JSon对象序列化为JSon字符串。
        public static string GetSerializedJSonStr(object obj)
        {
            string JSonStr = JsonConvert.SerializeObject(obj);
            return JSonStr;
        }

        public static Dictionary<string, string> GetJSonDic(string JSonStr)
        {
            JObject JSonData = (JObject)JsonConvert.DeserializeObject(JSonStr);

            Dictionary<string, string> JSonDataDic = new Dictionary<string, string>();

            foreach (KeyValuePair<string, JToken> JT in JSonData)
            {
                JSonDataDic.Add(JT.Key, Convert.ToString(JT.Value));
            }

            return JSonDataDic;
        }
    }
}