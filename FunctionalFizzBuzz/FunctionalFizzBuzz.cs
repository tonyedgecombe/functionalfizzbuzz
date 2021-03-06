﻿using System;

namespace FunctionalFizzBuzz
{
    public static class FunctionalFizzBuzz
    {
        // The identity function returns whatever is passed in
        public static Func<dynamic, dynamic> ID = x => x;

        // CONSTn allows us to return a function that returns a constant
        public static Func<dynamic, Func<dynamic>> CONST0 = c => () => c;
        public static Func<dynamic, Func<dynamic, dynamic>> CONST1 = c => _1 => c;
        public static Func<dynamic, Func<dynamic, dynamic, dynamic>> CONST2 = c => (_1, _2) => c;

        // Use _I for parameteres to ignore
        public static Func<dynamic> _I = () => F;

        // bool values have to have the same signature but differ in their application
        public static Func<dynamic, dynamic, dynamic> TRUE = (t, f) => t;
        public static Func<dynamic, dynamic, dynamic> FALSE = (t, f) => f;

        // A few operators give us binary and hence a working computer
        public static Func<dynamic, dynamic, dynamic, dynamic> IF = (c, t, e) => c(t, e);

        public static Func<dynamic, dynamic, dynamic> AND = (l, r) => IF(l, r, FALSE);
        public static Func<dynamic, dynamic, dynamic> OR = (l, r) => IF(l, TRUE, r);

        public static Func<dynamic, dynamic> NOT = a => IF(a, FALSE, TRUE);

        // Numbers are sets, where each element is represented by a function
        // so we have:
        // SUCC(ZERO) = 1
        // SUCC(SUCC(ZERO)) = 2
        // SUCC(SUCC(SUCC(ZERO))) = 3
        //
        // and so on
        public static Func<dynamic, dynamic, dynamic> ZERO = (z, _) => z;
        public static Func<dynamic, Func<dynamic, dynamic, dynamic>> SUCC = c => (_, i) => i(c);

        // Now we can decrement a number by removing the outer function, effectively by applying it
        public static Func<dynamic, dynamic> PREV = n => IF(IS_ZERO(n), ZERO, n(FALSE, ID));

        // As ZERO and SUCC have the same signature we can apply a little magic to determine
        // which is which.
        public static Func<dynamic, Func<dynamic, dynamic, dynamic>> IS_ZERO = n => (t, f) => n(t, CONST1(f));

        // Given to sets each representing a number we can find the total by removing all the items
        // from one set and adding it to the other.
        //
        // As we don't have access to loops we do this using recursion, one item at a time
        //
        // Note as the IF doesn't short circuit we have to use some tickery to make sure
        // the then and else parameters aren't called. Returning a function that returns the result
        // solves this problem, you will see this pattern everywhere we use an IF.
        public static Func<dynamic, dynamic, Func<dynamic>> _ADD =
            (a, b) => () => IF(IS_ZERO(b), CONST0(a), _ADD(SUCC(a), PREV(b)))();

        public static Func<dynamic, dynamic, dynamic> ADD = (a, b) => _ADD(a, b)();

        public static Func<dynamic, dynamic, Func<dynamic>> _SUB =
            (a, b) => () => IF(IS_ZERO(b), CONST0(a), _SUB(PREV(a), PREV(b)))();

        public static Func<dynamic, dynamic, dynamic> SUB = (a, b) => _SUB(a, b)();

        public static Func<dynamic, dynamic, dynamic, Func<dynamic>> _MUL =
            (a, b, t) => () => IF(IS_ZERO(a), CONST0(t), _MUL(PREV(a), b, ADD(b, t)))();

        public static Func<dynamic, dynamic, dynamic> MUL = (a, b) => _MUL(a, b, ZERO)();

        // LESS_THAN is a race to zero
        public static Func<dynamic, dynamic, Func<Func<dynamic, dynamic, dynamic>>> _LESS_THAN =
            (a, b) => () => IF(IS_ZERO(b), 
                                CONST0(FALSE), 
                                IF(IS_ZERO(a), 
                                    CONST0(TRUE), 
                                    _LESS_THAN(PREV(a), PREV(b))))();

        public static Func<dynamic, dynamic, dynamic> LESS_THAN = (a, b) => _LESS_THAN(a, b)();

        public static Func<dynamic, dynamic, dynamic, Func<dynamic>> _DIV =
            (a, b, t) => () => IF(LESS_THAN(a(), b()), 
                                   t, 
                                   _DIV(_SUB(a(), b()), b, CONST0(SUCC(t()))))();

        public static Func<dynamic, dynamic, dynamic> DIV = (a, b) => _DIV(CONST0(a), CONST0(b), CONST0(ZERO))();

        public static Func<dynamic, dynamic, Func<dynamic>> _MOD =
            (n, m) => () => IF(LESS_THAN(n(), m()), 
                                n, 
                                _MOD(_SUB(n(), m()), m))();

        public static Func<dynamic, dynamic, dynamic> MOD = (n, m) => _MOD(CONST0(n), CONST0(m))();

        // ACTION provides a simple wrapper that both calls a function and returns the same function
        public static Func<dynamic, dynamic> ACTION = a => IF(TRUE, a, a());
        public static Func<dynamic, dynamic, Func<dynamic>> ACTION2 = (s, a) => () => IF(TRUE, a, a(s));

        // Now we can apply an action over a given range
        public static Func<dynamic, dynamic, dynamic, Func<dynamic>> _WITH_RANGE =
            (n, m, a) => () => IF(LESS_THAN(n(), m()), 
                                _WITH_RANGE(CONST0(SUCC(n())), m, ACTION(a)), 
                                CONST0(FALSE))();

