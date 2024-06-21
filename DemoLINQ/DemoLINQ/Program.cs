using System;
using System.Linq;
using System.Collections.Generic;

public class LinqDemo
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public static void Main()
    {
        List<Student> students = new List<Student>
        {
            new Student { Name = "Anna", Age = 18 },
            new Student { Name = "Jane", Age = 20 },
            new Student { Name = "Jake", Age = 22 },
            new Student { Name = "Jessie", Age = 19 },
            new Student { Name = "Bill", Age = 20 }
        };

        //lọc các học sinh lớn hơn 20
        var studentsAgeGreaterThan20 = from student in students
                                       where student.Age > 20
                                       select student;

        foreach (var student in studentsAgeGreaterThan20)
        {
            Console.WriteLine($"{student.Name} is greater than 20.");
        }

        ////Dùng hàm select
        //Tham chiếu, các lệnh khác có ảnh hưởng.
        //var othersStudent = students.Where(student => student.Age <= 20).Select(student => { student.Age += 10; return student; });
        //foreach (var student in othersStudent)
        //{
        //    Console.WriteLine($"{student.Name} after ten years is {student.Age} years old");
        //}

        //Dùng hàm OrderBy
        var sortedStudents = students.OrderBy(student => student.Name);
        foreach(var student in sortedStudents)
        {
            Console.WriteLine($"{student.Name} is {student.Age}");
        }

        //Dùng hàm SingleOrDefault
        var JessieStudent = students.SingleOrDefault(student => student.Name == "Jessie");
        Console.WriteLine(JessieStudent.Name + JessieStudent.Age);

        //Dùng hàm Take
        var first2Student = students.Take(2);
        foreach (var student in first2Student)
        {
            Console.WriteLine($"Take {student.Name}");
        }

        //Dùng hàm TakeWhile student whike age 
        var StudentWhileAgeEqualOrGreaterTan20 = students.TakeWhile(student => student.Age >= 20);
        foreach (var student in StudentWhileAgeEqualOrGreaterTan20)
        {
            Console.WriteLine($"Take {student.Name} whith {student.Age} years old");
        }

        //Dùng hàm select
        var othersStudent = students.Where(student => student.Age <= 20).Select(student => { student.Age += 10; return student; });
        foreach (var student in othersStudent)
        {
            Console.WriteLine($"{student.Name} after ten years is {student.Age} years old");
        }
    }
}
