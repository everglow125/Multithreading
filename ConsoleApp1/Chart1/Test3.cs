using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ConsoleApp1.Chart1
{
    /// <summary>
    /// 线程等待
    /// </summary>
    public class Test3
    {
        public static void PrintNumbersWithDelay()
        {
            Console.WriteLine("Thread Starting...");
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
        }

        public static void Excute()
        {
            Console.WriteLine("Main Starting...");
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();
            t.Join();
            Console.WriteLine("Thread completed");
            Console.ReadKey();
        }
    }
}
