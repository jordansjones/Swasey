using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Tests.Helpers
{
    public static class KvPair
    {

        public static KeyValuePair<TKey, TValue> Of<TKey, TValue>(TKey key, TValue value)
        {
            return new KeyValuePair<TKey, TValue>(key, value);
        }

    }
}
