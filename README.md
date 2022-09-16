# MTS_NITRO-tasks


## Table of Contents
1. [Первое задание](#first)
2. [Второе задание](#second)
3. [Третье задание](#third)
4. [Четвертое задание](#fourth)
5. [Пятое задание](#fifth)

## Первое задание<a name="first"></a>
Реализуйте метод FailProcess так, чтобы процесс завершался. Предложите побольше различных решений. 

```c#
using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            FailProcess();
        }
        catch { }

        Console.WriteLine("Failed to fail process!");
        Console.ReadKey();
    }

    static void FailProcess()
    {
        //... write your code here 	} 
    }
}
```
 Возможными способами решения будет завершить программу с помощью:
 ```c#
    Environment.Exit(0);
 ```
или
```c#
    System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
    proc.Kill();
```
Кроме того можно убить программу с исключением, которое не поймать в ```Main()```:
```c#
    System.Environment.FailFast("Sweet release of death"); //-- exception party
```

## Второе задание<a name="second"></a>
Что выводится на экран? Измените класс Number так, чтобы на экран выводился результат сложения для любых значений ```someValue1``` и ```someValue2```. 
```c#
using System; 
using System.Globalization; 

class Program 
{ 
    static readonly IFormatProvider _ifp = CultureInfo.InvariantCulture; 

    class Number 
    { 
        readonly int _number; 

        public Number(int number) 
        { 
            _number = number; 
        } 

        public override string ToString() 
        { 
            return _number.ToString(_ifp); 
        } 
    } 

    static void Main(string[] args) 
    { 
    int someValue1 = 10; 
    int someValue2 = 5; 

    string result = new Number(someValue1) + someValue2.ToString(_ifp); 
    Console.WriteLine(result); 
    Console.ReadKey(); 
    } 
} 
```
В изначальной постановке происходит неявное приведение ```Number``` к ```string``` и после этого конкатенация двух строк.
в качестве решение можно добавить оператор сложения для Number и оператор приведения ```Number``` к ```string```.
```c#
        public Number(string s)
        {
            _number = Int32.Parse(s);
        }

        public static string operator +(Number a, Number b) => new Number(a._number + b._number);

        public static string operator +(Number a, string b) => a + new Number(b);

        public static implicit operator string(Number a) => a.ToString();
```
При это операторы сложения могут возвращать и ```Number```, просто приведение к ```string``` будет происходить позже.
## Третье задание<a name="third"></a>
Реализуйте метод по следующей сигнатуре: 

 ```C#
/// <summary> 
/// <para> Отсчитать несколько элементов с конца </para> 
/// <example> new[] {1,2,3,4}.EnumerateFromTail(2) = (1, ), (2, ), (3, 1), (4, 0)</example> 
/// </summary>  
/// <typeparam name="T"></typeparam> 
/// <param name="enumerable"></param> 
/// <param name="tailLength">Сколько элеметнов отсчитать с конца  (у последнего элемента tail = 0)</param> 
/// <returns></returns> 
public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength) 
 ```
К сожалению получить результат за один проход возможно только если нам заранее известно количество элементов в ```enumerable```.
```C#
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
```
Здесь будет совершен один проход если мы можем получить заранее количество элементов в ```enumerable``` и два в обратном случае.
В качестве вариантов реализации были рассмотрены: метод двух итераторов (первый пропускает ```tailLength``` элементов и пока он может двигаться заполняем ```(enumerator.Current, null)```, а после этого заполняем расстоянием до хвоста), рекурсивный спуск до хвоста с последующим развотом с помощью ```return recursive(...).Append(...);```.
Рекурсивные метод действительно пройдется всего один раз, однако использование ```Append``` может быть очень затратной операцией, так как та каждый раз создает копию последовательности.

## Четвертое задание<a name="fourth"></a>
Реализуйте метод ```Sort```. Известно, что потребители метода зачастую не будут вычитывать данные до конца. Оптимально ли Ваше решение с точки зрения скорости выполнения? С точки зрения потребляемой памяти? 
```C#
/// <summary> 
/// Возвращает отсортированный по возрастанию поток чисел 
/// </summary> 
/// <param name="inputStream">Поток чисел от 0 до maxValue. Длина потока не превышает миллиарда чисел.</param> 
/// <param name="sortFactor">Фактор упорядоченности потока. Неотрицательное число. Если в потоке встретилось число x, то в нём больше не встретятся числа меньше, чем (x - sortFactor).</param> 
/// <param name="maxValue">Максимально возможное значение чисел в потоке. Неотрицательное число, не превышающее 2000.</param> 
/// <returns>Отсортированный по возрастанию поток чисел.</returns> 
IEnumerable<int> Sort(IEnumerable<int> inputStream, int sortFactor, int maxValue) 
```

Предложенный способ решения создеает ```LinkedList``` в котором хранит отсортированные значения, которые еще не готовы к показу пользователю. Как только мы достигаем такого элемента, что после него не будет чисел меньших, чем уже сохраненные, мы начинаем возвращать элементы пользователю. Потребление памяти, в худшем случае, будет достигать размеров изначального потока, если последовательность будет неубывающей. Затраты времени на вставку будут линейными.

```c#
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
```
LinkedList используется ради простоты вставки и удаления элементов.

## Пятое задание<a name="fifth"></a>
Программа выводит на экран строку «Муха», а затем продолжает выполнять остальной код. Реализуйте метод TransformToElephant так, чтобы программа выводила на экран строку «Слон», а затем продолжала выполнять остальной код, не выводя перед этим на экран строку «Муха». 

```c#
using System; 

class Program 
{ 
    static void Main(string[] args) 
    { 
        TransformToElephant(); 
        Console.WriteLine("Муха");			 
        //... custom application code 
    } 
 
static void TransformToElephant() 	
    {		//... write your code here 	} 

    } 

}
```
Одним из спобов решения будет перенаправить поток вывода:
```c#
        Console.WriteLine("elephanto");
        Console.SetOut(TextWriter.Null); //bad one(
```
Очевидной проблемой будет необходимость восстановить стандартный поток назад, что не сложно, но легко забыть сделать.
К сожалению другого решения не нашлось.
