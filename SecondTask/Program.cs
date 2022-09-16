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

        public Number(string s)
        {
            _number = Int32.Parse(s);
        }

        public static Number operator +(Number a, Number b) => new Number(a._number + b._number);

        public static Number operator +(Number a, string b) => a + new Number(b);

        public static implicit operator string(Number a) => a.ToString();
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