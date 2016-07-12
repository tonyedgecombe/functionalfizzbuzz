using System;
// ReSharper disable FieldCanBeMadeReadOnly.Global

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming

namespace FunctionalFizzBuzz
{
    public static class FunctionalFizzBuzz
    {
        public static Func<dynamic, dynamic> ID = x => x;
        public static Func<dynamic, Func<dynamic, dynamic>> CONST = c => _ => c;
        public static Func<dynamic, Func<dynamic, dynamic, dynamic>> CONST2 = c => (_1, _2) => c;
        public static Func<dynamic, Func<dynamic>> RE = r => () => r;

        public static Func<dynamic> _I = () => F;

        public static Func<dynamic, dynamic, dynamic> TRUE = (t, f) => t;
        public static Func<dynamic, dynamic, dynamic> FALSE = (t, f) => f;

        public static Func<dynamic, dynamic, dynamic, dynamic> IF = (c, t, e) => c(t, e);

        public static Func<dynamic, dynamic, dynamic> AND = (l, r) => IF(l, r, FALSE);
        public static Func<dynamic, dynamic, dynamic> OR = (l, r) => IF(l, TRUE, r);

        public static Func<dynamic, dynamic> NOT = a => IF(a, FALSE, TRUE);

        public static Func<dynamic, dynamic, dynamic> ZERO = (z, _) => z;
        public static Func<dynamic, Func<dynamic, dynamic, dynamic>> SUCC = c => (_, i) => i(c);

        public static Func<dynamic, dynamic> PREV = (n) => IF(IS_ZERO(n), ZERO, n(FALSE, ID));

        public static Func<dynamic, Func<dynamic, dynamic, dynamic>> IS_ZERO = n => (t, f) => n(t, CONST(f));

        public static Func<dynamic, dynamic, Func<dynamic>> _ADD = (a, b) => () => IF(IS_ZERO(b), RE(a), _ADD(SUCC(a), PREV(b)))();
        public static Func<dynamic, dynamic, dynamic> ADD = (a, b) => _ADD(a, b)();

        public static Func<dynamic, dynamic, Func<dynamic>> _SUB = (a, b) => () => IF(IS_ZERO(b), RE(a), _SUB(PREV(a), PREV(b)))();
        public static Func<dynamic, dynamic, dynamic> SUB = (a, b) => _SUB(a, b)();

        public static Func<dynamic, dynamic, dynamic, Func<dynamic>> _MUL = (a, b, t) => () => IF(IS_ZERO(a), RE(t), _MUL(PREV(a), b, ADD(b,t)))();
        public static Func<dynamic, dynamic, dynamic> MUL = (a, b) => _MUL(a, b, ZERO)();

        public static Func<dynamic, dynamic, Func<Func<dynamic, dynamic, dynamic>>> _LESS_THAN = (a, b) => () => IF(IS_ZERO(b), RE(FALSE), IF(IS_ZERO(a), RE(TRUE), _LESS_THAN(PREV(a), PREV(b))))();
        public static Func<dynamic, dynamic, dynamic> LESS_THAN = (a, b) => _LESS_THAN(a, b)();

        public static Func<dynamic, dynamic, dynamic, Func<dynamic>> _DIV = (a, b, t) => () => IF(LESS_THAN(a(), b()), t, _DIV(_SUB(a(), b()), b, RE(SUCC(t()))))();
        public static Func<dynamic, dynamic, dynamic> DIV = (a, b) => _DIV(RE(a), RE(b), RE(ZERO))();

        public static Func<dynamic, dynamic, Func<dynamic>> _MOD = (n, m) => () => IF(LESS_THAN(n(), m()), n, _MOD(_SUB(n(), m()), m))();
        public static Func<dynamic, dynamic, dynamic> MOD = (n, m) => _MOD(RE(n), RE(m))();

        public static Func<dynamic, dynamic> ACTION = a => IF(TRUE, a, a());
        public static Func<dynamic, dynamic, dynamic, Func<dynamic>> _WITH_RANGE = (n, m, a) => () => IF(LESS_THAN(n(), m()), _WITH_RANGE(RE(SUCC(n())), m, ACTION(a)), RE(FALSE))();
        public static Func<dynamic, dynamic, dynamic, dynamic> WITH_RANGE = (n, m, a) => _WITH_RANGE(RE(n), RE(m), a)();

