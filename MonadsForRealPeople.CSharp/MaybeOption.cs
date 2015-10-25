using Monad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MonadsForRealPeople.CSharp
{
    public class MaybeExample
    {
        public static string MightReturnNull(int value)
        {
            return (value > 3) ? (value - 3).ToString() : null;
        }

        public static int[] MightAlsoReturnNull(string value)
        {
            switch (value)
            {
                case "1": return new[] { 1 };
                case "2": return new[] { 1, 2 };
                case "3": return new[] { 1, 2, 3 };
                default: return null;
            }
        }

        public static string CouldBeNullToo(int[] input)
        {
            if (input.SequenceEqual(new [] { 1, 2}))
            {
                return "OneTwoPunch";
            }
            else
            {
                return null;
            }
        }

        public static string FinalComputation(string x, int[] y, string z)
        {
            return string.Format("{0} {1} {2}", x, y, z);
        }

        public static string MaybeDoStuff()
        {
            var x = MightReturnNull(5);
            if (x == null) { return null; }
            var y = MightAlsoReturnNull(x);
            if (y == null) { return null; }
            var z = CouldBeNullToo(y);
            if (z == null) { return null; }
            return FinalComputation(x, y, z);
        }
    }

    public class TestMaybe
    {
        [Fact]
        public void MaybeWorks()
        {
            Assert.Equal("2 [1,2] OneTwoPunch", MaybeExample.MaybeDoStuff());
        }
    }


    public class MaybeCSharpMonad
    {
        public static Option<string> MightReturnNothing(int value)
        {
            return (value > 3) ? Option.Return(() => (value - 3).ToString()) : Option.Nothing<string>();
        }

        public static Option<int[]> MightAlsoReturnNothing(string value)
        {
            switch (value)
            {
                case "1": return Option.Return(() => new[] { 1 });
                case "2": return Option.Return(() => new[] { 1, 2 });
                case "3": return Option.Return(() => new[] { 1, 2, 3 });
                default: return Option.Nothing<int[]>();
            }
        }

        public static Option<string> CouldBeNothingToo(int[] input)
        {
            if (input.SequenceEqual(new [] { 1, 2}))
            {
                return Option.Return(() => "OneTwoPunch");
            }
            else
            {
                return Option.Nothing<string>();
            }
        }

        public static string MaybeDoStuff()
        {
            var result =
                from x in MightReturnNothing(5)
                from y in MightAlsoReturnNothing(x)
                from z in CouldBeNothingToo(y)
                select MaybeExample.FinalComputation(x, y, z);

            return result.Match(v => v, () => "Nope")();
        }
    }

    // example Maybe for showing how not to do it.
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

        public static Maybe<U> Bind<T,U>(this Maybe<T> m, Func<T,Maybe<U>> f)
        {
            if (m.IsNothing)
            {
                return Maybe.Nothing<U>();
            }
            else
            {
                var val = m.Value;
                /*
                if (val is int) {
                    val = (T)(object)((int)((object)val) - 1);
                }
                */
                return f(m.Value);
            }
        }

        public static Maybe<T> Return<T>(T value)
        {
            var val = value;
            /*
            if (val is int) {
                val = (T)(object)((int)((object)val) - 1);
            }
            */
            return Just(val);
        }
    }
}
