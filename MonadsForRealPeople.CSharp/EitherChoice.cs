using Monad;
using MonadsForRealPeople.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MonadsForRealPeople.CSharp
{
    public struct Choice<TFail,TSuccess>
    {
        private TSuccess _successValue;
        private TFail _failValue;
        private bool _isSuccess;

        public static Choice<TFail,TSuccess> Success(TSuccess successValue)
        {
            var ret = new Choice<TFail,TSuccess>();
            ret._successValue = successValue;
            ret._isSuccess = true;
            return ret;
        }

        public static Choice<TFail,TSuccess> Fail(TFail failValue)
        {
            var ret = new Choice<TFail,TSuccess>();
            ret._failValue = failValue;
            ret._isSuccess = false;
            return ret;
        }

        public bool IsSuccess { get { return _isSuccess; } }
        public TSuccess SuccessValue { get { return _successValue; } }
        public TFail FailValue { get { return _failValue; } }
    }

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
            return string.Format("{0} {1} {2}", x, y, z);
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

    public class TestChoice
    {
        [Fact]
        public void EitherWorks()
        {
            Assert.Equal("2 [1,2] OneTwoPunch", ChoiceExample.DoStuffThatMightFail());
        }
    }

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
