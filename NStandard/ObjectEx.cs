namespace NStandard
{
    public static class ObjectEx
    {
        public static object Invoke(object obj, string methodName, params object[] parameters) => obj.GetType().GetMethod(methodName).Invoke(obj, parameters);
        public static object GetPropertyValue(object obj, string propertyName) => obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        public static void SetPropertyValue(object obj, string propertyName, object value) => obj.GetType().GetProperty(propertyName).SetValue(obj, value);
        public static object GetFieldValue(object obj, string filedName) => obj.GetType().GetField(filedName).GetValue(obj);
        public static void SetFieldValue(object obj, string filedName, object value) => obj.GetType().GetField(filedName).SetValue(obj, value);
    }
}
