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
    {       //... write your code here 	} 
        Console.WriteLine("elephanto\r");
        Console.Write("\x1B[1D\x1B[1D\x1B[1D\x1B[1P\x1B[1P\x1B[1P\x1B[1P");
        //Console.SetOut(TextWriter.Null); //bad one(
    }
}