﻿using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public static T PickRandom<T>(this IEnumerable<T> source)=> source.PickRandom(1).Single();  
    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)=> source.Shuffle().Take(count);  
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)=> source.OrderBy(x => Guid.NewGuid());  
}
