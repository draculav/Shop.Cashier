using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Cashier.Util
{
    public class JsonHelper
    {
        public static string GetValueInJson(string json, string key)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            string result = "";
            try
            {

                result = jo[key].ToString();
            }
            catch (Exception ee)
            {
                string ss = ee.Message;
                //log
            }
            return result;
        }

        public static int GetIntValueInJson(string json, string key)
        {
            string s = GetValueInJson(json, key);
            int result;
            if (int.TryParse(s, out result))
            {
                return result;
            }
            return 0;
        }
    }
}
