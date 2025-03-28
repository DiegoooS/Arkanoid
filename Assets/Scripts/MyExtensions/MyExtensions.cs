using UnityEngine;
using System.Collections.Generic;

namespace Arkanoid
{
    public static class MyExtensions
    {
        public static List<T> ShuffleList<T>(List<T> listToShuffle)
        {
            for (int i = listToShuffle.Count - 1; i > 0; i--)
            {
                int rnd = Random.Range(0, i);
                T temp = listToShuffle[i];
                listToShuffle[i] = listToShuffle[rnd];
                listToShuffle[rnd] = temp;
            }

            return listToShuffle;
        }

        
    } 
}

