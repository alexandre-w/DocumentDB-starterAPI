using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DocumentDB_starterAPI.Helpers
{
    public class JsonHelper
    {
        /// <summary>
        /// Function to remove _rid _self _etag _attachments _ts form a JObject
        /// </summary>
        /// <param name="jObj">JObject to clean</param>
        /// <returns></returns>
        public static JObject cleanJObject(JObject jObj)
        {
            if (jObj != null)
            {
                jObj.Remove("_rid");
                jObj.Remove("_self");
                jObj.Remove("_etag");
                jObj.Remove("_attachments");
                jObj.Remove("_ts");
            }

            return jObj;
        }


        /// <summary>
        /// Function to remove _rid _self _etag _attachments _ts form a list of JObject
        /// </summary>
        /// <param name="JEnum">List of JObject to clean</param>
        /// <returns></returns>
        public static List<JObject> cleanJObjectList(List<JObject> JEnum)
        {
            if (JEnum != null)
            {
                foreach (JObject jObj in JEnum)
                {
                    jObj.Remove("_rid");
                    jObj.Remove("_self");
                    jObj.Remove("_etag");
                    jObj.Remove("_attachments");
                    jObj.Remove("_ts");
                }
            }

            return JEnum;
        }
    }
}