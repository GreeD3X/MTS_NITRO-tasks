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
        IEnumerable<int> Sort(IEnumerable<int> inputStream, int sortFactor, int maxValue) 
        {

        }




        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");
        }
    }
}