using Newtonsoft.Json;

namespace MvcWebUI.Extensions;

public static class SessionExtensionMethods
{
    public static void SetObject(this ISession session,string key, object value)
    {
        string objectString = JsonConvert.SerializeObject(value);
        session.SetString(key,objectString);
    }

    public static T GetObject<T>(this ISession session, string key) where T:class
    {
        string value = session.GetString(key);
        if (!String.IsNullOrEmpty(value))
        {
            T data = JsonConvert.DeserializeObject<T>(value);
            return data;
        }
        else
        {
            return null;
        }
    }
}