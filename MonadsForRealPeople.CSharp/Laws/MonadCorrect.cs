using MonadsForRealPeople.CSharp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadsForRealPeople.CSharp.Laws.Correct
{
    public static class Maybe_Correct
    { 
        public static Maybe<U> Bind<T,U>(this Maybe<T> m, Func<T,Maybe<U>> f)
        {
            if (m.IsNothing)
            {
                return Maybe.Nothing<U>();
            }
            else
            {
                var val = m.Value;
                return f(m.Value);
            }
        }

        public static Maybe<T> Return<T>(T value)
        {
            var val = value;
            return Maybe.Just(val);
        }
    }
}
