using System;
using System.Collections.Generic;

namespace ListTypeExtensions {
    public static class ListTypeExtensions {
        public static void Shuffle<T>(this IList<T> source, Random randomGenerator) {
            int n = source.Count;
            while (n > 1) {
                n--;
                int k = randomGenerator.Next(n + 1);
                T value = source[k];
                source[k] = source[n];  
                source[n] = value;
            } 
        }
    }
}
