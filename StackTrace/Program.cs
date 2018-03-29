using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            await Level1();
        } catch (Exception ex) 
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    static async Task Level1() {
        await Level2();
    }

    static async Task Level2() {
        await Level3();
    }

    static async Task Level3() {
        await Level4();
    }

    static async Task Level4() {
        await Level5();
    }    

    static async Task Level5() {
        await Level6();
    }       

    static async Task Level6() {
        await Task.Yield();
        throw new Exception("boom");
    }
}