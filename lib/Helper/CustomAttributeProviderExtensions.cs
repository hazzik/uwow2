using System;
using System.Reflection;

namespace Hazzik {
    public static class CustomAttributeProviderExtensions {
        public static T[] GetCustomAttributes<T>(this ICustomAttributeProvider type, bool inherit)
            where T : Attribute {
            return (T[])type.GetCustomAttributes(typeof(T), inherit);
        }
    }
}