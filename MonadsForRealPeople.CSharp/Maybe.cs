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

        public static int[] MightAlsoReturnNothing(string value)
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
            var y = MightAlsoReturnNothing(x);
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
}
