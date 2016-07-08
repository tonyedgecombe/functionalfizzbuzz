using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

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
        static Func<dynamic, Func<dynamic, dynamic>> CONST = c => x => c;

        static Func<dynamic, dynamic, dynamic> T = (t, f) => t;
        static Func<dynamic, dynamic, dynamic> F = (t, f) => f;

        static Func<dynamic, dynamic, dynamic, dynamic> IF = (c, t, e) => c(t, e);

        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> AND = (l, r) => IF(l, r, F);
        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> OR = (l, r) => IF(l, T, r);

        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> NOT = a => IF(a, F, T);

        static Func<dynamic, dynamic, dynamic, dynamic> ZERO = (s, z, i) => z;
        static Func<dynamic, dynamic, dynamic, Func<dynamic, dynamic, dynamic, dynamic>> SUCC = (s, z, i) => (s2, z2, i2) => i2(s);

        static Func<dynamic, Func<dynamic, dynamic, dynamic>> IS_ZERO = n => (t, f) => n(f, t, CONST(f));

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
            if (n == 0)
            {
                return ZERO;
            }
            else
            {
                return SUCC(fromNumber(n - 1), false, ID);
            }
        }

        private int ToNumber(dynamic n)
        {
            var result = 0;
            while (!(n(false, true, ID) is bool))
            {
                n = n(false, false, ID);
                result++;
            }

            return result;
        }

        [Test]
        public void IsZero()
        {
            Assert.That(ToBool(IS_ZERO(fromNumber(0))), Is.True);
            Assert.That(ToBool(IS_ZERO(fromNumber(1))), Is.False);
            Assert.That(ToBool(IS_ZERO(fromNumber(2))), Is.False);
        }

        [Test]
        public void TestNumber()
        {
            Assert.That(ToNumber(ZERO), Is.EqualTo(0));
            Assert.That(ToNumber(SUCC(ZERO, F, ID)), Is.EqualTo(1));
            Assert.That(ToNumber(SUCC(SUCC(ZERO, F, ID), F, ID)), Is.EqualTo(2));
            Assert.That(ToNumber(SUCC(SUCC(SUCC(ZERO, F, ID), F, ID), F, ID)), Is.EqualTo(3));

            Assert.That(ToNumber(fromNumber(0)), Is.EqualTo(0));
            Assert.That(ToNumber(fromNumber(6)), Is.EqualTo(6));
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

            Assert.That(ToBool(NOT(FromBool(true))), Is.False);
            Assert.That(ToBool(NOT(FromBool(false))), Is.True);
        }
    }
}
