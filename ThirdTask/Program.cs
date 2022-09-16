using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ThirdTask
{
    static class Program
    {
        /// <summary> 
        /// <para> Отсчитать несколько элементов с конца </para> 
        /// <example> new[] {1,2,3,4}.EnumerateFromTail(2) = (1, ), (2, ), (3, 1), (4, 0)</example> 
        /// </summary>  
        /// <typeparam name="T"></typeparam> 
        /// <param name="enumerable"></param> 
        /// <param name="tailLength">Сколько элеметнов отсчитать с конца  (у последнего элемента tail = 0)</param> 
        /// <returns></returns> 
        public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
        {
            if (enumerable.TryGetNonEnumeratedCount(out int count))
            {
                var enumer = enumerable.GetEnumerator();
                return EnumerateFromTail(enumer, tailLength, count);
            }
            else
            {
                count = 0;
                var enumer = enumerable.GetEnumerator();
                while (enumer.MoveNext())
                {
                    count++;
                }
                enumer.Reset();
                return EnumerateFromTail(enumer, tailLength, count);
            }
            //}
            //else
            //{
            //    var a = enumerable.GetEnumerator();
            //    if (a.MoveNext())
            //    {
            //        var res = EnumerateFromTaill(a, tailLength);
            //        return res;
            //    }
            //    else
            //    {
            //        throw new Exception("Empty list");
            //    }
            //}
        }

        static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(IEnumerator<T> enumerator, int? tailLength, int countEl)
        {
            if (countEl != 0)
            {
                for (int i = 0; i < countEl - (tailLength != null ? tailLength : 0); i++)
                {
                    enumerator.MoveNext();
                    yield return (enumerator.Current, null);
                }
                while (tailLength != null && tailLength != 0)
                {
                    enumerator.MoveNext();
                    tailLength = tailLength != null && tailLength > 0 ? --tailLength : null;
                    yield return (enumerator.Current, tailLength);
                }
                yield break;
            }
            else
            {
                yield return (default(T), null);
            }
        }

        static void Main(string[] args)
        {
            int[] ints = { 5, 2, 4, 6, 3, 2, 6, 6, 6, 8, 2, 3, 4, 56, 7, 8 };
            List<int> a = new List<int>(ints);
            List<(int, int?)> kek = a.EnumerateFromTail(2).ToList();
            Console.WriteLine("Hello World!");
        }
    }
}