        // Some constants
        public static Func<dynamic, dynamic, dynamic> ONE = SUCC(ZERO);
        public static Func<dynamic, dynamic, dynamic> TWO = SUCC(ONE);
        public static Func<dynamic, dynamic, dynamic> THREE = SUCC(TWO);
        public static Func<dynamic, dynamic, dynamic> FOUR = SUCC(THREE);
        public static Func<dynamic, dynamic, dynamic> FIVE = SUCC(FOUR);
        public static Func<dynamic, dynamic, dynamic> SIX = SUCC(FIVE);
        public static Func<dynamic, dynamic, dynamic> SEVEN = SUCC(SIX);
        public static Func<dynamic, dynamic, dynamic> EIGHT = SUCC(SEVEN);
        public static Func<dynamic, dynamic, dynamic> TEN = MUL(TWO, FIVE);
        public static Func<dynamic, dynamic, dynamic> THIRTEEN = ADD(EIGHT, FIVE);
        public static Func<dynamic, dynamic, dynamic> FIFTEEN = MUL(THREE, FIVE);
        public static Func<dynamic, dynamic, dynamic> SIXTEEN = MUL(TWO, EIGHT);
        public static Func<dynamic, dynamic, dynamic> FORTY_EIGHT = MUL(SIXTEEN, THREE);
        public static Func<dynamic, dynamic, dynamic> SIXTY_FOUR = MUL(EIGHT, EIGHT);
        public static Func<dynamic, dynamic, dynamic> ONE_HUNDRED = MUL(TEN, TEN);

        // Characters
        public static Func<dynamic, dynamic, dynamic> A = SUCC(SIXTY_FOUR);
        public static Func<dynamic, dynamic, dynamic> B = SUCC(A);
        public static Func<dynamic, dynamic, dynamic> C = SUCC(B);
        public static Func<dynamic, dynamic, dynamic> D = SUCC(C);
        public static Func<dynamic, dynamic, dynamic> E = SUCC(D);
        public static Func<dynamic, dynamic, dynamic> F = SUCC(E);
        public static Func<dynamic, dynamic, dynamic> G = SUCC(F);
        public static Func<dynamic, dynamic, dynamic> H = SUCC(G);
        public static Func<dynamic, dynamic, dynamic> I = SUCC(H);
        public static Func<dynamic, dynamic, dynamic> J = SUCC(I);
        public static Func<dynamic, dynamic, dynamic> K = SUCC(J);
        public static Func<dynamic, dynamic, dynamic> L = SUCC(K);
        public static Func<dynamic, dynamic, dynamic> M = SUCC(L);
        public static Func<dynamic, dynamic, dynamic> N = SUCC(M);
        public static Func<dynamic, dynamic, dynamic> O = SUCC(N);
        public static Func<dynamic, dynamic, dynamic> P = SUCC(O);
        public static Func<dynamic, dynamic, dynamic> Q = SUCC(P);
        public static Func<dynamic, dynamic, dynamic> R = SUCC(Q);
        public static Func<dynamic, dynamic, dynamic> S = SUCC(R);
        public static Func<dynamic, dynamic, dynamic> T = SUCC(S);
        public static Func<dynamic, dynamic, dynamic> U = SUCC(T);
        public static Func<dynamic, dynamic, dynamic> V = SUCC(U);
        public static Func<dynamic, dynamic, dynamic> W = SUCC(V);
        public static Func<dynamic, dynamic, dynamic> X = SUCC(W);
        public static Func<dynamic, dynamic, dynamic> Y = SUCC(X);
        public static Func<dynamic, dynamic, dynamic> Z = SUCC(Y);

        public static Func<dynamic, dynamic, dynamic> FIRST = (f, s) => f;
        public static Func<dynamic, dynamic, dynamic> SECOND = (f, s) => s;

        public static Func<dynamic, dynamic, dynamic, dynamic> END = (e, n, _) => e;
        public static Func<dynamic, Func<dynamic, dynamic, dynamic>> IS_END = n => (t, f) => n(t, t, CONST2(f));

