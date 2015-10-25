using Monad;
using MonadsForRealPeople.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MonadsForRealPeople.CSharp.Types;

namespace MonadsForRealPeople.CSharp.Longhand
{
    public class ChoiceExample
    {
        public static Choice<Exception,string> MightFail(int value)
        {
            return (value > 3) 
                ? Choice<Exception,string>.Success((value - 3).ToString())
                : Choice<Exception,string>.Fail(new Exception("too small"));
        }

        public static Choice<Exception,int[]> MightAlsoFail(string value)
        {
            switch (value)
            {
                case "1": return Choice<Exception,int[]>.Success(new[] { 1 });
                case "2": return Choice<Exception,int[]>.Success(new[] { 1, 2 });
                case "3": return Choice<Exception,int[]>.Success(new[] { 1, 2, 3 });
                default: return Choice<Exception,int[]>.Fail(new Exception("too big"));
            }
        }

        public static Choice<Exception,string> CouldFailToo(int[] input)
        {
            if (input.SequenceEqual(new [] { 1, 2}))
            {
                return Choice<Exception, string>.Success("OneTwoPunch");
            }
            else
            {
                return Choice<Exception, string>.Fail(new Exception("Knockout"));
            }
        }

        public static string FinalComputation(string x, int[] y, string z)
        {
            return string.Format("{0} [{1}] {2}", x, string.Join(",", y), z);
        }

        public static string DoStuffThatMightFail()
        {
            var x = MightFail(5);
            if (!x.IsSuccess) { return x.FailValue.Message; }
            var y = MightAlsoFail(x.SuccessValue);
            if (!y.IsSuccess) { return y.FailValue.Message; }
            var z = CouldFailToo(y.SuccessValue);
            if (!z.IsSuccess) { return z.FailValue.Message; }
            return FinalComputation(x.SuccessValue, y.SuccessValue, z.SuccessValue);
        }
    }

    [TestFixture]
    public class TestChoice
    {
        [Test]
        public void EitherWorks()
        {
            Assert.AreEqual("2 [1,2] OneTwoPunch", ChoiceExample.DoStuffThatMightFail());
        }
    }
}
