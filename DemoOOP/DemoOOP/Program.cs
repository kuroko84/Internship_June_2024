using System.Xml.Linq;

namespace ZooManagement
{
    //Tính trừu tượng
    public abstract class Animal
    {
        //Tính đóng gói
        protected string name {  get; set; }
        protected int limbs { get; set; }
        protected int age { get; set; }

        protected Animal() { }

        protected Animal(string name, int limbs, int age)
        {
            this.name = name;
            this.limbs = limbs;
            this.age = age;
        }
        public abstract void MakeSound();
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Name: {name}, Limbs: {limbs}, Age: {age}");
        }
    }
    //Tính kế thừa
    public class Mammals : Animal
    {
        protected string foodType { get; set; }

        public Mammals() { }
        public Mammals(string name, int limbs, int age, string foodType) : base(name, limbs, age)
        {
            this.foodType = foodType;
        }
        public override void MakeSound()
        {
            Console.WriteLine($"Mamals sound like Gao Gao");
        }
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Food Type: {foodType}");
        }
    }
    public class Birds : Animal
    {
        protected double weight { get; set; }

        public Birds(string name, int limbs, int age, double weight) : base(name, limbs, age)
        {
            this.weight = weight;
        }
        //Tính đa hình
        public override void MakeSound()
        {
            Console.WriteLine($"Birds sound like Grec Grec Grec");
        }
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Weight: {weight}");
        }
    }
    public class Lion : Mammals
    {
        public Lion(string name, int limbs, int age, string foodType) : base(name, limbs, age, foodType) { }
        public override void MakeSound()
        {
            Console.WriteLine("GAOOOOO");
        }
    }    
    public class Tiger : Mammals
    {
        public Tiger(string name, int limbs, int age, string foodType) : base(name, limbs, age, foodType) { }
        public override void MakeSound()
        {
            Console.WriteLine("GAUUUUU");
        }
    }

    public class Zoo
    {
        public void ShowInfo(Animal anAnimal)
        {
            anAnimal.DisplayInfo();
            Console.WriteLine();
        }
    }
    public class DemoOOP
    {
        public static void Main(string[] args)
        {
            Mammals aMammal = new("Lion", 4, 7, "Meat");
            aMammal.DisplayInfo();
            aMammal.MakeSound();

            Console.WriteLine();

            Birds aBird = new("Ostrich", 2, 5, 40);
            aBird.DisplayInfo();
            aBird.MakeSound();

            Console.WriteLine();

            Lion lion = new("Lion", 4, 10, "Meat");
            lion.DisplayInfo();
            lion.MakeSound();

            Console.WriteLine();

            Tiger tiger = new("Tiger", 4, 9, "Meat");
            tiger.DisplayInfo(); 
            tiger.MakeSound();

            Console.WriteLine();

            Zoo zoo = new Zoo();
            zoo.ShowInfo(tiger);
            zoo.ShowInfo(lion);
        }
    }
}