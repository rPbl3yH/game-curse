using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Modules.Extensions
{
    public static class ListExtensions
    {
        public static T GetRandom<T>(this List<T> list)
        {
            var randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }

    public static class ArrayExtensions
    {
        public static T GetRandom<T>(this T[] arrays)
        {
            var randomIndex = Random.Range(0, arrays.Length);
            return arrays[randomIndex];
        }
    }
}