using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1.Chart1
{
    /// <summary>
    /// 终止线程
    /// </summary>
    public class Test4
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
        public static void PrintNumbers()
        {
            Console.WriteLine("Starting...");
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }
        public static void Excute()
        {
            Console.WriteLine("Main Starting...");
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();
            Thread.Sleep(TimeSpan.FromSeconds(6));
            t.Abort();
            Console.WriteLine("A Thread has been aborted");
            t = new Thread(PrintNumbers);
            t.Start();
            PrintNumbers();
            Console.ReadKey();
        }
    }
}
