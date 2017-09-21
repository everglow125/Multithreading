using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ConsoleApp1.Chart1
{
    /// <summary>
    /// 前台线程和后台线程
    /// </summary>
    public class Test7
    {
        public static void Excute()
        {
            var sampleForeground = new ThreadSample7(10);
            var sampleBackground = new ThreadSample7(20);
            var threadOne = new Thread(sampleForeground.CountNumbers);
            threadOne.Name = "ForegroundThread";
            var threadTwo = new Thread(sampleBackground.CountNumbers);
            threadTwo.Name = "BackgroundThread";
            threadTwo.IsBackground = true;
            threadOne.Start();
            threadTwo.Start();
            Console.ReadKey();
        }
    }

    public class ThreadSample7
    {
        private readonly int _iterations;
        public ThreadSample7(int iterations)
        {
            _iterations = iterations;
        }
        public void CountNumbers()
        {
            for (int i = 0; i < _iterations; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
            }
        }
    }
}
