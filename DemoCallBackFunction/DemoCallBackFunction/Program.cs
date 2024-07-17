using System;

internal class Program
{
    delegate int Operation(int x, int y);
    

    private static void Main(string[] args)
    {
        Action<int, int> prinInfo;
        int a, b;
        a = GetValidNumber("Enter number A: ");
        b = GetValidNumber("Enter number B: ");

        Action<int, int> infoAction = (a, b) => Console.WriteLine($"Hai so A va B lan luot la {a} va {b}.");
        Action<int, int> sumAndPrint = (a, b) => Console.WriteLine($"Tong hai so {a} va {b} la {a + b}");

        prinInfo = infoAction;

        prinInfo += sumAndPrint;

        prinInfo(a, b);

        Predicate<int> isEven = number => number % 2 == 0;

        if (isEven(a + b))
        {
            Console.WriteLine($"{a} + {b} la so chan");
        }
        else
        {
            Console.WriteLine($"{a} + {b} la so le");
        }

        Func<int, int, int> add = (x, y) => x + y;
        Func<int, int, int> subtract = (x, y) => x - y;
        Func<int, int, int> multiply = (x, y) => x * y;
        Func<int, int, int> divide = (x, y) => y != 0 ? x / y : throw new DivideByZeroException("Không thể chia cho 0");

        // Thực hiện các phép tính và in kết quả
        Console.WriteLine("Phép cộng: " + add(a, b));
        Console.WriteLine("Phép trừ: " + subtract(a, b));
        Console.WriteLine("Phép nhân: " + multiply(a, b));
        try
        {
            Console.WriteLine("Phép chia: " + divide(a, b));
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Demo callback function
        PerformOperation(a, b, add, result => Console.WriteLine("Kết quả phép cộng (callback): " + result));
        PerformOperation(a, b, subtract, result => Console.WriteLine("Kết quả phép trừ (callback): " + result));
    }

    private static int GetValidNumber(string prompt)
    {
        int number = 0;
        string input;

        Predicate<string> isValid = num => int.TryParse(num, out number);

        Console.WriteLine(prompt);
        do
        {
            input = Console.ReadLine();
            if (!isValid(input))
            {
                Console.Write("Vui lòng nhập một số hợp lệ: ");
            }
        } while (!isValid(input));

        return number;
    }

    // Hàm thực hiện phép toán và gọi hàm callback với kết quả
    private static void PerformOperation(int x, int y, Func<int, int, int> operation, Action<int> callback)
    {
        int result = operation(x, y);
        callback(result);
    }
}
