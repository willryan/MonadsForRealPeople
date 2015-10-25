using Monad;
using MonadsForRealPeople.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MonadsForRealPeople.CSharp.Types
{
    public struct Choice<TFail, TSuccess>
    {
        private TSuccess _successValue;
        private TFail _failValue;
        private bool _isSuccess;

        public static Choice<TFail, TSuccess> Success(TSuccess successValue)
        {
            var ret = new Choice<TFail, TSuccess>();
            ret._successValue = successValue;
            ret._isSuccess = true;
            return ret;
        }

        public static Choice<TFail, TSuccess> Fail(TFail failValue)
        {
            var ret = new Choice<TFail, TSuccess>();
            ret._failValue = failValue;
            ret._isSuccess = false;
            return ret;
        }

        public bool IsSuccess { get { return _isSuccess; } }
        public TSuccess SuccessValue { get { return _successValue; } }
        public TFail FailValue { get { return _failValue; } }
    }
}
