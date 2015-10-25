using Monad;
using MonadsForRealPeople.CSharp.Longhand;
using System.Linq;

namespace MonadsForRealPeople.CSharp.Monadic
{
    public class MaybeMonad
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
}
