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
        static Func<dynamic, dynamic, dynamic> T = (t, f) => t;
        static Func<dynamic, dynamic, dynamic> F = (t, f) => f;

        static Func<dynamic, dynamic, dynamic, dynamic> IF = (c, t, e) => c(t, e);

        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> AND = (l, r) => IF(l, r, F);
        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> OR = (l, r) => IF(l, T, r);

        static Func<Func<dynamic, dynamic, dynamic>, Func<dynamic, dynamic, dynamic>> NOT = a => IF(a, F, T);

        static Func<dynamic, dynamic, dynamic> ZERO = (s, z) => z;
        static Func<dynamic, dynamic, Func<dynamic, dynamic, dynamic>> SUCC = (s, z) => (s2, z2) => s;

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
                return SUCC(fromNumber(n - 1), null);
            }
        }

        private int ToNumber(dynamic n)
        {
            if (n(false, true) is bool)
            {
                return 0;
            }
            else
            {
                return 1 + ToNumber(n(F, F));
            }
        }

        [Test]
        public void TestNumber()
        {
            Assert.That(ToNumber(ZERO), Is.EqualTo(0));
            Assert.That(ToNumber(SUCC(ZERO, F)), Is.EqualTo(1));
            Assert.That(ToNumber(SUCC(SUCC(ZERO, F), F)), Is.EqualTo(2));
            Assert.That(ToNumber(SUCC(SUCC(SUCC(ZERO, F), F), F)), Is.EqualTo(3));

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
