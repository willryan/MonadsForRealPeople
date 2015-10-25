using MonadsForRealPeople.CSharp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadsForRealPeople.CSharp.Laws.Wrong
{
    public static class Maybe_Wrong
    {
        public static Maybe<U> Bind<T, U>(this Maybe<T> m, Func<T, Maybe<U>> f)
        {
            if (m.IsNothing)
            {
                return Maybe.Nothing<U>();
            }
            else
            {
                // subtract one from bound value
                var val = m.Value;
                if (val is decimal)
                {
                    var decVal = ((decimal)((object)val));
                    decVal = decVal * decVal;
                    val = (T)(object)(decVal);
                }
                return f(val);
            }
        }

        public static Maybe<T> Return<T>(T value)
        {
            // add one to lifted value
            var val = value;
            if (val is decimal)
            {
                var decVal = ((decimal)((object)val));
                decVal = (decimal)Math.Sqrt((double)decVal);
                val = (T)(object)(decVal);
            }
            return Maybe.Just(val);
        }
    }

    public static class List_Wrong
    {
        public static List<U> Bind<T, U>(this List<T> m, Func<T, List<U>> f)
        {
            if (m.Count == 0)
            {
                return new List<U>();
            }
            else
            {
                return m.SelectMany(f).ToList();
            }
        }

        public static List<T> Return<T>(T value)
        {
            return new List<T>() { value };
        }
    }
}
