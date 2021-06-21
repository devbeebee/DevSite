using System;

static class IntExtensions
{
    public static bool IsEven(this int num) => num % 2 == 0;
    public static bool IsOdd(this int num) => !num.IsEven();
}