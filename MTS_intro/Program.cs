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
    {       //... write your code here 	} 


        //Environment.Exit(0);


        /*System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
        proc.Kill();*/


        //System.Environment.FailFast("Sweet release of death"); //-- exception party



    }
}