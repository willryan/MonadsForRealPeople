using Monad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadsForRealPeople.CSharp.Types
{
    public class Maybe<T>
    {
        public bool IsNothing { get; }
        private T _value;
        public T Value
        {
            get
            {
                if (IsNothing)
                {
                    throw new Exception("No Value");
                }
                return _value;
            }
        }
        public Maybe(T value)
        {
            _value = value;
            IsNothing = false;
        }
        public Maybe()
        {
            _value = default(T);
            IsNothing = true;
        }
        public override bool Equals(object obj)
        {
            var oMaybe = obj as Maybe<T>;
            if (oMaybe == null) return false;
            if (oMaybe.IsNothing != IsNothing) return false;
            if (!IsNothing)
            {
                if (!oMaybe.Value.Equals(Value)) return false;
            }
            return true;
        }
        public override string ToString()
        {
            if (IsNothing) return "Nothing";
            return string.Format("Just {0}", Value);
        }
    }

    public static class Maybe
    {
        public static Maybe<T> Just<T>(T value)
        {
            return new Maybe<T>(value);
        }

        public static Maybe<T> Nothing<T>()
        {
            return new Maybe<T>();
        }
    }

}
