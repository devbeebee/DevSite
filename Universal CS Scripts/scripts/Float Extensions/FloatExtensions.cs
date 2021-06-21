using System;
using System.Collections.Generic;
using System.IO;

static class FloatExtensions
{
   public static float Percentage(this float current ,float maximum) => (current / maximum) * 100;
}