using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace FunctionalFizzBuzz
{
    public class FunctionalFizzBuzzTests
    {
        private string ToString(dynamic s)
        {
            StringBuilder sb = new StringBuilder();

            while (!s(true, false, FunctionalFizzBuzz.CONST2(false)))
            {
                sb.Append((char) FunctionalFizzBuzz.ToNumber(s(false, false, FunctionalFizzBuzz.FIRST)));

                s = s(false, false, FunctionalFizzBuzz.SECOND);
            }
            return sb.ToString();
        }

        private bool ToBool(dynamic f)
        {
            return f(true, false);
        }

        private dynamic FromBool(bool b)
        {
            return b ? FunctionalFizzBuzz.TRUE : FunctionalFizzBuzz.FALSE;
        }

        private dynamic fromNumber(int n)
        {
            return n == 0 ? FunctionalFizzBuzz.ZERO : FunctionalFizzBuzz.SUCC(fromNumber(n - 1));
        }


        [Test]
        public void Line()
        {
            Assert.That(ToString(FunctionalFizzBuzz.LINE(FunctionalFizzBuzz.ONE)), Is.EqualTo("1"));    
            Assert.That(ToString(FunctionalFizzBuzz.LINE(FunctionalFizzBuzz.TWO)), Is.EqualTo("2"));    
            Assert.That(ToString(FunctionalFizzBuzz.LINE(FunctionalFizzBuzz.THREE)), Is.EqualTo("FIZZ"));    
            Assert.That(ToString(FunctionalFizzBuzz.LINE(FunctionalFizzBuzz.FOUR)), Is.EqualTo("4"));    
            Assert.That(ToString(FunctionalFizzBuzz.LINE(FunctionalFizzBuzz.FIVE)), Is.EqualTo("BUZZ"));    
            Assert.That(ToString(FunctionalFizzBuzz.LINE(FunctionalFizzBuzz.SIX)), Is.EqualTo("FIZZ"));    
            Assert.That(ToString(FunctionalFizzBuzz.LINE(FunctionalFizzBuzz.TEN)), Is.EqualTo("BUZZ"));    
            Assert.That(ToString(FunctionalFizzBuzz.LINE(FunctionalFizzBuzz.FIFTEEN)), Is.EqualTo("FIZZBUZZ"));    
            Assert.That(ToString(FunctionalFizzBuzz.LINE(FunctionalFizzBuzz.SIXTEEN)), Is.EqualTo("16"));    
        }

        [Test]
        public void DivisibleBy()
        {
            for (int i = 1; i <= 100; i++)
            {
                Assert.That(ToBool(FunctionalFizzBuzz.DIVISIBLE_BY(fromNumber(i), FunctionalFizzBuzz.THREE)), Is.EqualTo(i % 3 == 0));
                Assert.That(ToBool(FunctionalFizzBuzz.DIVISIBLE_BY_THREE(fromNumber(i))), Is.EqualTo(i % 3 == 0));
                Assert.That(ToBool(FunctionalFizzBuzz.DIVISIBLE_BY_FIVE(fromNumber(i))), Is.EqualTo(i % 5 == 0));
                Assert.That(ToBool(FunctionalFizzBuzz.DIVISIBLE_BY_FIFTEEN(fromNumber(i))), Is.EqualTo(i % 15 == 0));
            }
        }

        [Test]
        public void NumToStr()
        {
            Assert.That(ToString(FunctionalFizzBuzz.NUMTOSTR(fromNumber(0))), Is.EqualTo("0"));
            Assert.That(ToString(FunctionalFizzBuzz.NUMTOSTR(fromNumber(9))), Is.EqualTo("9"));
            Assert.That(ToString(FunctionalFizzBuzz.NUMTOSTR(fromNumber(10))), Is.EqualTo("10"));
            Assert.That(ToString(FunctionalFizzBuzz.NUMTOSTR(fromNumber(123))), Is.EqualTo("123"));
        }

        [Test]
        public void Div()
        {
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.DIV(fromNumber(i), fromNumber(j))), Is.EqualTo(i/j));
                }
            }
        }

        [Test]
        public void Join()
        {
            Assert.That(ToString(FunctionalFizzBuzz.JOIN(FunctionalFizzBuzz.CHR(FunctionalFizzBuzz.A, FunctionalFizzBuzz.END), FunctionalFizzBuzz.CHR(FunctionalFizzBuzz.B, FunctionalFizzBuzz.END))), Is.EqualTo("AB"));
        }

        [Test]
        public void Print()
        {
            var writer = new StringWriter();
            Func<dynamic, dynamic> wc = c =>
            {
                writer.Write((char) FunctionalFizzBuzz.ToNumber(c(false, false, FunctionalFizzBuzz.FIRST)));
                return false;
            };


            FunctionalFizzBuzz.PRINT(FunctionalFizzBuzz.FIZZBUZZ_STR, wc);

            Assert.That(writer.ToString(), Is.EqualTo("FIZZBUZZ"));
        }

        [Test]
        public void End()
        {
            Assert.That(ToBool(FunctionalFizzBuzz.IS_END(FunctionalFizzBuzz.END)), Is.True);
            Assert.That(ToBool(FunctionalFizzBuzz.IS_END(FunctionalFizzBuzz.FIZZ_STR)), Is.False);
        }

        [Test]
        public void String()
        {
            Assert.That(ToString(FunctionalFizzBuzz.FIZZ_STR), Is.EqualTo("FIZZ"));
            Assert.That(ToString(FunctionalFizzBuzz.BUZZ_STR), Is.EqualTo("BUZZ"));
            Assert.That(ToString(FunctionalFizzBuzz.FIZZBUZZ_STR), Is.EqualTo("FIZZBUZZ"));
        }

        [Test]
        public void Constants()
        {
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.ONE), Is.EqualTo(1));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.TWO), Is.EqualTo(2));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.THREE), Is.EqualTo(3));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.FOUR), Is.EqualTo(4));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.FIVE), Is.EqualTo(5));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.SIX), Is.EqualTo(6));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.SEVEN), Is.EqualTo(7));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.EIGHT), Is.EqualTo(8));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.TEN), Is.EqualTo(10));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.THIRTEEN), Is.EqualTo(13));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.FIFTEEN), Is.EqualTo(15));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.SIXTEEN), Is.EqualTo(16));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.FORTY_EIGHT), Is.EqualTo(48));    
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.ONE_HUNDRED), Is.EqualTo(100));    
        }

        [Test]
        public void WithRange()
        {

            var result = 0;
            FunctionalFizzBuzz.WITH_RANGE(fromNumber(1), fromNumber(100), new Func<int>(() => result++));
            Assert.That(result, Is.EqualTo(100));
        }

        [Test]
        public void Mul()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.MUL(fromNumber(i), fromNumber(j))), Is.EqualTo(i*j));
                }
            }
        }

        [Test]
        public void Mod()
        {
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.MOD(fromNumber(4), fromNumber(5))), Is.EqualTo(4));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.MOD(fromNumber(5), fromNumber(5))), Is.EqualTo(0));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.MOD(fromNumber(8), fromNumber(5))), Is.EqualTo(3));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.MOD(fromNumber(13), fromNumber(5))), Is.EqualTo(3));
        }

        [Test]
        public void LessThan()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Assert.That(ToBool(FunctionalFizzBuzz.LESS_THAN(fromNumber(i), fromNumber(j))), Is.EqualTo(i < j));
                }
            }
        }

        [Test]
        public void Add()
        {
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.ADD(FunctionalFizzBuzz.ZERO, FunctionalFizzBuzz.ZERO)), Is.EqualTo(0));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.ADD(fromNumber(1), FunctionalFizzBuzz.ZERO)), Is.EqualTo(1));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.ADD(fromNumber(1), fromNumber(1))), Is.EqualTo(2));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.ADD(fromNumber(3), fromNumber(4))), Is.EqualTo(7));
        }

        [Test]
        public void Sub()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = i; j < 10; j++)
                {
                    Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.SUB(fromNumber(j), fromNumber(i))), Is.EqualTo(j-i));
                }
            }
        }

        [Test]
        public void Zero()
        {
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.ZERO), Is.Zero);    
        }

        [Test]
        public void IsZero()
        {
            Assert.That(ToBool(FunctionalFizzBuzz.IS_ZERO(fromNumber(0))), Is.True);
            Assert.That(ToBool(FunctionalFizzBuzz.IS_ZERO(fromNumber(1))), Is.False);
            Assert.That(ToBool(FunctionalFizzBuzz.IS_ZERO(fromNumber(2))), Is.False);

            Assert.That(ToBool(FunctionalFizzBuzz.IF(FunctionalFizzBuzz.IS_ZERO(FunctionalFizzBuzz.ZERO), FunctionalFizzBuzz.TRUE, FunctionalFizzBuzz.FALSE)), Is.True);
            Assert.That(ToBool(FunctionalFizzBuzz.IF(FunctionalFizzBuzz.IS_ZERO(FunctionalFizzBuzz.PREV(fromNumber(1))), FunctionalFizzBuzz.TRUE, FunctionalFizzBuzz.FALSE)), Is.True);
            Assert.That(ToBool(FunctionalFizzBuzz.IF(FunctionalFizzBuzz.IS_ZERO(FunctionalFizzBuzz.PREV(fromNumber(2))), FunctionalFizzBuzz.TRUE, FunctionalFizzBuzz.FALSE)), Is.False);
        }

        [Test]
        public void If()
        {
            Assert.That(ToBool(FunctionalFizzBuzz.IF(FunctionalFizzBuzz.TRUE, FunctionalFizzBuzz.TRUE, FunctionalFizzBuzz.FALSE)), Is.True);
            Assert.That(ToBool(FunctionalFizzBuzz.IF(FunctionalFizzBuzz.FALSE, FunctionalFizzBuzz.TRUE, FunctionalFizzBuzz.FALSE)), Is.False);

            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.IF(FunctionalFizzBuzz.TRUE, fromNumber(1), fromNumber(2))), Is.EqualTo(1));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.IF(FunctionalFizzBuzz.FALSE, fromNumber(1), fromNumber(2))), Is.EqualTo(2));
        }

        [Test]
        public void Number()
        {
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.ZERO), Is.EqualTo(0));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.SUCC(FunctionalFizzBuzz.ZERO)), Is.EqualTo(1));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.SUCC(FunctionalFizzBuzz.SUCC(FunctionalFizzBuzz.ZERO))), Is.EqualTo(2));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.SUCC(FunctionalFizzBuzz.SUCC(FunctionalFizzBuzz.SUCC(FunctionalFizzBuzz.ZERO)))), Is.EqualTo(3));

            Assert.That(FunctionalFizzBuzz.ToNumber(fromNumber(0)), Is.EqualTo(0));
            Assert.That(FunctionalFizzBuzz.ToNumber(fromNumber(6)), Is.EqualTo(6));
        }

        [Test]
        public void Prev()
        {
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.PREV(fromNumber(0))), Is.EqualTo(0));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.PREV(fromNumber(1))), Is.EqualTo(0));
            Assert.That(FunctionalFizzBuzz.ToNumber(FunctionalFizzBuzz.PREV(fromNumber(2))), Is.EqualTo(1));
        }

        [Test]
        public void Bool()
        {
            Assert.That(ToBool(FunctionalFizzBuzz.TRUE), Is.True);
            Assert.That(ToBool(FunctionalFizzBuzz.FALSE), Is.False);
        }

        [Test]
        public void BinaryOperators()
        {
            Assert.That(ToBool(FunctionalFizzBuzz.AND(FromBool(true), FromBool(true))), Is.True);
            Assert.That(ToBool(FunctionalFizzBuzz.AND(FromBool(false), FromBool(true))), Is.False);
            Assert.That(ToBool(FunctionalFizzBuzz.AND(FromBool(true), FromBool(false))), Is.False);
            Assert.That(ToBool(FunctionalFizzBuzz.AND(FromBool(false), FromBool(false))), Is.False);

            Assert.That(ToBool(FunctionalFizzBuzz.OR(FromBool(true), FromBool(true))), Is.True);
            Assert.That(ToBool(FunctionalFizzBuzz.OR(FromBool(false), FromBool(true))), Is.True);
            Assert.That(ToBool(FunctionalFizzBuzz.OR(FromBool(true), FromBool(false))), Is.True);
            Assert.That(ToBool(FunctionalFizzBuzz.OR(FromBool(false), FromBool(false))), Is.False);

            Assert.That(ToBool(FunctionalFizzBuzz.NOT(FunctionalFizzBuzz.TRUE)), Is.False);
            Assert.That(ToBool(FunctionalFizzBuzz.NOT(FunctionalFizzBuzz.FALSE)), Is.True);
        }
    }
}