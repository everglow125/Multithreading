using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace ConsoleApp1.Chart1
{
    /// <summary>
    /// 线程优先级
    /// </summary>
    public class Test6
    {
        static void RunThreads()
        {
            var sample = new ThreadSample();
            var threadOne = new Thread(sample.CountNumbers);
            threadOne.Name = "ThreadOne";
            var threadTow = new Thread(sample.CountNumbers);
            threadTow.Name = "ThreadTow";

            threadOne.Priority = ThreadPriority.Highest;
            threadTow.Priority = ThreadPriority.Lowest;
            threadOne.Start();
            threadTow.Start();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            sample.Stop();

        }

        public static void Excute()
        {
            Console.WriteLine("Current thread priority:{0}", Thread.CurrentThread.Priority);
            Console.WriteLine("Running on all cores available");
            RunThreads();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Running on a single core");
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
            RunThreads();
            Console.ReadKey();
        }
    }
    class ThreadSample
    {
        private bool _isStopped = false;
        public void Stop()
        {
            _isStopped = true;
        }
        public void CountNumbers()
        {
            long counter = 0;
            while (!_isStopped)
            {
                counter++;
            }
            Console.WriteLine("{0} with {1,11} priority has a count={2,13}",
                Thread.CurrentThread.Name,
                Thread.CurrentThread.Priority,
                counter.ToString("N0"));
        }
    }
}
