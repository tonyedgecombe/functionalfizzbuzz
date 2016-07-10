using System;
using System.IO;
using System.Text;
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
        static Func<dynamic, Func<dynamic, dynamic, dynamic>> CONST2 = c => (_1, _2) => c;
        static Func<dynamic, Func<dynamic>> RE = r => () => r;

        private static Func<dynamic> _I = () => F;

        static Func<dynamic, dynamic, dynamic> TRUE = (t, f) => t;
        static Func<dynamic, dynamic, dynamic> FALSE = (t, f) => f;

        static Func<dynamic, dynamic, dynamic, dynamic> IF = (c, t, e) => c(t, e);

        static Func<dynamic, dynamic, dynamic> AND = (l, r) => IF(l, r, FALSE);
        static Func<dynamic, dynamic, dynamic> OR = (l, r) => IF(l, TRUE, r);

        static Func<dynamic, dynamic> NOT = a => IF(a, FALSE, TRUE);

        static Func<dynamic, dynamic, dynamic> ZERO = (z, _) => z;
        static Func<dynamic, Func<dynamic, dynamic, dynamic>> SUCC = c => (_, i) => i(c);

        static Func<dynamic, dynamic> PREV = (n) => IF(IS_ZERO(n), ZERO, n(FALSE, ID));

        static Func<dynamic, Func<dynamic, dynamic, dynamic>> IS_ZERO = n => (t, f) => n(t, CONST(f));

        static Func<dynamic, dynamic, Func<dynamic>> _ADD = (a, b) => () => IF(IS_ZERO(b), RE(a), _ADD(SUCC(a), PREV(b)))();
        static Func<dynamic, dynamic, dynamic> ADD = (a, b) => _ADD(a, b)();

        static Func<dynamic, dynamic, Func<dynamic>> _SUB = (a, b) => () => IF(IS_ZERO(b), RE(a), _SUB(PREV(a), PREV(b)))();
        static Func<dynamic, dynamic, dynamic> SUB = (a, b) => _SUB(a, b)();

        static Func<dynamic, dynamic, dynamic, Func<dynamic>> _MUL = (a, b, t) => () => IF(IS_ZERO(a), RE(t), _MUL(PREV(a), b, ADD(b,t)))();
        static Func<dynamic, dynamic, dynamic> MUL = (a, b) => _MUL(a, b, ZERO)();

        static Func<dynamic, dynamic, Func<Func<dynamic, dynamic, dynamic>>> _LESS_THAN = (a, b) => () => IF(IS_ZERO(b), RE(FALSE), IF(IS_ZERO(a), RE(TRUE), _LESS_THAN(PREV(a), PREV(b))))();
        static Func<dynamic, dynamic, dynamic> LESS_THAN = (a, b) => _LESS_THAN(a, b)();

        static Func<dynamic, dynamic, dynamic, Func<dynamic>> _DIV = (a, b, t) => () => IF(LESS_THAN(a(), b()), t, _DIV(_SUB(a(), b()), b, RE(SUCC(t()))))();
        static Func<dynamic, dynamic, dynamic> DIV = (a, b) => _DIV(RE(a), RE(b), RE(ZERO))();

        static Func<dynamic, dynamic, Func<dynamic>> _MOD = (n, m) => () => IF(LESS_THAN(n(), m()), n, _MOD(_SUB(n(), m()), m))();
        static Func<dynamic, dynamic, dynamic> MOD = (n, m) => _MOD(RE(n), RE(m))();

        static Func<dynamic, dynamic> ACTION = a => IF(TRUE, a, a());
        static Func<dynamic, dynamic, dynamic, Func<dynamic>> _WITH_RANGE = (n, m, a) => () => IF(LESS_THAN(n(), m()), _WITH_RANGE(RE(SUCC(n())), m, ACTION(a)), RE(FALSE))();
        static Func<dynamic, dynamic, dynamic, dynamic> WITH_RANGE = (n, m, a) => _WITH_RANGE(RE(n), RE(m), a)();

        // Some constants
        static Func<dynamic, dynamic, dynamic> ONE = SUCC(ZERO);
        static Func<dynamic, dynamic, dynamic> TWO = SUCC(ONE);
        static Func<dynamic, dynamic, dynamic> THREE = SUCC(TWO);
        static Func<dynamic, dynamic, dynamic> FOUR = SUCC(THREE);
        static Func<dynamic, dynamic, dynamic> FIVE = SUCC(FOUR);
        static Func<dynamic, dynamic, dynamic> SIX = SUCC(FIVE);
        static Func<dynamic, dynamic, dynamic> SEVEN = SUCC(SIX);
        static Func<dynamic, dynamic, dynamic> EIGHT = SUCC(SEVEN);
        static Func<dynamic, dynamic, dynamic> TEN = MUL(TWO, FIVE);
        static Func<dynamic, dynamic, dynamic> THIRTEEN = ADD(EIGHT, FIVE);
        static Func<dynamic, dynamic, dynamic> FIFTEEN = MUL(THREE, FIVE);
        static Func<dynamic, dynamic, dynamic> SIXTEEN = MUL(TWO, EIGHT);
        static Func<dynamic, dynamic, dynamic> FORTY_EIGHT = MUL(SIXTEEN, THREE);
        static Func<dynamic, dynamic, dynamic> SIXTY_FOUR = MUL(EIGHT, EIGHT);
        static Func<dynamic, dynamic, dynamic> ONE_HUNDRED = MUL(TEN, TEN);

        // Characters
        static Func<dynamic, dynamic, dynamic> A = SUCC(SIXTY_FOUR);
        static Func<dynamic, dynamic, dynamic> B = SUCC(A);
        static Func<dynamic, dynamic, dynamic> C = SUCC(B);
        static Func<dynamic, dynamic, dynamic> D = SUCC(C);
        static Func<dynamic, dynamic, dynamic> E = SUCC(D);
        static Func<dynamic, dynamic, dynamic> F = SUCC(E);
        static Func<dynamic, dynamic, dynamic> G = SUCC(F);
        static Func<dynamic, dynamic, dynamic> H = SUCC(G);
        static Func<dynamic, dynamic, dynamic> I = SUCC(H);
        static Func<dynamic, dynamic, dynamic> J = SUCC(I);
        static Func<dynamic, dynamic, dynamic> K = SUCC(J);
        static Func<dynamic, dynamic, dynamic> L = SUCC(K);
        static Func<dynamic, dynamic, dynamic> M = SUCC(L);
        static Func<dynamic, dynamic, dynamic> N = SUCC(M);
        static Func<dynamic, dynamic, dynamic> O = SUCC(N);
        static Func<dynamic, dynamic, dynamic> P = SUCC(O);
        static Func<dynamic, dynamic, dynamic> Q = SUCC(P);
        static Func<dynamic, dynamic, dynamic> R = SUCC(Q);
        static Func<dynamic, dynamic, dynamic> S = SUCC(R);
        static Func<dynamic, dynamic, dynamic> T = SUCC(S);
        static Func<dynamic, dynamic, dynamic> U = SUCC(T);
        static Func<dynamic, dynamic, dynamic> V = SUCC(U);
        static Func<dynamic, dynamic, dynamic> W = SUCC(V);
        static Func<dynamic, dynamic, dynamic> X = SUCC(W);
        static Func<dynamic, dynamic, dynamic> Y = SUCC(X);
        static Func<dynamic, dynamic, dynamic> Z = SUCC(Y);

        static Func<dynamic, dynamic, dynamic> FIRST = (f, s) => f;
        static Func<dynamic, dynamic, dynamic> SECOND = (f, s) => s;

        static Func<dynamic, dynamic, dynamic, dynamic> END = (e, n, _) => e;
        static Func<dynamic, Func<dynamic, dynamic, dynamic>> IS_END = n => (t, f) => n(t, t, CONST2(f));

        static Func<dynamic, dynamic, Func<dynamic, dynamic, dynamic, dynamic>> CHR = (c, n) => (_1, _2, i) => i(c, n);

        static Func<dynamic, dynamic, Func<dynamic>> _JOIN = (l, r) => () => CHR(l()(_I, _I, FIRST), IF(IS_END(l()(_I, _I, SECOND)), r, _JOIN(RE(l()(_I, _I, SECOND)), r))());
        static Func<dynamic, dynamic, dynamic> JOIN = (l, r) => _JOIN(RE(l), RE(r))();

        static Func<dynamic, dynamic, dynamic, dynamic> FIZZ_STR = CHR(F, CHR(I, CHR(Z, CHR(Z, END))));
        static Func<dynamic, dynamic, dynamic, dynamic> BUZZ_STR = CHR(B, CHR(U, CHR(Z, CHR(Z, END))));
        static Func<dynamic, dynamic, dynamic, dynamic> FIZZBUZZ_STR = JOIN(FIZZ_STR, BUZZ_STR);

        static Func<dynamic, dynamic, dynamic, dynamic> CRLF = CHR(THIRTEEN, CHR(TEN, END));

        static Func<dynamic> NULL_FUNCTION = () => F;
        static Func<dynamic, dynamic, Func<dynamic>> ACTION2 = (s, a) => () => IF(TRUE, a, a(s));


        static Func<dynamic, dynamic, Func<dynamic>> _PRINT = (s, a) => () => IF(IS_END(s()), NULL_FUNCTION, _PRINT(RE(s()(F, F, SECOND)), ACTION2(s(),a())))();
        static Func<dynamic, dynamic, dynamic> PRINT = (s, a) => _PRINT(RE(s), RE(a))();

        static Func<dynamic, Func<dynamic>> _NUMTOSTR = n => () => IF(LESS_THAN(n(), TEN), RE(CHR(ADD(FORTY_EIGHT, n()), END)), _JOIN(_NUMTOSTR(RE(DIV(n(), TEN))), _NUMTOSTR(RE(MOD(n(), TEN)))))();
        static Func<dynamic, dynamic> NUMTOSTR = n => _NUMTOSTR(RE(n))();

        static Func<dynamic, dynamic, dynamic> DIVISIBLE_BY = (a, b) => IS_ZERO(MOD(a, b));
        static Func<dynamic, dynamic> DIVISIBLE_BY_THREE = (a) => IS_ZERO(MOD(a, THREE));
        static Func<dynamic, dynamic> DIVISIBLE_BY_FIVE = (a) => IS_ZERO(MOD(a, FIVE));
        static Func<dynamic, dynamic> DIVISIBLE_BY_FIFTEEN= (a) => IS_ZERO(MOD(a, FIFTEEN));

        static Func<dynamic, dynamic> LINE =
            n =>
                IF(DIVISIBLE_BY_FIFTEEN(n), FIZZBUZZ_STR,
                    IF(DIVISIBLE_BY_THREE(n), FIZZ_STR, IF(DIVISIBLE_BY_FIVE(n), BUZZ_STR, NUMTOSTR(n))));

        static Func<dynamic, dynamic> LINE_WITH_CRLF = (n) => JOIN(LINE(n), CRLF);

        static Func<dynamic, dynamic, dynamic, Func<dynamic>> _FIZZ_BUZZ = (s, e, o) => () => IF(LESS_THAN(s, e), _FIZZ_BUZZ(SUCC(s), e, o), RE(PRINT(LINE_WITH_CRLF(s), o)))();
        static Func<dynamic, dynamic> FIZZ_BUZZ = o => _FIZZ_BUZZ(ONE, ONE_HUNDRED, o)();

        // Helpers for tests

        private string ToString(dynamic s)
        {
            StringBuilder sb = new StringBuilder();

            while (!s(true, false, CONST2(false)))
            {
                sb.Append((char)ToNumber(s(false, false, FIRST)));

                s = s(false, false, SECOND);
            }
            return sb.ToString();
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

        private static int ToNumber(dynamic n)
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
        public void Line()
        {
            Assert.That(ToString(LINE(ONE)), Is.EqualTo("1"));    
            Assert.That(ToString(LINE(TWO)), Is.EqualTo("2"));    
            Assert.That(ToString(LINE(THREE)), Is.EqualTo("FIZZ"));    
            Assert.That(ToString(LINE(FOUR)), Is.EqualTo("4"));    
            Assert.That(ToString(LINE(FIVE)), Is.EqualTo("BUZZ"));    
            Assert.That(ToString(LINE(SIX)), Is.EqualTo("FIZZ"));    
            Assert.That(ToString(LINE(TEN)), Is.EqualTo("BUZZ"));    
            Assert.That(ToString(LINE(FIFTEEN)), Is.EqualTo("FIZZBUZZ"));    
            Assert.That(ToString(LINE(SIXTEEN)), Is.EqualTo("16"));    
        }

        [Test]
        public void DivisibleBy()
        {
            for (int i = 1; i <= 100; i++)
            {
                Assert.That(ToBool(DIVISIBLE_BY(fromNumber(i), THREE)), Is.EqualTo(i % 3 == 0));
                Assert.That(ToBool(DIVISIBLE_BY_THREE(fromNumber(i))), Is.EqualTo(i % 3 == 0));
                Assert.That(ToBool(DIVISIBLE_BY_FIVE(fromNumber(i))), Is.EqualTo(i % 5 == 0));
                Assert.That(ToBool(DIVISIBLE_BY_FIFTEEN(fromNumber(i))), Is.EqualTo(i % 15 == 0));
            }
        }

        [Test]
        public void NumToStr()
        {
            Assert.That(ToString(NUMTOSTR(fromNumber(0))), Is.EqualTo("0"));
            Assert.That(ToString(NUMTOSTR(fromNumber(9))), Is.EqualTo("9"));
            Assert.That(ToString(NUMTOSTR(fromNumber(10))), Is.EqualTo("10"));
            Assert.That(ToString(NUMTOSTR(fromNumber(123))), Is.EqualTo("123"));
        }

        [Test]
        public void Div()
        {
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    Assert.That(ToNumber(DIV(fromNumber(i), fromNumber(j))), Is.EqualTo(i/j));
                }
            }
        }

        [Test]
        public void Join()
        {
            Assert.That(ToString(JOIN(CHR(A, END), CHR(B, END))), Is.EqualTo("AB"));
        }

        [Test]
        public void Print()
        {
            var writer = new StringWriter();
            Func<dynamic, dynamic> wc = c =>
            {
                writer.Write((char)ToNumber(c(false, false, FIRST)));
                return false;
            };


            PRINT(FIZZBUZZ_STR, wc);

            Assert.That(writer.ToString(), Is.EqualTo("FIZZBUZZ"));
        }

        [Test]
        public void End()
        {
            Assert.That(ToBool(IS_END(END)), Is.True);
            Assert.That(ToBool(IS_END(FIZZ_STR)), Is.False);
        }

        [Test]
        public void String()
        {
            Assert.That(ToString(FIZZ_STR), Is.EqualTo("FIZZ"));
            Assert.That(ToString(BUZZ_STR), Is.EqualTo("BUZZ"));
            Assert.That(ToString(FIZZBUZZ_STR), Is.EqualTo("FIZZBUZZ"));
        }

        [Test]
        public void Constants()
        {
            Assert.That(ToNumber(ONE), Is.EqualTo(1));    
            Assert.That(ToNumber(TWO), Is.EqualTo(2));    
            Assert.That(ToNumber(THREE), Is.EqualTo(3));    
            Assert.That(ToNumber(FOUR), Is.EqualTo(4));    
            Assert.That(ToNumber(FIVE), Is.EqualTo(5));    
            Assert.That(ToNumber(SIX), Is.EqualTo(6));    
            Assert.That(ToNumber(SEVEN), Is.EqualTo(7));    
            Assert.That(ToNumber(EIGHT), Is.EqualTo(8));    
            Assert.That(ToNumber(TEN), Is.EqualTo(10));    
            Assert.That(ToNumber(THIRTEEN), Is.EqualTo(13));    
            Assert.That(ToNumber(FIFTEEN), Is.EqualTo(15));    
            Assert.That(ToNumber(SIXTEEN), Is.EqualTo(16));    
            Assert.That(ToNumber(FORTY_EIGHT), Is.EqualTo(48));    
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
        public void Mod()
        {
            Assert.That(ToNumber(MOD(fromNumber(4), fromNumber(5))), Is.EqualTo(4));
            Assert.That(ToNumber(MOD(fromNumber(5), fromNumber(5))), Is.EqualTo(0));
            Assert.That(ToNumber(MOD(fromNumber(8), fromNumber(5))), Is.EqualTo(3));
            Assert.That(ToNumber(MOD(fromNumber(13), fromNumber(5))), Is.EqualTo(3));
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
            Assert.That(ToNumber(PREV(fromNumber(0))), Is.EqualTo(0));
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

        public static void Main()
        {
            Func<dynamic, dynamic> consoleWriter = c =>
            {
                Console.Write((char)ToNumber(c(false, false, FIRST)));
                return false;
            };

            FIZZ_BUZZ(consoleWriter);
        }
    }
}
