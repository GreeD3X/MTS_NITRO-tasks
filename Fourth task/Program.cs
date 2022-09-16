using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ThirdTask
{
    static class Program
    {
        /// <summary> 
        /// Возвращает отсортированный по возрастанию поток чисел 
        /// </summary> 
        /// <param name="inputStream">Поток чисел от 0 до maxValue. Длина потока не превышает миллиарда чисел.</param> 
        /// <param name="sortFactor">Фактор упорядоченности потока. Неотрицательное число. Если в потоке встретилось число x, то в нём больше не встретятся числа меньше, чем (x - sortFactor).</param> 
        /// <param name="maxValue">Максимально возможное значение чисел в потоке. Неотрицательное число, не превышающее 2000.</param> 
        /// <returns>Отсортированный по возрастанию поток чисел.</returns> 
        static IEnumerable<int> Sort(IEnumerable<int> inputStream, int sortFactor, int maxValue)
        {
            int curMin = int.MaxValue;
            int minAfter = int.MaxValue;
            IEnumerator<int> enumerator = inputStream.GetEnumerator();
            enumerator.MoveNext();
            minAfter = Math.Max(0, enumerator.Current - sortFactor);
            curMin = enumerator.Current;
            int curVal = enumerator.Current;
            LinkedList<int> sorted = new LinkedList<int>();
            while (true)
            {
                if (curVal < curMin)
                {
                    if (curVal < minAfter)
                    {
                        yield return curVal;
                    }
                    else
                    {
                        curMin = curVal;
                        sorted.AddFirst(curVal);
                    }

                }
                else
                {
                    if(curVal>=sorted.Last())
                        sorted.AddLast(curVal);
                    else
                        sorted.Insert(curVal);
                }
                while (sorted.Count() > 0 && curMin <= minAfter)
                {
                    yield return sorted.First();
                    sorted.RemoveFirst();
                    if (sorted.Count() > 0)
                        curMin = sorted.First();
                }

                if (!enumerator.MoveNext())
                    break;
                else
                {
                    curVal = enumerator.Current;
                    minAfter = Math.Max(curVal - sortFactor, minAfter);
                    curMin = Math.Min(curVal, curMin);
                }
            }
            while(sorted.Count() > 0)
            {
                yield return sorted.First();
                sorted.RemoveFirst();
            }
            yield break;
        }

        static void Insert(this LinkedList<int> list, int val)
        {
            if (list.Count() == 0)
            {
                list.AddFirst(val);
                return;
            }
            LinkedListNode<int> cur = list.First;
            while (cur.Value < val)
            {
                cur = cur.Next;
                if (cur == null)
                    break;
            }
            if (cur == null)
            {
                list.AddLast(val);
                return;
            }
            list.AddBefore(cur, val);
            return;
        }

        //static int BinSeach(LinkedList<int> list, int val, int l, int r)
        //{
        //    int mid = l + (r - l) / 2;
        //    if(l == r || list.)
        //}//может еще и [i] элемент в списке возьмешь()))()


        static void Main(string[] args)
        {
            int[] ints = { 5, 2, 4, 6, 3, 2, 6, 6, 6, 8, 4, 5, 4, 56, 57, 58 };
            List<int> a = new List<int>(ints);
            var kek = Sort(a, 4, 60);
            int count = 0;
            foreach (int i in kek)
            {
                Console.Write(i.ToString() + ' ');
                count++;
                if (count >= 5)
                    break;
            }
            Console.Write('\n');
        }
    }
}