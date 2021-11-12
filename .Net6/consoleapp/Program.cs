// See https://aka.ms/new-console-template for more information
using consoleapp;

Console.WriteLine("Hello, World!");
Print();

var item = new Price();
item.Print();

partial class Program
{
    public static void Print()
    {
            System.Console.WriteLine("this is form program class.");
    }
}