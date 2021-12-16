using System;

namespace iParty.Business.Infra.Extensions
{
    public static class ExceptionExtension
    {
        public static T ExceptionIfNull<T>(this T obj, string msg) where T : class
        {
            if (obj == null)
                throw new Exception(msg);
            return obj;
        }
    }
}
