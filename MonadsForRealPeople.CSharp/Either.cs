using MonadsForRealPeople.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MonadsForRealPeople.CSharp
{
    public struct Either<TSuccess,TFail>
    {
        private TSuccess _successValue;
        private TFail _failValue;
        private bool _isSuccess;

        public static Either<TSuccess,TFail> Success(TSuccess successValue)
        {
            var ret = new Either<TSuccess,TFail>();
            ret._successValue = successValue;
            ret._isSuccess = true;
            return ret;
        }

        public static Either<TSuccess,TFail> Fail(TFail failValue)
        {
            var ret = new Either<TSuccess,TFail>();
            ret._failValue = failValue;
            ret._isSuccess = false;
            return ret;
        }

        public bool IsSuccess { get { return _isSuccess; } }
        public TSuccess SuccessValue { get { return _successValue; } }
        public TFail FailValue { get { return _failValue; } }
    }

    public class EitherExample
    {
        public static Either<string,string> MightFail(int value)
        {
            return (value > 3) 
                ? Either<string,string>.Success((value - 3).ToString())
                : Either<string,string>.Fail("too small");
        }

        public static Either<int[],string> MightAlsoFail(string value)
        {
            switch (value)
            {
                case "1": return Either<int[],string>.Success(new[] { 1 });
                case "2": return Either<int[],string>.Success(new[] { 1, 2 });
                case "3": return Either<int[], string>.Success(new[] { 1, 2, 3 });
                default: return Either<int[],string>.Fail("too big");
            }
        }

        public static Either<string,string> CouldFailToo(int[] input)
        {
            if (input.SequenceEqual(new [] { 1, 2}))
            {
                return Either<string, string>.Success("OneTwoPunch");
            }
            else
            {
                return Either<string, string>.Fail("Knockout");
            }
        }

        public static string FinalComputation(string x, int[] y, string z)
        {
            return string.Format("{0} {1} {2}", x, y, z);
        }

        public static string DoStuffThatMightFail()
        {
            var x = MightFail(5);
            if (!x.IsSuccess) { return x.FailValue; }
            var y = MightAlsoFail(x.SuccessValue);
            if (!y.IsSuccess) { return y.FailValue; }
            var z = CouldFailToo(y.SuccessValue);
            if (!z.IsSuccess) { return z.FailValue; }
            return FinalComputation(x.SuccessValue, y.SuccessValue, z.SuccessValue);
        }
    }

    public class TestEither
    {
        [Fact]
        public void EitherWorks()
        {
            Assert.Equal("2 [1,2] OneTwoPunch", EitherExample.DoStuffThatMightFail());
        }
    }
}
