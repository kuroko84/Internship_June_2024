// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

using System;

public class Animal
{
    public string name { get; set; }
    public int limbs { get; set; }
    public string sound { get; set; }

    public Animal(string aName, int aLimbs, string aSound)
    {
        this.name = aName;
        this.limbs = aLimbs;
        this.sound = aSound;
    }
    public Animal() { }
    public virtual void Bawl()
    {
        Console.WriteLine($"Their sounds like {this.sound}");
    }
    public void PrintAnimal()
    {
        Console.WriteLine($"This animal is called {this.name}");
        Console.WriteLine($"They have {this.limbs} limbs");
        this.Bawl();
    }
}

public class Mammals : Animal
{
    public string foodType { get; set; }
    public Mammals(string aName, int aLimbs, string aSound, string aFoodType)
    {
        this.name = aName;
        this.limbs = aLimbs;
        this.sound = aSound;
        this.foodType = aFoodType;
    }

    public override void Bawl()
    {
        Console.WriteLine($"{this.name} sound like {this.sound}");
    }
}

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Animal Lion = new ("Lion", 4, "Gru Gru");
        Lion.PrintAnimal();
        Mammals Tiger = new("Tiger", 4, "Gao Gao", "Meat");
        Tiger.PrintAnimal();
    }
}