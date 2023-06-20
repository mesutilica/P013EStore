using Newtonsoft.Json;

namespace P013EStore.MVCUI.ExtensionMethods
{
    public static class SessionExtensionMethods
    {
        public static void SetJson(this ISession session, string key, object value) // dışarıdan object(her türlü nesne) türünde değer aldık
        {
            string objectString = JsonConvert.SerializeObject(value); // string bir değişken oluşturup parametreden gelen value yi json a çevirip değişkene atadık
            session.SetString(key, objectString); // session a json nesnesini string olarak yükledik
        }
        public static T GetJson<T>(this ISession session, string key) where T : class // sessiondaki veriyi T(yani generic olarak) tutan ve bize kullanmak istediğimiz yerde getirecek metodumuz
        {
            string objectString = session.GetString(key); // session daki veriyi string değişkenimize aktardık
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }
            T value = JsonConvert.DeserializeObject<T>(objectString); // json verimizi tekrardan nesneye çevirip çağrıldığı yere gönderdik.
            return value;
        }
    }
}
