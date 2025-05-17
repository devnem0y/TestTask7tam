using System;
using System.Collections.Generic;

public static class MyTools
{
    public static void Shuffle<T>(IList<T> list)
    {
        var rnd = new Random();

        for (var i = list.Count - 1; i >= 1; i--)
        {
            var j = rnd.Next(i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }
}