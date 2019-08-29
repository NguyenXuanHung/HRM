using System.Web.Script.Serialization;

namespace Web.Core
{
    public static class JsonHelper
    {
        public static string ToJson(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static T FromJson<T>(string str)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(str);
        }

        public static string ToJson(this object obj, int recursionDepth)
        {
            var serializer = new JavaScriptSerializer { RecursionLimit = recursionDepth };
            return serializer.Serialize(obj);
        }

    }
}
