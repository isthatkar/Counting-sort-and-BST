using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritmai_LD1
{
    class Array
    {
        public static void CountingSortArray(int[] Initial, int[] Result, int k)
        {
            int[] A = new int[k + 1];                

            for (int i = 0; i < Initial.Length; i++)
            {
                A[Initial[i]]++;
            }

            for (int i = 1; i <= k; i++)
            {
                A[i] += A[i - 1];
            }

            for (int i = Initial.Length - 1; i >= 0; i--)
            {
                Result[A[Initial[i]] - 1] = Initial[i];
                A[Initial[i]]--;
            }
        }
        public static void PrintArray(int[] Array, string Header)
        {
            Console.WriteLine(Header);
            for (int i = 0; i < Array.Length; i++)
                Console.Write(Array[i] + " ");
            Console.WriteLine();
        }
    }
}
