using System;

namespace Hazzik.Utils
{
    public static class Monads
    {
        public static TResult With<T, TResult>(this T self, Func<T, TResult> monad) where T : class
        {
            if (self == null)
                return default(TResult);
            return monad(self);
        }
    }
}