using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace StudentDashboard.Controllers;
[Authorize]
public class CoursesController : Controller { public IActionResult Index() => View(); 
    public void ExampleRef()
    {
        var x = 4;
        var y  = Calculation(ref x);
        Console.WriteLine(y);
    }

    public int Calculation(ref int num )
    {
        num = num * 2;
            return num;
    }

    public void ExamplleOut()
    {
        
        var y = OutCalculation(out int x);
        Console.WriteLine(y);
    }

    public int OutCalculation(out int num)
    {
        num = 5;
        return num;
    }

    static Func<int> CreateCounter()
    {
        int count = 0;

        return () =>
        {
            count++;
            return count;
        };
    }

    static void Main()
    {
        var counter = CreateCounter();

        Console.WriteLine(counter()); // 1
        Console.WriteLine(counter()); // 2
        Console.WriteLine(counter()); // 3
    }
}
