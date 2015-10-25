using Monad;
using System;
using System.Linq;
using MonadsForRealPeople.CSharp.Longhand;

namespace MonadsForRealPeople.CSharp.Monadic
{
    public class EitherMonad
    {
        public static Either<Exception,string> MightFail(int value)
        {
            if (value > 3)
            {
                return (() => (value - 3).ToString());
            } else
            {
                return  () => new Exception("too small");
            }
        }

        public static Either<Exception,int[]> MightAlsoFail(string value)
        {
            switch (value)
            {
                case "1": return (() => new[] { 1 });
                case "2": return (() => new[] { 1, 2 });
                case "3": return (() => new[] { 1, 2, 3 });
                default: return (() => new Exception("too big"));
            }
        }

        public static Either<Exception,string> CouldFailToo(int[] input)
        {
            if (input.SequenceEqual(new [] { 1, 2}))
            {
                return () => "OneTwoPunch";
            }
            else
            {
                return () => new Exception("Knockout");
            }
        }

        public static string DoStuffThatMightFail()
        {
            var res = 
                from x in MightFail(5)
                from y in MightAlsoFail(x)
                from z in CouldFailToo(y)
                select ChoiceExample.FinalComputation(x, y, z);
            return res.Match(v => v, v => v.Message)();
        }
    }
}
