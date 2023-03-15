using System;
using System.Collections;
using System.Collections.Generic;


static class Extension
{
    public static void Shuffle<T>(this IList<T> list)
    {
        Random rand = new Random();
        int l = list.Count;

        for (int i = l - 1; i > 0; i--)
        {
            int rnd = rand.Next(i + 1);
            (list[rnd], list[i]) = (list[i], list[rnd]);
        }
    }
}