using System;
using NUnit.Framework;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming

namespace FunctionalFizzBuzz
{
    [TestFixture]
    public class FunctionalFizzBuzz
    {
        static Func<dynamic, dynamic> ID = x => x;
        static Func<dynamic, Func<dynamic, dynamic>> CONST = c => _ => c;
        static Func<dynamic, Func<dynamic>> RETURN = r => () => r;

        static Func<dynamic, dynamic, dynamic> T = (t, f) => t;
        static Func<dynamic, dynamic, dynamic> F = (t, f) => f;

        static Func<dynamic, dynamic, dynamic, dynamic> IF = (c, t, e) => c(t, e);

        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> AND = (l, r) => IF(l, r, F);
        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> OR = (l, r) => IF(l, T, r);

        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> NOT = a => IF(a, F, T);

        static Func<dynamic, dynamic, dynamic> ZERO = (z, _) => z;
        static Func<dynamic, Func<dynamic, dynamic, dynamic>> SUCC = s => (_, i) => i(s);

        static Func<dynamic, dynamic>  PREV = (n) => n(F, ID);


        static Func<dynamic, Func<dynamic, dynamic, dynamic>> IS_ZERO = n => (t, f) => n(t, CONST(f));

        static Func<dynamic, dynamic, Func<dynamic>> ADD = (a, b) => () => IF(IS_ZERO(b), RETURN(a), ADD(SUCC(a), PREV(b)))();

        static Func<dynamic, dynamic, Func<dynamic>> SUB = (a, b) => () => IF(IS_ZERO(b), RETURN(a), SUB(PREV(a), PREV(b)))();

        // Helpers for tests

        private bool ToBool(dynamic f)
        {
            return f(true, false);
        }

        private dynamic FromBool(bool b)
        {
            return b ? T : F;
        }

        dynamic fromNumber(int n)
        {
            return n == 0 ? ZERO : SUCC(fromNumber(n - 1));
        }

        private int ToNumber(dynamic n)
        {
            var result = 0;
            while (!n(true, CONST(false)))
            {
                n = n(false, ID);
                result++;
            }

            return result;
        }

        // Tests

        [Test]
        public void TestAdd()
        {
            Assert.That(ToNumber(ADD(ZERO, ZERO)()), Is.EqualTo(0));
            Assert.That(ToNumber(ADD(fromNumber(1), ZERO)()), Is.EqualTo(1));
            Assert.That(ToNumber(ADD(fromNumber(1), fromNumber(1))()), Is.EqualTo(2));
            Assert.That(ToNumber(ADD(fromNumber(3), fromNumber(4))()), Is.EqualTo(7));
        }

        [Test]
        public void TestSub()
        {
            Assert.That(ToNumber(SUB(ZERO, ZERO)()), Is.EqualTo(0));
            Assert.That(ToNumber(SUB(fromNumber(1), ZERO)()), Is.EqualTo(1));
            Assert.That(ToNumber(SUB(fromNumber(5), fromNumber(2))()), Is.EqualTo(3));
        }

        [Test]
        public void Zero()
        {
            Assert.That(ToNumber(ZERO), Is.Zero);    
        }

        [Test]
        public void IsZero()
        {
            Assert.That(ToBool(IS_ZERO(fromNumber(0))), Is.True);
            Assert.That(ToBool(IS_ZERO(fromNumber(1))), Is.False);
            Assert.That(ToBool(IS_ZERO(fromNumber(2))), Is.False);

            Assert.That(ToBool(IF(IS_ZERO(ZERO), T, F)), Is.True);
            Assert.That(ToBool(IF(IS_ZERO(PREV(fromNumber(1))), T, F)), Is.True);
            Assert.That(ToBool(IF(IS_ZERO(PREV(fromNumber(2))), T, F)), Is.False);
        }

        [Test]
        public void If()
        {
            Assert.That(ToBool(IF(T, T, F)), Is.True);
            Assert.That(ToBool(IF(F, T, F)), Is.False);

            Assert.That(ToNumber(IF(T, fromNumber(1), fromNumber(2))), Is.EqualTo(1));
            Assert.That(ToNumber(IF(F, fromNumber(1), fromNumber(2))), Is.EqualTo(2));
        }

        [Test]
        public void TestNumber()
        {
            Assert.That(ToNumber(ZERO), Is.EqualTo(0));
            Assert.That(ToNumber(SUCC(ZERO)), Is.EqualTo(1));
            Assert.That(ToNumber(SUCC(SUCC(ZERO))), Is.EqualTo(2));
            Assert.That(ToNumber(SUCC(SUCC(SUCC(ZERO)))), Is.EqualTo(3));

            Assert.That(ToNumber(fromNumber(0)), Is.EqualTo(0));
            Assert.That(ToNumber(fromNumber(6)), Is.EqualTo(6));
        }

        [Test]
        public void Prev()
        {
            Assert.That(ToNumber(PREV(fromNumber(1))), Is.EqualTo(0));
            Assert.That(ToNumber(PREV(fromNumber(2))), Is.EqualTo(1));
        }

        [Test]
        public void TestBool()
        {
            Assert.That(ToBool(T), Is.True);
            Assert.That(ToBool(F), Is.False);
        }

        [Test]
        public void TestBinaryOperators()
        {
            Assert.That(ToBool(AND(FromBool(true), FromBool(true))), Is.True);
            Assert.That(ToBool(AND(FromBool(false), FromBool(true))), Is.False);
            Assert.That(ToBool(AND(FromBool(true), FromBool(false))), Is.False);
            Assert.That(ToBool(AND(FromBool(false), FromBool(false))), Is.False);

            Assert.That(ToBool(OR(FromBool(true), FromBool(true))), Is.True);
            Assert.That(ToBool(OR(FromBool(false), FromBool(true))), Is.True);
            Assert.That(ToBool(OR(FromBool(true), FromBool(false))), Is.True);
            Assert.That(ToBool(OR(FromBool(false), FromBool(false))), Is.False);

            Assert.That(ToBool(NOT(T)), Is.False);
            Assert.That(ToBool(NOT(F)), Is.True);
        }
    }
}
