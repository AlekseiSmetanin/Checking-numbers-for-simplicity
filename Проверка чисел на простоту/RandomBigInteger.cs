using System;
using System.Collections.Generic;
using System.Text;

using System.Numerics;

namespace Проверка_чисел_на_простоту
{
    class RandomBigInteger
    {
        //Во всех методах класса:
        //minValue - включённый предел
        //maxValue - исключённый предел

        private Random random;

        public RandomBigInteger()
        {
            random = new Random();
        }

        public BigInteger Next(BigInteger maxValue)
        {
            if (maxValue == 0)
                return 0;

            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("maxValue", maxValue, "Значение параметра maxValue меньше 0");

            byte[] bytes = maxValue.ToByteArray();

            random.NextBytes(bytes);
            bytes[bytes.Length - 1] &= 127;
            return new BigInteger(bytes) % maxValue;
        }

        public BigInteger Next(BigInteger minValue, BigInteger maxValue)
        {
            if (minValue == maxValue)
                return minValue;

            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue", minValue, "Значение minValue больше значения maxValue");

            return minValue + Next(maxValue - minValue);
        }
    }
}
