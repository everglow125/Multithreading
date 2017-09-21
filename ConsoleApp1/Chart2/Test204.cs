using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ConsoleApp1.Chart2
{
    /// <summary>
    /// AutoResetEvent 从一个线程向另一个线程发送通知
    /// </summary>
    public class Test204
    {
        private static AutoResetEvent _workerEvent = new AutoResetEvent(false);
        private static AutoResetEvent _mainEvent = new AutoResetEvent(false);

        static void Process(int seconds)
        {
            Console.WriteLine("Starting a long running work....");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("Work is done");
            _workerEvent.Set();
            Console.WriteLine("Waiting for a main thread to complete its work");
            _mainEvent.WaitOne();
            Console.WriteLine("Starting second operation ...");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("Work is done!");
            _workerEvent.Set();

        }

        public static void Excute()
        {
            var t = new Thread(() => Process(10));
            t.Start();
            Console.WriteLine("First operation is completed");
            Console.WriteLine("Performing an operation on a main thread");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            _mainEvent.Set();
            Console.WriteLine("Now running the second operation on a second thread");
            _workerEvent.WaitOne();
            Console.WriteLine("Second operation is completed!");
            Console.ReadKey();
        }
    }
}