        public static Func<dynamic, dynamic, Func<dynamic, dynamic, dynamic, dynamic>> CHR = (c, n) => (_1, _2, i) => i(c, n);

        public static Func<dynamic, dynamic, Func<dynamic>> _JOIN = (l, r) => () => CHR(l()(_I, _I, FIRST), IF(IS_END(l()(_I, _I, SECOND)), r, _JOIN(RE(l()(_I, _I, SECOND)), r))());
        public static Func<dynamic, dynamic, dynamic> JOIN = (l, r) => _JOIN(RE(l), RE(r))();

        public static Func<dynamic, dynamic, dynamic, dynamic> FIZZ_STR = CHR(F, CHR(I, CHR(Z, CHR(Z, END))));
        public static Func<dynamic, dynamic, dynamic, dynamic> BUZZ_STR = CHR(B, CHR(U, CHR(Z, CHR(Z, END))));
        public static Func<dynamic, dynamic, dynamic, dynamic> FIZZBUZZ_STR = JOIN(FIZZ_STR, BUZZ_STR);

        public static Func<dynamic, dynamic, dynamic, dynamic> CRLF = CHR(THIRTEEN, CHR(TEN, END));

        public static Func<dynamic> NULL_FUNCTION = () => F;
        public static Func<dynamic, dynamic, Func<dynamic>> ACTION2 = (s, a) => () => IF(TRUE, a, a(s));


        public static Func<dynamic, dynamic, Func<dynamic>> _PRINT = (s, a) => () => IF(IS_END(s()), NULL_FUNCTION, _PRINT(RE(s()(F, F, SECOND)), ACTION2(s(),a())))();
        public static Func<dynamic, dynamic, dynamic> PRINT = (s, a) => _PRINT(RE(s), RE(a))();

        public static Func<dynamic, Func<dynamic>> _NUMTOSTR = n => () => IF(LESS_THAN(n(), TEN), RE(CHR(ADD(FORTY_EIGHT, n()), END)), _JOIN(_NUMTOSTR(RE(DIV(n(), TEN))), _NUMTOSTR(RE(MOD(n(), TEN)))))();
        public static Func<dynamic, dynamic> NUMTOSTR = n => _NUMTOSTR(RE(n))();

        public static Func<dynamic, dynamic, dynamic> DIVISIBLE_BY = (a, b) => IS_ZERO(MOD(a, b));
        public static Func<dynamic, dynamic> DIVISIBLE_BY_THREE = (a) => IS_ZERO(MOD(a, THREE));
        public static Func<dynamic, dynamic> DIVISIBLE_BY_FIVE = (a) => IS_ZERO(MOD(a, FIVE));
        public static Func<dynamic, dynamic> DIVISIBLE_BY_FIFTEEN= (a) => IS_ZERO(MOD(a, FIFTEEN));

        public static Func<dynamic, dynamic> LINE =
            n =>
                IF(DIVISIBLE_BY_FIFTEEN(n), FIZZBUZZ_STR,
                    IF(DIVISIBLE_BY_THREE(n), FIZZ_STR, IF(DIVISIBLE_BY_FIVE(n), BUZZ_STR, NUMTOSTR(n))));

        public static Func<dynamic, dynamic> LINE_WITH_CRLF = (n) => JOIN(LINE(n), CRLF);

        public static Func<dynamic, dynamic, dynamic, Func<dynamic>> _FIZZ_BUZZ = (s, e, o) => () => IF(LESS_THAN(s, e), _FIZZ_BUZZ(SUCC(s), e, o), RE(PRINT(LINE_WITH_CRLF(s), o)))();
        public static Func<dynamic, dynamic> FIZZ_BUZZ = o => _FIZZ_BUZZ(ONE, ONE_HUNDRED, o)();

        public static int ToNumber(dynamic n)
        {
            var result = 0;
            while (!n(true, CONST(false)))
            {
                n = n(false, ID);
                result++;
            }

            return result;
        }

        public static void Main()
        {
            Func<dynamic, dynamic> consoleWriter = c =>
            {
                Console.Write((char) ToNumber(c(false, false, FIRST)));
                return false;
            };

            FIZZ_BUZZ(consoleWriter);
        }
    }
}
