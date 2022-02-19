using System;
using System.Collections.Generic;
#if UNITY_5_3_OR_NEWER
using Unity.Collections.LowLevel.Unsafe;
#endif

namespace EnumBitSet
{
    public static class Commons
    {
        public static IEnumerable<int> EnumerateSetBits(int mask)
        {
            var i = 0;
            while (mask != 0 && i < sizeof(int) * 8)
            {
                if ((mask & 1) != 0)
                {
                    yield return i;
                }

                i++;
                mask >>= 1;
            }
        }

        public static IEnumerable<int> EnumerateSetBits(long mask)
        {
            var i = 0;
            while (mask != 0 && i < sizeof(long) * 8)
            {
                if ((mask & 1) != 0)
                {
                    yield return i;
                }

                i++;
                mask >>= 1;
            }
        }

        public static int CountSetBits(int mask)
        {
#if NETCOREAPP3_0_OR_GREATER || NET5_0_OR_GREATER
            return BitOperations.PopCount(mask);
#else
            var count = 0;
            foreach (var _ in EnumerateSetBits(mask))
            {
                count++;
            }
            return count;
#endif
        }
        
        public static int CountSetBits(long mask)
        {
#if NETCOREAPP3_0_OR_GREATER || NET5_0_OR_GREATER
            return BitOperations.PopCount(mask);
#else
            var count = 0;
            foreach (var _ in EnumerateSetBits(mask))
            {
                count++;
            }
            return count;
#endif
        }

        public static int EnumToInt<T>(T value) where T : struct, Enum
        {
#if UNITY_5_3_OR_NEWER
            return UnsafeUtility.EnumToInt(value);
#elif NETCOREAPP3_0_OR_GREATER || NET5_0_OR_GREATER
            return Unsafe.As<T, int>(ref value);
#else
            return Convert.ToInt32(value);
#endif
        }

        public static T IntToEnum<T>(int value) where T : Enum
        {
#if UNITY_5_3_OR_NEWER
            return UnsafeUtility.As<int, T>(ref value);
#elif NETCOREAPP3_0_OR_GREATER || NET5_0_OR_GREATER
            return Unsafe.As<int, T>(ref value);
#else
            return (T) Enum.ToObject(typeof(T), value);
#endif
        }
    }
}