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

        static Func<dynamic, dynamic, dynamic> TRUE = (t, f) => t;
        static Func<dynamic, dynamic, dynamic> FALSE = (t, f) => f;

        static Func<dynamic, dynamic, dynamic, dynamic> IF = (c, t, e) => c(t, e);

        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> AND = (l, r) => IF(l, r, FALSE);
        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> OR = (l, r) => IF(l, TRUE, r);

        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> NOT = a => IF(a, FALSE, TRUE);

        static Func<dynamic, dynamic, dynamic> ZERO = (z, _) => z;
        static Func<dynamic, Func<dynamic, dynamic, dynamic>> SUCC = c => (_, i) => i(c);

        static Func<dynamic, dynamic>  PREV = (n) => n(FALSE, ID);

        static Func<dynamic, Func<dynamic, dynamic, dynamic>> IS_ZERO = n => (t, f) => n(t, CONST(f));

        static Func<dynamic, dynamic, Func<dynamic>> _ADD = (a, b) => () => IF(IS_ZERO(b), RETURN(a), _ADD(SUCC(a), PREV(b)))();
        static Func<dynamic, dynamic, dynamic> ADD = (a, b) => _ADD(a, b)();

        static Func<dynamic, dynamic, Func<dynamic>> _SUB = (a, b) => () => IF(IS_ZERO(b), RETURN(a), _SUB(PREV(a), PREV(b)))();
        static Func<dynamic, dynamic, dynamic> SUB = (a, b) => _SUB(a, b)();

        private static Func<dynamic, dynamic, dynamic, Func<dynamic>> _MUL = (a, b, t) => () => IF(IS_ZERO(a), RETURN(t), _MUL(PREV(a), b, ADD(b,t)))();
        static Func<dynamic, dynamic, dynamic> MUL = (a, b) => _MUL(a, b, ZERO)();

        static Func<dynamic, dynamic, Func<Func<dynamic, dynamic, dynamic>>> _LESS_THAN = (a, b) => () => IF(IS_ZERO(b), RETURN(FALSE), IF(IS_ZERO(a), RETURN(TRUE), _LESS_THAN(PREV(a), PREV(b))))();
        static Func<dynamic, dynamic, dynamic> LESS_THAN = (a, b) => _LESS_THAN(a, b)();

        static Func<dynamic, dynamic, Func<dynamic>> _MOD = (n, m) => () => IF(LESS_THAN(n(), m()), n, _MOD(_SUB(n(), m()), m))();
        static Func<dynamic, dynamic, dynamic> MOD = (n, m) => _MOD(RETURN(n), RETURN(m))();

        static Func<dynamic, dynamic> ACTION = a => IF(TRUE, a, a());
        static Func<dynamic, dynamic, dynamic, Func<dynamic>> _WITH_RANGE = (n, m, a) => () => IF(LESS_THAN(n(), m()), _WITH_RANGE(RETURN(SUCC(n())), m, ACTION(a)), RETURN(FALSE))();
        static Func<dynamic, dynamic, dynamic, dynamic> WITH_RANGE = (n, m, a) => _WITH_RANGE(RETURN(n), RETURN(m), a)();

        // Some constants
        static Func<dynamic, dynamic, dynamic> ONE = SUCC(ZERO);
        static Func<dynamic, dynamic, dynamic> TWO = SUCC(ONE);
        static Func<dynamic, dynamic, dynamic> THREE = SUCC(TWO);
        static Func<dynamic, dynamic, dynamic> FOUR = SUCC(THREE);
        static Func<dynamic, dynamic, dynamic> FIVE = SUCC(SUCC(THREE));
        static Func<dynamic, dynamic, dynamic> TEN = MUL(TWO, FIVE);
        static Func<dynamic, dynamic, dynamic> ONE_HUNDRED = MUL(TEN, TEN);

        // Helpers for tests

        [Test]
        public void Mul()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Assert.That(ToNumber(MUL(fromNumber(i), fromNumber(j))), Is.EqualTo(i*j));
                }
            }
        }

        [Test]
        public void Constants()
        {
            Assert.That(ToNumber(ONE), Is.EqualTo(1));    
            Assert.That(ToNumber(TWO), Is.EqualTo(2));    
            Assert.That(ToNumber(THREE), Is.EqualTo(3));    
            Assert.That(ToNumber(FOUR), Is.EqualTo(4));    
            Assert.That(ToNumber(FIVE), Is.EqualTo(5));    
            Assert.That(ToNumber(TEN), Is.EqualTo(10));    
            Assert.That(ToNumber(ONE_HUNDRED), Is.EqualTo(100));    
        }

        [Test]
        public void WithRange()
        {

            var result = 0;
            WITH_RANGE(fromNumber(1), fromNumber(100), new Func<int>(() => result++));
            Assert.That(result, Is.EqualTo(100));
        }

        [Test]
        public void Mod()
        {
            Assert.That(ToNumber(MOD(fromNumber(4), fromNumber(5))), Is.EqualTo(4));
            Assert.That(ToNumber(MOD(fromNumber(5), fromNumber(5))), Is.EqualTo(0));
            Assert.That(ToNumber(MOD(fromNumber(8), fromNumber(5))), Is.EqualTo(3));
            Assert.That(ToNumber(MOD(fromNumber(13), fromNumber(5))), Is.EqualTo(3));
        }

        private bool ToBool(dynamic f)
        {
            return f(true, false);
        }

        private dynamic FromBool(bool b)
        {
            return b ? TRUE : FALSE;
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

        [Test]
        public void LessThan()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Assert.That(ToBool(LESS_THAN(fromNumber(i), fromNumber(j))), Is.EqualTo(i < j));
                }
            }
        }


        [Test]
        public void Add()
        {
            Assert.That(ToNumber(ADD(ZERO, ZERO)), Is.EqualTo(0));
            Assert.That(ToNumber(ADD(fromNumber(1), ZERO)), Is.EqualTo(1));
            Assert.That(ToNumber(ADD(fromNumber(1), fromNumber(1))), Is.EqualTo(2));
            Assert.That(ToNumber(ADD(fromNumber(3), fromNumber(4))), Is.EqualTo(7));
        }

        [Test]
        public void Sub()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = i; j < 10; j++)
                {
                    Assert.That(ToNumber(SUB(fromNumber(j), fromNumber(i))), Is.EqualTo(j-i));
                }
            }
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

            Assert.That(ToBool(IF(IS_ZERO(ZERO), TRUE, FALSE)), Is.True);
            Assert.That(ToBool(IF(IS_ZERO(PREV(fromNumber(1))), TRUE, FALSE)), Is.True);
            Assert.That(ToBool(IF(IS_ZERO(PREV(fromNumber(2))), TRUE, FALSE)), Is.False);
        }

        [Test]
        public void If()
        {
            Assert.That(ToBool(IF(TRUE, TRUE, FALSE)), Is.True);
            Assert.That(ToBool(IF(FALSE, TRUE, FALSE)), Is.False);

            Assert.That(ToNumber(IF(TRUE, fromNumber(1), fromNumber(2))), Is.EqualTo(1));
            Assert.That(ToNumber(IF(FALSE, fromNumber(1), fromNumber(2))), Is.EqualTo(2));
        }

        [Test]
        public void Number()
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
        public void Bool()
        {
            Assert.That(ToBool(TRUE), Is.True);
            Assert.That(ToBool(FALSE), Is.False);
        }

        [Test]
        public void BinaryOperators()
        {
            Assert.That(ToBool(AND(FromBool(true), FromBool(true))), Is.True);
            Assert.That(ToBool(AND(FromBool(false), FromBool(true))), Is.False);
            Assert.That(ToBool(AND(FromBool(true), FromBool(false))), Is.False);
            Assert.That(ToBool(AND(FromBool(false), FromBool(false))), Is.False);

            Assert.That(ToBool(OR(FromBool(true), FromBool(true))), Is.True);
            Assert.That(ToBool(OR(FromBool(false), FromBool(true))), Is.True);
            Assert.That(ToBool(OR(FromBool(true), FromBool(false))), Is.True);
            Assert.That(ToBool(OR(FromBool(false), FromBool(false))), Is.False);

            Assert.That(ToBool(NOT(TRUE)), Is.False);
            Assert.That(ToBool(NOT(FALSE)), Is.True);
        }
    }
}
