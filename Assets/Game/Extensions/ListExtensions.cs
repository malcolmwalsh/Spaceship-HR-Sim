using System;
using System.Collections.Generic;

namespace Assets.Game.Extensions
{
    public static class ListExtensions
    {
        private static readonly Random _rng = new();

        public static T RandomElement<T>(this IList<T> list)
        {
            return list[_rng.Next(list.Count)];
        }

        public static T RemoveAndGet<T>(this IList<T> list, int index)
        {
            T value = list[index];
            list.RemoveAt(index);
            
            return value;
        }
    }
}
