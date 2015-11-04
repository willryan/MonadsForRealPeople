using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonadsForRealPeople.CSharp.Types;
//using MonadsForRealPeople.CSharp.Laws.Correct;
//using M = MonadsForRealPeople.CSharp.Laws.Correct;
using MonadsForRealPeople.CSharp.Laws.Wrong;
using M = MonadsForRealPeople.CSharp.Laws.Wrong.Maybe_Wrong;
using NUnit.Framework;

namespace MonadsForRealPeople.CSharp.Laws
{
    using MFunc = System.Func<int, Maybe<int>>;

    public static class MaybeFuncs
    {
        public static Maybe<int> Add2(int x) => Maybe.Just(x + 2);
        public static Maybe<int> Add3(int x) => Maybe.Just(x + 3);
        public static Maybe<int> Add5(int x) => Maybe.Just(x + 5);
    }
    [TestFixture]
    public class Test
    {
        [Test]
        public void Laws_LeftIdentity()
        {
            var m = M.Return(7);
            var output = m.Bind(MaybeFuncs.Add2);
            Assert.AreEqual(MaybeFuncs.Add2(7), output);
        }

        [Test]
        public void Laws_RightIdentity()
        {
            var input = Maybe.Just(7);
            var output = input.Bind(M.Return);
            Assert.AreEqual(input, output);
        }

        [Test]
        public void Laws_Associativity()
        {
            // (f >=> g) >=> h
            MFunc sub5 = v =>
                 MaybeFuncs.Add2(v).Bind(MaybeFuncs.Add3);
            MFunc mFinal1 = v =>
                 sub5(v).Bind(MaybeFuncs.Add5);

            // f >=> (g >=> h)
            MFunc sub8 = v =>
                 MaybeFuncs.Add3(v).Bind(MaybeFuncs.Add5);
            MFunc mFinal2 = v =>
                 MaybeFuncs.Add2(v).Bind(sub8); 

            var v1 = mFinal1(7);
            var v2 = mFinal2(7);
            Assert.AreEqual(v1, v2);
        }

        [Test]
        public void List_LeftIdentity()
        {
            var m = M.Return(7);
            var output = m.Bind(MaybeFuncs.Add2);
            Assert.AreEqual(MaybeFuncs.Add2(7), output);
        }

        [Test]
        public void List_RightIdentity()
        {
            var input = Maybe.Just(7m);
            var output = input.Bind(M.Return);
            Assert.AreEqual(input, output);
        }

        [Test]
        public void List_Associativity()
        {
            // (f >=> g) >=> h
            MFunc sub5 = v =>
                 MaybeFuncs.Add2(v).Bind(MaybeFuncs.Add3);
            MFunc mFinal1 = v =>
                 sub5(v).Bind(MaybeFuncs.Add5);

            // f >=> (g >=> h)
            MFunc sub8 = v =>
                 MaybeFuncs.Add3(v).Bind(MaybeFuncs.Add5);
            MFunc mFinal2 = v =>
                 MaybeFuncs.Add2(v).Bind(sub8); 

            var v1 = mFinal1(7);
            var v2 = mFinal2(7);
            Assert.AreEqual(v1, v2);
        }
    }
}
