using System;
using System.Collections.Generic;
using System.Text;

using System.Numerics;

namespace Проверка_чисел_на_простоту
{
    delegate bool PrimalityTest(BigInteger n, BigInteger a);

    static class PrimalityTests
    {
        public static bool IsProbablyPrimeFermatTest(BigInteger n, BigInteger a)
        {
            if (n.IsEven)
                return false;

            if (n < 5)
            {
                if (n == 4)
                    return false;
                else
                    return true;
            }

            if (!(a >= 2 && a <= n - 2))
                throw new ArgumentOutOfRangeException("a", a, "Значение a должно находится в пределах 2 <= a <= n-2");

            BigInteger r = BigInteger.ModPow(a, n - 1, n);

            if (r == 1)
                return true;
            else
                return false;
        }

        public static bool IsProbablyPrimeSolovayStrassenTest(BigInteger n, BigInteger a)
        {
            if (n.IsEven)
                return false;

            if (n < 5)
            {
                if (n == 4)
                    return false;
                else
                    return true;
            }

            if (!(a >= 2 && a <= n - 2))
                throw new ArgumentOutOfRangeException("a", a, "Значение a должно находится в пределах 2 <= a <= n-2");

            BigInteger r = BigInteger.ModPow(a, (n - 1) / 2, n);

            if (r != 1 && r != n - 1)
                return false;

            int s = Jacobi(a, n);

            if ((r == s) || (r - n == s))
                return true;
            else
                return false;
        }

        public static bool IsProbablyPrimeRabinMillerTest(BigInteger n, BigInteger a)
        {
            if (n.IsEven)
                return false;

            if (n < 5)
            {
                if (n == 4)
                    return false;
                else
                    return true;
            }

            if (!(a >= 2 && a <= n - 2))
                throw new ArgumentOutOfRangeException("a", a, "Значение a должно находится в пределах 2 <= a <= n-2");

            BigInteger r = n - 1, s = 0;

            while (r % 2 == 0)
            {
                r /= 2;
                s++;
            }

            BigInteger y = BigInteger.ModPow(a, r, n);

            if (y != 1 && y != n - 1)
            {
                for (BigInteger j = 1; j <= s - 1 && y != n - 1; j++)
                {
                    y = BigInteger.ModPow(y, 2, n);
                    if (y == 1)
                        return false;
                }

                if (y != n - 1)
                    return false;
            }

            return true;
        }

        private static int Jacobi(BigInteger a, BigInteger n)
        {
            if (n % 2 == 0)
                throw new ArgumentOutOfRangeException("n", n, "Значение n должно быть нечётным");

            if (n < 3)
                throw new ArgumentOutOfRangeException("n", n, "Значение n должно быть больше, либо равно 3");

            if (!(0 <= a && a < n))
                throw new ArgumentOutOfRangeException("a", a, "Значение a должно находится в пределах 0 <= a < n");

            if (a == 0)
                return 0;

            if (a == 1)
                return 1;

            int s = 1;
            BigInteger k = 0;

            while (a % 2 == 0)
            {
                a /= 2;
                k++;
            }

            if (k % 2 != 0)
            {
                if (n % 8 == 3 || n % 8 == 5)
                    s *= -1;
            }

            if (a % 4 == 3 && n % 4 == 3)
                s *= -1;

            if (a == 1)
                return s;
            else
                return s * Jacobi(n % a, a);
        }
    }
}
