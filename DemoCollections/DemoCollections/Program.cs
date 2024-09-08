using System;
using System.Collections;
using System.Collections.Generic;

class Program
{
    public static async Task FuncArrayList()
    {
        ArrayList arrList = new ArrayList();

        arrList.Add(1);
        arrList.Add(2);
        arrList.Add(3);
        foreach (var item in arrList)
        {
            Console.WriteLine(item + " Arr");
            await Task.Delay(1000);
        }
        Console.WriteLine();
    }
    public static async void FuncStack()
    {
        Stack stack = new Stack();

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        while (stack.Count > 0)
        {
            Console.WriteLine(stack.Pop() + " Stack");
            await Task.Delay(1000);
        }

        Console.WriteLine();
    }
    public static void FuncQueue() {
        Queue queue = new Queue();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);  

        while(queue.Count > 0)
        {
            Console.WriteLine(queue.Dequeue());
        }

        Console.WriteLine();
    }
    public static void FuncHashtable()
    {
        Hashtable hash = new Hashtable();
        hash.Add(1, "One");
        hash.Add(2, "Two");
        hash[3] = "Three";

        foreach (DictionaryEntry entry in hash)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
        Console.WriteLine();
    }
    public static void FuncSortedList()
    {
        SortedList list = new SortedList();

        list.Add(1, 1);
        list.Add(2, 2);
        list.Add(3, 3);
        foreach (DictionaryEntry entry in list)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }

        Console.WriteLine();
    }
    public static async Task Main()
    {
        //ArrayList
        await FuncArrayList();
        //Stack
        FuncStack();
        //Queue
        FuncQueue();
        //HashTable
        FuncHashtable();
        //SortedList
        FuncSortedList();
    }
}