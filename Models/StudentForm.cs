using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace StudentDashboard.Models
{
    public class StudentForm
    {
        public int Id { get; set; }
        [Required, StringLength(50), Display(Name = "First name")]
        public string FirstName { get; set; } = string.Empty;
        [Required, StringLength(50), Display(Name = "Last name")]
        public string LastName { get; set; } = string.Empty;
        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(30), Display(Name = "Enrollment number")]
        public string EnrollmentNumber { get; set; } = string.Empty;
        [Required, DataType(DataType.Date), Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }
        [Required, StringLength(100)]
        public string Course { get; set; } = string.Empty;
    }
}

//Absract class and Interface

public abstract class Animal
{
    public void Run()
    {
        Console.WriteLine("Animal is rUNNING");
    }
    public abstract void Sleep();
}

public class Cat : Animal
{
    public override void Sleep()
    {
        Console.WriteLine("Dogg is Sleeping");
    }

}

//Interface
public interface IRunnable
{
    void Run();
}

public class Elephant: IRunnable
{
    public void Run()
    {
        Console.WriteLine("Elephant isRunning");
    }
}

//Polymorphism

public class Trees
{
    public virtual void UsefulTrees()
    {
        Console.WriteLine("Tree Gives Oxygen");
    }
}

public class NeemTree: Trees
{
    public override void UsefulTrees()
    {
        Console.WriteLine("NeemTree Gives Medical Benefits");
    }
}

public class LemonTree: Trees
{
    public override void UsefulTrees()
    {
        Console.WriteLine("Lemon tree Gives Lemons");
    }
}
//Method OverLoading
public class Calculation
{
    public int Add(int a,int b,int c)
    {
        return a + b + c;
    }

    public int Add(int a, int b)
    {
        return a + b;
    }
}

//Metod Overidding

public class Birds
{
    public virtual void Fly()
    {
        Console.WriteLine("Birds are Flying");
    }
}

public class Ostrich : Birds
{
    public  override void Fly()
    {
        Console.WriteLine("Ostrich is not Flying");
    }
}

//Tuple

public class TupleExample
{
   public static void main()
    {
        var student = (id: 1, name: "Raj", Percent: 72.4);
        Console.WriteLine(student.id);
        Console.WriteLine(student.name);
        Console.WriteLine(student.Percent);
        var newStudent = GetStudents();
        Console.WriteLine(newStudent.id);
        Console.WriteLine(newStudent.name);
    }

    public static( int id,string name) GetStudents()
    {
        return (2, "Raj");
    }
}

public class Circle
{
    public double Radius { get; set; }
}

public class Square
{
    public double Side { get; set; }
}

class PatternMatching
{
    static void Main()
    {
        object shape = new Circle { Radius = 5 };

        double area;

        switch (shape)
        {
            case Circle c:
                area = Math.PI * c.Radius * c.Radius;
                break;

            case Square s:
                area = s.Side * s.Side;
                break;

            default:
                area = 0;
                break;
        }

        Console.WriteLine($"Area: {area}");

        var age = 6;

        string result = age switch
        {
            < 13 => "Child",
            >= 13 and < 20 => "Teenager",
            >= 20 => "Adult"
        };
        Console.WriteLine(result);
    }

}