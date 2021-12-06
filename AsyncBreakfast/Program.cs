using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBreakfast
{

  

    class Program
    {
        static void Main(string[] args)
        {


            var t = function2();

            Console.WriteLine("here");

            
           var s = t.Result;

           Console.WriteLine(s);


            Console.WriteLine("end");
            Console.ReadKey();
        }

       
        static async Task function1()
        {

            Console.WriteLine("startet function 1");

            await Task.Delay(TimeSpan.FromSeconds(1));


            Console.WriteLine("finished func 1");

        }


        static async Task<string> function2()
        {

            Console.WriteLine("startet function 2");

            await Task.Delay(TimeSpan.FromSeconds(1));


            return "cool";

        }





    }
}