        public static Func<dynamic, dynamic, dynamic, dynamic> WITH_RANGE = 
            (n, m, a) => _WITH_RANGE(CONST0(n), CONST0(m), a)();

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

        // Strings are similar to numbers but with the additional requirement of each function
        // needing to hold an ASCII value

        // We need a couple of functions to extract either the current character or the rest
        // of the string

        public static Func<dynamic, dynamic, dynamic> FIRST = (f, s) => f;
        public static Func<dynamic, dynamic, dynamic> SECOND = (f, s) => s;

        public static Func<dynamic, dynamic, dynamic, dynamic> END = (e, n, _) => e;
        public static Func<dynamic, Func<dynamic, dynamic, dynamic>> IS_END = n => (t, f) => n(t, t, CONST2(f));

        public static Func<dynamic, dynamic, Func<dynamic, dynamic, dynamic, dynamic>> CHR =
            (c, n) => (_1, _2, i) => i(c, n);

        public static Func<dynamic, dynamic, Func<dynamic>> _JOIN = (l, r) =>
                () => CHR(l()(_I, _I, FIRST), IF(IS_END(l()(_I, _I, SECOND)), 
                                                    r, 
                                                    _JOIN(CONST0(l()(_I, _I, SECOND)), r))());

        public static Func<dynamic, dynamic, dynamic> JOIN = (l, r) => _JOIN(CONST0(l), CONST0(r))();

        // A few constant strings
        public static Func<dynamic, dynamic, dynamic, dynamic> FIZZ_STR = CHR(F, CHR(I, CHR(Z, CHR(Z, END))));
        public static Func<dynamic, dynamic, dynamic, dynamic> BUZZ_STR = CHR(B, CHR(U, CHR(Z, CHR(Z, END))));
        public static Func<dynamic, dynamic, dynamic, dynamic> FIZZBUZZ_STR = JOIN(FIZZ_STR, BUZZ_STR);

        // And carriage control
        public static Func<dynamic, dynamic, dynamic, dynamic> CRLF = CHR(THIRTEEN, CHR(TEN, END));

        // Applying NULL_FUNCTION returns a NULL_FUNCTION
        public static Func<dynamic> NULL_FUNCTION = () => NULL_FUNCTION;

        // Print a string, this requires a function that prints a single ASCII character to work
        public static Func<dynamic, dynamic, Func<dynamic>> _PRINT =
            (s, a) => () => IF(IS_END(s()), 
                                NULL_FUNCTION, 
                                _PRINT(CONST0(s()(F, F, SECOND)), ACTION2(s(), a())))();

        public static Func<dynamic, dynamic, dynamic> PRINT = (s, a) => _PRINT(CONST0(s), CONST0(a))();

        // Convert numbers to strings
        public static Func<dynamic, Func<dynamic>> _NUMTOSTR =
            n =>
                () =>
                    IF(LESS_THAN(n(), TEN), 
                        CONST0(CHR(ADD(FORTY_EIGHT, n()), END)),
                        _JOIN(_NUMTOSTR(CONST0(DIV(n(), TEN))), _NUMTOSTR(CONST0(MOD(n(), TEN)))))();

        public static Func<dynamic, dynamic> NUMTOSTR = n => _NUMTOSTR(CONST0(n))();

        // A few helpers for the divisibility tests
        public static Func<dynamic, dynamic, dynamic> DIVISIBLE_BY = (a, b) => IS_ZERO(MOD(a, b));
        public static Func<dynamic, dynamic> DIVISIBLE_BY_THREE = (a) => IS_ZERO(MOD(a, THREE));
        public static Func<dynamic, dynamic> DIVISIBLE_BY_FIVE = (a) => IS_ZERO(MOD(a, FIVE));
        public static Func<dynamic, dynamic> DIVISIBLE_BY_FIFTEEN = (a) => IS_ZERO(MOD(a, FIFTEEN));

        // Produce the correct line based on divisibilty
        //
        // As 3 and 5 are coprime we can test for divisibility by 15 to check when a number is 
        // divisible by both.
        public static Func<dynamic, dynamic> LINE =
            n =>
                IF(DIVISIBLE_BY_FIFTEEN(n), 
                    FIZZBUZZ_STR,
                    IF(DIVISIBLE_BY_THREE(n), 
                        FIZZ_STR, 
                        IF(DIVISIBLE_BY_FIVE(n), 
                            BUZZ_STR, 
                            NUMTOSTR(n))));

        // Add carriage control to the line
        public static Func<dynamic, dynamic> LINE_WITH_CRLF = (n) => JOIN(LINE(n), CRLF);

        // Finally the completed program
        public static Func<dynamic, dynamic, dynamic, Func<dynamic>> _FIZZ_BUZZ =
            (s, e, o) => () => IF(LESS_THAN(s, e), 
                                    _FIZZ_BUZZ(SUCC(s), e, o), 
                                    CONST0(PRINT(LINE_WITH_CRLF(s), o)))();

        public static Func<dynamic, dynamic> FIZZ_BUZZ = o => _FIZZ_BUZZ(ONE, ONE_HUNDRED, o)();

        // Helper to extract integer from encoding
        public static int ToNumber(dynamic n)
        {
            return n(true, CONST1(false)) ? 0 : ToNumber(n(false, ID)) + 1;
        }

        public static void Main()
        {
            // Pass in a simple function that writes ASCII characters to the console
            Func<dynamic, dynamic> consoleWriter = c =>
            {
                Console.Write((char) ToNumber(c(false, false, FIRST)));
                return false;
            };

            FIZZ_BUZZ(consoleWriter);
        }
    }
}