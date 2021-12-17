using System;

namespace iParty.Business.Infra.Extensions
{
    public static class ExtensionMethods
    {
        public static T ExceptionIfNull<T>(this T obj, string msg) where T : class
        {
            if (obj == null)
                throw new Exception(msg);
            return obj;
        }

        public static T IfNull<T>(this T obj, Action action) where T : class
        {
            if (obj == null)
                action?.Invoke();   
            return obj; 
        }
    }
}
