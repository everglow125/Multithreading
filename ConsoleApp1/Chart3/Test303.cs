using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace ConsoleApp1.Chart3
{
    /// <summary>
    /// 线程池与并行度
    /// </summary>
    public class Test303
    {
        static void UseThreads(int numberOfOperations)
        {
            using (var countdown = new CountdownEvent(numberOfOperations))
            {
                Console.WriteLine("Scheduling work by creating threads");
                for (int i = 0; i < numberOfOperations; i++)
                {
                    var thread = new Thread(() =>
                    {
                        Console.WriteLine("{0}", Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                        countdown.Signal();
                    });
                    thread.Start();
                }
                countdown.Wait();
                Console.WriteLine();
            }
        }
        static void UseThreadPool(int numberofOperations)
        {
            using (var countdown = new CountdownEvent(numberofOperations))
            {
                Console.WriteLine("Starting work on a threadpool");
                for (int i = 0; i < numberofOperations; i++)
                {
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        Console.Write("{0}", Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                        countdown.Signal();
                    });
                }
                countdown.Wait();
                Console.WriteLine();
            }
        }
        public static void Excute()
        {
            const int numberofOperations = 500;
            var sw = new Stopwatch();
            sw.Start();
            UseThreads(numberofOperations);
            sw.Stop();
            Console.WriteLine("Execution time using threads:{0}", sw.ElapsedMilliseconds);

            sw.Reset();
            sw.Start();
            UseThreadPool(numberofOperations);
            sw.Stop();
            Console.WriteLine("Execution time using threads:{0}", sw.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
