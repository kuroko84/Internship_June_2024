using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void PrintNumbers()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(i);
            Thread.Sleep(200);
        }
    }

    static void TaskPrintNumber(int start)
    {
        for (int i = start; i < start + 10; i++)
        {
            Console.WriteLine(i + " Task");
            Thread.Sleep(200);
        }
    }

    static async void Tasks(){
        Task task1 = Task.Run(() => TaskPrintNumber(0));
        Task task2 = Task.Run(() => TaskPrintNumber(10));
        Task task3 = Task.Run(() => TaskPrintNumber(20));

        await Task.WhenAll(task1, task2, task3);
        Console.WriteLine("Completed");
    }

    static void Main()
    {
        //Tuần tự
        PrintNumbers();
        
        //Bất đồng bộ
        Thread thread = new Thread(PrintNumbers);

        thread.Start();

        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(i + " Main");
            Thread.Sleep(200);
        }
        Tasks();
        Console.ReadLine();
    }
}